import time

temp_sensor1 = '/sys/bus/w1/devices/28-031685a74bff/w1_slave'
temp_sensor2 = '/sys/bus/w1/devices/28-03168a1eedff/w1_slave'
temp_read_delay = 5

def temp_raw():
    f1 = open(temp_sensor1, 'r')
    lines = f1.readlines()
    f1.close()
    return lines

def read_temp():
    lines = temp_raw()
    while lines[0].strip()[-3:]!='YES':
        time.sleep(0.2)
        print('Sleeping...')
        lines = temp_raw()
    temp_output = lines[1].find('t=')
    if temp_output != -1:
        temp_string = lines[1].strip()[temp_output+2:]
        temp_c = float(temp_string) / 1000.0
        temp_f = temp_c * 9.0 / 5.0 + 32.0
        return temp_f

while True:
    print(read_temp())
    time.sleep(temp_read_delay)
