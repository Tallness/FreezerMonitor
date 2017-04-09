import sqlite3

from w1thermsensor import W1ThermSensor

conn = sqlite3.connect('/home/pi/Freezer/freezer_temps.db')
curs = conn.cursor()

sensors = W1ThermSensor.get_available_sensors()
for i, sensor in enumerate(sensors):
    #print("%s %s %.4f" % (datetime.now().isoformat(), sensor.id, sensor.get_temperature(W1ThermSensor.DEGREES_F)))
    #log.write("%s %s %.4f\n" % (datetime.now().isoformat(), sensor.id, sensor.get_temperature(W1ThermSensor.DEGREES_F)))
    curs.execute("""INSERT INTO temperatures values(datetime('now'),(?),(?))""",
                 (sensor.id, sensor.get_temperature(W1ThermSensor.DEGREES_F)))
    conn.commit()
