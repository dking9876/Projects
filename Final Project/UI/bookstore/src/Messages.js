import React, { useState } from 'react';


function Messages(username, ) {
    const [data, setdata] = useState([])
    
    

    const handleSubmit = (e) => {
        e.preventDefault();
        fetch(`http://localhost:3000/api/message/` + username.username + `/getmessages`, {method: 'GET'})
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
      <h1>Messages</h1>
      <form onSubmit={handleSubmit}>
      <button type="submit">Click to see messages</button>
      </form>
        <div>
        <table border={1}>
          <thead>
            <tr>
              <th>city</th>
              <th>source</th>
              <th>body</th>
              <th>time</th>
            </tr>
          </thead>
          <tbody>
            {data.map(item => (
              <tr key={item.body}>
                <td>{item.city}</td>
                <td>{item.source}</td>
                <td>{item.body}</td>
                <td>{(new Date(item.time)).toLocaleString()}</td>
              </tr>
            ))}
          </tbody>
        </table>
        </div>
    </div>
    )
            }
    

  export default Messages;
