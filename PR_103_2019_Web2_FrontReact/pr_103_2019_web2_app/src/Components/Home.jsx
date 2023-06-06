import React from 'react';
import { useNavigate } from 'react-router';


const Home = ({ user, onLogout }) => {
    const navigate = useNavigate();


    const renderDashboardItems = () => {
      // Define the items based on the user's role
      const handleLogout = () => {
        // Call the logout method passed as a prop
        onLogout();
        navigate('/');

      };
      let items = [];
  
      // Common item visible for all roles
      items.push(<div key="profile">Profil</div>);
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
  
    return (
      <div className="home-container">
        <h2 className="dashboard-heading" style={{color:'white'}}>Welcome to the Dashboard, {user.username}!</h2>
        <div className="dashboard-items-container">{renderDashboardItems()}</div>
      </div>
    );
  };
  
  export default Home;