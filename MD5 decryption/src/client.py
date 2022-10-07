import socket
import hashlib
import time
import threading
import multiprocessing
from multiprocessing import Process
number_of_threads = 1
jump = 10000000
global number_found
global correct_num
global ans

number_found = False
correct_num = 0
IP = '127.0.0.1'
PORT = 5476
my_socket = socket.socket()
my_socket.connect((IP, PORT))
global flag

number_of_process = 5

def hash(number):
    return hashlib.md5(str(number).encode("utf-8")).hexdigest()


def check_num(start_num, end_num):
    global number_found
    global correct_num
    for i in range(start_num, end_num):

        if hash(i) == ans:
            number_found = True
            correct_num = i

def client():
    global number_found
    global correct_num
    flag = True
    jump = 10000000
    number_found = False
    correct_num = 0
    IP = '127.0.0.1'
    PORT = 5476
    my_socket = socket.socket()
    my_socket.connect((IP, PORT))
    msg = "send the answer"
    my_socket.send(msg.encode())
    global ans
    ans = my_socket.recv(1024).decode()
    while flag:
        start = time.time()
        ##time.sleep(1)
        msg = "send number"
        my_socket.send(msg.encode())
        number = my_socket.recv(1024).decode()
        if number == "end":
            flag = False
        else:
            threads = []

            for i in range(0, number_of_threads):
                x = threading.Thread(target=check_num, args=(int(number), int(number) + int(jump / number_of_threads),))
                x.start()
                number = int(number) + jump / number_of_threads
                threads.append(x)

            for t in threads:
                t.join()
            end = time.time()
            print("time: " + str(end - start) + " numbers: " + str(int(number)))
            if number_found:
                time.sleep(1)
                my_socket.send(str(correct_num).encode())
                print("correct num:" + str(correct_num))

    time.sleep(1)
    my_socket.send(b"end")
    print('connection to be closed')
    my_socket.close()

if __name__ == '__main__':

    for i in range(0, number_of_process):
            x = Process(target=client).start()



