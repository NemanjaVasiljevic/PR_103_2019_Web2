import { Route, Routes } from 'react-router';
import './App.css';
import Login from './Components/Login';
import Register from './Components/Register';
import Home from './Components/Home';
import { useState, useEffect } from 'react';
import SocialMediaRegistration from './Components/SocialMediaRegistration';
import { useNavigate } from 'react-router';


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

  return (
    <Routes>
      <Route path="/" element={<Login onLogin={handleLogin}/>} />
      <Route path="/registration" element={<Register/>} />
      <Route path="/media" element={<SocialMediaRegistration/>} />
      <Route path="/home" element={<Home user={user} onLogout={handleLogout}/>} />
    </Routes>
  );
}

export default App;
