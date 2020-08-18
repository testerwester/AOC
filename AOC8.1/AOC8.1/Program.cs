using System;

namespace AOC8._1
{
    
    class Program
    {

        static void Main(string[] args)
        {
            Image image = new Image();
            Layer checkLayer, finalLayer;
            
            if((image.ReadImageFile()) != null)
            {
                /*  Part one    */
                image.PopulateLayers();
                checkLayer = image.FindCheckLayer();
                Console.WriteLine($"1. The value of 1's * 2's is: " +
                    $"{checkLayer.CalculateOccurances(1) * checkLayer.CalculateOccurances(2)}");

                /*  Part Two    */
                finalLayer = image.ComposeLayers();
                finalLayer.PrintImage();
            }
        }
    }
}
