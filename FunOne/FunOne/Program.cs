using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace FunOne
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputFile = "AOCInput1.txt";
            int result = 0;
            Console.WriteLine("Attempting to parse mass file..");
            Console.WriteLine("---------------------------------------------------");

            if ((result = ParseMassFile(inputFile)) > 0)
            {
                Console.WriteLine("---------------------------------------------------");
                Console.WriteLine($"Total fuel needed: {result}");
            }
            else Console.WriteLine("Unable to calculate fuel needed");
            

        }

        public static int ParseMassFile(string fileName)
        {
            int totalFuel = 0;

            try
            {
                using (var sr = new StreamReader(fileName))
                {
                    string module;
                    int fuel;

                    //Parses one module at a time and calculates fuel needed
                    while ((module = sr.ReadLine()) != null)
                    {
                        Console.WriteLine($"Calculating fuel for module mass: {module}");
                        fuel = AllMassToFuel(Int32.Parse(module));
                        totalFuel += fuel;
                    }
                }

            }

            //Catches any errors while opening file
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read");
                Console.WriteLine(e.Message);
                totalFuel = 0;
            }

            return totalFuel;

        }

        /*  Calculates total fuel needed based on all mass  */
        public static int AllMassToFuel(int startMass)
        {
            int totalFuel = 0, fuel = startMass;

            //Magic number 6. Everything below will give a negative value
            while(fuel > 6)
            {
                fuel = MassToFuel(fuel);
                totalFuel += fuel;
            }

            return totalFuel;
        }

        /*  Calculates fuel needed based on mass */
        public static int MassToFuel(int mass)
        {
            return (mass / 3) - 2;
        }
    }



}
