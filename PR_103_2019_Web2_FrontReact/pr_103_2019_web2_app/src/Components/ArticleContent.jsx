import React from 'react';
import { useState } from 'react'; 
import OrderForm from './OrderForm';

const ArticleContent = ({ user,newOrder,articles }) => {
  const [quantities, setQuantities] = useState({});

  const handleQuantityChange = (event, articleId) => {
    const { value } = event.target;
    setQuantities(prevQuantities => ({
      ...prevQuantities,
      [articleId]: parseInt(value),
    }));
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
              {newOrder && user.role === 1 && <th>Order</th>}
            </tr>
          </thead>
          <tbody>
            {articles.map(article => (
              <tr key={article.id}>
                <td>{article.name}</td>
                <td>{article.price} din</td>
                <td>{article.quantity}</td>
                <td>{article.description}</td>
                {newOrder && user.role === 1 && (
                    <td>
                    <div className="order-container">
                      <input
                        type="number"
                        min="0"
                        max={article.quantity}
                        value={quantities[article.id] || 0}
                        onChange={(event) => handleQuantityChange(event, article.id)}
                      />
                      
                      {quantities[article.id] > 0 && (
                        <OrderForm
                          article={article}
                          quantity={quantities[article.id]}
                          user={user}
                        />
                      )}
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

  export default ArticleContent;