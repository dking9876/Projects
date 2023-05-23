import React, { useState } from 'react';
import Login from './Login';
import Main from './Main';
import Messages from './Messages';
import SentMessages from './SentMessages'
import SearchBook from './SearchBook'
import CreateMessage from './CreateMessage'
import CreateUserBook from './CreateUserBook'
import Signup from './Signup';
import DeleteUserBook from './DeleteUserBook';
import MyBooks from './MyBooks';
import ReactDOM from "react-dom/client";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import Layout from "./Layout";

function App() {
  const [isLoggedIn, setIsLoggedIn] = useState(false);
  const [username, setUsername] = useState('');

  const handleLogin = (username) => {
    setIsLoggedIn(true);
    setUsername(username);
  }

  // <Login onLogin={handleLogin} />

  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Layout />}>
          <Route index element={<Main />} />
          <Route path="signup" element={<Signup />} />
          <Route path="Login" element={<Login />} />
          <Route path="SearchBook" element={<SearchBook />} />
          <Route path="CreateUserBook" element={<CreateUserBook />} />
          <Route path="DeleteUserBook" element={<DeleteUserBook />} />
          <Route path="MyBooks" element={<MyBooks />} />
          <Route path="CreateMessage" element={<CreateMessage />} />
          <Route path="Messages" element={<Messages />} />
          <Route path="SentMessages" element={<SentMessages />} />
        </Route>
      </Routes>
    </BrowserRouter>
  );
}

export default App;
