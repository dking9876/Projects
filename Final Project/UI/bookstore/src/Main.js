import React from 'react';

function Main({ username }) {
  return (
    <div>
      <h2>Welcome, {username}!</h2>
      <p>This is the main page of your application.</p>
    </div>
  );
}

export default Main;