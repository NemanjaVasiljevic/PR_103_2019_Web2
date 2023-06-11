import React from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router';

const SellerContent = ({ user , articles}) => {
  const navigate = useNavigate();

  const handleArticleChange = id =>{
    navigate(`/editArticle/${id}`);
  }

  const handleArticleDelete = (Id) => {
    axios
      .delete(`https://localhost:7100/api/Articles/${Id}`)
      .then(response => {
        // Handle the successful deletion
        console.log(response.data);
        navigate('/home');
      })
      .catch(error => {
        // Handle any errors that occur during the deletion
        console.error(error);
      });
  };

    const renderArticleTable = () => {
      return (
        <div className="article-table-container">
        <table className="article-table">
          <thead>
            <tr>
              <th>Name</th>
              <th>Price</th>
              <th>Quantity</th>
              <th>Description</th>
              {user.role === 2 && <th>Actions</th>}

            </tr>
          </thead>
          <tbody>
            {articles
              .filter(article => article.sellerId === user.id)
              .map(article => (
              <tr key={article.id}>
                <td>{article.name}</td>
                <td>{article.price} din</td>
                <td>{article.quantity}</td>
                <td>{article.description}</td>
                {user.role === 2 && (
                <td>
                  <div className="actions-container">
                    <button
                      className="submit-button"
                      onClick={() => handleArticleChange(article.id)}
                    >
                      Change
                    </button>
                    <br/>
                    <button
                      className="submit-button"
                      style={{backgroundColor:'red'}}
                      onClick={() => handleArticleDelete(article.id)}
                    >
                      Delete
                    </button>
                  </div>
                </td>
              )}
              </tr>
            ))}
          </tbody>
        </table>
      </div>
      );
    };
  
    return (
      <div>
        {renderArticleTable()}
      </div>
    );
  };

  export default SellerContent;