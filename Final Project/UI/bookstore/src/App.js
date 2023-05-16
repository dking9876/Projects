import React, { useState } from 'react';
import Login from './Login';
import Main from './Main';

function App() {
  const [isLoggedIn, setIsLoggedIn] = useState(false);
  const [username, setUsername] = useState('');

  const handleLogin = (username) => {
    setIsLoggedIn(true);
    setUsername(username);
  }

  const handleLogout = () => {
    setIsLoggedIn(false);
    setUsername('');
  }

  return (
    <div>
        <Login onLogin={handleLogin} />
    </div>
  );
}

export default App;
