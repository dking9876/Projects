import React, { useState } from 'react';


function SentMessages(username) {
    const [data, setdata] = useState([])

      const handleSubmit = (e) => {
        e.preventDefault();
        fetch(` http://localhost:3000/api/message/` + username.username + `/sentmessages`, {
        method: 'GET'})
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
      <h1>SentMessages</h1>
      <form onSubmit={handleSubmit}>
      <button type="submit">Click to see messages</button>
      </form>
        <div>
        <table border={1}>
          <thead>
            <tr>
              <th>city</th>
              <th>destanation</th>
              <th>body</th>
            </tr>
          </thead>
          <tbody>
            {data.map(item => (
              <tr key={item.body}>
                <td>{item.city}</td>
                <td>{item.destination}</td>
                <td>{item.body}</td>
              </tr>
            ))}
          </tbody>
        </table>
        </div>
    </div>
    )
            }
    

  export default SentMessages;
