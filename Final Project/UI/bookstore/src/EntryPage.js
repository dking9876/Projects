import React from 'react';
import Layout from "./Layout";
import Login from './Login';
import Signup from './Signup';
import ReactDOM from "react-dom/client";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import EntryLayout from './EntryLayout';
function EntryPage({ onLogin }) {
    const handleLoginEntry = (username, city, data) => {
        onLogin(username, city, data )
      }
  return (
    <div>
      <h2>Please login or signin </h2>
      <BrowserRouter>
      <Routes>
  <Route path="/" element={<EntryLayout />}>
    
    <Route path="login" element={<Login onLogin = {handleLoginEntry}/>} />
    <Route path="signup" element={<Signup />} />
  </Route>
</Routes>
</BrowserRouter>
  
    </div>
  );
}

export default EntryPage;