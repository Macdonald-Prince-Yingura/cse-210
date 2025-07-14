using System;
using System.Collections.Generic;

class Program
{
    static readonly List<string> _prompts = new()
    {
        "Who was the most interesting person I interacted with today?",
        "What was the best part of my day?",
        "How did I see the hand of the Lord in my life today?",
        "What was the strongest emotion I felt today?",
        "If I had one thing I could do over today, what would it be?"
    };

    static void Main()
    {
        Journal journal = new();
        string choice;

        do
        {
            Console.WriteLine("\n=== Journal Menu ===");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display the journal");
            Console.WriteLine("3. Save the journal to a file");
            Console.WriteLine("4. Load the journal from a file");
            Console.WriteLine("5. Quit");
            Console.Write("Choose an option: ");
            choice = Console.ReadLine()?.Trim();

            switch (choice)
            {
                case "1":
                    WriteEntry(journal);
                    break;
                case "2":
                    journal.DisplayJournal();
                    break;
                case "3":
                    Console.Write("Filename to save (e.g., journal.csv): ");
                    journal.SaveToFile(Console.ReadLine());
                    break;
                case "4":
                    Console.Write("Filename to load: ");
                    journal.LoadFromFile(Console.ReadLine());
                    break;
                case "5":
                    Console.WriteLine("Good‑bye!");
                    break;
                default:
                    Console.WriteLine("Please choose 1‑5.");
                    break;
            }
        } while (choice != "5");
    }

    static void WriteEntry(Journal journal)
    {
        string prompt = _prompts[new Random().Next(_prompts.Count)];
        Console.WriteLine($"\nPrompt: {prompt}");
        Console.Write("Your response: ");
        string response = Console.ReadLine();

        Console.Write("Mood rating 1‑10 (optional, Enter to skip): ");
        string moodText = Console.ReadLine();
        int mood = int.TryParse(moodText, out int m) && m is >= 1 and <= 10 ? m : 0;

        journal.AddEntry(new Entry(prompt, response, mood));
    }
}
