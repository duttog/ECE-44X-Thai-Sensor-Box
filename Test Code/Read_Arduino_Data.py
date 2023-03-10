import serial
import numpy as np
import matplotlib.pyplot as plt
import matplotlib.animation as animation
import pylab
import time

ser = serial.Serial('COM6', 9600, timeout=10)

temp = []
humidity = []
wind = []
water = []
cal_temp = []
pkt_no = []
index = 0

x = []
i = 0



while(1):
    data = ser.readline()
    print(data[5:10])
    temp.append(float(data[5:10]))
    humidity.append(float(data[20:25]))
    wind.append(int(data[37:40]))
    water.append(int(data[53:56]))
    cal_temp.append(int(data[66:69]))
    pkt_no.append(int(data[80:]))

    f = open("test_data.csv", 'a')
    f.write(str(temp[i]))
    f.write(',')
    f.write(str(humidity[i]))
    f.write(',')
    f.write(str(wind[i]))
    f.write(',')
    f.write(str(water[i]))
    f.write(',')
    f.write(str(cal_temp[i]))
    f.write(',')
    f.write(str(pkt_no[i]))
    f.write('\n')
    f.close()
    i += 1
    

ser.close()
