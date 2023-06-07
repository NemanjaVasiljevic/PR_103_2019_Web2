import React, { useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router';

const AddArticle = ({user}) => {
  const [name, setName] = useState('');
  const [price, setPrice] = useState('');
  const [quantity, setQuantity] = useState('');
  const [description, setDescription] = useState('');
  const navigate = useNavigate();


  const handleSubmit = (e) => {
    e.preventDefault();

    const articleData = {
      Name: name,
      Price: price,
      Quantity: quantity,
      Description: description
    };

    axios
      .post('https://localhost:7100/api/Articles', articleData, {params:{sellerId:user.id}})
      .then((response) => {
        console.log('Product successfully created:', response.data);
        navigate('/home');
      })
      .catch((error) => {
        console.error('Error creating product:', error);
      });
  };

  return (
    <form onSubmit={handleSubmit} className="article-form"  style={{color:'white'}}>
    <div className="form-group">
      <br/>
      <h1>Add new article</h1>
      <br/>
      <br/>
      <label htmlFor="name">Name:</label>
      <input
        type="text"
        id="name"
        value={name}
        onChange={(e) => setName(e.target.value)}
        className="form-control"
      />
    </div>
    <div className="form-group">
      <label htmlFor="price">Price:</label>
      <input
        type="number"
        id="price"
        value={price}
        onChange={(e) => setPrice(e.target.value)}
        className="form-control"
      />
    </div>
    <div className="form-group">
      <label htmlFor="quantity">Quantity:</label>
      <input
        type="number"
        id="quantity"
        value={quantity}
        onChange={(e) => setQuantity(e.target.value)}
        className="form-control"
      />
    </div>
    <div className="form-group">
      <label htmlFor="description">Description:</label>
      <textarea
        id="description"
        value={description}
        onChange={(e) => setDescription(e.target.value)}
        className="form-control"
      />
    </div>
    <button type="submit" className="btn btn-primary">Submit</button>
  </form>
  );
};

export default AddArticle;