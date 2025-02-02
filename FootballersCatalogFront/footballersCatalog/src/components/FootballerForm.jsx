import { Form, Input, Radio, DatePicker, Select} from "antd";
import { fetchTeams } from "../services/teams";
import { useEffect, useState } from "react";



function FootballerForm({ form, showId}) {
    const [teams, setTeams] = useState([]);

    const fetchData = async () => {
        try {
            let teams = await fetchTeams();
            setTeams(teams);
        } catch (error) {
            console.error("Error fetching footballers:", error);
        }
    };

    useEffect(() => {
        fetchData();
    }, []);

  return (
      <Form form={form} layout="vertical" className="m">
            {showId ? (
                <Form.Item
                    name="id"
                    label="Id"
                    rules={[{ required: true, message: 'Пожалуйста, введите ID!' }]}
                >
                    <Input disabled />
                </Form.Item>
            ) : null}
          <Form.Item
              name="firstName"
              label="Имя"
              rules={[{ required: true, message: 'Пожалуйста, введите имя!' }]}
          >
              <Input />
          </Form.Item>
          <Form.Item
              name="lastName"
              label="Фамилия"
              rules={[{ required: true, message: 'Пожалуйста, введите фамилию!' }]}
          >
              <Input />
          </Form.Item>
          <Form.Item
              name="gender"
              label="Пол"
              rules={[{ required: true, message: 'Пожалуйста, выберите пол!' }]}
          >
                <Radio.Group
                    name="radiogroup"
                    options={[
                    {
                        value: "Мужчина",
                        label: "Мужчина",
                    },
                    {
                        value: "Женщина",
                        label: "Женщина",
                    },
                    ]}
                />
          </Form.Item>
          <Form.Item
              name="birthday"
              label="День рождения"
              rules={[{ required: true, message: 'Пожалуйста, выберите дату рождения!' }]}
          >
              <DatePicker
                placeholder="Укажи дату"
                format="DD.MM.YYYY"
              />
          </Form.Item>

          <Form.Item
              name="teamName"
              label="Название команды"
              rules={[{ required: true, message: 'Пожалуйста, введите название команды!' }]}
          >
               <Select 
                mode="tags"
                onClick={fetchData}
                placeholder="Выберите или введите название команды"
                maxCount='1'
                options={teams}/>
          </Form.Item>

          <Form.Item
              name="countryName"
              label="Название страны"
              rules={[{ required: true, message: 'Пожалуйста, введите название страны!' }]}
          >
              <Select 
              placeholder="Выбери страну"
              options={[
                {
                    value: 'Россия',
                    label: 'Россия',
                  },
                  {
                    value: 'США',
                    label: 'США',
                  },
                  {
                    value: 'Италия',
                    label: 'Италия',
                  },
              ]}/>
          </Form.Item>
      </Form>
  )
}

export default FootballerForm

