using System;
using System.IO;

namespace AOC2
{
    class Program
    {
        /*  Constants   */
        public const int GOAL_VAL = 19690720;
        public const int BUFF_SIZE = 1024;

        class OpCode
        {
            public const int OPLENGTH = 4;
            public const int ADD = 1;
            public const int MULT = 2;
            public const int END = 99;
        }

        static void Main(string[] args)
        {

            string[] values = CsvToArray("AOCInput2.txt");
            int[] opcodes = StrToInt(values);
            

            if(values != null && opcodes != null)
            {
                /*  Part one    */
                RunPartOne(opcodes);

                /* Part Two */
                RunPartTwo(opcodes);
            }
            else
            {
                Console.WriteLine("Unable to run program.");
            }




        }

        private static void RunPartOne(int[] opcodes)
        {
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine($"1. Value at 0 is: {RunProgram(opcodes, 12, 2)}");
            Console.WriteLine("------------------------------------------------------------");
        }

        private static void RunPartTwo(int[] opcodes)
        {
            bool foundVal = false;

            for (int noun = 0; noun < 100; noun++)
            {
                for (int verb = 0; verb < 100; verb++)
                {
                    if (RunProgram(opcodes, noun, verb) == GOAL_VAL)
                    {
                        foundVal = true;
                        Console.WriteLine($"2. Noun: {noun}, Verb: {verb} where (100 * noun + verb) = {100 * noun + verb}");
                        break;
                    }
                }
            }

            if (!foundVal)
            {
                Console.WriteLine($"No value found that corresponds to {GOAL_VAL}");
            }
            Console.WriteLine("------------------------------------------------------------");
        }


        private static int RunProgram(int[] indata, int noun, int verb)
        {
            int PC = 0; //ProgramCounter for opcodes
            int[] ops = new int[indata.Length];

            /*  Copies values for to temp array */
            for (int i = 0; i < indata.Length; i++) ops[i] = indata[i]; 

            /*  Adds noun and verb on correct spot in instructions */
            ops[1] = noun; 
            ops[2] = verb;


            /*  Runs instructions as long as opcodes are accepted by runOP   */
            while (runOP(PC, ops))
            {
                PC += OpCode.OPLENGTH;
            }

            return ops[0];
        }

        private static bool runOP(int PC, int[] opArr)
        {
            int opOneIndex = opArr[PC + 1];
            int oPTwoIndex = opArr[PC + 2];
            int destIndex = opArr[PC + 3];

            switch(opArr[PC])
            {
                case OpCode.ADD:
                    opArr[destIndex] = opArr[opOneIndex] + opArr[oPTwoIndex];
                    return true;

                case OpCode.MULT:
                    opArr[destIndex] = opArr[opOneIndex] * opArr[oPTwoIndex];
                    return true;

                /*  For troubleshooting/extension   */
                case OpCode.END: 
                    return false;

                /*  catches all "unknown op codes"  */
                default:
                    return false;
            }

        }



        private static int[] StrToInt(string[] stringCodes)
        {
            int[] intArr = new int[stringCodes.Length];

            for(int i = 0; i<stringCodes.Length; i++)
            {
                intArr[i] = Int32.Parse(stringCodes[i]);
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
                    string[] characters = new string[BUFF_SIZE];

                    while((read = sr.ReadLine()) != null)
                    {
                        characters = read.Split(",");
                    }
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

