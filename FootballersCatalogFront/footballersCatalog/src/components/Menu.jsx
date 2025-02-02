import {Routes, Route} from "react-router-dom"
import { Link } from "react-router-dom";
import { Header } from "antd/es/layout/layout";
import { Menu } from "antd";

const items =[
    {key: "Add", label : <Link to={"/add"}>Добавление</Link>},
    {key: "Catalog", label : <Link to={"/catalog"}>Каталог</Link>}
  ];

function MyMenu() {
    return (
        <Header>
            <Menu 
                theme="dark"
                mode="horizontal"
                items={items}
                style={{flex: 1, minWidth: 0}}
            />
        </Header>
    )
  }

export default MyMenu; 