import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router';
import axios from 'axios';  
import ArticleContent from './ArticleContent';
import { Route, Routes } from 'react-router';
import AdminContent from './AdminContent';


const Home = ({ user, onLogout }) => {
    const navigate = useNavigate();
    const [articles, setArticles] = useState([]);
    const [users, setUsers] = useState([]);


    useEffect(() => {
        // Fetch article data from the server
        axios.get('https://localhost:7100/api/Articles')
          .then(response => {
            // Update the articles state with the fetched data
            setArticles(response.data);
          })
          .catch(error => {
            console.error('Error fetching articles:', error);
          });
      }, []);
    
      useEffect(() => {
        // Fetch article data from the server
        axios.get('https://localhost:7100/api/Users')
          .then(response => {
            // Update the articles state with the fetched data
            setUsers(response.data);
          })
          .catch(error => {
            console.error('Error fetching articles:', error);
          });
      }, []);

      

      const renderRoutes = () => {
        if (user.role === 1) {
          return (
            <>
             <Route path="/" element={<ArticleContent user={user} articles={articles} />} />

            </>
          );
        } else if (user.role === 0) {
          return (
            <>
              <Route path="/" element={<AdminContent user={user} users={users} />} />
            </>
          );
        } 
      };


    const renderDashboardItems = () => {
      // Define the items based on the user's role
    const handleLogout = () => {
      // Call the logout method passed as a prop
      onLogout();
      localStorage.removeItem('user');
      navigate('/');

    };


    const handleProfile = () =>{
      navigate('/edit');
    };

    let items = [];

    // Common item visible for all roles
    items.push(<div key="profile" onClick={handleProfile}>Profil</div>);
    items.push(<div key="log-out" onClick={handleLogout}>Log out</div>);

    // Role-specific items
    if (user.role === 2) { // prodavac
      items.push(<div key="dodavanje-artikla">Dodavanje artikla</div>);
      items.push(<div key="nove-porudzbine">Nove porudzbine</div>);
      items.push(<div key="moje-porudzbine">Moje porudzbine</div>);
    } else if (user.role === 1) { // korisnik
      items.push(<div key="nova-porudzbina">Nova porudzbina</div>);
      items.push(<div key="prethodne-porudzbine">Prethodne porudzbine</div>);
    } else if (user.role === 0) {// admin
      items.push(<div key="verifikacija">Verifikacija</div>);
      items.push(<div key="sve-porudzbine">Sve porudzbine</div>);
    }

    return items;
  };
  
  if (!user) {
      navigate('/');
      return null;
    }

  return (
  <div style={{color:'white'}}>

      <div className="home-container">
          <h2 className="dashboard-heading" style={{color:'white'}}>Welcome to the Dashboard, {user.username}!</h2>
          <div className="dashboard-items-container">{renderDashboardItems()}</div>       
      </div>

      <Routes>
        {renderRoutes()}
      </Routes>
  </div>
  );
  };
  export default Home;