import { Route, Routes } from 'react-router';
import './App.css';
import Login from './Components/Login';
import Register from './Components/Register';
import Home from './Components/Home';
import { useState } from 'react';

const App = () => {
  const [user, setUser] = useState(null);

  const handleLogin = (userData) => {
    setUser(userData);
  };
  const handleLogout = () => {
    setUser(null);
  };

  return (
    <Routes>
      <Route path="/" element={<Login onLogin={handleLogin}/>} />
      <Route path="/registration" element={<Register/>} />
      <Route path="/home" element={<Home user={user} onLogout={handleLogout}/>} />
    </Routes>
  );
}

export default App;
