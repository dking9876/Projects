import React, { useState } from 'react';
function CreateMessage({ username, city }) {
    
    const [destination, setDestination] = useState('');
    const [body, setBody] = useState('');

    const handleSubmit = (e) => {
      e.preventDefault();
      fetch(`http://localhost:3000/api/message`, {method: 'POST', headers: {'Content-Type': 'application/json'},
        body: JSON.stringify(
            {
                "city":city,
                "source":username,
                "destination":destination,
                "body": body
             
             }
        ) })
        .then(response => {
          if (response.ok) {
            // Successful response
            alert('Message sent successfully')
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
        <h2>Create Message</h2>
        <form onSubmit={handleSubmit}>
          <div>
            <label htmlFor="destination">Destination:</label>
            <input type="text" id="destination" value={destination} onChange={(e) => setDestination(e.target.value)}  />
          </div>
          <div>
            <label htmlFor="body">Body:</label>
            <input type="text" id="body" value={body} onChange={(e) => setBody(e.target.value)}  />
          </div>
          <button type="submit">Submit</button>
        </form>
      </div>
    );
  }
  
  export default CreateMessage;