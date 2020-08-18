using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace AOC8._1
{
    class Image
    {
        /*  Constants   */
        private const string IMAGE_PATH = "AOC8Input.txt";
        private const int BUFF_SIZE = 15000;

        /*  Variables   */
        private Layer[] layers;
        private char[] rawData;

        public Image()
        {
            rawData = new char[BUFF_SIZE];
        }

        /*  Composes layers where transparent can be overwritten    */
        public Layer ComposeLayers()
        {
            int numLayers = layers.Length;
            Layer endLayer = new Layer(Layer.COL_TRANSPARENT);
            int i, row, column;

            for (i = 0; i < numLayers; i++)
            {
                for (column = 0; column < Layer.IMG_HEIGHT; column++)
                {
                    for (row = 0; row < Layer.IMG_WIDTH; row++)
                    {
                        if (endLayer.GetSingle(column, row) == Layer.COL_TRANSPARENT)
                        {
                            endLayer.SetSingle(column, row, layers[i].GetSingle(column, row));
                        }
                    }
                }
            }

            return endLayer;
        }

        /*  Finds the layer with the most dark pixels   */
        public Layer FindCheckLayer()
        {
            int lowestNum = (Layer.IMG_HEIGHT * Layer.IMG_WIDTH);
            int lowestIndex = -1;
            int num;

            for (int i = 0; i < layers.Length; i++)
            {
                num = layers[i].CalculateOccurances(Layer.COL_BLACK);
                if (num < lowestNum)
                {
                    lowestNum = num;
                    lowestIndex = i;
                }
            }
            return layers[lowestIndex];
        }

        /*  Fills layers with rawData   */
        public void PopulateLayers()
        {
            int bufferSize = (Layer.IMG_WIDTH * Layer.IMG_HEIGHT);
            int numLayers = rawData.Length / bufferSize;
            

            layers = new Layer[numLayers];

            for (int i = 0; i < numLayers; i++)
            {
                layers[i] = new Layer();
                layers[i].Fill(rawData.Skip(bufferSize * i).Take((bufferSize)).ToArray());
            }
        }


        /*  Reads a file and stores single chars in an array    */
        public char[] ReadImageFile()
        {
            try
            {
                using (StreamReader sr = new StreamReader(IMAGE_PATH))
                {
                    int i = 0;

                    while (sr.Peek() >= 0 && i < BUFF_SIZE)
                    {
                        rawData[i] = (char)sr.Read();
                        i++;
                    }
                }
            }

            catch (IOException e)
            {
                Console.WriteLine($"Unable to read file: {e}");
                return null;
            }

            return rawData;
        }
    }
}


