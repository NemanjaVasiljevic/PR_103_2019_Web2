import { Route, Routes } from 'react-router';
import './App.css';
import Login from './Components/Login';
import Register from './Components/Register';
import Home from './Components/Home';
import { useState, useEffect } from 'react';
import SocialMediaRegistration from './Components/SocialMediaRegistration';
import { useNavigate } from 'react-router';
import EditProfile from './Components/EditProfile';
import AddArticle from './Components/AddArticle';
import AllOrdersAdmin from './Components/AllOrdersAdmin';
import MyOrdersUser from './Components/MyOrdersUser';
import ActiveOrdersUser from './Components/ActiveOrdersUser';
import EditArticle from './Components/EditArticle';
//import EditArticle from './Components/EditArticle';



const App = () => {
  const [user, setUser] = useState(null);
  const navigate = useNavigate();


  useEffect(() => {
    const storedUser = localStorage.getItem('user');
    if (storedUser) {
      setUser(JSON.parse(storedUser));
    }
  }, []);
  const handleLogin = (userData) => {
    setUser(userData);
    localStorage.setItem('user', JSON.stringify(userData));
    navigate('/home');

  };
  const handleLogout = () => {
    setUser(null);
    localStorage.removeItem('user');
  };
  const handleUpdate = (updatedUser) => {
    // Call the logout method passed as a prop
    localStorage.setItem('user',JSON.stringify(updatedUser))
    navigate('/home');
    //<Route path="/editArticle" element={<EditArticle userId={user.id}/>} />


  };
  return (
    <Routes>
      <Route path="/" element={<Login onLogin={handleLogin}/>} />
      <Route path="/registration" element={<Register/>} />
      <Route path="/media" element={<SocialMediaRegistration/>} />
      <Route path="/home" element={<Home user={user} onLogout={handleLogout}/>} />
      <Route path="/edit" element={<EditProfile user={user} onUpdate={handleUpdate} />} />
      <Route path="/addArticle" element={<AddArticle user={user}/>} />
      <Route path="/ordersAdmin" element={<AllOrdersAdmin/>} />
      <Route path="/myOrdersUser" element={<MyOrdersUser user={user}/>} />
      <Route path="/activeOrdersUser" element={<ActiveOrdersUser user={user}/>} />
      <Route path="/editArticle/:articleId" element={<EditArticle user={user}/>}/>
    </Routes>
  );
}

export default App;
