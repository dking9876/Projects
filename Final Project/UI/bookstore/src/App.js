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
import Login1 from "./Login"
import EntryPage from "./EntryPage"
function App() {
  const [isLoggedIn, setIsLoggedIn] = useState(false);
  const [username, setUsername] = useState('');
  const [city, setCity] = useState('');

  const handleLoginMain = (username, city, data) => {
    setIsLoggedIn(data);
    setCity(city);
    setUsername(username);
  }

  const pageStyle = {
    backgroundColor: 'lightblue',
    padding: '20px',
    fontSize: '18px',
    color: 'black',
  }
  

  return (
    <div style={pageStyle}>
      {isLoggedIn ? (
        <BrowserRouter>
        <Routes>
        <Route path="/login" element={<Layout />}>
          <Route index element={<Main/>} />
          <Route path="searchBook" element={<SearchBook city = {city}/>} />
          <Route path="CreateUserBook" element={<CreateUserBook username = {username} city = {city}  />} />
          <Route path="DeleteUserBook" element={<DeleteUserBook username = {username} city = {city} />} />
          <Route path="MyBooks" element={<MyBooks username = {username} />} />
          <Route path="CreateMessage" element={<CreateMessage username = {username} city = {city} />} />
          <Route path="Messages" element={<Messages username = {username} />} />
          <Route path="SentMessages" element={<SentMessages username = {username} />} />
        </Route>
      </Routes>
      </BrowserRouter>
      ) : (
        <EntryPage onLogin={handleLoginMain} />
      )}
    </div>
    
  );
}

export default App;
