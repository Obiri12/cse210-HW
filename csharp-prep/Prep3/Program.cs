using System;

class GuessMyNumber
{
    static void Main()
    {
        // Core Requirement 1: Ask user for the magic number
        Console.Write("What is the magic number? ");
        int magicNumber = Convert.ToInt32(Console.ReadLine());

        // Core Requirement 2: Loop until the user guesses the magic number
        int guess;
        do
        {
            // Ask user for a guess
            Console.Write("What is your guess? ");
            guess = Convert.ToInt32(Console.ReadLine());

            // Determine if the user needs to guess higher or lower
            if (guess < magicNumber)
                Console.WriteLine("Higher");
            else if (guess > magicNumber)
                Console.WriteLine("Lower");
            else
                Console.WriteLine("You guessed it!");

        } while (guess != magicNumber);

        // Core Requirement 3: Generate a random number from 1 to 100
        Random random = new Random();
        magicNumber = random.Next(1, 101);

        // Play the game again
        do
        {
            // Ask user for a guess
            Console.Write("What is your guess? ");
            guess = Convert.ToInt32(Console.ReadLine());

            // Determine if the user needs to guess higher or lower
            if (guess < magicNumber)
                Console.WriteLine("Higher");
            else if (guess > magicNumber)
                Console.WriteLine("Lower");
            else
                Console.WriteLine("You guessed it!");

        } while (guess != magicNumber);

        // Stretch Challenge 1: Keep track of guesses
        int guessCount = 0;
        do
        {
            // Ask user for a guess
            Console.Write("What is your guess? ");
            guess = Convert.ToInt32(Console.ReadLine());

            // Determine if the user needs to guess higher or lower
            if (guess < magicNumber)
                Console.WriteLine("Higher");
            else if (guess > magicNumber)
                Console.WriteLine("Lower");
            else
                Console.WriteLine("You guessed it!");

            // Increment guess count
            guessCount++;

        } while (guess != magicNumber);

        // Stretch Challenge 2: Ask if the user wants to play again
        Console.Write("Do you want to play again? (yes/no) ");
        string playAgain = Console.ReadLine().ToLower();

        while (playAgain == "yes")
        {
            // Generate a new random number
            magicNumber = random.Next(1, 101);

            // Reset guess count
            guessCount = 0;

            // Play the game again
            do
            {
                // Ask user for a guess
                Console.Write("What is your guess? ");
                guess = Convert.ToInt32(Console.ReadLine());

                // Determine if the user needs to guess higher or lower
                if (guess < magicNumber)
                    Console.WriteLine("Higher");
                else if (guess > magicNumber)
                    Console.WriteLine("Lower");
                else
                    Console.WriteLine("You guessed it!");

                // Increment guess count
                guessCount++;

            } while (guess != magicNumber);

            // Ask if the user wants to play again
            Console.Write("Do you want to play again? (yes/no) ");
            playAgain = Console.ReadLine().ToLower();
        }

        // Inform the user of the number of guesses made
        Console.WriteLine($"You took {guessCount} guesses. Thanks for playing!");
    }
}