
import scapy
from scapy.layers.dns import DNS, DNSQR
from scapy.layers.inet import IP, UDP
from scapy.sendrecv import send

message = input("write your massage")
list_of_letters = list(message)
for i in list_of_letters:
    port_num = ord(i)
    packet = IP(dst="10.0.0.31") / UDP(sport=24601, dport=port_num + 20000)
    send(packet)
