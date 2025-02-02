"use client";

import { useState, useEffect } from 'react';
import { Button, Modal, Form } from 'antd';
import moment from 'moment';
import FootballerForm from './FootballerForm';
import { deleteFootballer, updateFootballer } from '../services/footballers';
import { HubConnectionBuilder } from "@microsoft/signalr";

export const ModalUpdate = ({ footballer, setFootballers }) => {
  const [loading, setLoading] = useState(false);
  const [open, setOpen] = useState(false);
  const [form] = Form.useForm();
  const [hubConnection, setHubConnection] = useState(null);

  const showModal = async () => {
    setOpen(true);
    if (footballer) {
        form.setFieldsValue({
            id: footballer.id,
            firstName: footballer.firstName,
            lastName: footballer.lastName,
            gender: footballer.gender,
            birthday: moment(footballer.birthday, 'DD-MM-YYYY'),
            teamName: footballer.teamName,
            countryName: footballer.countryName
        });
    }

    const connect = async () => {
      try {
          const connection = new HubConnectionBuilder()
              .withUrl("http://localhost:5194/football")
              .withAutomaticReconnect()
              .build();
          await connection.start();
          setHubConnection(connection);
      } catch (error) {
          console.error("Error connecting to SignalR:", error);
      }
  };
  connect();
  };

  const handleOk = async () => {
      try {
          const values = await form.validateFields();
          values.birthday = values.birthday.format('YYYY-MM-DD');
          values.teamName = Array.isArray(values.teamName) ? values.teamName[0] : values.teamName;
          values.updatedDate = new Date()
          setLoading(true);
          let response = await updateFootballer({ values });

          if (response.status == 200) {
              setLoading(false);
              setOpen(false);
              hubConnection.invoke("Editor", values);
          } else {
              console.error('Error updating footballer:', response.statusText);
          }
      } catch (error) {
          setLoading(false);
          console.error('Error updating footballer:', error);
      }
  };

  const handleCancel = () => {
      setOpen(false);
      if (hubConnection) {
          hubConnection.stop();
          setHubConnection(null);
      }
  };

  const handleDelete = async () => {
      setOpen(false);
      let id = form.getFieldValue('id');
      await deleteFootballer({ id });
      hubConnection.invoke("Deleter", id);
    };

    return (
        <>
            <Button type="primary" onClick={showModal} size="large">
                <p className="text-sm font-semibold">Редактировать</p>
            </Button>
            <Modal
                open={open}
                title="Изменить футболиста"
                onOk={handleOk}
                onCancel={handleCancel}
                footer={[
                    <Button key="back" onClick={handleCancel}> Назад </Button>,
                    <Button key="delete" type="primary" danger onClick={handleDelete}> Удалить </Button>,
                    <Button key="submit" type="primary" loading={loading} onClick={handleOk}> Изменить </Button>,
                ]}
            >
                <FootballerForm form={form} showId={true} />
            </Modal>
        </>
    );
};
