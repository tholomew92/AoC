import os

here = os.path.dirname(os.path.abspath(__file__))
testfile = os.path.join(here, "test.txt")
test = open(testfile, "r")
h = v = 0
testlines = [x.strip() for x in test.read().split(',')]
for walk in testlines:
    direction = walk[0]
    steps =  walk[1]