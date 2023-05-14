import React, { useState } from 'react';
import Login from './Login';

function App() {
  const [isLoggedIn, setIsLoggedIn] = useState(false);

  const handleLogin = () => {
    setIsLoggedIn(true);
  }

  const handleLogout = () => {
    setIsLoggedIn(false);
  }

  return (
    <div className="App">
      <header>
        <h1>Welcome to My Website</h1>
        {isLoggedIn ? (
          <button onClick={handleLogout}>Logout</button>
        ) : (
          <Login onLogin={handleLogin} />
        )}
      </header>
      <main>
        {/* Main content of your website */}
      </main>
      <footer>
        {/* Footer content of your website */}
      </footer>
    </div>
  );
}

export default App;