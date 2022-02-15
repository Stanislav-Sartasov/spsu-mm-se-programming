using System;
using System.IO;
using BMPFilter.Filters;

namespace BMPFilter
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintDescription();

            if (args.Length != 3)
            {
                Console.WriteLine(
                    "Неверное количество аргументов. Формат: <программа> <имя_входного_файла> <название_фильтра> <имя выходного файла>");
                return;
            }

            string currentFilter = args[1];

            if (!currentFilter.Equals("median") && !currentFilter.Equals("gauss") && !currentFilter.Equals("sobelx") &&
                !currentFilter.Equals("sobely") && !currentFilter.Equals("gray"))
            {
                Console.WriteLine("Введённый фильтр не относится к списку доступных. Попробуйте ещё раз.");
                return;
            }

            FileStream fileIn;
            try
            {
                fileIn = new FileStream(args[0], FileMode.Open, FileAccess.ReadWrite);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Ошибка! Входной файл не был найден! Попробуйте ещё раз.");
                return;
            }

            FileStream fileOut;

            try
            {
                fileOut = new FileStream(args[2], FileMode.Create, FileAccess.ReadWrite);
            }
            catch (Exception)
            {
                fileIn.Close();
                Console.WriteLine("Ошибка! Не удалось создать/открыть выходной файл. Попробуйте ещё раз.");
                return;
            }

            BitMapFile file = new(fileIn);
            fileIn.Close();

            if (currentFilter.Equals("gray"))
            {
                Console.WriteLine("Выбран фильтр серого");
                GrayFilter.ApplyFilter(file);
            }
            else if (currentFilter.Equals("median"))
            {
                Console.WriteLine("Выбран усредняющий фильтр");
                MedianFilter.ApplyFilter(file);
            }
            else if (currentFilter.Equals("gauss"))
            {
                Console.WriteLine("Выбран фильтр Гаусса");
                GaussFilter.ApplyFilter(file);
            }
            else if (currentFilter.Equals("sobelx"))
            {
                Console.WriteLine("Выбран фильтр Собеля по X");
                SobelFilter.ApplyFilter(file, SobelFilter.Type.X);
            }
            else if (currentFilter.Equals("sobely"))
            {
                Console.WriteLine("Выбран фильтр Собеля по Y");
                SobelFilter.ApplyFilter(file, SobelFilter.Type.Y);
            }

            Console.WriteLine("Фильтр был успешно применён");

            file.WriteResult(fileOut);
            fileOut.Close();
        }

        private static void PrintDescription()
        {
            Console.WriteLine("Данная программа применяет фильтры к BMP-файлу (24-bit или 32-bit)");
            Console.WriteLine("Формат: <программа> <имя_входного_файла> <название_фильтра> <имя выходного файла>");
            Console.WriteLine(
                "Далее будет выведен список доступных фильтров и их названий. Внимание: названия регистрозависимы.");
            Console.WriteLine("Усредняющий фильтр 3x3 - median");
            Console.WriteLine("Усредняющий фильтр Гаусса 3x3 - gauss");
            Console.WriteLine("Фильтр Собеля по X - sobelx");
            Console.WriteLine("Фильтр Собеля по Y - sobely");
            Console.WriteLine("Перевод в оттенки серого - gray");
        }
    }
}