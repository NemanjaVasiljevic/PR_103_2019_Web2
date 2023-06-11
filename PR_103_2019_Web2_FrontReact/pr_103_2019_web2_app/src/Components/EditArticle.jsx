import React, { useState } from 'react';
import axios from 'axios';
import { useParams } from 'react-router';

const EditArticle = ({ user }) => {
  const [name, setName] = useState('');
  const [price, setPrice] = useState(0);
  const [quantity, setQuantity] = useState(0);
  const [description, setDescription] = useState('');

  const { articleId } = useParams();

  const handleFormSubmit = (event) => {
    event.preventDefault();
    console.log("Article id u editu:",articleId);

    // Create an ArticleDto object with the updated values
    const updatedArticle = {
      Id: articleId,
      Name: name,
      Price: price,
      Quantity: quantity,
      Description: description,
      SellerId: user.id
    };

    // Send the updated article data to the server
    axios
      .put(`https://localhost:7100/api/Articles/${user.id}`, updatedArticle)
      .then((response) => {
        // Handle the successful update
        console.log(response.data);

        // Invoke the onArticleUpdated callback to notify the parent component
      })
      .catch((error) => {
        // Handle any errors that occur during the update
        console.error(error);
      });
  };

  return (
    <div className="edit-article-container">
      <h2 className="edit-article-heading">Edit Article</h2>
      <form onSubmit={handleFormSubmit} className="edit-article-form">
        <div className="edit-article-field">
          <label className="edit-article-label">Name:</label>
          <input
            type="text"
            value={name}
            onChange={(event) => setName(event.target.value)}
            className="edit-article-input"
          />
        </div>
        <div className="edit-article-field">
          <label className="edit-article-label">Price:</label>
          <input
            type="number"
            value={price}
            onChange={(event) => setPrice(Number(event.target.value))}
            className="edit-article-input"
          />
        </div>
        <div className="edit-article-field">
          <label className="edit-article-label">Quantity:</label>
          <input
            type="number"
            value={quantity}
            onChange={(event) => setQuantity(Number(event.target.value))}
            className="edit-article-input"
          />
        </div>
        <div className="edit-article-field">
          <label className="edit-article-label">Description:</label>
          <textarea
            value={description}
            onChange={(event) => setDescription(event.target.value)}
            className="edit-article-textarea"
          ></textarea>
        </div>
        <button type="submit" className="edit-article-button">Update</button>
      </form>
    </div>
  );
};

export default EditArticle;
