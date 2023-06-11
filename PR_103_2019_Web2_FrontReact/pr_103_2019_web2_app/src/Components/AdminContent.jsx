import React from 'react';
import axios from 'axios';


const getRoleString = (role) => {
    switch (role) {
      case 0:
        return 'Admin';
      case 1:
        return 'User';
      case 2:
        return 'Seller';
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

  const handleVerify = (userId) => {
    const data = {
      UserId: userId,
      VerificationStatus: 0
    };
    
    axios.post(process.env.REACT_APP_USER_VERIFY, data)
      .then(response => {
        // Handle the successful verification response
        console.log('User verification successful:', response.data);
        window.location.reload();

      })
      .catch(error => {
        // Handle the verification error
        console.error('Error verifying user:', error);
      });
  };
  const handleReject = (userId) => {
    const data = {
      UserId: userId,
      VerificationStatus: 1
    };
    
    axios.post(process.env.REACT_APP_USER_VERIFY, data)
      .then(response => {
        // Handle the successful verification response
        console.log('User verification successful:', response.data);
        window.location.reload();
      })
      .catch(error => {
        // Handle the verification error
        console.error('Error verifying user:', error);
      });
  };

const AdminContent = ({verifikacija, users }) => {

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
                {
                users
                    .filter(user => user.role !== 0) // Filter users with role 2
                    .filter(user => !verifikacija || user.verificationStatus === 2)
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
                    <button className='submit-button' style={{ width: '110%' }} onClick={() => handleVerify(user.id)}>Verify</button>
                    <br/>
                    <button className='submit-button' style={{ width: '110%'}} onClick={() => handleReject(user.id)}>Reject</button>
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