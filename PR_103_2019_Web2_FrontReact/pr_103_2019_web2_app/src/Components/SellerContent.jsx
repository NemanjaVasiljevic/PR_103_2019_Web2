import React from 'react';

const SellerContent = ({ user , articles}) => {

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