"""Script reads the temperature from each available sensor,
and logs to a local sqlite database
"""
import sqlite3
from datetime import datetime

import requests
from w1thermsensor import W1ThermSensor

BASE_URL = "http://localhost:10108/"


def main():
    """ Main entry point"""
    read_time = datetime.utcnow().isoformat()
    # status = log_reading("03168a1eedff", datetime.utcnow().isoformat(), 100)
    # print status
    sensors = W1ThermSensor.get_available_sensors()

    for dummy_i, sensor in enumerate(sensors):
        temperature = sensor.get_temperature(W1ThermSensor.DEGREES_F)
        status = log_reading(sensor.id, read_time, temperature)
        log_to_sqlite(sensor.id, read_time, temperature)
        print status


def log_reading(sensor_id, time, temperature):
    """Publish a temperature reading to web service"""
    url = BASE_URL + "readings/" + sensor_id
    payload = {'time': time, 'temperature': temperature}
    response = requests.put(url, json=payload)
    return response.status_code


def log_to_sqlite(sensor_id, time, temperature):
    """Log temp reading to local storage"""
    conn = sqlite3.connect('/home/pi/Freezer/freezer_temps.db')
    curs = conn.cursor()
    curs.execute("""INSERT INTO temperatures values((?),(?),(?))""",
                 (time, sensor_id, temperature))
    conn.commit()


if __name__ == "__main__":
    main()
