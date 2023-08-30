namespace Task_V2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            decimal balance = 0;
            while (true)
            {
                Console.WriteLine("Pleasem submit action:");
                string inputCommand = Console.ReadLine();
                string[] dataIntput = inputCommand.Split(" ");
                ConsoleInputDataValidator consoleInputDataValidator = new ConsoleInputDataValidator();
                bool isInputValid = consoleInputDataValidator.ValidateInput(dataIntput);
                if (isInputValid)
                {
                    string command = consoleInputDataValidator.GetCommand(dataIntput);
                    decimal amount = consoleInputDataValidator.GetAmount(dataIntput);
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
                        balance = BetCalculation(amount,balance);
                    }
                }
            }
        }
        private static decimal BetCalculation(decimal amount,decimal balance)
        {
            Random random = new Random();
            int randomNumber = random.Next(1, 101);

            int smallWinChance = 50;
            int bigWinChance = 10;

            double smallWin = random.NextDouble() * 2.0;
            double bigWin = random.NextDouble() * (10.0 - 2.0) + 2.0;
            if (randomNumber > smallWinChance)
            {
                balance -= amount;
                Console.WriteLine($"No luck this time! Your current balance is: ${Math.Round(balance, 2)}");
            }

            else
            {

                int decimalPlaces = 2;
                decimal multiplier = (decimal)Math.Pow(10, decimalPlaces);
                decimal winMoney;
                decimal roundedWinMoney;
                decimal realProfit;
                randomNumber = 11;
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