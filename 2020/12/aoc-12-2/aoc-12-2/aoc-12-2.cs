﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace aoc_12_2
{
    class Program
    {
        static string workdir = Environment.CurrentDirectory;
        static string inputPath = new DirectoryInfo(workdir).Parent.Parent.Parent.Parent.Parent.ToString();
        List<string> input = File.ReadAllLines(inputPath + "\\input.txt").ToList();
        //List<string> input = File.ReadAllLines(inputPath + "\\test.txt").ToList();
        List<Point> path = new List<Point>();

        void FollowPath()
        {
            int wpx = 10;
            int wpy = 1;
            int shpx, shpy;
            shpx = shpy = 0;
            foreach (var row in input)
            {
                char direction = row[0];
                switch (direction)
                {
                    case 'F':
                        shpx += wpx * int.Parse(row[1..]);
                        shpy += wpy * int.Parse(row[1..]);
                        Point p = new Point { x = shpx, y = shpy };
                        path.Add(p);
                        break;
                    case 'N':
                        wpy += int.Parse(row[1..]);
                        break;
                    case 'S':
                        wpy -= int.Parse(row[1..]);
                        break;
                    case 'W':
                        wpx -= int.Parse(row[1..]);
                        break;
                    case 'E':
                        wpx += int.Parse(row[1..]);
                        break;
                    case 'R':
                        int rturn = int.Parse(row[1..]) / 90;
                        for (int i = 0; i < rturn; i++)
                        {
                            int oldx = wpx;
                            wpx = wpy;
                            wpy = -oldx;
                        }
                        break;
                    case 'L':
                        int lturn = int.Parse(row[1..]) / 90;
                        for (int i = 0; i < lturn; i++)
                        {
                            int oldx = wpx;
                            wpx = -wpy;
                            wpy = oldx;
                        }
                        break;
                }
                

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
