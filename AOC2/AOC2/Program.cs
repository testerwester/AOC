using System;
using System.IO;

namespace AOC2
{
    class Program
    {
        /*  Constant used for Program counter   */
        public const int OPLENGTH = 4;

        static void Main(string[] args)
        {

            string[] values = CsvToArray("AOCInput2.txt");
            int[] opcodes = StrToInt(values);

            /*  Manually change index 1 and 2 according to task description */
            opcodes[1] = 12; 
            opcodes[2] = 2;
            Console.WriteLine("----------------------------------------------------");
            RunProgram(opcodes);

             
            

        }


        private static void RunProgram(int[] ops)
        {
            int PC = 0; //ProgramCounter for opcodes

            /*  Runs instructions as long as opcodes are accepted by runOP   */
            while(runOP(PC, ops))
            {
                PC += OPLENGTH;
            }

            /*  End output to test program */
            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine($"Value at position 0 is: {ops[0]}");
            Console.WriteLine("----------------------------------------------------");
        }

        private static bool runOP(int PC, int[] opArr)
        {
            int opOneIndex = opArr[PC + 1];
            int oPTwoIndex = opArr[PC + 2];
            int destIndex = opArr[PC + 3];

            Console.Write($"PC {PC}: ");

            switch(opArr[PC])
            {
                case 1:
                    Console.WriteLine($"At index {opArr[(PC + 3)]} setting {opArr[(PC + 1)]} + {opArr[(PC + 2)]}");
                    opArr[destIndex] = opArr[opOneIndex] + opArr[oPTwoIndex];
                    return true;

                case 2:
                    Console.WriteLine($"At index {opArr[(PC + 3)]} setting {opArr[(PC + 1)]} * {opArr[(PC + 2)]}");
                    opArr[destIndex] = opArr[opOneIndex] * opArr[oPTwoIndex];
                    return true;

                case 99:
                    Console.WriteLine($"Instruction END");
                    return false;

                default:
                    Console.WriteLine($"Unknown OP code");
                    return false;
            }

        }



        private static int[] StrToInt(string[] arr)
        {
            int[] intArr = new int[arr.Length];

            for(int i = 0; i<arr.Length; i++)
            {
                intArr[i] = Int32.Parse(arr[i]);
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
                    //Bygg någon kontrollfunktion
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

