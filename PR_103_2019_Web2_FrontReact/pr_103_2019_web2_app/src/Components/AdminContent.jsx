import React from 'react';
 
const getRoleString = (role) => {
    switch (role) {
      case 0:
        return 'Seller';
      case 1:
        return 'User';
      case 2:
        return 'Admin';
      default:
        return '';
    }
  };
  
  const getVerificationStatusString = (verificationStatus) => {
    switch (verificationStatus) {
      case 0:
        return 'Accepted';
      case 1:
        return 'Rejected';
      case 2:
        return 'Pending';
      default:
        return '';
    }
  };

  const formatDate = (dateString) => {
    const options = { year: 'numeric', month: 'long', day: 'numeric' };
    const date = new Date(dateString);
    return date.toLocaleDateString(undefined, options);
  };

const AdminContent = ({user, users }) => {
    const renderAdminTable = () => {
      return (
            <div className="user-table-container">
            <table className="user-table">
                <thead>
                <tr>
                    <th>Username</th>
                    <th>Email</th>
                    <th>Surname</th>
                    <th>Address</th>
                    <th>Birthday</th>
                    <th>Role</th>
                    <th>Verification Status</th>
                    <th>Verify</th>
                </tr>
                </thead>
                <tbody>
                {users
                    .filter(user => user.role !== 0) // Filter users with role 2
                    .map(user => (
                    <tr key={user.id}>
                    <td>{user.username}</td>
                    <td>{user.email}</td>
                    <td>{user.surname}</td>
                    <td>{user.address}</td>
                    <td>{formatDate(user.birthDay)}</td>
                    <td>{getRoleString(user.role)}</td>
                    <td>{getVerificationStatusString(user.verificationStatus)}</td>                   
                    <td>
                    <button className='submit-button' style={{ width: '110%' }}>Verify</button>
                    <br/>
                    <button className='submit-button' style={{ width: '110%'}}>Reject</button>
                    <br/>
                    </td>
                    
                    </tr>
                ))}
                </tbody>
            </table>
            </div>
      );
    };
  
    return (
      <div>
        {renderAdminTable()}
      </div>
    );
  };

  export default AdminContent;