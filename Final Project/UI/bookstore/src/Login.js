import React, { useState } from 'react';

function Login() {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');

  const handleSubmit = (e) => {
    e.preventDefault();
    // Do something with email and password
  }
  function sayHello() {
    //alert('Hello, World!');
    CheckLogin("", "", "");
    

  }
  
  async function CheckLogin(username, password, city) {
    const response = await fetch(` http://localhost:7071/api/user/Mark/login`, {
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
    return await response.json();
}

  return (
    <div>
      <h2>Login</h2>
      <form onSubmit={handleSubmit}>
        <div>
          <label>Username:</label>
          <input type="email" value={email} onChange={(e) => setEmail(e.target.value)} />
        </div>
        <div>
          <label>Password:</label>
          <input type="password" value={password} onChange={(e) => setPassword(e.target.value)} />
        </div>
        <button onClick={sayHello}>Click me!</button>
      </form>
    </div>
  );
}

export default Login;