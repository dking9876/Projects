import React, { useState } from 'react';
function CreateMessage({ username }) {
    const [city, setCity] = useState('');
    const [destination, setDestination] = useState('');
    const [body, setBody] = useState('');

    const handleSubmit = (e, param) => {
      e.preventDefault();
      alert('Message send successfully')
      fetch(`http://localhost:3000/api/message`, {method: 'POST', headers: {'Content-Type': 'application/json'},
        body: JSON.stringify(
            {
                "city":city,
                "source":username,
                "destination":destination,
                "body": body
             
             }
        ) })
             
      
    }
  
    return (
      <div>
        <h2>Create Message</h2>
        <form onSubmit={handleSubmit}>
          <div>
            <label htmlFor="city">City:</label>
            <input type="text" id="city" value={city} onChange={(e) => setCity(e.target.value)}  />
          </div>
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