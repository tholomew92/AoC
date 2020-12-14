using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace aoc_12_1
{
    class Program
    {
        //List<string> input = File.ReadAllLines(@"C:\Users\Sebbe\Desktop\aoc\2020\12\test.txt").ToList();
        List<string> input = File.ReadAllLines(@"C:\Users\Sebbe\Desktop\aoc\2020\12\input.txt").ToList();
        char[] directions = new char[] { 'N', 'E', 'S', 'W' };
        List<Point> path = new List<Point>();

        void FollowPath()
        {
            int x = 0;
            int y = 0;
            int facingDir = 1;
            foreach(var row in input)
            {
                char direction = ' ';
                if (row[0] == 'F')
                    direction = directions[facingDir];
                else direction = row[0];
                switch (direction)
                {
                    case 'N':
                        y += int.Parse(row[1..]);
                        break;
                    case 'S':
                        y -= int.Parse(row[1..]);
                        break;
                    case 'W':
                        x -= int.Parse(row[1..]);
                        break;
                    case 'E':
                        x += int.Parse(row[1..]);
                        break;
                    case 'R':
                        int rturn = int.Parse(row[1..]) / 90;
                        if (facingDir + rturn > 3) facingDir -= 4;
                        facingDir += rturn;
                        break;
                    case 'L':
                        int lturn = int.Parse(row[1..]) / 90;
                        if (facingDir - lturn < 0) facingDir += 4;
                        facingDir -= lturn;
                        break;
                }
                Point p = new Point { x = x, y = y };
                path.Add(p);
            }
            Console.WriteLine(path[path.Count-1].PosFromOrigo());
        }

        static void Main(string[] args)
        {
            var watch = new System.Diagnostics.Stopwatch();
            Program p = new Program();
            watch.Start();
            p.FollowPath();
            watch.Stop();
            Console.WriteLine(watch.ElapsedMilliseconds.ToString());
        }
    }


    class Point
    {
        public int x { get; set; }
        public int y { get; set; }

        public bool Equals(Point other)
        {
            if (other == null)
                return false;

            if (this.x == other.x & this.y == other.y)
                return true;
            else
                return false;
        }

        public int PosFromOrigo()
        {
            if (Math.Abs(x) + Math.Abs(y) > 0) return Math.Abs(x) + Math.Abs(y);
            return int.MaxValue;
        }

        public override string ToString()
        {
            return x + "," + y;
        }

        public override bool Equals(Object obj)
        {
            if (obj == null) return false;

            Point pointObj = obj as Point;
            if (pointObj == null) return false;
            else return Equals(pointObj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(x, y);
        }

        public static bool operator ==(Point p1, Point p2)
        {
            if (((object)p1) == null || ((object)p2) == null)
                return Object.Equals(p1, p2);

            return p1.Equals(p2);
        }

        public static bool operator !=(Point p1, Point p2)
        {
            if (((object)p1) == null || ((object)p2) == null)
                return !Object.Equals(p1, p2);

            return !(p1.Equals(p2));
        }
    }
}
