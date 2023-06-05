import React, { useState } from 'react';
import axios from 'axios';  

function Login(props){
    const[email, setEmail] = useState('');
    const[password, setPassword] = useState('');
  
  
  
    const handleSubmit = async (e) => {
        e.preventDefault();

    
        return await axios
                .post("https://localhost:7100/api/Users/login", {
                    Email:email,
                    Password:password
                }).then((result) => {
                    if(result.data !== ""){
                        console.log("Ulogovan korisnik");
                        console.log(email);
                    }
                });
      };
  
    return (  
        <form onSubmit={handleSubmit}>
            <input
            type="email"
            placeholder="Email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            />
            <input
            type="password"
            placeholder="Password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            />
            <button type="submit">Log In</button>
        </form>
    )  
}export default Login
