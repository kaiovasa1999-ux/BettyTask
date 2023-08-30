using System;
using System.Drawing;
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
            decimal balance = 0;
            while (true)
            {
                Console.WriteLine("Pleasem submit action: ");

                string inputCommand = Console.ReadLine();
                string[] dataIntput = inputCommand.Split(" ");
                string command = dataIntput[0];
                string[] validCommands = { "exit", "bet", "deposit", "withdrawal" };

                if(!ValidCommand(command, validCommands) || dataIntput.Count() > 2)
                {
                    Console.WriteLine($"invalid command or amount. Please provide beteween {minBet} and {maxBet} and commands should be {string.Join(", ",validCommands)}");
                    continue;
                }
                if (command == "exit")
                {
                    Console.WriteLine("Thank you for playing! Hope to see you again soon.");
                    break;
                }
                else if(command != "exit" && dataIntput.Count() <2)
                {
                    Console.WriteLine("Invalid input");
                    continue;
                }

                string input = dataIntput[1];
                decimal amount;

                if (decimal.TryParse(input, out amount))
                {
                    bool isValidAmount = ValidateAmount(amount, command);
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
                            if (amount <= balance)
                            {
                                decimal left = balance -= amount;
                                Console.WriteLine($"Your withdrawal of {amount}. Your current balance is: ${Math.Round(left, 2)}");
                                Console.WriteLine("If you want to continue just press enter otherwise type 'exit'");
                                var secondInput = Console.ReadLine();
                                if (secondInput == "exit")
                                {
                                    Console.WriteLine("Thank you for playing! Hope to see you again soon.");
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Good choice!!!");
                                    continue;

                                }
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
                            balance = BetCalculation(amount, /*betsCounter,*/balance);
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Please provide us amount between {minBet} and {maxBet}");
                    }
                }
                else
                {
                    Console.WriteLine($"invalid command or amount. Please provide beteween {minBet} and {maxBet} and commands should be {string.Join(", ", validCommands)}");
                }
            }
        }
        public static bool ValidateAmount(decimal amount, string cmd)
        {
            if(cmd == "withdrawal" && amount >= 1)
            {
                return true;
            }
            if(amount >= 1 && amount <= maxBet)
            {
                return true;
            }
            return false;
        }

        private static bool ValidCommand(string cmd, string[] commands)
        {
            return commands.Contains(cmd);
        }

        //I thought about the betsCounter and decide to return the previous logic because with this validation
        //(x % 2 == 0) the how logic is pretty predictable, but is still 50% at the end. With this randomNumber > 50
        //the chance to win is again 50% but it is not that predictable
        private static decimal BetCalculation(decimal amount,/*int betsCounter,*/ decimal balance)
        {
            Random random = new Random();
            int randomNumber = random.Next(1,101);
            
            int smallWinChance = 50;
            int bigWinChance = 10;

            double smallWin = random.NextDouble() * 2.0;
            double bigWin = random.NextDouble() * (10.0 - 2.0) + 2.0;

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
                // PS:I decided to pay attention to the small numbers to prevent some loses to out casino :) 
                // For example if the algorithm returns winMoney =  22.755141.. if I had leaved  the default behavior
                // after the rounding the output will be 22.76 and in that case our casino losing money.
                // But now with this custom rounding I am kind a trimming the numbers behind the second decimal point
                // and in that case if winMoney = 22.755141 the output will be 22.75 

                int decimalPlaces = 2;
                decimal multiplier = (decimal)Math.Pow(10, decimalPlaces);
                decimal winMoney;
                decimal roundedWinMoney;
                decimal realProfit;
                randomNumber =11;
                if (randomNumber <= smallWinChance && randomNumber > bigWinChance)
                {
                    winMoney = amount * (decimal)smallWin;
                    roundedWinMoney = Math.Floor(winMoney * multiplier) / multiplier;
                    realProfit = Math.Abs(roundedWinMoney - amount);
                    balance += realProfit;
                    Console.WriteLine($"Congrats SMALL JAK - you won {Math.Round(realProfit, 2)}.Your current balance is: ${Math.Round(balance, 2)}");
                }
                if (randomNumber <= bigWinChance)
                {
                    winMoney = amount * (decimal)bigWin;
                    roundedWinMoney = Math.Floor(winMoney * multiplier) / multiplier;
                    realProfit = Math.Abs(roundedWinMoney - amount);
                    balance += realProfit;
                    Console.WriteLine($"Congrats BIG JAK - you won {realProfit:f2}.Your current balance is: ${Math.Round(balance, 2)}");
                }
            }

            return balance;
        }
    }
}