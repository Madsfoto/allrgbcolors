﻿using System;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System.Diagnostics;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Png;


namespace allrgbcolors
{
    internal class Program
    {

        static Color SetColor(int R, int G, int B)
        {
            // Color requires the pixels to be Rgb24 (eg 8 bit), Rgb24 requres the data to be i bytes (to fit the 0-255).
            Color colInt = new Color(new Rgba32((byte)R, (byte)G, (byte)B));
            

            return colInt;
        }

        static int[] GenerateInts(int number)
        {
            //Console.WriteLine("The numbers");

            int[] ints = new int[number];

            var rand = new Random();

            for (int i = 0; i < ints.Length; i++)
            {
                ints[i] = rand.Next(0, 256);
                //Console.WriteLine(ints[i]);
            }



            return ints;
        }

        static void Main(string[] args)
        {
            // size of the image: 
            int xSizeInt = 4096;
            int ySizeInt = 4096;
            int numberofRawValues = xSizeInt * ySizeInt * 3;


            // make the actual image
            Image<Rgba32> image = new Image<Rgba32>(xSizeInt, ySizeInt);
            Image<Rgba32> imageSorted = new Image<Rgba32>(xSizeInt, ySizeInt);

            Console.WriteLine("image created");

            // GenerateInts()
            int[] intArr = GenerateInts(numberofRawValues);
            Console.WriteLine("Ints created");

            int[] allInts = new int[numberofRawValues];

            int value = 0;
            for (int r = 0; r < 256; r++)
            {
                for (int g = 0; g < 256; g++)
                {
                    for (int b = 0; b < 256; b++)
                    {
                        allInts[value] = r;
                        allInts[value+1] = g;
                        allInts[value+2] = b;
                        value = value + 3;
                    }

                }

            }

            int[] redValues = new int[xSizeInt * ySizeInt];
            int[] blueValues = new int[xSizeInt * ySizeInt];
            int[] greenValues = new int[xSizeInt * ySizeInt];
            int valueNumber = 0;

            for (int i = 0; i < numberofRawValues; i = i + 3)
            {
                redValues[valueNumber] = allInts[i];
                blueValues[valueNumber] = allInts[i + 1];
                greenValues[valueNumber] = allInts[i + 2];
                valueNumber++;
            }

            // Actually sort the red values

            //Array.Sort(redValues);
            //Array.Sort(blueValues);
            //Array.Sort(greenValues);

            int arraySortedPlacement = 0;
            for (int y = 0; y < ySizeInt; y++)
            {
                for (int x = 0; x < xSizeInt; x++)
                {
                    imageSorted[x, y] = SetColor(redValues[arraySortedPlacement], blueValues[arraySortedPlacement], greenValues[arraySortedPlacement]);

                    arraySortedPlacement++;

                }

            }
            
            imageSorted.SaveAsPng("AllRGB.png");
            Console.WriteLine("File written");


            // read the random data into pixels: 

            // We increment this value at each position (x,y) by 3, then take the value at (arrayplacement, arrayplacement+1 and arrayplacement+2)
            // and put into the color value
            //int arrayplacement = 0;

            // go through the image
            for (int y = 0; y < ySizeInt; y++)
            {
                for (int x = 0; x < xSizeInt; x++)
                {
                    // set the actual pixel values

                    //image[x, y] = SetColor(intArr[arrayplacement], intArr[arrayplacement + 1], intArr[arrayplacement + 2]);
                    //arrayplacement = arrayplacement + 3;


                }
            }


            Console.WriteLine("random data written to image");

            // we now have an image size filled with random pixel data
            // save the image

            //image.SaveAsPng("random.png");
            Console.WriteLine("File written");


            //using (Image imageRS = Image.Load("random.png"))
            //{
            //    int width = imageRS.Width / 2;
            //    int height = imageRS.Height / 2;
            //    //imageRS.Mutate(x => x.Resize(width, height, KnownResamplers.Box));
            //    //imageRS.Mutate(x => x.Resize(width*16, height*16));

            //    //imageRS.SaveAsPng("RandomHalfsize.png");
            //}



        }
    }
}


