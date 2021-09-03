

import os
import requests

print('syncing _pcr_data.py...')
url = 'https://raw.githubusercontent.com/Ice-Cirno/HoshinoBot/master/hoshino/modules/priconne/_pcr_data.py'
r = requests.get(url, timeout=5)
pcrDataPath = os.path.join(os.path.dirname(__file__), '_pcr_data.py')
with open(pcrDataPath, "wb") as code:
    code.write(r.content)
print('sync _pcr_data.py done')

import _pcr_data

from utils import res
from utils.csv import Csv

# https://raw.githubusercontent.com/Ice-Cirno/HoshinoBot/master/hoshino/modules/priconne/_pcr_data.py
csvPath = res.getResPath("PCR/Csv/Unit.csv")
csv = Csv()
csv.setHeaders(('ID', 'Nicknames'))
for key, value in _pcr_data.CHARA_NAME.items():
    csv.addDataRow((str(key), ';'.join(value)))
csv.saveAsFile(csvPath)
print('Done')
