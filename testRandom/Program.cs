using System;

namespace testRandom
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Random random = new Random();
                double smallWin = random.NextDouble() * 2.0;
                double bigWin = random.NextDouble() * (10.0 - 2.0) + 2.0;
                Console.WriteLine(smallWin);
                Console.WriteLine(bigWin);
                if(bigWin < 2.00 || bigWin > 10.00)
                {
                    Console.WriteLine("WROOOONG");
                } 
            }

        }
    }
}