import React, { useState } from 'react';
import axios from 'axios';  

function Register(props){
    const[username, setUsername] = useState('');
    const[password, setPassword] = useState('');
    const[email, setEmail] = useState('');
    const[name, setName] = useState('');
    const[surname, setSurname] = useState('');
    const[address, setAddress] = useState('');
    const[birthDay, setBirthDay] = useState('');
    const[role, setRole] = useState('');
    const[profilePicture, setProfilePicture] = useState('');

    const userDto = {
        Username: username,
        Password: password,
        Email: email,
        Name: name,
        Surname: surname,
        Address: address,
        BirthDay: birthDay,
        Role: parseInt(role),
        ProfilePicture: profilePicture
    }


    const handleRegister = async (e) =>{
        e.preventDefault();

        console.log(userDto.Role);

        return axios
                    .post("https://localhost:7100/api/Users", userDto);
    }

    return(
        <form onSubmit={handleRegister}>
            <div>
                <input
                type="username"
                placeholder="Username"
                value={username}
                onChange={(e) => setUsername(e.target.value)}
                />
            </div>
            <div>
                <input
                type="email"
                placeholder="Email"
                value={email}
                onChange={(e) => setEmail(e.target.value)}
                />
            </div>
            <div>
                <input
                type="password"
                placeholder="Password"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
                />
            </div>
            <div>
                <input
                type="name"
                placeholder="Name"
                value={name}
                onChange={(e) => setName(e.target.value)}
                />
            </div>
            <div>
                <input
                type="surname"
                placeholder="Surname"
                value={surname}
                onChange={(e) => setSurname(e.target.value)}
                />
            </div>
             <div>
                <input
                type="address"
                placeholder="Address"
                value={address}
                onChange={(e) => setAddress(e.target.value)}
                />
            </div>
            <div>
                <input  
                type="date"
                placeholder="BirthDay"
                value={birthDay}
                onChange={(e) => setBirthDay(e.target.value)}
                />
            </div>
            <div>
                <label>Choose a role:</label>
                <select value={role} onChange={(e) => setRole(e.target.value)}>
                    <option value="">Select a role</option>
                    <option value="1">User</option>
                    <option value="2">Seller</option>
                </select>
            </div>
            <div>
                <input
                type="profilePicture"
                placeholder="PFP"
                value={profilePicture}
                onChange={(e) => setProfilePicture(e.target.value)}
                />
            </div>
            <button type="submit">Register</button>
        </form>
    );

}export default Register