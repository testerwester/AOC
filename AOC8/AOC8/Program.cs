using System;
using System.IO;

namespace AOC8
{

   
    class Program
    {
        /*  Constants   */
        public const string IMAGE_PATH = "AOC8Input.txt";
        public const int BUFF_SIZE = 15000;
        public const int IMG_WIDTH = 25;
        public const int IMG_HEIGHT = 6;


        public class Layer
        {
            /*  Variables   */
            public int[,] codes;
            public int numZeroes;

            public Layer()
            {
                codes = new int[IMG_HEIGHT, IMG_WIDTH];
                numZeroes = 0;
            }

            public void WriteRow(int index, int[] values)
            {
                for(int i = 0; i<IMG_WIDTH; i++)
                {
                    this.codes[index, i] = values[i];
                    if (values[i] == 0) numZeroes++;
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

                if(numToCount == 0) this.numZeroes = counter; //Failsafe since nuMZeroes is public
                return counter;
            }
            
        }


        static void Main(string[] args)
        {
            int lowestIndex;
            Layer[] layers;
            char[] rawData = new char[BUFF_SIZE];

            if (ReadImage(rawData) != null)
            {
                /*  Part one    */
                layers = PopulateLayers(rawData);
                lowestIndex = FindCheckLayer(layers);

                Console.WriteLine($"The value of 1 * 2 is: " +
                    $"{(layers[lowestIndex].CalculateOccurances(1) * layers[lowestIndex].CalculateOccurances(2))}");
            }
        }

        public static int FindCheckLayer(Layer[] layers)
        {
            int lowestNum = (IMG_HEIGHT * IMG_WIDTH);
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
            int numLayers = rawFile.Length / (IMG_WIDTH * IMG_HEIGHT);
            int[] rowBuffer = new int[IMG_WIDTH];
            int pointer = 0;

            Layer[] layers = new Layer[numLayers];

            for(int i = 0; i<numLayers; i++)
            {
                /*  Creates one layer per loop  */
                layers[i] = new Layer();

                for(int row = 0; row < IMG_HEIGHT; row++)
                {
                    for (int column = 0; column < IMG_WIDTH; column++)
                    {
                        /*  Takes one sign, converts to int and stores in layer */
                        layers[i].codes[row, column] = Int32.Parse(rawFile[pointer++].ToString());
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
