
import os

resRootDir = './res'

def getResPath(*paths):
    return os.path.join(resRootDir, *paths)