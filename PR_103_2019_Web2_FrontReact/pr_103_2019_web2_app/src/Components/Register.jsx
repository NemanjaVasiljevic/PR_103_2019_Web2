import React, { useState } from 'react';
import axios from 'axios';  
import { useNavigate } from 'react-router';

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

    const navigate = useNavigate();


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

        try{ 
            axios.post("https://localhost:7100/api/Users", userDto)
            .then((response) =>{
            console.log('Product successfully created:', response.data);
            navigate('/');
            })
            .catch(() => {
            window.alert('Username or email are already taken');
            navigate('/registration');
            });

        }catch{
            window.alert("Something went wrong");
            navigate('/registration');

        }
    }

    return(
        <form className="register-form" onSubmit={handleRegister}>
            <br/>
            <br/> 
            <h3 style={{color:'white'}}>Register</h3>
            <br/>
            <div className="form-group">
                <input
                type="text"
                className="form-control"
                placeholder="Username"
                value={username}
                onChange={(e) => setUsername(e.target.value)}
                />
            </div>
            <div className="form-group">
                <input
                type="email"
                className="form-control"
                placeholder="Email"
                value={email}
                onChange={(e) => setEmail(e.target.value)}
                />
            </div>
            <div className="form-group">
                <input
                type="password"
                className="form-control"
                placeholder="Password"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
                />
            </div>
            <div className="form-group">
                <input
                type="text"
                className="form-control"
                placeholder="Name"
                value={name}
                onChange={(e) => setName(e.target.value)}
                />
            </div>
            <div className="form-group">
                <input
                type="text"
                className="form-control"
                placeholder="Surname"
                value={surname}
                onChange={(e) => setSurname(e.target.value)}
                />
            </div>
            <div className="form-group">
                <input
                type="text"
                className="form-control"
                placeholder="Address"
                value={address}
                onChange={(e) => setAddress(e.target.value)}
                />
            </div>
            <div className="form-group">
                <input
                type="date"
                className="form-control"
                placeholder="BirthDay"
                value={birthDay}
                onChange={(e) => setBirthDay(e.target.value)}
                />
            </div>
            <div className="form-group">
                <label className="role-label" style={{color:'white'}}>So...Who are you?</label>
                <select className="form-control" value={role} onChange={(e) => setRole(e.target.value)}>
                <option value="">Select a role</option>
                <option value="1">User</option>
                <option value="2">Seller</option>
                </select>
            </div>
            <div className="form-group">
                <input
                type="text"
                className="form-control"
                placeholder="Profile Picture"
                value={profilePicture}
                onChange={(e) => setProfilePicture(e.target.value)}
                />
            </div>
            <button type="submit" className="btn btn-primary">Register</button>
            <div>
            <br/>
            <button
                type="button"
                className="btn btn-secondary"
                onClick={()=>navigate('/media')}
             >
                    Register using social media
            </button>
            </div>
        </form>
    );

}export default Register