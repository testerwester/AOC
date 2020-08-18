using System;
using System.Collections.Generic;
using System.Text;

namespace AOC8._1
{
    class Layer
    {
        /*  Constants   */
        public const int IMG_WIDTH = 25;
        public const int IMG_HEIGHT = 6;
        /*  COLORS  */
        public const int COL_BLACK = 0;
        public const int COL_WHITE = 1;
        public const int COL_TRANSPARENT = 2;
        /*  Variables   */
        private int[,] codes;


        /*  Creates an empty 2d array based */
        public Layer()
        {
            codes = new int[IMG_HEIGHT, IMG_WIDTH];
        }

        /*  When a specific value should be written to all spots    */
        public Layer(int input)
        {
            codes = new int[IMG_HEIGHT, IMG_WIDTH];
            int i, j;
            for (i = 0; i < IMG_HEIGHT; i++)
            {
                for (j = 0; j < IMG_WIDTH; j++)
                {
                    codes[i, j] = input;
                }
            }
        }

        public int GetSingle(int column, int row)
        {
            return codes[column, row];
        }

        public void SetSingle(int column, int row, int value)
        {
            codes[column, row] = value;
        }

        public void Fill(char[] data)
        {
            int pointer = 0;
            int column, row;

            for (column = 0; column < Layer.IMG_HEIGHT; column++)
            {
                for (row = 0; row < Layer.IMG_WIDTH; row++)
                {
                    codes[column, row] = Int32.Parse(data[pointer++].ToString());
                }
            }
        }


        public int CalculateOccurances(int numToCount)
        {
            int i, j, counter = 0;

            for (i = 0; i < IMG_HEIGHT; i++)
            {
                for (j = 0; j < IMG_WIDTH; j++)
                {
                    if (codes[i, j] == numToCount) counter++;
                }
            }
            return counter;
        }

        public void PrintImage()
        {
            int column, row;

            for (column = 0; column < IMG_HEIGHT; column++)
            {
                for (row = 0; row < IMG_WIDTH; row++)
                {
                    if (codes[column, row] != COL_BLACK) Console.Write($"{codes[column, row]}");
                    else Console.Write(" ");
                }
                Console.WriteLine("");
            }

        }

    }
}
