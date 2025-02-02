import { Link, Outlet } from "react-router-dom";
import Menu from './Menu';

function Layout() {

    return (
      <>
        <Menu />
        <Outlet />
        <footer className="bg-gray-900 text-white py-8">
          <div className="container mx-auto px-4">
            <div className="flex flex-col md:flex-row justify-between items-center">
              <div className="text-center md:text-left mb-4 md:mb-0">
                <a href="https://github.com/ManualCode"><img src="./imgs/github-mark-white.png" alt="github" className="h-12 w-12" /></a>
              </div>
              <div className="flex space-x-4">
                <a href="/add" className="text-gray-400 hover:text-white">Добавление</a>
                <a href="/catalog" className="text-gray-400 hover:text-white">Каталог</a>
              </div>
            </div>
            <div className="mt-4 text-center text-gray-400">
              <p>&copy; 2025. Created by Kirill.</p>
            </div>
          </div>
        </footer>
      </>
    )
  }
  
  export default Layout