using System;
using System.Collections.Generic;
using System.Linq;

class NumberProcessor
{
    static void Main()
    {
        // Core Requirement: Ask the user for a series of numbers and compute sum, average, and find the largest number
        List<int> numbers = new List<int>();

        Console.WriteLine("Enter a list of numbers, type 0 when finished.");

        int userInput;
        do
        {
            Console.Write("Enter number: ");
            userInput = Convert.ToInt32(Console.ReadLine());

            if (userInput != 0)
                numbers.Add(userInput);

        } while (userInput != 0);

        // Core Requirement 1: Compute the sum
        int sum = numbers.Sum();

        // Core Requirement 2: Compute the average
        double average = numbers.Average();

        // Core Requirement 3: Find the largest number
        int largestNumber = numbers.Max();

        Console.WriteLine($"The sum is: {sum}");
        Console.WriteLine($"The average is: {average}");
        Console.WriteLine($"The largest number is: {largestNumber}");

        // Stretch Challenge 1: Find the smallest positive number
        int smallestPositive = numbers.Where(x => x > 0).DefaultIfEmpty(0).Min();
        Console.WriteLine($"The smallest positive number is: {smallestPositive}");

        // Stretch Challenge 2: Sort the numbers and display the sorted list
        numbers.Sort();
        Console.WriteLine("The sorted list is:");
        foreach (var number in numbers)
        {
            Console.WriteLine(number);
        }
    }
}