import React, { useState } from 'react';

function Signup({  }) {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [city, setCity] = useState('');


  const handleSubmit = (e) => {
    e.preventDefault();
    alert('Your account created successfully')
    fetch(`http://localhost:3000/api/user`, {method: 'POST', headers: {'Content-Type': 'application/json'},
      body: JSON.stringify(
        {
            "UserName":username,
            "Password":password,
            "City":city
         }
      ) })
           
  }

  return (
    <div>
      <h2>Sign Up</h2>
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

export default Signup;
