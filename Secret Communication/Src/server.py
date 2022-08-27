from scapy.layers.inet import UDP
from scapy.sendrecv import sniff
import scapy


def filter_dns(packet):
    return  UDP in packet and 20000 < packet[UDP]. dport < 20225

messege=""

packets = sniff(timeout= 20, lfilter=filter_dns)
for i in packets:
    port_num = i[UDP].dport- 20000
    letter=chr(port_num)
    messege+=letter
print(messege)


