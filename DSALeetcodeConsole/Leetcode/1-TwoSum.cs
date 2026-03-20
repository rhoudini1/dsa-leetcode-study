using System;

namespace DSALeetcodeConsole.Leetcode;

public static class _1_TwoSum
{
    public static string Name = "1. Two Sum";

    public static void SolveTests()
    {
        Console.WriteLine($"Case 1 result: [{string.Join(",", NotSoGoodSolution([2, 7, 11, 15], 9))}]");
        Console.WriteLine($"Case 2 result: [{string.Join(",", NotSoGoodSolution([3, 2, 4], 6))}]");
        Console.WriteLine($"Case 3 result: [{string.Join(",", NotSoGoodSolution([3, 3], 6))}]");

        Console.WriteLine($"Case 1 result: [{string.Join(",", Solution([2, 7, 11, 15], 9))}]");
        Console.WriteLine($"Case 2 result: [{string.Join(",", Solution([3, 2, 4], 6))}]");
        Console.WriteLine($"Case 3 result: [{string.Join(",", Solution([3, 3], 6))}]");
    }

    public static int[] NotSoGoodSolution(int[] nums, int target)
    {
        // This problem is very interesting, and probably the most famous from Leetcode.
        // It looks easy at first: try every combination of two numbers and see which one works.
        // The first idea that comes to mind is to make two loops.
        for (int i = 0; i < nums.Length; i++)
        {
            for (int j = i + 1; j < nums.Length; j++)
            {
                if (nums[i] + nums[j] == target)
                    return [i, j];
            }
            // It works well for our test data that contains but a few elements.
            // However, this solution has a time complexity: we will visit each array twice,
            // and as the number of elements grow, the execution time will also grow.
            // We call this complexity O(n^2), because we run the array n * n times.
            // However, there is a much more elegant (and faster) solution.
        }
        return [];
    }

    // We can solve this by going through the array a single time!
    public static int[] Solution(int[] nums, int target)
    {
        // We can store some info about the number that we just have seen,
        // so that we can access it in the future if needed.
        // This is very common: we trade some memory for speed.
        var complementDict = new Dictionary<int, int>();
        // In this dictionary we will store...
        // as keys: the complement for each number that we might see
        // as values: the index of each number that we might see.
        // It works like this: the first number we see is 2, and target is 9, so its complement is 7.
        // We store the key,value pair (7,0) in our dictionary.
        // If the next number is 7, we look at our dictionary and see that 7 is in the dictionary,
        // then we can safely assure that those two numbers add up.
        for (int i = 0; i < nums.Length; i++)
        {
            int complement = target - nums[i];

            if (complementDict.TryGetValue(complement, out int complementIndex))
                return [complementIndex, i];

            complementDict.Add(nums[i], i);
        }
        return [];
    }
}
