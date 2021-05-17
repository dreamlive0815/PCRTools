
import os
import _pcr_data

dir = os.path.dirname(__file__)
csvPath = dir + '/_pcr_data.csv'

f = open(csvPath, mode = 'w', encoding = 'utf-8')

def appendLine(line):
    f.write(line)
    f.write("\n")

appendLine('ID,Nicknames')

for key, value in _pcr_data.CHARA_NAME.items():
    l = []
    l.append(key)
    l.append(';'.join(value))
    appendLine(','.join('%s' % i for i in l))

f.flush()
f.close()



