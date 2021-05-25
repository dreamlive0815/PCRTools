

class Csv:

    def __init__(self):
        self.headers = []
        self.dataRows = []

    def loadFromFile(self, filePath):
        with open(filePath, 'r', encoding='utf-8') as f:
            lineCount = 0
            for line in f.readlines():
                lineCount += 1
                vals = line.split(',')
                if lineCount == 1:
                    self.setHeaders(vals)
                else:
                    self.addDataRow(vals)

    def setHeaders(self, headerVals):
        self.headers = headerVals

    def addDataRow(self, dataVals):
        self.dataRows.append(dataVals)

    def getRowKeys(self):
        keys = []
        for row in self.dataRows:
            keys.append(row[0])
        return keys

    def saveAsFile(self, filePath):
        with open(filePath, 'w', encoding='utf-8') as f:
            def writeLine(vals):
                f.write(','.join(vals))
                f.write("\n")
            writeLine(self.headers)
            for row in self.dataRows:
                writeLine(row)
            





                
