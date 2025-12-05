import os
import time

start_time = time.time()

here = os.path.dirname(os.path.abspath(__file__))
testfilepath = os.path.join(here, "test.txt")
testfile = open(testfilepath, "r")
inputfilepath = os.path.join(here, "input.txt")
inputfile = open(inputfilepath, "r")

data = inputfile.readlines()

breakind = data.index("\n")
idranges = data[:breakind]
ids = data[breakind + 1:]

p1 = p2 = 0

for id in ids:
    val = int(id.rstrip())
    for idrange in idranges:
        start, end = map(int, idrange.rstrip().split('-'))
        if start <= val <= end:
            p1 += 1
            break

parsed_ranges = []

for idrange in idranges:
    start, end = map(int, idrange.rstrip().split('-'))
    parsed_ranges.append((start, end))

parsed_ranges = sorted(parsed_ranges, key=lambda x: x[0])

merged_ranges = []
for start, end in parsed_ranges:
    if not merged_ranges:
        merged_ranges.append([start, end])
    else:
        last_start, last_end = merged_ranges[-1]
        if start <= last_end + 1:
            merged_ranges[-1][1] = max(last_end, end)
        else:
            merged_ranges.append([start, end])

p2 = sum(end - start + 1 for start, end in merged_ranges)
end_time = time.time() - start_time

print(f"Part one: {p1}")
print(f"Part two: {p2}")
print(f"Time taken: {end_time:3f} s")