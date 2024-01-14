using System;

class GradeCalculator
{
    static void Main()
    {
        // Core Requirement 1: Ask user for their grade percentage
        Console.Write("Enter your grade percentage: ");
        double gradePercentage = Convert.ToDouble(Console.ReadLine());

        // Core Requirement 2: Determine and print the letter grade
        char letter;
        if (gradePercentage >= 90)
            letter = 'A';
        else if (gradePercentage >= 80)
            letter = 'B';
        else if (gradePercentage >= 70)
            letter = 'C';
        else if (gradePercentage >= 60)
            letter = 'D';
        else
            letter = 'F';

        // Core Requirement 3: Print the letter grade
        Console.WriteLine($"Your letter grade is: {letter}");

        // Check if the user passed the course
        if (gradePercentage >= 70)
            Console.WriteLine("Congratulations! You passed the course.");
        else
            Console.WriteLine("Keep working hard for the next time.");
    }
}