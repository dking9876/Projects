import React, { useState } from 'react';
import Login from './Login';
import Main from './Main';
import Messages from './Messages';
import SentMessages from './SentMessages'
import SearchBook from './SearchBook'
import CreateMessage from './CreateMessage'
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
        <CreateMessage username = "daniel" />
    </div>
  );
}

export default App;
