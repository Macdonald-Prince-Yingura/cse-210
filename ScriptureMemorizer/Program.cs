// Exceeding Requirements:
// - Loads multiple scriptures from an external file (scriptures.txt)
// - Randomly selects a scripture at runtime
// - Selects only visible words when hiding (not already hidden)
// - Hides a specified number of random words each time
// - Displays the reference and text of the scripture
using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        // Bonus: Load random scripture from file
        Scripture scripture = LoadRandomScripture("scriptures.txt");

        while (!scripture.AllWordsHidden())
        {
            Console.Clear();
            Console.WriteLine(scripture.GetDisplayText());

            Console.WriteLine("\nPress Enter to hide more words or type 'quit' to exit:");
            string input = Console.ReadLine();

            if (input.ToLower() == "quit")
                break;

            scripture.HideRandomWords(3);
        }

        // Final display
        Console.Clear();
        Console.WriteLine(scripture.GetDisplayText());
        Console.WriteLine("\nAll words hidden. Press Enter to exit.");
        Console.ReadLine();
    }

    static Scripture LoadRandomScripture(string filePath)
    {
        string[] lines = File.ReadAllLines(filePath);
        Random rand = new Random();
        string randomLine = lines[rand.Next(lines.Length)];

        string[] parts = randomLine.Split('|');
        Reference reference = new Reference(parts[0]);
        return new Scripture(reference, parts[1]);
    }
}
