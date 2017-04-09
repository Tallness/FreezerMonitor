from w1thermsensor import W1ThermSensor
from time import sleep
from datetime import datetime

with open("freezer_temps.log", "a") as log:
    while True:
        sensors = W1ThermSensor.get_available_sensors()
        for i,sensor in enumerate(sensors):
            print("%s %s %.4f" % (datetime.now().isoformat(), sensor.id, sensor.get_temperature(W1ThermSensor.DEGREES_F)))
            log.write("%s %s %.4f\n" % (datetime.now().isoformat(), sensor.id, sensor.get_temperature(W1ThermSensor.DEGREES_F)))
        sleep(5)
