import React, { useState } from 'react';
function SearchBook({ city }) {
    const [bookName, setBookName] = useState('');
    const [price, setPrice] = useState('');
    const [condition, setCondition] = useState('');
    const [data, setdata] = useState([])
  
    const handleSubmit = (e) => {
        e.preventDefault();
        fetch(`http://localhost:3000/api/userbook/searchbook`, {method: 'POST', headers: {'Content-Type': 'application/json'},
        body: JSON.stringify(
            {
                "city":city,
                "bookname":bookName,
                "price":price,
                "condition":condition
            }
        ) })
        .then(response => response.json())
        .then(json => setdata(json))
        .catch(error => {
          // Handle any errors
          console.error('Error:', error);
        })
        .finally(() => {
        })
    }
  
    return (
      <div>
        <h2>Search Book</h2>
        <form onSubmit={handleSubmit}>
          <div>
            <label htmlFor="bookName">BookName:</label>
            <input type="bookName" id="bookName" value={bookName} onChange={(e) => setBookName(e.target.value)}  />
          </div>
          <div>
            <label htmlFor="price">Price:</label>
            <input type="price" id="price" value={price} onChange={(e) => setPrice(e.target.value)}  />
          </div>
          <div>
            <label htmlFor="condition">Condition:</label>
            <input type="condition" id="condition" value={condition} onChange={(e) => setCondition(e.target.value)}  />
          </div>
          <button type="submit">Submit</button>
        </form>
        <table border={1}>
          <thead>
            <tr>
              <th>city</th>
              <th>ownerUserName</th>
              <th>price</th>
              <th>bookname</th>
              <th>condition</th>
            </tr>
          </thead>
          <tbody>
            {data.map(item => (
              <tr key={item.price}>
                <td>{item.city}</td>
                <td>{item.username}</td>
                <td>{item.price}</td>
                <td>{item.bookname}</td>
                <td>{item.condition}</td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    );
  }
  export default SearchBook;