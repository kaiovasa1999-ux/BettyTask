namespace Task_V2
{
    public class ConsoleInputDataValidator
    {
        private string[] validCommands = { "exit", "bet", "deposit", "withdrawal" };
       
        public bool ValidateInput(string[] input)
        {
            bool validSize = ValidateInputSize(input);
            bool result = false;

            if (validSize)
            {
                string cmd = GetCommand(input);
                decimal amount = GetAmount(input);
                bool isValidCommand = ValidateCommand(cmd, validCommands);
                bool isValidAmount = ValidateAmount(amount);

                if(cmd == "withdrawal")
                {
                    bool isvalidWithdrawal = ValidateWithdrawalAmount(cmd, amount);
                    if (!isvalidWithdrawal)
                    {
                        return false;
                    }
                }

                if (isValidCommand && isValidAmount && input.Count() <= 2)
                {
                  

                    return true;
                }
            }
          
            return result;
        }

        public bool ValidateCommand(string cmd, string[] validCommands)
        {
            bool result = validCommands.Contains(cmd);
            if (!result)
            {
                Console.WriteLine($"invalid command or amount. Please provide beteween 1 and 10 and commands should be {string.Join(", ", validCommands)}");
            }
            return result;
        }
        public bool ValidateAmount(decimal amount)
        {
            if (amount >= 1 && amount <= 10)
            {
                return true;
            }
            Console.WriteLine($"Please provide us amount between 1 and 10");
            return false;
        }

        public bool ValidateWithdrawalAmount(string cmd, decimal amount)
        {
            bool result = false;
            if(cmd == "withdrawal" && amount >= 1)
            {
                result = true;
            }
            return result;
        }

        public string GetCommand(string[] input)
        {
            if (input.Length == 0 || input.Length > 2)
            {
                Console.WriteLine("Invalid input");
            }
            return input[0];
        }
        public bool ValidateInputSize(string[] input)
        {
            bool result = false;
            if (input.Count() > 0 || input.Count() < 2)
            {
                result = true;
            }
            return result;
        }
        public decimal GetAmount(string[] input)
        {
            decimal amount;
            decimal.TryParse(input[1], out amount);
            return amount;
        }
    }
}
