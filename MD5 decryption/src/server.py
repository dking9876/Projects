import socket
import select
current_number = 1000000000
max_number = 9999999999
number_found = False

def handle_client(msg, current_number ):
    print(current_number, max_number )
    if msg == "send the answer":
        return "4565821f945d17da5a4f4c050b42423d"
    if msg == "send number":
        if current_number < max_number:
            return current_number
        else:
            return "end"
    else:
        print(msg)
        return "end"

def send_waiting_messages(wlist):
    for message in messages_to_send:
        current_socket, data = message
        if current_socket in wlist:
            current_socket.send(data)
        messages_to_send.remove(message)


IP = '127.0.0.1'
PORT = 5476
server_socket = socket.socket()
server_socket.bind((IP, PORT))
##print('server is up at : ', IP, PORT)
server_socket.listen(10)
open_client_sockets = []
messages_to_send = []
while True:
    rlist, wlist, xlist = select.select([server_socket] + open_client_sockets, open_client_sockets, [])
    for current_socket in rlist:
        if current_socket is server_socket:
            (new_socket, address) = server_socket.accept()
            ##print("new socket connected to server: ", new_socket.getpeername())
            open_client_sockets.append(new_socket)
        else:
            ##print('New data from client!')
            data = current_socket.recv(1024).decode()


            ##print(data)
            return_msg = handle_client(data, current_number)
            if data == "send number":
                current_number = current_number + 10000000
            if data == "end":
                p_id = current_socket.getpeername()
                open_client_sockets.remove(current_socket)
                print(f"Connection with client {p_id} closed.")
                number_found = True
                ##messages_to_send.append((current_socket, b"end"))

            else:
                p_id = current_socket.getpeername()
                ##print(f"client: {p_id}", data)
                if number_found:
                    return_msg = "end"
                messages_to_send.append((current_socket, str(return_msg).encode()))


    send_waiting_messages(wlist)


