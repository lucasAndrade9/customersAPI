namespace ATM
{
    internal static class AtmCalculator
    {
        private static readonly int[] _denominations = { 10, 50, 100 };
        private static readonly List<Stack<int>> _result = new();

        public static void Calculate(int[] payoutAmounts)
        {
            foreach (int amount in payoutAmounts)
            {
                Console.WriteLine($"Possible combinations for {amount} EUR:");
                CalculateCombinations(amount, new Stack<int>(), 0);
                foreach (var combination in _result)
                {
                    PrintCombination(combination.ToList());
                }
                _result.Clear();
                Console.WriteLine();
            }
        }

        private static void CalculateCombinations(int remainingAmount, Stack<int> currentCombination, int startIndex)
        {
            if (remainingAmount == 0)
            {
                _result.Add(new Stack<int>(currentCombination));
                return;
            }

            for (int i = startIndex; i < _denominations.Length; i++)
            {
                if (_denominations[i] <= remainingAmount)
                {
                    currentCombination.Push(_denominations[i]);

                    CalculateCombinations(remainingAmount - _denominations[i], currentCombination, i);

                    currentCombination.Pop();
                }
            }
        }

        private static void PrintCombination(List<int> combination)
        {
            List<string> combinationStrings = new();

            foreach (int denomination in combination.Distinct())
            {
                int count = CountOccurrences(combination, denomination);
                combinationStrings.Add($"{count} x {denomination} EUR");
            }

            Console.WriteLine(string.Join(" + ", combinationStrings));
        }

        private static int CountOccurrences(List<int> stack, int value)
        {
            int count = 0;
            foreach (int item in stack)
            {
                if (item == value)
                {
                    count++;
                }
            }
            return count;
        }
    }
}
