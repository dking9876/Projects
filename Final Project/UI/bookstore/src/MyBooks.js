import React, { useState } from 'react';


function MyBooks(username) {
    const [data, setdata] = useState([])

    const handleSubmit = (e) => {
        e.preventDefault();
        fetch(`http://localhost:3000/api/userbook/` + username.username + ``, {method: 'GET'})
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
      <h1>MyBooks</h1>
      <form onSubmit={handleSubmit}>
      <button type="submit">Click to see your books</button>
      </form>
        <div>
        <table border={1}>
          <thead>
            <tr>
              <th>city</th>
              <th>price</th>
              <th>bookname</th>
              <th>condition</th>
            </tr>
          </thead>
          <tbody>
            {data.map(item => (
              <tr key={item.body}>
                <td>{item.city}</td>
                <td>{item.price}</td>
                <td>{item.bookname}</td>
                <td>{item.condition}</td>
              </tr>
            ))}
          </tbody>
        </table>
        </div>
    </div>
    )
            }
    

  export default MyBooks;
