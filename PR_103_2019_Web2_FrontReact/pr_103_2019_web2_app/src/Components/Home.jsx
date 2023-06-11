import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router';
import axios from 'axios';  
import ArticleContent from './ArticleContent';
import { Route, Routes } from 'react-router';
import AdminContent from './AdminContent';
import SellerContent from './SellerContent';


const Home = ({ user, onLogout }) => {
    const navigate = useNavigate();
    const [articles, setArticles] = useState([]);
    const [users, setUsers] = useState([]);
    const [verifikacija, setVerifikacija] = useState(false);
    const [newOrder, setNewOrder] = useState(false);


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
             <Route path="/" element={<ArticleContent user={user} newOrder={newOrder} articles={articles} />} />

            </>
          );
        } else if (user.role === 0) {
          return (
            <>
              <Route path="/" element={<AdminContent verifikacija={verifikacija} users={users} />} />
            </>
          );
        }else if (user.role === 2) {
          console.log(user.verificationStatus)
          if(user.verificationStatus === 0){
            return (
              <>
                <Route path="/" element={<SellerContent user={user} articles={articles}/>} />
              </>
            );
          }else{
            return(
              <Route path="/" element={<h1>
                Vas nalog mora prvi da bude verifikovan od strane admina da bi se mogao koristiti
              </h1>} />
              
            )
          }
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

    const handleAddArticle = () =>{
      navigate('/addArticle');
    }

    const handleVerifikacija = () =>{
      setVerifikacija(!verifikacija);
      navigate('/home');
    }
    const handleNewOrder = () =>{
      setNewOrder(!newOrder);
      navigate('/home');
    }

    const handlePorudzbineAdmin = () =>{
      navigate('/ordersAdmin');
    }

    const handlePrethodnePorudzbine = () =>{
      navigate('/myOrdersUser');
    }

    const handleActiveOrders = () =>{
      navigate('/activeOrdersUser');
    }

    let items = [];

    // Common item visible for all roles
    items.push(<div key="profile" onClick={handleProfile}>Profil</div>);
    items.push(<div key="log-out" onClick={handleLogout}>Log out</div>);

    // Role-specific items
    if (user.role === 2) { // prodavac
      items.push(<div key="dodavanje-artikla" onClick={handleAddArticle}>Dodavanje artikla</div>);
      items.push(<div key="nove-porudzbine" onClick={handleActiveOrders}>Nove porudzbine</div>);
      items.push(<div key="moje-porudzbine" onClick={handlePrethodnePorudzbine}>Moje porudzbine</div>);
    } else if (user.role === 1) { // korisnik
      items.push(<div key="nova-porudzbina" onClick={handleNewOrder}>Nova porudzbina</div>);
      items.push(<div key="nova-porudzbina" onClick={handleActiveOrders}>Aktivne porudzbine</div>);
      items.push(<div key="prethodne-porudzbine" onClick={handlePrethodnePorudzbine}>Prethodne porudzbine</div>);
    } else if (user.role === 0) {// admin
      items.push(<div key="verifikacija" onClick={handleVerifikacija}>Verifikacija</div>);
      items.push(<div key="sve-porudzbine" onClick={handlePorudzbineAdmin}>Sve porudzbine</div>);
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
          <h2 className="dashboard-heading" style={{color:'white'}}>Welcome to the web shop, {user.username}!</h2>
          <div className="dashboard-items-container">{renderDashboardItems()}</div>       
      </div>

      <Routes>
        {renderRoutes()}
      </Routes>
  </div>
  );
  };
  export default Home;