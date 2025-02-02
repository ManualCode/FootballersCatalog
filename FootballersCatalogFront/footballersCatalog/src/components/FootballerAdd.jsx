"use client";

import { useEffect, useState } from "react";
import { createFootballer } from "../services/footballers";
import FootballerForm from "./FootballerForm";
import { Form, Button } from 'antd';
import { HubConnectionBuilder } from "@microsoft/signalr";

function AddPage() {
    const [form] = Form.useForm();
    const [hubConnection, setHubConnection] = useState(null);

    useEffect(() => {
        const connect = async () => {
            try {
                const connection = new HubConnectionBuilder()
                    .withUrl("http://localhost:5194/football")
                    .withAutomaticReconnect()
                    .build();
                await connection.start();
                setHubConnection(connection);
            } catch (error) {
                console.error(error);
            }
        };
        connect();
        return () => {
            if (hubConnection) {
                hubConnection.stop();
            }
        };
    }, []);

    const handleOk = async () => {
      const values = await form.validateFields();
      let footballerId = await createFootballer({ values });
        try {
            const newValues = {
              id: footballerId,
              firstName: values.firstName,
              lastName: values.lastName,
              gender: values.gender,
              birthday: values.birthday.format('YYYY-MM-DD'),
              teamName: values.teamName[0],
              countryName: values.countryName,
              updatedDate: new Date()
            }
            await hubConnection.invoke("Sender", newValues);
        } catch (error) {
            console.error(error);
        }
        form.resetFields();
    };

    return (
        <div className="bg-gray-200 pb-10 ">
            <div className="flex flex-col items-center justify-center bg-gray-200 p-6">
                <h1 className="text-4xl font-bold text-gray-800 mb-4">Добавь футболиста</h1>
                <p className="text-lg text-gray-600">
                    На этой странице вы можете добавить футболиста в каталог
                </p>
            </div>
            <div className="max-w-md mx-auto bg-gray-100 p-8 rounded-2xl shadow-xl relative mt-10">
                <FootballerForm form={form} showId={false} />
                <div className="absolute bottom-0 right-0 mb-4 mr-8">
                    <Button id="sendBtn" key="submit" type="primary" onClick={handleOk}>Добавить</Button>
                </div>
            </div>
        </div>
    );
}

export default AddPage;
