using System;
using System.Drawing;

class PNGColorParser
{
    static void Main(string[] args)
    {
        string filePath = "item_heal.png"; // Укажите путь к файлу PNG

        if (System.IO.File.Exists(filePath))
        {
            try
            {
                using (Bitmap bitmap = new Bitmap(filePath))
                {
                    int width = bitmap.Width;
                    int height = bitmap.Height;

                    Console.WriteLine("Ширина: " + width);
                    Console.WriteLine("Высота: " + height);

                    // Перебор пикселей и получение цветовых данных
                    for (int y = 0; y < height; y++)
                    {
                        for (int x = 0; x < width; x++)
                        {
                            Color pixelColor = bitmap.GetPixel(x, y);
                            int red = pixelColor.R;
                            int green = pixelColor.G;
                            int blue = pixelColor.B;
                            int alpha = pixelColor.A;

                            Console.WriteLine($"Пиксель ({x}, {y}): R={red}, G={green}, B={blue}, A={alpha}");
                        }
                    }
                }

                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка при чтении файла PNG: " + ex.Message);
            }
        }
        else
        {
            Console.WriteLine("Файл не найден.");
        }
    }
}
