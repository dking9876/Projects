##  Trivia Game 
**Description:**
The Trivia game is a well known question based game. The user gets questions and get points for every correct answer.

**Technologies:**
Python, client-server, multi-user design, HTTP, socket

**System design:**
This is client-server based system. The communication between the client and the server uses HTTP like protocol.  The client sends a request to the server. The server parses the request into its parts. It takes the command part of the request , executes the command and returns a response to the client. The client also parses the request, takes the parts it needs and uses them.
The server implements  a multi-user design. The server is able to receive requests and send responses to several clients at the same time.  

**Client requests:**
|    Requests     |       Description                                                  |
|----------------|-------------------------------|
|LOGIN | send a username and password to the server and the server checks if there is an account with the data you send and login.                    |
|GET_QUESTION          |ask the server for a question and the server response with a new question from the list of questions. if there are no more questions the server sends a proper response.                     |
|SEND_ANSWER          |client sends  question number and answer. the server checks if its correct or not and sends you a proper response. if your answer is correct you get 5 points.
|MY_SCORE          |server response with current score.
|HIGHSCORE          |server response with names of the people with the highest score.
|LOGGED          |server response with currently active people.
|LOGOUT          |disconnect client from the game.