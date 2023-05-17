import React, { useState } from 'react';


function Messages() {
    const [data, setdata] = useState([])

    const handleSubmit = (e) => {
        e.preventDefault();
        fetch(`http://localhost:3000/api/message/mark/getmessages`, {method: 'GET'})
        .then(response => response.json())
        .then(json => setdata(json))
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
            </tr>
          </thead>
          <tbody>
            {data.map(item => (
              <tr key={item.body}>
                <td>{item.city}</td>
                <td>{item.source}</td>
                <td>{item.body}</td>
              </tr>
            ))}
          </tbody>
        </table>
        </div>
    </div>
    )
            }
    

  export default Messages;
