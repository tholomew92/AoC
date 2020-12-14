using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;

namespace DayTwelve
{
    class DayTwelveChallenge
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Day Twelve Challenge! Advent of Code");
            List<string> rows = GetFileValues(@"C:\Users\Sebbe\Desktop\aoc\2020\12\input.txt");

            string SampleInput = @"F10
N3
F7
R90
F11";


            List<string> SampleRows = SampleInput.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None).ToList();


            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();


            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var resultPartOne = PartOne(rows);

            stopwatch.Stop();


            Stopwatch stopwatch2 = new Stopwatch();
            stopwatch2.Start();
            var resultPartTwo = PartTwo(rows);

            stopwatch2.Stop();

            Console.WriteLine();
            Console.WriteLine("------------------");
            Console.WriteLine("PartOneOutput: " + resultPartOne);
            Console.WriteLine("------------------");
            Console.WriteLine();

            Console.WriteLine("------------------");
            Console.WriteLine("PartTwo output: " + resultPartTwo);
            Console.WriteLine("------------------");
            Console.WriteLine();
            Console.WriteLine("Runtime Part One: (--) " + stopwatch.Elapsed.ToString());
            Console.WriteLine("Runtime Part One: (ms) " + stopwatch.ElapsedMilliseconds.ToString());

            Console.WriteLine("Runtime Part Two: (--) " + stopwatch2.Elapsed.ToString());
            Console.WriteLine("Runtime Part Two: (ms) " + stopwatch2.ElapsedMilliseconds.ToString());
        }







        #region PartOne


        public static int PartOne(List<string> Input, int startDegrees = 90)
        {

            var pointCloud = new List<PointAndDirection>();
            pointCloud.Add(new PointAndDirection { X = 0, Y = 0, Orientation = startDegrees });
            int position = 0;


            for (int i = 0; i < Input.Count; i++)
            {
                var command = Input[i].Substring(0, 1);
                int unit = int.Parse(Input[i].Substring(1, Input[i].Length - 1));

                if (command.Equals("F"))
                {
                    var currOrientation = pointCloud[position].Orientation;

                    if (currOrientation == 0)
                        command = "N";
                    else if (currOrientation == 90)
                        command = "E";
                    else if (currOrientation == 180)
                        command = "S";
                    else if (currOrientation == 270)
                        command = "W";

                }



                if (command.Equals("R") || command.Equals("L"))
                {
                    if (command.Equals("R"))
                    {
                        var newOrientation = pointCloud[position].Orientation + unit;
                        if (newOrientation >= 360)
                            newOrientation = newOrientation - 360;

                        pointCloud.Add(new PointAndDirection { X = pointCloud[position].X, Y = pointCloud[position].Y, Orientation = newOrientation });
                        position++;
                    }
                    else
                    {
                        var newOrientation = pointCloud[position].Orientation - unit;
                        if (newOrientation < 0)
                            newOrientation = newOrientation + 360;

                        pointCloud.Add(new PointAndDirection { X = pointCloud[position].X, Y = pointCloud[position].Y, Orientation = newOrientation });
                        position++;

                    }

                }
                else
                {
                    if (command.Equals("N"))
                    {
                        var newY = pointCloud[position].Y + unit;

                        pointCloud.Add(new PointAndDirection { X = pointCloud[position].X, Y = newY, Orientation = pointCloud[position].Orientation });
                        position++;

                    }
                    else if (command.Equals("E"))
                    {
                        var newX = pointCloud[position].X + unit;

                        pointCloud.Add(new PointAndDirection { X = newX, Y = pointCloud[position].Y, Orientation = pointCloud[position].Orientation });
                        position++;
                    }
                    else if (command.Equals("S"))
                    {
                        var newY = pointCloud[position].Y - unit;

                        pointCloud.Add(new PointAndDirection { X = pointCloud[position].X, Y = newY, Orientation = pointCloud[position].Orientation });
                        position++;
                    }
                    else if (command.Equals("W"))
                    {
                        var newX = pointCloud[position].X - unit;

                        pointCloud.Add(new PointAndDirection { X = newX, Y = pointCloud[position].Y, Orientation = pointCloud[position].Orientation });
                        position++;
                    }


                }



            }


            //all Done, lets math!

            var x = Math.Abs(pointCloud[position].X);
            var y = Math.Abs(pointCloud[position].Y);

            var result = y + x;


            return result;
        }




        #endregion





        #region PartTwo


        public static int PartTwo(List<string> Input, int startDegrees = 90)
        {

            var pointCloud = new List<PointAndDirection>();
            pointCloud.Add(new PointAndDirection { X = 0, Y = 0, Orientation = startDegrees });
            int position = 0;

            var relativeWaypointPosition = new PointAndDirection { X = 10, Y = 1 };


            for (int i = 0; i < Input.Count; i++)
            {
                var command = Input[i].Substring(0, 1);
                int unit = int.Parse(Input[i].Substring(1, Input[i].Length - 1));

                if (command.Equals("F"))
                {
                    var newX = pointCloud[position].X + (relativeWaypointPosition.X * unit);
                    var newY = pointCloud[position].Y + (relativeWaypointPosition.Y * unit);

                    pointCloud.Add(new PointAndDirection { X = newX, Y = newY });
                    position++;

                }
                else if (command.Equals("R") || command.Equals("L"))
                {

                    int newX = 0;
                    int newY = 0;
                    for (var roteate = 0; roteate < unit / 90; roteate++)
                    {
                        if (command.Equals("R"))
                        {
                            newY = relativeWaypointPosition.X * -1;
                            newX = relativeWaypointPosition.Y;
                        }
                        else
                        {
                            newY = relativeWaypointPosition.X;
                            newX = relativeWaypointPosition.Y * -1;
                        }

                        relativeWaypointPosition.X = newX;
                        relativeWaypointPosition.Y = newY;
                    }

                }
                else
                {
                    if (command.Equals("N"))
                    {
                        var newY = relativeWaypointPosition.Y + unit;
                        relativeWaypointPosition.Y = newY;
                    }
                    else if (command.Equals("E"))
                    {
                        var newX = relativeWaypointPosition.X + unit;
                        relativeWaypointPosition.X = newX;
                    }
                    else if (command.Equals("S"))
                    {
                        var newY = relativeWaypointPosition.Y - unit;
                        relativeWaypointPosition.Y = newY;
                    }
                    else if (command.Equals("W"))
                    {
                        var newX = relativeWaypointPosition.X - unit;
                        relativeWaypointPosition.X = newX;
                    }


                }



            }


            //all Done, lets math!

            var x = Math.Abs(pointCloud[position].X);
            var y = Math.Abs(pointCloud[position].Y);

            var result = y + x;


            return result;
        }










        #endregion


















        #region Support


        public static List<string> GetFileValues(string path)
        {
            List<string> resultList = new List<string>();
            FileStream fileStream = new FileStream(path, FileMode.Open);
            using (StreamReader reader = new StreamReader(fileStream))
            {
                while (!reader.EndOfStream)
                {
                    List<int> rowData = new List<int>();
                    string line = reader.ReadLine();
                    resultList.Add(line);
                }
            }
            return resultList;

        }


        #endregion




    }


    public class PointAndDirection
    {

        public int X { get; set; }

        public int Y { get; set; }

        public int Orientation { get; set; }

    }
}
