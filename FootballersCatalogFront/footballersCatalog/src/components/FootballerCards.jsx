"use client";

import { useEffect, useState } from "react";
import { fethFootballers } from "../services/footballers";
import FootballerCard from "./FootballerCard";
import { LoadingOutlined } from '@ant-design/icons';
import { Flex, Spin } from "antd";
import { HubConnectionBuilder } from "@microsoft/signalr";

function FootballerCards() {
    const [footballers, setFootballers] = useState([]);
    const [isLoaded, setIsLoaded] = useState(false);
    const [hubConnection, setHubConnection] = useState(null);

    useEffect(() => {
        const fetchData = async () => {
            if (!isLoaded) {
                try {
                    let footballerss = await fethFootballers();
                    setFootballers(footballerss);
                    setIsLoaded(true);
                } catch (error) {
                    console.error("Error fetching footballers:", error);
                }
            }
        };

        const connect = async () => {
            if (!hubConnection) {
                try {
                    const connection = new HubConnectionBuilder()
                        .withUrl("http://localhost:5194/football")
                        .withAutomaticReconnect()
                        .build();

                    connection.on("Receive", (model) => {
                        setFootballers((prevFootballers) => [...prevFootballers, model]);
                    });

                    connection.on("ReceiveEdit", (model) => {
                        setFootballers((prevFootballers) =>
                            prevFootballers.map(f => f.id === model.id ? model : f)
                        );
                    });

                    connection.on("ReceiveDelete", (id) => {
                        setFootballers((prevFootballers) =>
                            prevFootballers.filter(f => f.id !== id)
                        );
                    });

                    await connection.start();
                    setHubConnection(connection);
                } catch (error) {
                    console.error("Error connecting to SignalR:", error);
                }
            }
        };

        fetchData();
        connect();

        return () => {
            if (hubConnection) {
                hubConnection.stop();
            }
        };
    }, [isLoaded, hubConnection]);

    if (!isLoaded) {
        return (
            <Flex className="flex items-center justify-center h-screen bg-gray-100">
                <Spin indicator={<LoadingOutlined spin />} size="large" />
                <div className="ml-4 text-gray-600 text-lg">Loading...</div>
            </Flex>
        );
    }

    return (
        <div className="bg-gray-200 min-h-198 pl-10 pr-10">
            <div className="pb-10 text-center pt-3">
                <h1 className="text-4xl font-bold text-gray-800 mb-4">Список футболистов</h1>
                <p className="text-lg text-gray-600">
                    На этой странице вы можете ознакомиться со всеми футболистами. Также вы можете отредактировать данные
                </p>
            </div>
            {
                footballers ?
                (<div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 gap-6 pb-5">
                    {footballers.map((footballer) => (<FootballerCard footballer={footballer} key={footballer.id} />))}
                </div>) : 
                (<div className="flex flex-col items-center justify-center h-full">
                    <p className="text-gray-600 text-lg mb-4">Не удалось загрузить данные с сервера</p>
                    <img src="./imgs/error_cat_2.jpg" alt="Error Cat" className="w-90 h-90 shadow-2xl" />
                </div>)
            }
        </div>
    );
}

export default FootballerCards;
