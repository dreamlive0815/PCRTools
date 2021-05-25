

import os
import _pcr_data

from utils import res
from utils.csv import Csv

csvPath = res.getResPath("PCR/Csv/Unit.csv")
csv = Csv()
csv.setHeaders(('ID', 'Nicknames'))
for key, value in _pcr_data.CHARA_NAME.items():
    csv.addDataRow((str(key), ';'.join(value)))
csv.saveAsFile(csvPath)
print('Done')
