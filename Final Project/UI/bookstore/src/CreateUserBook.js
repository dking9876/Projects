import React, { useState } from 'react';
function CreateUserBook({ username, city }) {
    
    const [bookName, setBookName] = useState('');
    const [price, setPrice] = useState('');
    const [condition, setCondition] = useState('');

    
    const handleSubmit = (e) => {
      e.preventDefault();
      fetch(`http://localhost:3000/api/userbook`, {method: 'POST', headers: {'Content-Type': 'application/json'},
        body: JSON.stringify(
            {
                "city":city,
                "username":username,
                "price":price,
                "bookname": bookName,
                "condition":condition
             
             }
        ) })
        .then(response => {
          if (response.ok) {
            // Successful response
            alert('Book for sale was created successfully')
            console.log('Status code:', response.status);
            
            
          } else {
            alert('An error occurred ')
            // Handle error response
            console.log('Status code:', response.status);
          }  
        })
        .catch(error => {
          // Handle any errors
          console.error('Error:', error);
        });
             
      
    }
  
    return (
      <div>
        <h2>Create new book for sale</h2>
        <form onSubmit={handleSubmit}>
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
  
  export default CreateUserBook;