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
import picture from './ProjectLogo.jpg'; // Import the JPG picture


function App() {
  const [isLoggedIn, setIsLoggedIn] = useState(false);
  const [username, setUsername] = useState('');
  const [city, setCity] = useState('');
  const [token, setToken] = useState('');

  const handleLoginMain = (username, city, data, token) => {
    setIsLoggedIn(data);
    setCity(city);
    setUsername(username);
    setToken(token);
    console.log('token:',token);
  }

  const styles = {
    container: {
      display: 'flex',
      justifyContent: 'center',
      alignItems: 'center',
      height: '100vh',
      backgroundColor: '#E0E8F0',
    },
    content: {
      textAlign: 'center',
      backgroundColor: '#FFFFFF',
      padding: '60px',
      borderRadius: '8px',
      boxShadow: '0 4px 8px rgba(0, 0, 0, 0.1)',
    },
    title: {
      fontSize: '50px',
      marginBottom: '20px',
      color: '#34495E',
    },
    description: {
      fontSize: '40px',
      marginBottom: '40px',
      color: '#666666',
    },
    button: {
      padding: '14px 28px',
      backgroundColor: '#3498DB',
      color: '#FFFFFF',
      borderRadius: '4px',
      fontSize: '20px',
      cursor: 'pointer',
      transition: 'background-color 0.3s ease',
    },
    buttonHover: {
      backgroundColor: '#1ABC9C',
    },
  };
  
  

  return (
    <div style={styles.container}>
      <div style={styles.content}>
      
      <img src={picture} alt="Picture" style={{ width: '20%', height: 'auto' }} />
        <h1 style={styles.title}>Welcome {username} to BookStore          </h1>
        
       
       
    <div>
      {isLoggedIn ? (
        <BrowserRouter>
        <Routes>
        <Route path="/login" element={<Layout />}>
          <Route index element={<Main/>} />
          <Route path="searchBook" element={<SearchBook city = {city} token = {token}/>} />
          <Route path="CreateUserBook" element={<CreateUserBook username = {username} city = {city} token = {token}  />} />
          <Route path="DeleteUserBook" element={<DeleteUserBook username = {username} city = {city} token = {token} />} />
          <Route path="MyBooks" element={<MyBooks  token = {token} />} />
          <Route path="CreateMessage" element={<CreateMessage username = {username} city = {city} token = {token}/>} />
          <Route path="Messages" element={<Messages token = {token} />} />
          <Route path="SentMessages" element={<SentMessages  token = {token} />} />
        </Route>
      </Routes>
      </BrowserRouter>
      ) : (
        <EntryPage onLogin={handleLoginMain} />
      )}
    </div>
    </div>
    </div>
    
  );
}

export default App;
