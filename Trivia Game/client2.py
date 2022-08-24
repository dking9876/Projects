import socket
import chatlib  # To use chatlib functions or consts, use chatlib.****

SERVER_IP = "127.0.0.1"  # Our server will run on same computer as client
SERVER_PORT = 5678


# HELPER SOCKET METHODS

def build_and_send_message(conn, code, msg):
    """
    Builds a new message using chatlib, wanted code and message.
    Prints debug info, then sends it to the given socket.
    Paramaters: conn (socket object), code (str), msg (str)
    Returns: Nothing
    """
    protocol_msg = chatlib.build_message(code, msg).encode()
    print(protocol_msg)
    conn.send(protocol_msg)


# Implement Code


def recv_message_and_parse(conn):
    """
    Recieves a new message from given socket.
    Prints debug info, then parses the message using chatlib.
    Paramaters: conn (socket object)
    Returns: cmd (str) and data (str) of the received message.
    If error occured, will return None, None
    """
    data = conn.recv(10000).decode()
    cmd, msg = chatlib.parse_message(data)
    if cmd != None or msg != None:
        print(f"The server sent: {data}")
        print(f"Interpretation:\nCommand: {cmd}, message: {msg}")
        return cmd, msg
    else:
        return None, None




def connect():
    # Implement Code
    my_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    my_socket.connect((SERVER_IP, SERVER_PORT))
    return my_socket


def error_and_exit(msg):
    print(msg)
    exit



def login(conn):
    server_cmd = "ERROR"
    server_msg = ""
    while(server_cmd != "LOGIN_OK" and server_msg != "You are already connected"):
        username = input("Please enter username: \n")
        password = input("Please enter password: \n")
        cmd = "LOGIN"
        data = username + "#" + password
        build_and_send_message(conn, cmd, data)
        server_cmd, server_msg = recv_message_and_parse(conn)
        if server_cmd == "LOGIN_OK":
            print("The login was successful")
            return None
        else:
            print(server_msg)
            print("The login failed, try again")



        # Implement code




# Implement code




def logout(conn):
    # Implement code
    build_and_send_message(conn, chatlib.PROTOCOL_CLIENT["logout_msg"], "")


def build_send_recv_parse(conn, cmd, data):
    build_and_send_message(conn, cmd, data)
    msg_code, msg = recv_message_and_parse(conn)
    return msg_code, msg

def get_score(conn):
    msg_code, msg = build_send_recv_parse(conn, "MY_SCORE", "")
    if msg_code == "ERROR":
        print("ERROR")
    else:
        print("your score is: " + msg)


def play_question(conn):
    try:
        msg_code, msg = build_send_recv_parse(conn, "GET_QUESTION", "")
        if msg_code == "NO_QUESTIONS":
            print("The game is finished, you answered all the questions")
        else:
            split_msg = msg.split("#")
            id = split_msg[0]
            question = split_msg[1]
            print("the question is: " + question)
            print("answer number one is: " + split_msg[2])
            print("answer number two is: " + split_msg[3])
            print("answer number three is: " + split_msg[4])
            print("answer number four is: " + split_msg[5])
            answer = input("Please enter the number of the right answer: \n")
            if 0 < int(answer) < 5:
                data = id + "#" + answer
                server_response_cmd, server_response_msg = build_send_recv_parse(conn, "SEND_ANSWER", data)
                if server_response_cmd == "CORRECT_ANSWER":
                    print("you are correct")
                    print("The right answer is answer number " + answer)
                else:
                    print("your answer is wrong")
                    print("The right answer is answer number " + server_response_msg)
            else:
                print("your answer is not one of the options ")
    except BaseException as err:
        print("There was an error")
        return None

def get_highscore(conn):
    msg_code, msg = build_send_recv_parse(conn, "HIGHSCORE", "")
    print("The high score is: " + "\n" + msg)


def get_logged_users(conn):
    msg_code, msg = build_send_recv_parse(conn, "LOGGED ", "")
    print("The logged accounts are: " + "\n" + msg)



def main():
    conn = connect()
    choose_action = input("Please enter action: \n" + "options: login,  play question, get score, get highscore, get logged users, logout  \n")
    while choose_action != "login":
        print("you have to login first")
        choose_action = input("Please enter action: \n" + "options: login,  play question, get score, get highscore, get logged users, logout  \n")
    while(choose_action != "logout" ):
        if choose_action == "login":
            login(conn)
        elif choose_action == "get score":
            get_score(conn)
        elif choose_action == "play question":
            play_question(conn)
        elif choose_action == "get highscore":
            get_highscore(conn)
        elif choose_action == "get logged users":
            get_logged_users(conn)
        else:
            build_and_send_message(conn, "ERROR", "")
            msg_code, msg = recv_message_and_parse(conn)
            print(msg)
        choose_action = ("Please enter action: \n" + "options: login,  play question, get score, get highscore, get logged users, logout  \n")
    logout(conn)


if __name__ == '__main__':
    main()