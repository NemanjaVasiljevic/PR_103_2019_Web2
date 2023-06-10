import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router';


const AllOrdersAdmin = () => {
    const [orders, setOrders] = useState([]);
    const navigate = useNavigate();

    const handleHome = ()=>{
        navigate('/home');
    }

    useEffect(() => {
        // Fetch article data from the server
        axios.get('https://localhost:7100/api/Orders')
            .then(response => {
            // Update the articles state with the fetched data
            setOrders(response.data);
            })
            .catch(error => {
            console.error('Error fetching articles:', error);
            });
        }, []);


    const formatDate = (dateString) => {
        const options = { year: 'numeric', month: 'long', day: 'numeric', hour: 'numeric', minute: 'numeric' };
        return new Date(dateString).toLocaleDateString(undefined, options);
        };

  return (
<div className="article-table-container" style={{color:'white'}}>
    <button
        className="submit-button"
        style={{ width: '100%' }}
        onClick={handleHome}
        >
        Return to home
     </button>
      <table className="article-table">
        <thead>
          <tr>
            <th>ID</th>
            <th>Article Quantity</th>
            <th>Article</th>
            <th>Buyer</th>
            <th>Status</th>
            <th>Address</th>
            <th>Total Price</th>
            <th>Comment</th>
            <th>Quantity</th>
            <th>Ordered Date</th>
            <th>Arrival Date</th>
          </tr>
        </thead>
        <tbody>
          {orders.map(order => (
            <tr key={order.id}>
              <td>{order.id}</td>
              <td>{order.articleQuantity}</td>
              <td>{order.articleName}</td>
              <td>{order.buyerName}</td>
              <td>{order.status}</td>
              <td>{order.address}</td>
              <td>{order.totalPrice + 500}</td>
              <td>{order.comment}</td>
              <td>{order.quantity}</td>
              <td>{formatDate(order.ordredDate)}</td>
              <td>{formatDate(order.arrivalDate)}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default AllOrdersAdmin;