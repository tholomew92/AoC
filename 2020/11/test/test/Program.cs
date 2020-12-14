using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace DayEleven
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Day Eleven Challenge! Advent of Code");
            List<string> rows = GetFileValues(@"C:\Users\Sebbe\Desktop\aoc\2020\11\input.txt");

            string SampleInput = @"L.LL.LL.LL
LLLLLLL.LL
L.L.L..L..
LLLL.LL.LL
L.LL.LL.LL
L.LLLLL.LL
..L.L.....
LLLLLLLLLL
L.LLLLLL.L
L.LLLLL.LL";


            List<string> SampleRows = SampleInput.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None).ToList();



            char[][] seatingarrangement = new char[rows.Count][];

            int row = 0;
            foreach (var column in rows)
            {
                seatingarrangement[row] = new char[column.Length];
                int pos = 0;
                foreach (var thing in column)
                {
                    seatingarrangement[row][pos] = thing;
                    pos++;
                }
                row++;
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();


            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var resultPartOne = PartOne(seatingarrangement);

            stopwatch.Stop();


            Stopwatch stopwatch2 = new Stopwatch();
            stopwatch2.Start();
            var resultPartTwo = PartTwo(seatingarrangement);

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


        public static int PartOne(char[][] seatingarrangement)
        {
            Console.WriteLine("PartOne Start");
            bool doCOntinue = true;

            int itteration = 0;
            while (doCOntinue)
            {
                Console.WriteLine("Itteration: " + itteration);
                var result = PartOneSeatParser(seatingarrangement);

                if (!result.registeredSeatChange)
                {
                    int count = 0;
                    //count taken seats!
                    for (int outer = 0; outer < result.newseatingarrangement.Length; outer++)
                    {
                        for (int inner = 0; inner < result.newseatingarrangement[outer].Length; inner++)
                        {
                            if (result.newseatingarrangement[outer][inner] == '#')
                                count++;

                        }
                    }

                    return count;
                }
                else
                    seatingarrangement = result.newseatingarrangement;

                itteration++;
            }



            //return seats taken

            return -1;

        }


        public static (bool registeredSeatChange, char[][] newseatingarrangement) PartOneSeatParser(char[][] seatingarrangement)
        {
            var CurrentItterationSeatingArrangement = JsonConvert.DeserializeObject<char[][]>(JsonConvert.SerializeObject(seatingarrangement)); //craete copy
            bool registeredSeatChange = false;

            for (int currentRow = 0; currentRow < seatingarrangement.Length; currentRow++)
            {

                for (int currentSeat = 0; currentSeat < seatingarrangement[currentRow].Length; currentSeat++)
                {
                    //is current seat empty or taken?
                    //  then check the closest seata

                    if (seatingarrangement[currentRow][currentSeat] == '.')
                        continue; //is floor, leave empty!

                    int countRelatedSeatsTaken = 0;

                    bool currentSeatIsTaken = false;
                    //check current seat
                    if (seatingarrangement[currentRow][currentSeat] == '#')
                        currentSeatIsTaken = true;


                    //check seat to the left - is the currentSeat -x outside the array?
                    if (currentSeat - 1 >= 0)
                    {
                        if (seatingarrangement[currentRow][currentSeat - 1] == '#')
                            countRelatedSeatsTaken++;
                    }

                    //chekc the seat to the right - is the currentSeat +x outside the array?
                    if (currentSeat + 1 < seatingarrangement[currentRow].Length)
                    {
                        if (seatingarrangement[currentRow][currentSeat + 1] == '#')
                            countRelatedSeatsTaken++;
                    }


                    //check the seat one up - outside?
                    if (currentRow - 1 >= 0)
                    {
                        if (seatingarrangement[currentRow - 1][currentSeat] == '#')
                            countRelatedSeatsTaken++;
                    }

                    //check the seat one down
                    if (currentRow + 1 < seatingarrangement.Length)
                    {
                        if (seatingarrangement[currentRow + 1][currentSeat] == '#')
                            countRelatedSeatsTaken++;
                    }





                    //check diagonally - 4 checks
                    //check seat to the agonally up left - is the currentSeat -x outside the array?
                    if (currentSeat - 1 >= 0 && currentRow - 1 >= 0)
                    {
                        if (seatingarrangement[currentRow - 1][currentSeat - 1] == '#')
                            countRelatedSeatsTaken++;
                    }

                    //chekc the seat diagonally up to the right - is the currentSeat +x outside the array?
                    if (currentRow - 1 >= 0 && currentSeat + 1 < seatingarrangement[currentRow - 1].Length)
                    {
                        if (seatingarrangement[currentRow - 1][currentSeat + 1] == '#')
                            countRelatedSeatsTaken++;
                    }


                    //check the seat diagonally down left - outside?
                    if (currentRow + 1 < seatingarrangement.Length && currentSeat - 1 >= 0)
                    {
                        if (seatingarrangement[currentRow + 1][currentSeat - 1] == '#')
                            countRelatedSeatsTaken++;
                    }

                    //check the seat diagonally down right 
                    if (currentRow + 1 < seatingarrangement.Length && currentSeat + 1 < seatingarrangement[currentRow + 1].Length)
                    {
                        if (seatingarrangement[currentRow + 1][currentSeat + 1] == '#')
                            countRelatedSeatsTaken++;
                    }



                    if (currentSeatIsTaken)
                    {
                        if (countRelatedSeatsTaken >= 4)
                        {
                            CurrentItterationSeatingArrangement[currentRow][currentSeat] = 'L';
                            //Console.WriteLine("Set Seat; " + currentRow + "," + currentSeat + " as empty");
                            registeredSeatChange = true;
                        }

                    }
                    else
                    {
                        if (countRelatedSeatsTaken == 0)
                        {
                            CurrentItterationSeatingArrangement[currentRow][currentSeat] = '#';
                            //Console.WriteLine("Set Seat; " + currentRow + "," + currentSeat + " as taken");
                            registeredSeatChange = true;
                        }
                    }


                }






            }



            //we reurn if a seat has been changed!
            return (registeredSeatChange, CurrentItterationSeatingArrangement);
        }


        #endregion



        #region PartTwo



        public static int PartTwo(char[][] seatingarrangement)
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("PartTwo Start");
            bool doCOntinue = true;

            Int64 ItterationCount = 0;

            while (doCOntinue)
            {
                var result = PartTwoSeatParser(seatingarrangement);

                Console.WriteLine("Itteration: " + ItterationCount);
                //for (int outer = 0; outer < result.newseatingarrangement.Length; outer++)
                //{
                //    Console.WriteLine();
                //    for (int inner = 0; inner < result.newseatingarrangement[outer].Length; inner++)
                //        Console.Write(result.newseatingarrangement[outer][inner]);
                //}

                //Console.WriteLine();
                //Console.WriteLine();
                Console.WriteLine();

                if (!result.registeredSeatChange)
                {
                    int count = 0;
                    //count taken seats!
                    for (int outer = 0; outer < result.newseatingarrangement.Length; outer++)
                    {
                        //Console.WriteLine();
                        for (int inner = 0; inner < result.newseatingarrangement[outer].Length; inner++)
                        {
                            //Console.Write(result.newseatingarrangement[outer][inner]);
                            if (result.newseatingarrangement[outer][inner] == '#')
                                count++;

                        }
                    }


                    return count;
                }
                else
                    seatingarrangement = result.newseatingarrangement;

                ItterationCount++;
            }



            //return seats taken

            return -1;

        }


        public static (bool registeredSeatChange, char[][] newseatingarrangement) PartTwoSeatParser(char[][] seatingarrangement)
        {
            var CurrentItterationSeatingArrangement = JsonConvert.DeserializeObject<char[][]>(JsonConvert.SerializeObject(seatingarrangement)); //craete copy
            bool registeredSeatChange = false;

            for (int currentRow = 0; currentRow < seatingarrangement.Length; currentRow++)
            {

                for (int currentSeat = 0; currentSeat < seatingarrangement[currentRow].Length; currentSeat++)
                {
                    //is current seat empty or taken?
                    //  then check the closest seata

                    if (seatingarrangement[currentRow][currentSeat] == '.')
                        continue; //is floor, leave empty!

                    int countRelatedSeatsTaken = 0;

                    bool currentSeatIsTaken = false;
                    //check current seat
                    if (seatingarrangement[currentRow][currentSeat] == '#')
                        currentSeatIsTaken = true;

                    //check seat to the left - is the currentSeat -x outside the array?
                    countRelatedSeatsTaken += SeatToTheLeft(seatingarrangement, currentRow, currentSeat);

                    //chekc the seat to the right - is the currentSeat +x outside the array?
                    countRelatedSeatsTaken += SeatToTheRight(seatingarrangement, currentRow, currentSeat);


                    //check the seat one up - outside?
                    countRelatedSeatsTaken += SeatToTheUp(seatingarrangement, currentRow, currentSeat);

                    //check the seat one down
                    countRelatedSeatsTaken += SeatToTheDown(seatingarrangement, currentRow, currentSeat);





                    //check diagonally - 4 checks
                    //check seat to the agonally up left - is the currentSeat -x outside the array?
                    countRelatedSeatsTaken += SeatDiagonallyUpLeft(seatingarrangement, currentRow, currentSeat);

                    //chekc the seat diagonally up to the right - is the currentSeat +x outside the array?
                    countRelatedSeatsTaken += SeatDiagonallyUpRight(seatingarrangement, currentRow, currentSeat);


                    //check the seat diagonally down left - outside?
                    countRelatedSeatsTaken += SeatDiagonallyDownLeft(seatingarrangement, currentRow, currentSeat);

                    //check the seat diagonally down right 
                    countRelatedSeatsTaken += SeatDiagonallyDownRight(seatingarrangement, currentRow, currentSeat);


                    if (currentSeatIsTaken)
                    {
                        if (countRelatedSeatsTaken >= 5)
                        {
                            CurrentItterationSeatingArrangement[currentRow][currentSeat] = 'L';
                            registeredSeatChange = true;
                        }

                    }
                    else
                    {
                        if (countRelatedSeatsTaken == 0)
                        {
                            CurrentItterationSeatingArrangement[currentRow][currentSeat] = '#';
                            registeredSeatChange = true;
                        }
                    }


                }






            }



            //we reurn if a seat has been changed!
            return (registeredSeatChange, CurrentItterationSeatingArrangement);
        }

        private static int SeatDiagonallyDownRight(char[][] seatingarrangement, int currentRow, int currentSeat)
        {
            if (currentRow + 1 < seatingarrangement.Length && currentSeat + 1 < seatingarrangement[currentRow + 1].Length)
            {
                if (seatingarrangement[currentRow + 1][currentSeat + 1] == 'L')
                    return 0;
                else if (seatingarrangement[currentRow + 1][currentSeat + 1] == '#')
                    return 1;
                else if (seatingarrangement[currentRow + 1][currentSeat + 1] == '.')
                    return SeatDiagonallyDownRight(seatingarrangement, currentRow + 1, currentSeat + 1);
            }
            return 0;
        }

        private static int SeatDiagonallyDownLeft(char[][] seatingarrangement, int currentRow, int currentSeat)
        {
            if (currentRow + 1 < seatingarrangement.Length && currentSeat - 1 >= 0)
            {
                if (seatingarrangement[currentRow + 1][currentSeat - 1] == 'L')
                    return 0;
                else if (seatingarrangement[currentRow + 1][currentSeat - 1] == '#')
                    return 1;
                else if (seatingarrangement[currentRow + 1][currentSeat - 1] == '.')
                    return SeatDiagonallyDownLeft(seatingarrangement, currentRow + 1, currentSeat - 1);
            }
            return 0;
        }

        private static int SeatDiagonallyUpRight(char[][] seatingarrangement, int currentRow, int currentSeat)
        {
            if (currentRow - 1 >= 0 && currentSeat + 1 < seatingarrangement[currentRow - 1].Length)
            {
                if (seatingarrangement[currentRow - 1][currentSeat + 1] == 'L')
                    return 0;
                else if (seatingarrangement[currentRow - 1][currentSeat + 1] == '#')
                    return 1;
                else if (seatingarrangement[currentRow - 1][currentSeat + 1] == '.')
                    return SeatDiagonallyUpRight(seatingarrangement, currentRow - 1, currentSeat + 1);
            }
            return 0;
        }

        private static int SeatDiagonallyUpLeft(char[][] seatingarrangement, int currentRow, int currentSeat)
        {
            if (currentSeat - 1 >= 0 && currentRow - 1 >= 0)
            {
                if (seatingarrangement[currentRow - 1][currentSeat - 1] == 'L')
                    return 0;
                else if (seatingarrangement[currentRow - 1][currentSeat - 1] == '#')
                    return 1;
                else if (seatingarrangement[currentRow - 1][currentSeat - 1] == '.')
                    return SeatDiagonallyUpLeft(seatingarrangement, currentRow - 1, currentSeat - 1);
            }
            return 0;
        }

        private static int SeatToTheDown(char[][] seatingarrangement, int currentRow, int currentSeat)
        {
            if (currentRow + 1 < seatingarrangement.Length)
            {
                if (seatingarrangement[currentRow + 1][currentSeat] == 'L')
                    return 0;
                else if (seatingarrangement[currentRow + 1][currentSeat] == '#')
                    return 1;
                else if (seatingarrangement[currentRow + 1][currentSeat] == '.')
                    return SeatToTheDown(seatingarrangement, currentRow + 1, currentSeat);
            }
            return 0;
        }

        private static int SeatToTheUp(char[][] seatingarrangement, int currentRow, int currentSeat)
        {
            if (currentRow - 1 >= 0)
            {
                if (seatingarrangement[currentRow - 1][currentSeat] == 'L')
                    return 0;
                else if (seatingarrangement[currentRow - 1][currentSeat] == '#')
                    return 1;
                else if (seatingarrangement[currentRow - 1][currentSeat] == '.')
                    return SeatToTheUp(seatingarrangement, currentRow - 1, currentSeat);
            }
            return 0;
        }

        private static int SeatToTheRight(char[][] seatingarrangement, int currentRow, int currentSeat)
        {
            if (currentSeat + 1 < seatingarrangement[currentRow].Length)
            {
                if (seatingarrangement[currentRow][currentSeat + 1] == 'L')
                    return 0;
                else if (seatingarrangement[currentRow][currentSeat + 1] == '#')
                    return 1;
                else if (seatingarrangement[currentRow][currentSeat + 1] == '.')
                    return SeatToTheRight(seatingarrangement, currentRow, currentSeat + 1);
            }
            return 0;
        }

        private static int SeatToTheLeft(char[][] seatingarrangement, int currentRow, int currentSeat)
        {
            if (currentSeat - 1 >= 0)
            {
                if (seatingarrangement[currentRow][currentSeat - 1] == 'L')
                    return 0;
                else if (seatingarrangement[currentRow][currentSeat - 1] == '#')
                    return 1;
                else if (seatingarrangement[currentRow][currentSeat - 1] == '.')
                    return SeatToTheLeft(seatingarrangement, currentRow, currentSeat - 1);
            }
            return 0;
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
}
