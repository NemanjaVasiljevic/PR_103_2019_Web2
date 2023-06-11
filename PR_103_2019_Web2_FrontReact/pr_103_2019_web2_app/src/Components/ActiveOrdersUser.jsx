import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router';

const ActiveOrdersUser = ({ user }) => {
  const [orders, setOrders] = useState([]);
  const navigate = useNavigate();

  const handleHome = () => {
    navigate('/home');
  };

  useEffect(() => {
    // Fetch user orders from the server
    axios
      .get(process.env.REACT_APP_GET_ORDERS)
      .then(response => {
        // Filter orders where arrival date has passed the current moment in time
        let filteredOrders = [];
        if (user.role === 2) {
          filteredOrders = response.data.filter(order => order.sellerId === user.id)
                                        .filter(order => new Date(order.arrivalDate) > new Date());;
        } else {
          // Filter orders where arrival date has passed the current moment in time
          filteredOrders = response.data.filter(order => new Date(order.arrivalDate) > new Date());
        }
        setOrders(filteredOrders);
      })
      .catch(error => {
        console.error('Error fetching user orders:', error);
      });
  }, [user]);

  const formatDate = dateString => {
    const options = { year: 'numeric', month: 'long', day: 'numeric', hour: 'numeric', minute: 'numeric' };
    return new Date(dateString).toLocaleDateString(undefined, options);
  };


  const handleUpdateOrder = orderId => {
    // Handle updating the order with the given orderId
    // Redirect or perform any necessary action
    window.alert("Update ce biti uradjen u nekom od sledecih patcheva");
  };

  const handleCancelOrder = orderId => {
    axios
    .delete(`${process.env.REACT_APP_GET_ORDERS}/${orderId}`)
    .then(response => {
      // Handle the successful cancellation
      window.alert("Porudzbina uspesno otkazana");
      navigate('/home');
    })
    .catch(error => {
      // Handle the error
      window.alert("Otkazivanje nije dozvoljeno, nije prosao 1h od narucivanja. Pokusajte ponovo kasnije");
    });
  };


  return (
    <div className="article-table-container" style={{ color: 'white' }}>
      <button className="submit-button" style={{ width: '100%' }} onClick={handleHome}>
        Return to home
      </button>
      <table className="article-table">
        <thead>
          <tr>
            <th>ID</th>
            <th>Article Quantity</th>
            <th>Article</th>
            <th>Buyer</th>
            <th>Seller</th>
            <th>Status</th>
            <th>Address</th>
            <th>Total Price with delivery</th>
            <th>Comment</th>
            <th>Ordered Date</th>
            <th>Arrival Date</th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          {orders
            .map(order => (
            <tr key={order.id}>
              <td>{order.id}</td>
              <td>{order.articleQuantity}</td>
              <td>{order.articleName}</td>
              <td>{order.buyerName}</td>
              <td>{order.sellerName}</td>
              <td>{order.status}</td>
              <td>{order.address}</td>
              <td>{order.totalPrice + 500} din</td>
              <td>{order.comment}</td>
              <td>{formatDate(order.ordredDate)}</td>
              <td>{formatDate(order.arrivalDate)}</td>
              {user.role!==2 &&(
              <td>
                <button className="action-button" onClick={() => handleUpdateOrder(order.id)}>Update</button>
                <button className="action-button" onClick={() => handleCancelOrder(order.id)}>Cancel</button>
              </td>
              )}
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default ActiveOrdersUser;