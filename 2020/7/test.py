from collections import defaultdict, deque
from re import search

# Return bag contents parsed as a tuple (qty, color)
def parse_content(content_str):
  pieces = search("^(\d+) (.*) bags?$", content_str)
  return (int(pieces.group(1)), pieces.group(2))

# Return list of outer bag color and content tuples
def parse(line):
  outer, content_str = line.split(' contain ')

  outer_color = outer[:-5]  # Strip " bags"

  if content_str == 'no other bags.':
    contents = []
  else:
    contents = [parse_content(c) for c in content_str[:-1].split(', ')]
  
  return (outer_color, contents)

# Recursively get 
def get_bag_count(content_dict, quantity, color):
  if len(content_dict[color]) == 0:
    return quantity
  return quantity * (sum([get_bag_count(content_dict, q, c) for q, c in content_dict[color]]) + 1)

def part1(lines):
  # Build a map from bags to other bags that can hold them
  inner_to_outer = defaultdict(set)
  for line in lines:
    outer = line[0]
    inners = [inner for qty, inner in line[1]]
    for inner in inners:
      inner_to_outer[inner].add(outer)

  # Assuming no infinite loops, add all outer colors to set.
  inner_queue = deque(['shiny gold'])
  seen = set()
  while inner_queue:
    curr = inner_queue.popleft()
    for color in inner_to_outer[curr]:
      if color in seen:
        continue
      inner_queue.append(color)
      seen.add(color)

  return len(seen)

def part2(lines):
  content_dict = {line[0]: line[1] for line in lines}
  return get_bag_count(content_dict, 1, 'shiny gold') - 1


with open("c:\\users\\sebbe\\desktop\\aoc\\2020\\7\\input.txt") as f:
  lines = [parse(line.rstrip()) for line in f.readlines()]
  print(part1(lines))
  print(part2(lines))