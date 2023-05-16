import React, { useState } from 'react';
import Login from './Login';
import Main from './Main';
import Messages from './Messages';

function App() {
  const [isLoggedIn, setIsLoggedIn] = useState(false);
  const [username, setUsername] = useState('');

  const handleLogin = (username) => {
    setIsLoggedIn(true);
    setUsername(username);
  }

  // <Login onLogin={handleLogin} />

  return (
    <div>
        <Messages />
    </div>
  );
}

export default App;
