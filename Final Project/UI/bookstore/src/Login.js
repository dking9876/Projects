import React, { useState } from 'react';
import { ServerUrl } from './Globals';

function Login({ onLogin }) {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [city, setCity] = useState('');
    const [data, setdata] = useState([]);
    
    

    const handleSubmit = (e) => {
        e.preventDefault();
        fetch(ServerUrl + 'user/login' , {method: 'POST', headers: {'Content-Type': 'application/json'},
        body: JSON.stringify(
            {
                "UserName":username,
                "Password":password,
                "City":city
                
            }
        ) })
        .then(async response => {
            if (response.ok) {
              // Successful response
              const token = await response.text();
              
              setdata(true) ;
              
              onLogin(username, city, data, token )
            } else {
              // Handle error response
              alert('login failed ')
              console.log('Status code:', response.status);
              setdata(false);
            }  
          })
          .catch(error => {
            // Handle any errors
            console.error('Error:', error);
          });

          
        }
  
    return (
      <div>
      
        <h2>Login</h2>
        <form onSubmit={handleSubmit}>
          <div>
            <label htmlFor="username">Username:</label>
            <input type="text" id="username" value={username} onChange={(e) => setUsername(e.target.value)} required />
          </div>
          <div>
            <label htmlFor="password">Password:</label>
            <input type="password" id="password" value={password} onChange={(e) => setPassword(e.target.value)} required />
          </div>
          <div>
            <label htmlFor="city">City:</label>
            <input type="city" id="city" value={city} onChange={(e) => setCity(e.target.value)} required />
          </div>
          
          <button type="submit">Submit</button>
        </form>
      </div>
     
    );
  }
  export default Login;