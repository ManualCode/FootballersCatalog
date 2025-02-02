import FootballerCards from './components/FootballerCards';
import AddPage from './components/FootballerAdd';
import {Routes, Route} from "react-router-dom"
import Layout from './components/Layout';

function App() {
  return (
    <Routes>
      <Route path="/" element={<Layout />}>
        <Route index element={<AddPage />}/>
        <Route path="add" element={<AddPage />}/>
        <Route path="catalog" element={<FootballerCards />}/>
      </Route>
    </Routes>
  )
}

export default App
