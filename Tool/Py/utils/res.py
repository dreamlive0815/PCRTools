
import os

resRootDir = './res/PCR/Taiwan'

def getResPath(*paths):
    return os.path.join(resRootDir, *paths)