import React, { useState } from 'react';
import axios from 'axios';  

const Login = ({onLogin}) =>{
    const[email, setEmail] = useState('');
    const[password, setPassword] = useState('');
  
  
    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            const response = await axios.post("https://localhost:7100/api/Users/login", {
              Email: email,
              Password: password
            });
      
            const result = response.data;        
            if (result !== '') {
              localStorage.setItem('token',result.data);
              console.log("Upao u trazenje emaila:");
              console.log(email);
              axios.get("https://localhost:7100/api/Users/email",{
                    params:{
                        email:email
                    }
                }
                )
                .then((result) => {
                  if (result.data !== null) {
                    const userData = result.data;
                    onLogin(userData);
                  }
                });
            }
          } catch (error) {
            window.alert("Wrong email or password")
          }
      };
  
    
    return(  
        <form onSubmit={handleSubmit} className="login-form">
            <br/>
            <br/>
            <br/>
            <br/>
            <h3 style={{color:'white'}}>Log in</h3>
            <div className="form-group">
                <input
                type="email"
                placeholder="Email"
                value={email}
                onChange={(e) => setEmail(e.target.value)}
                className="form-input"
                />
            </div>
            <div className="form-group">
                <input
                type="password"
                placeholder="Password"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
                className="form-input"
                />
            </div>
            <button type="submit" className="submit-button">Log In</button>
            <a href='/registration'>Dont have an account? Register now!</a>
        </form>
    )  
};
export default Login
