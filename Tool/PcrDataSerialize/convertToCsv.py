
import _pcr_data

f = open('./_pcr_data.csv', mode = 'w', encoding='utf-8')

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



