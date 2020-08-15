using System;
using System.IO;

namespace AOC2
{
    class Program
    {
        static void Main(string[] args)
        {

            string[] values = CsvToArray("AOCInput2.txt");
            int[] opcodes = StrToInt(values);

            RunProgram(opcodes);

        }


        private static void RunProgram(int[] ops)
        {
            int opLength = 4;
            int PC = 0;

            Console.WriteLine("Hello from RunProgram");

            while(ops[PC] != 99)
            {
                runOP(PC, ops);
                PC += opLength;
            }
            



            
        }

        private static int runOP(int PC, int[] opArr)
        {
            switch(opArr[PC])
            {
                case 1:
                    Console.WriteLine("Instruction one");
                    opArr[(PC + 3)] = opArr[PC + 1] + opArr[PC + 2];
                    return 1;

                case 2:
                    Console.WriteLine("Instruction Two");
                    opArr[(PC + 3)] = opArr[PC + 1] * opArr[PC + 2];
                    return 2;

                case 99:
                    Console.WriteLine("Instruction END");
                    return 99;

                default:
                    return -1;
            }

        }


        private static void WriteValue(int[] opArr, int index, int value)
        {
            opArr[index] = value;
        }

        private static void Addition(int[] opArr, int index1, int index2, int resultIndex)
        {

        }

        private static void Multiplication()
        {

        }

        private static int[] StrToInt(string[] arr)
        {
            int[] intArr = new int[arr.Length];

            for(int i = 0; i<arr.Length; i++)
            {
                intArr[i] = Int32.Parse(arr[i]);
                Console.WriteLine(arr[i]);
            }

            return intArr;
        }

        /*  Extracts values from a CSV file, removes commas and return an array of values */
        private static string[] CsvToArray(string filename)
        {

            try
            {
                using (StreamReader sr = new StreamReader(filename))
                {
                    string read;
                    string[] characters = new string[1024];

                    while((read = sr.ReadLine()) != null)
                    {
                        characters = read.Split(",");
                    }


                    //VAD HÄNDER OM MER ÄN 1024 tecken?????????+
                    return characters;
                }

                    
            }

            catch(IOException e)
            {
                Console.WriteLine("Unable to read file");
                Console.WriteLine($"{e}");

                return null;
            }
        }
    }

    
}

