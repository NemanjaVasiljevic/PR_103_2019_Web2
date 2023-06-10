import React, { useState } from 'react';
import axios from 'axios';

const OrderForm = ({ article, quantity, user }) => {
  const [address, setAddress] = useState('');
  const [comment, setComment] = useState('');

  const orderDto = {
    ArticleQuantity:quantity,
    ArticleId:article.id,
    ArticleName:article.name,
    BuyerName:user.name,
    BuyerId:user.id,
    Status:1,
    Address:address,
    Comment:comment
  }

  const handleAddressChange = (event) => {
    setAddress(event.target.value);
  };

  const handleCommentChange = (event) => {
    setComment(event.target.value);
  };

  const handleSubmit = (event) => {
    event.preventDefault();


    // Make the POST request using axios
    axios.post('https://localhost:7100/api/Orders', orderDto, {params:{userId:user.id}})
      .then(response => {
        // Handle the successful response
        console.log(response.data);
      })
      .catch(error => {
        // Handle the error
        console.error(error);
      });

    // Reset the form
    setAddress('');
    setComment('');
  };

  return (
    <form onSubmit={handleSubmit}>
      <div>
        <label htmlFor="address">Address:</label>
        <input
          type="text"
          id="address"
          placeholder={user.address}
          onChange={handleAddressChange}
        />
      </div>
      <div>
        <label htmlFor="comment">Comment:</label>
        <textarea
          id="comment"
          value={comment}
          onChange={handleCommentChange}
        ></textarea>
      </div>
      <button type="submit" className="submit-button" >Order</button>
    </form>
  );
};

export default OrderForm;
