import React, { useState } from 'react';
function DeleteUserBook({ username }) {
    const [city, setCity] = useState('');
    const [bookName, setBookName] = useState('');
    const [price, setPrice] = useState('');
    const [condition, setCondition] = useState('');

    
    const handleSubmit = (e) => {
      e.preventDefault();
      alert('Your book for sale created successfully')
      fetch(`http://localhost:3000/api/userbook/` + username.username + ``, {method: 'DELETE', headers: {'Content-Type': 'application/json'},
        body: JSON.stringify(
            {
                "city":city,
                "username":username,
                "price":price,
                "bookname": bookName,
                "condition":condition
             
             }
        ) })
             
      
    }
  
    return (
      <div>
        <h2>Delete your book</h2>
        <form onSubmit={handleSubmit}>
          <div>
            <label htmlFor="city">City:</label>
            <input type="text" id="city" value={city} onChange={(e) => setCity(e.target.value)}  />
          </div>
          <div>
            <label htmlFor="price">Price:</label>
            <input type="price" id="price" value={price} onChange={(e) => setPrice(e.target.value)}  />
          </div>
          <div>
            <label htmlFor="bookName">BookName:</label>
            <input type="bookName" id="bookName" value={bookName} onChange={(e) => setBookName(e.target.value)}  />
          </div>
          <div>
            <label htmlFor="condition">Condition:</label>
            <input type="condition" id="condition" value={condition} onChange={(e) => setCondition(e.target.value)}  />
          </div>
          <button type="submit">Submit</button>
        </form>
      </div>
    );
  }
  
  export default DeleteUserBook;