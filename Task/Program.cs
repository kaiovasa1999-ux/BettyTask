using System;
using System.Runtime.CompilerServices;

namespace Task
{
    internal class Program
    {
        const int minBet = 1;
        const int maxBet = 10;
        
        static void Main(string[] args)
        {
            //int betsCounter = 0;
            double balance = 0;
            while (true)
            {
                Console.WriteLine("Pleasem submit action: ");

                string inputCommand = Console.ReadLine();
                string[] daintput = inputCommand.Split(" ");
                string command = daintput[0];
                if (command == "exit")
                {
                    Console.WriteLine("Thank you for plaing! Hope to see you again soon.");
                    break;
                }
                int amount = int.Parse(daintput[1]);

                bool isValidAmount = ValidateAmount(amount);
                if (isValidAmount)
                {
                    if (command == "deposit")
                    {
                        balance += amount;
                        Console.WriteLine($"Your deposit of ${amount} was successful. Your current balance is: ${balance}");
                        continue;
                    }
                    if (command == "withdrawal")
                    {
                        if(amount <= balance)
                        {
                            double left = balance -= amount;
                            Console.WriteLine($"Your withdrawal of {amount}. Your current balance is: ${Math.Round(left,2)}");
                            Console.WriteLine("If you want to continue just press enter otherwise type 'exit'");
                            var secondInput = Console.ReadLine();
                            if(secondInput == "exit")
                            {
                                Console.WriteLine("Thank you for plaing! Hope to see you again soon.");
                                break;
                            }
                            continue;
                        }
                        else
                        {
                            Console.WriteLine("NOT ENOUGH BALANCE!!!");
                        }
                        continue;
                    }

                    if (command == "bet" && amount <= balance)
                    {
                        //betsCounter++;
                        balance += BetCalculation(amount, /*betsCounter,*/balance) - balance;
                    }
                }
                else
                {
                    Console.WriteLine("invalid amount. Please provide beteween");
                }
            }

        }
        public static bool ValidateAmount(int? input)
        {
            if (input != null && input > minBet && input <= maxBet)
            {
                return true;
            }
            Console.WriteLine($"Please provide us amount between {minBet} and {maxBet}");
            return false;
        }
        //II thought about the betsCounter and decide to return the previous logic because with this validation
        //(x% 2 == 0) the how logic is pretty predictable, but is steel 50% at the end. With this randomNumber > 50
        //the chance to win is again 50% but it is not that predictable
        private static double BetCalculation(int amount,/*int betsCounter,*/ double balance)
        {
            Random random = new Random();
            int randomNumber = random.Next(1,101);
            
            int smallWinChance = 50;
            int bigWinChance = 10;

            double smallWin = random.NextDouble() * 2.0;
            double bigWin = random.NextDouble() * (10.0 - 2.0) + 2.0;
            randomNumber = 1;
            //if(betsCounter % 2 == 0)
            //{
            //    balance -= amount;
            //    Console.WriteLine($"No luck this time! Your current balance is: ${Math.Round(balance, 2)}");
            //    return balance;
            //}
            if (randomNumber > smallWinChance)
            {
                balance -= amount;
                Console.WriteLine($"No luck this time! Your current balance is: ${Math.Round(balance,2)}");
            }
            else
            {
                if (randomNumber <= smallWinChance && randomNumber > bigWinChance)
                {
                    double winMoney = amount * smallWin;
                    balance += winMoney;
                    Math.Round(balance, 2);
                    Console.WriteLine($"Congrats SMALL JAK - you won {Math.Round(winMoney, 2)}.Your current balance is: ${Math.Round(balance, 2)}");
                }
                if (randomNumber <= bigWinChance)
                {
                    double winMoney = amount * bigWin;
                    balance += winMoney;
                    Math.Round(balance, 2);
                    Console.WriteLine($"Congrats BIG JAK - you won {Math.Round(winMoney, 2)}.Your current balance is: ${Math.Round(balance, 2)}");
                }
            }
            return balance;
        }
    }
}