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
pkt_no = []
index = 0

x = []
i = 0



while(1):
    data = ser.readline()
    readings = data.split(b',')
    temp.append(float(readings[0][5:]))
    humidity.append(float(readings[1][9:]))
    if(readings[2][11] == 'n'):
        wind.append(0.0)
    else:
        wind.append(float(readings[2][11:]))
    water.append(max(0, (800.0 - int(readings[3][12:])) * float(12 / 400) * 2.54))
    pkt_no.append(int(readings[4][10:]))

    f = open("test_data.csv", 'a')
    f.write(str(temp[i]))
    f.write(',')
    f.write(str(humidity[i]))
    f.write(',')
    f.write(str(wind[i]))
    f.write(',')
    f.write(str(water[i]))
    f.write(',')
    f.write(str(pkt_no[i]))
    f.write('\n')
    f.close()
    i += 1
    

ser.close()
