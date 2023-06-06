import React from 'react';
 
const ArticleContent = ({ user,articles }) => {
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
              {user.role === 1 && <th>Action</th>}
            </tr>
          </thead>
          <tbody>
            {articles.map(article => (
              <tr key={article.id}>
                <td>{article.name}</td>
                <td>{article.price} din</td>
                <td>{article.quantity}</td>
                <td>{article.description}</td>
                {user.role === 1 && (
                <td>
                <button className='submit-button' style={{width:'50%'}}>Add to shopping cart</button>
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