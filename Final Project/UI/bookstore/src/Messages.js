import React, { useState } from 'react';
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
  })
  .catch(error => {
    // Handle any errors
    console.error('Error:', error);
  })
  
}

function Messages() {

    const handleSubmit = (e) => {
        e.preventDefault();
        
        // Do something with username and password, such as sending to a server
        const response =  GetMessages()
    }
    return (
        <div>
        <form onSubmit={handleSubmit}>
        <button type="submit">Submit</button>
      </form>
      </div>
    );
  }
  
  export default Messages;
