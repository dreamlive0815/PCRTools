

from utils import res
from utils.csv import Csv

import os
import requests
from io import BytesIO
from PIL import Image

class PCRUnit:


    @staticmethod
    def getIconFileName(id, star):
        if star >= 6:
            star = 6
        elif star >= 3:
            star = 3
        else:
            star = 1
        return f'icon_unit_{id}{star}1.png'

    @staticmethod
    def downloadIcons():
        csvPath = res.getResPath("PCR/Csv/Unit.csv")
        csv = Csv(csvPath)
        ids = csv.getRowKeys()
        stars = (1, 3, 6)
        for id in ids:
            for star in stars:
                try:
                    PCRUnit.downloadIcon(id, star)
                except Exception as e:
                    print(f'Failed To Download Icon id = {id} star = {star}')


    @staticmethod
    def downloadIcon(id, star):
        fileName = PCRUnit.getIconFileName(id, star)
        savePath = res.getResPath(f'PCR/Image/Unit/{fileName}')
        if os.path.exists(savePath):
            print(f'Icon: {savePath} exists, skip')
            return
        url = f'https://redive.estertion.win/icon/unit/{id}{star}1.webp'
        print(f'Downloading Icon From {url}')
        rsp = requests.get(url, stream=True, timeout=5)
        img = Image.open(BytesIO(rsp.content))
        img.save(savePath)
        print(f'Saved Icon To {savePath}')



PCRUnit.downloadIcons()