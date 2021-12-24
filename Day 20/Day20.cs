using System;
using System.Text;

public class AdventOfCodeDay20
{

    public static void run()
    {
        string[] lines = System.IO.File.ReadAllLines("./Day 20/Problem1Input.txt");
        int numBrightPixels = Problem1(lines);
        int after50Enhances = Problem2(lines);
        Console.WriteLine("Day 20 - Problem 1: After 2 enhances, there are " + numBrightPixels + " bright pixels");
        Console.WriteLine("Day 20 - Problem 2: After 50 enhances, there are " + after50Enhances + " lit pixels");
    }

    private static int Problem1(string[] lines)
    {
        string algorithm = lines[0];
        string[,] image = ParseInput(lines);
        for (int i = 0; i < 2; i++)
        {
            bool pixelsOutsideBoundsAreLit = i % 2 == 0 ? false : true;
            image = EnhanceImage(image, algorithm, pixelsOutsideBoundsAreLit);
        }
        return CountLitPixels(image);
    }

    private static int Problem2(string[] lines)
    {
        string algorithm = lines[0];
        string[,] image = ParseInput(lines);
        for (int i = 0; i < 50; i++)
        {
            bool pixelsOutsideBoundsAreLit = i % 2 == 0 ? false : true;
            image = EnhanceImage(image, algorithm, pixelsOutsideBoundsAreLit);
        }
        return CountLitPixels(image);
    }

    private static int CountLitPixels(string[,] image)
    {
        int numLitPixels = 0;
        for (int i = 0; i < image.GetLength(0); i++)
        {
            for (int j = 0; j < image.GetLength(1); j++)
            {
                if (image[i, j].Equals("1"))
                {
                    numLitPixels++;
                }
            }
        }
        return numLitPixels;
    }

    private static string[,] EnhanceImage(string[,] image, string algorithm, bool pixelsOutsideBoundsAreLit)
    {
        string[,] enhancedImage = new string[image.GetLength(0) + 2, image.GetLength(1) + 2];
        for (int i = 0; i < enhancedImage.GetLength(0); i++)
        {
            for (int j = 0; j < enhancedImage.GetLength(1); j++)
            {
                string binaryPixel = BuildBinaryPixel(image, i, j, pixelsOutsideBoundsAreLit);
                enhancedImage[i, j] = ConvertBinaryPixel(algorithm, binaryPixel);
            }
        }
        return enhancedImage;
    }

    private static string BuildBinaryPixel(string[,] image, int x, int y, bool pixelsOutsideBoundsAreLit)
    {
        StringBuilder pixel = new StringBuilder();
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                pixel.Append(GetPixelFromImage(image, x + i, y + j, pixelsOutsideBoundsAreLit));
            }
        }
        return pixel.ToString();
    }

    private static string ConvertBinaryPixel(string algorithm, string binaryPixel)
    {
        int index = Convert.ToInt32(binaryPixel, 2);
        return algorithm[index].Equals('#') ? "1" : "0";
    }

    private static string GetPixelFromImage(string[,] image, int x, int y, bool pixelsOutsideBoundsAreLit)
    {
        string outsideBounds = pixelsOutsideBoundsAreLit ? "1" : "0";
        return CoordinateOnImageBoundary(image, x, y) ? outsideBounds : image[x - 1, y - 1];
    }

    private static bool CoordinateOnImageBoundary(string[,] image, int x, int y)
    {
        if (x <= 0 || y <= 0)
        {
            return true;
        }
        if ((x - 1) >= image.GetLength(0) || (y - 1) >= image.GetLength(1))
        {
            return true;
        }
        return false;
    }

    private static string[,] ParseInput(string[] lines)
    {
        string[,] image = new string[lines.Length - 2, lines[2].Length];
        for (int i = 2; i < lines.Length; i++)
        {
            for (int j = 0; j < lines[i].Length; j++)
            {
                image[i - 2, j] = PixelToBinary(lines[i][j]);
            }
        }
        return image;
    }

    private static string PixelToBinary(char pixel)
    {
        return pixel.Equals('#') ? "1" : "0";
    }

}