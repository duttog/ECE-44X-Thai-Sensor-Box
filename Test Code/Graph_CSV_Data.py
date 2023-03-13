import matplotlib.pyplot as plt
import numpy as np

temp = []
humidity  = []
wind = []
water = []
pkt_no = []

f = open("test_data.csv", 'r')
i = 0
for x in f:
    i += 1
    tokens = x.split(',')
    temp.append(float(tokens[0]))
    humidity.append(float(tokens[1]))
    wind.append(float(tokens[2]))
    water.append(float(tokens[3]))
    pkt_no.append(int(tokens[4]))
    

x = np.arange(0, i)

figure, axis = plt.subplots(2, 2)
axis[0, 0].plot(x, temp, humidity)
axis[0, 0].set_title("Temperature and Humidity")
axis[0, 1].plot(x, wind)
axis[0, 1].set_title("Wind Speed")
axis[1, 0].plot(x, water)
axis[1, 0].set_title("Water Level")
axis[1, 1].plot(x, pkt_no)
axis[1, 1].set_title("Packet Number")

plt.show()