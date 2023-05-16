import React, { useState } from 'react';
async function CheckLogin(username, password, city) {
  const response = await fetch(`http://localhost:3000/api/user/Mark/login`, {
      method: 'POST',
      headers: {'Content-Type': 'application/json'},
      body: JSON.stringify(
          {
          "UserName":"Mark",
          "Password":"123",
          "City":"Ramat-Gan"
          }
      )
  })
  .then(response => {
    if (response.ok) {
      // Successful response
      console.log('Status code:', response.status);
      return true;
    } else {
      // Handle error response
      console.log('Status code:', response.status);
      return false;
    }  
  })
  .catch(error => {
    // Handle any errors
    console.error('Error:', error);
  });
}


function Login({ onLogin }) {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');

  const handleSubmit = (e) => {
    e.preventDefault();
    // Do something with username and password, such as sending to a server
    const response =  CheckLogin(username, password, "city")
    onLogin(username);
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
        <button type="submit">Submit</button>
      </form>
    </div>
  );
}

export default Login;




