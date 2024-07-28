using System;
using System.IO;
using System.Linq;
using System.Threading;

namespace dz17_06
{
    class Program
    {
        static void Main(string[] args)
        {
            // Генерація набору чисел
            Random random = new Random();
            int[] numbers = new int[10000];
            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = random.Next(1, 10001);
            }

            // Змінні для результатів
            int max = int.MinValue;
            int min = int.MaxValue;
            double average = 0.0;

            // Потоки для пошуку максимума, мінімуму і середнього арифметичного
            Thread maxThread = new Thread(() => max = FindMax(numbers));
            Thread minThread = new Thread(() => min = FindMin(numbers));
            Thread avgThread = new Thread(() => average = CalculateAverage(numbers));

            // Запуск потоків
            maxThread.Start();
            minThread.Start();
            avgThread.Start();

            // Очікування завершення потоків
            maxThread.Join();
            minThread.Join();
            avgThread.Join();

            // Потік для запису результатів у файл
            Thread fileWriteThread = new Thread(() => WriteResultsToFile(numbers, max, min, average));
            fileWriteThread.Start();
            fileWriteThread.Join();

            Console.WriteLine("Результати записано у файл.");
        }

        static int FindMax(int[] numbers)
        {
            return numbers.Max();
        }

        static int FindMin(int[] numbers)
        {
            return numbers.Min();
        }

        static double CalculateAverage(int[] numbers)
        {
            return numbers.Average();
        }

        static void WriteResultsToFile(int[] numbers, int max, int min, double average)
        {
            string filePath = "results.txt";
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine("Набір чисел:");
                foreach (int number in numbers)
                {
                    writer.WriteLine(number);
                }
                writer.WriteLine();
                writer.WriteLine($"Максимум: {max}");
                writer.WriteLine($"Мінімум: {min}");
                writer.WriteLine($"Середнє арифметичне: {average:F2}");
            }
        }
    }
}
