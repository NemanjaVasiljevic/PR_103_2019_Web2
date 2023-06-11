import React, { useState } from 'react';
import axios from 'axios';

const EditProfile = ({ user, onUpdate }) => {
  const [username, setUsername] = useState(user.username);
  const [password, setPassword] = useState(user.password);
  const [email, setEmail] = useState(user.email);
  const [name, setName] = useState(user.name);
  const [surname, setSurname] = useState(user.surname);
  const [address, setAddress] = useState(user.address);
  const [birthDay, setBirthDay] = useState(user.birthDay);
  const [profilePicture, setProfilePicture] = useState(user.profilePicture);

  const userDto = {
    Username: username,
    Password: password,
    Email: email,
    Name: name,
    Surname: surname,
    Address: address,
    BirthDay: birthDay,
    ProfilePicture: profilePicture
}

  const handleSubmit = (e) => {
    e.preventDefault();
    axios
      .put(`https://localhost:7100/api/Users/${user.id}`, userDto)
      .then((response) => {
        onUpdate(response.data); // Call the onUpdate function with the updated user data
      })
      .catch((error) => {
        console.error('Error updating user:', error);
      });
  };

  return (
        <form className="modern-form" onSubmit={handleSubmit} style={{color:'white'}}>
        <h1>Change your profile</h1>

        <div className="form-group">
                <input
                type="text"
                className="form-control"
                placeholder="Profile Picture"
                value={profilePicture}
                onChange={(e) => setProfilePicture(e.target.value)}
                />
        </div>

        <div className="form-group">
            <label htmlFor="username">Username:</label>
            <input
            type="text"
            id="username"
            className="form-control"
            value={username}
            onChange={(e) => setUsername(e.target.value)}
            />
        </div>
        <div className="form-group">
            <label htmlFor="password">Password:</label>
            <input
            type="password"
            id="password"
            className="form-control"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            />
        </div>
        <div className="form-group">
            <label htmlFor="email">Email:</label>
            <input
            type="email"
            id="email"
            className="form-control"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            />
        </div>
        <div className="form-group">
            <label htmlFor="name">Name:</label>
            <input
            type="text"
            id="name"
            className="form-control"
            value={name}
            onChange={(e) => setName(e.target.value)}
            />
        </div>
        <div className="form-group">
            <label htmlFor="surname">Surname:</label>
            <input
            type="text"
            id="surname"
            className="form-control"
            value={surname}
            onChange={(e) => setSurname(e.target.value)}
            />
        </div>
        <div className="form-group">
            <label htmlFor="address">Address:</label>
            <input
            type="text"
            id="address"
            className="form-control"
            value={address}
            onChange={(e) => setAddress(e.target.value)}
            />
        </div>
        <div className="form-group">
            <label htmlFor="birthday">Birthday:</label>
            <input
            type="date"
            id="birthday"
            className="form-control"
            value={birthDay}
            onChange={(e) => setBirthDay(e.target.value)}
            />
        </div>
        <button type="submit" className="btn btn-primary">Save</button>
        </form>

  );
};

export default EditProfile;