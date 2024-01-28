using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
    {
        // Create a sample scripture
        Scripture john316 = new Scripture("John 3:16", "For God so loved the world...");

        // Display the complete scripture
        john316.Display();

        // Allow the user to hide words until all words are hidden or they type quit
        while (!john316.AllWordsHidden())
        {
            Console.WriteLine("Press Enter to hide more words, type 'quit' to exit, or 'hint' for a hint.");
            string userInput = Console.ReadLine();

            if (userInput.ToLower() == "quit")
                break;
            else if (userInput.ToLower() == "hint")
                john316.DisplayHint();
            else
            {
                john316.HideRandomWords();
                john316.Display();
            }
        }
    }
}

class Scripture
{
    private string reference;
    private string text;
    private List<string> hiddenWords;

    public Scripture(string reference, string text)
    {
        this.reference = reference;
        this.text = text;
        this.hiddenWords = new List<string>();
    }

    public void Display()
    {
        Console.Clear();
        Console.WriteLine($"{reference}\n");

        // Display text with hidden words
        string[] words = text.Split(' ');
        foreach (string word in words)
        {
            if (hiddenWords.Contains(word.ToLower()))
                Console.Write("____ ");
            else
                Console.Write($"{word} ");
        }

        Console.WriteLine("\n");
    }

    public void DisplayHint()
    {
        Console.WriteLine("Here's a hint: ");
        Random random = new Random();
        int randomIndex = random.Next(hiddenWords.Count);
        Console.WriteLine(hiddenWords[randomIndex]);
        Console.WriteLine();
    }

    public bool AllWordsHidden()
    {
        string[] words = text.Split(' ');
        return hiddenWords.Count == words.Length;
    }

    public void HideRandomWords()
    {
        string[] words = text.Split(' ');

        // Get a list of words that are not hidden
        List<string> availableWords = words.Except(hiddenWords).ToList();

        // Randomly select a few words to hide
        Random random = new Random();
        int wordsToHide = Math.Min(2, availableWords.Count); // You can adjust the number of words to hide
        for (int i = 0; i < wordsToHide; i++)
        {
            int randomIndex = random.Next(availableWords.Count);
            hiddenWords.Add(availableWords[randomIndex].ToLower());
            availableWords.RemoveAt(randomIndex);
        }
    }
}