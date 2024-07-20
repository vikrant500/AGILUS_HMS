import './App.css';

import Post from "./Post";

import Header from "./Header";

import Layout from './Layouts';

import { Route, Routes } from "react-router-dom";
import IndexPage from './pages/IndexPage';
import LoginPage from './pages/LoginPage';
import RegisterPage from './pages/RegisterPage';
import { UserContextProvider } from './UserContext';
import CreatePost from './pages/CreatePost';
import CreateDisease from './pages/CreateDisease';
import PostPage from './pages/PostPage';
import EditPost from './pages/EditPost';
import AboutDisease from './pages/AboutDisease';
import DiseasePage from './pages/DiseasePage';
import EditDisease from './pages/EditDisease';

function App() {

  return (

    <UserContextProvider>
      <Routes>

        <Route path="/" element={<Layout />}>

          <Route index element={<IndexPage />} />

          <Route path={'/login'} element={<LoginPage />} />

          <Route path={'/register'} element={<RegisterPage />} />

          <Route path={'/create'} element={<CreatePost />} />

          <Route path="/post/:id" element={<PostPage />} />

          <Route path={'/createdisease'} element={<CreateDisease />} />

          <Route path={'/about_disease'} element={<AboutDisease />} />

          <Route path="/disease/:id" element={<DiseasePage />} />

          <Route path="/editdisease/:id" element={<EditDisease />} />

          <Route path='/edit/:id' element={<EditPost />} />

        </Route>

      </Routes>

    </UserContextProvider>
      
);

}

export default App;