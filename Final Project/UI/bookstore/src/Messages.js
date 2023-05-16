import React, { useState } from 'react';
import { Bar } from 'react-chartjs-2';
import { Chart, registerables } from 'chart.js';
Chart.register(...registerables);


async function GetMessages() {
  const response = await fetch(`http://localhost:3000/api/message/mark/getmessages`, {
      method: 'GET'
  })
  .then(response => {
    if (response.ok) {
      // Successful response
      console.log('Status code:', response.status);
      return response.json();
    } else {
      // Handle error response
      console.log('Status code:', response.status);
    }  
  })
  .then(data => {
    // Process the data
    console.log('Data:', data);
    return data;
  })
  .catch(error => {
    // Handle any errors
    console.error('Error:', error);
  })
  
}

function Messages() {
    const data = [
        { id: 1, name: 'John', age: 25 },
        { id: 2, name: 'Jane', age: 30 },
        { id: 3, name: 'Bob', age: 35 },
        { id: 4, name: 'Alice', age: 28 },
      ];

    const handleSubmit = (e) => {
        e.preventDefault();
        
        // Do something with username and password, such as sending to a server
        const response =  GetMessages()
    }
    //<form onSubmit={handleSubmit}>
        //<button type="submit">Submit</button>
      //</form>
      return (
        <table>
          <thead>
            <tr>
              <th>ID</th>
              <th>Name</th>
              <th>Age</th>
            </tr>
          </thead>
          <tbody>
            {data.map(item => (
              <tr key={item.id}>
                <td>{item.id}</td>
                <td>{item.name}</td>
                <td>{item.age}</td>
              </tr>
            ))}
          </tbody>
        </table>
      );
    }
  
  export default Messages;
