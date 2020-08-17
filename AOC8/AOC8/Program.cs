using System;
using System.IO;

namespace AOC8
{

   
    class Program
    {
        /*  Constants   */
        public const string IMAGE_PATH = "AOC8Input.txt";
        public const int BUFF_SIZE = 15000;



        public class Layer
        {
            /*  Constants   */
            public const int IMG_WIDTH = 25;
            public const int IMG_HEIGHT = 6;
            /*  Variables   */
            public int[,] codes;

            /*  Creates an empty 2d array based */
            public Layer()
            {
                codes = new int[IMG_HEIGHT, IMG_WIDTH];
            }

            /*  When a specific value should be written to all spots    */
            public Layer(int input)
            {
                codes = new int[IMG_HEIGHT, IMG_WIDTH];
                for(int i = 0; i < IMG_HEIGHT; i++)
                {
                    for(int j = 0; j < IMG_WIDTH; j++)
                    {
                        codes[i, j] = input;
                    }
                }
            }


            public int CalculateOccurances(int numToCount)
            {
                int i, j, counter = 0;

                for(i = 0; i<IMG_HEIGHT; i++)
                {
                    for(j = 0; j<IMG_WIDTH; j++)
                    {
                        if (codes[i, j] == numToCount) counter++;
                    }
                }
                return counter;
            }

            public void PrintImage()
            {
                int column, row;

                for(column = 0; column < IMG_HEIGHT; column++)
                {
                    for(row = 0; row < IMG_WIDTH; row++)
                    {
                        if (codes[column, row] != 0) Console.Write($"{codes[column, row]}");
                        else Console.Write(" ");
                    }
                    Console.WriteLine("");
                }

            }
            
        }


        static void Main(string[] args)
        {
            int checkIndex;
            Layer[] layers;
            char[] rawData = new char[BUFF_SIZE];

            if (ReadImage(rawData) != null)
            {
                /*  Part one    */
                layers = PopulateLayers(rawData);
                checkIndex = FindCheckLayer(layers);

                Console.WriteLine($"1. The value of 1's * 2's is: " +
                    $"{(layers[checkIndex].CalculateOccurances(1) * layers[checkIndex].CalculateOccurances(2))}");

                /*  Part Two    */
                Layer endLayer = new Layer(2);
                ComposeLayers(endLayer, layers);
                Console.WriteLine("2. BIOS Password:");
                endLayer.PrintImage();

            }
        }

        public static void ComposeLayers(Layer endLayer, Layer[] layers)
        {
            int numLayers = layers.Length;
            int i, row, column;

            for(i = 0; i<numLayers; i++)
            {
                for(column = 0; column < Layer.IMG_HEIGHT; column++)
                {
                    for(row = 0; row < Layer.IMG_WIDTH; row++)
                    {
                        if(endLayer.codes[column, row] == 2)
                        {
                            /*  More efficient to write than an extra if statement? ( ..&& layers[i].codes[column, row]*/
                            endLayer.codes[column, row] = layers[i].codes[column, row];
                        }
                    }
                }
            }
            

        }

        public static int FindCheckLayer(Layer[] layers)
        {
            int lowestNum = (Layer.IMG_HEIGHT * Layer.IMG_WIDTH);
            int lowestIndex = -1;
            int num;

            for (int i = 0; i < layers.Length; i++)
            {
                num = layers[i].CalculateOccurances(0);

                if (num < lowestNum)
                {
                    lowestNum = num;
                    lowestIndex = i;
                }
            }

            return lowestIndex;
        }

        public static Layer[] PopulateLayers(char[] rawFile)
        {
            int numLayers = rawFile.Length / (Layer.IMG_WIDTH * Layer.IMG_HEIGHT);
            int pointer = 0;

            Layer[] layers = new Layer[numLayers];

            for(int i = 0; i<numLayers; i++)
            {
                /*  Creates one layer per loop  */
                layers[i] = new Layer();

                for(int column = 0; column < Layer.IMG_HEIGHT; column++)
                {
                    for (int row = 0; row < Layer.IMG_WIDTH; row++)
                    {
                        /*  Takes one sign, converts to int and stores in layer */
                        layers[i].codes[column, row] = Int32.Parse(rawFile[pointer++].ToString());
                        //Convert from char to string to int feels awkward. 
                    }
                }
            }

            return layers;
        }


        public static char[] ReadImage(char[] imageInput)
        {
            try
            {
                using (StreamReader sr = new StreamReader(IMAGE_PATH))
                {
                    int i = 0;

                    while (sr.Peek() >= 0 && i < BUFF_SIZE)
                    {
                        imageInput[i] = (char)sr.Read();
                        i++;
                    }
                }
            }

            catch(IOException e)
            {
                Console.WriteLine($"Unable to read file: {e}");
                return null;
            }

            return imageInput;
        }
    }
}
