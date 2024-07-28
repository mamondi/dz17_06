using System;
using System.Threading;

namespace dz17_06
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введiть початок дiапазону чисел: ");
            int start = int.Parse(Console.ReadLine());

            Console.WriteLine("Введiть кiнець дiапазону чисел: ");
            int end = int.Parse(Console.ReadLine());

            Console.WriteLine("Введiть кількiсть потокiв: ");
            int threadCount = int.Parse(Console.ReadLine());


            int totalNumbers = end - start + 1;
            int numbersPerThread = totalNumbers / threadCount;
            int remainingNumbers = totalNumbers % threadCount;

            Thread[] threads = new Thread[threadCount];

            for (int i = 0; i < threadCount; i++)
            {
                int threadStart = start + i * numbersPerThread;
                int threadEnd = threadStart + numbersPerThread - 1;

                if (i == threadCount - 1)
                {
                    threadEnd += remainingNumbers;
                }

                threads[i] = new Thread(() => PrintNumbers(threadStart, threadEnd));
                threads[i].Start();
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }

            Console.WriteLine("Числа виведено.");
        }

        static void PrintNumbers(int start, int end)
        {
            for (int i = start; i <= end; i++)
            {
                Console.WriteLine(i);
                Thread.Sleep(100);
            }
        }
    }
}
