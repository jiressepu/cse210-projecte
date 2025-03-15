using System;

class Program
{
    static void Main()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;

        Journal journal = new Journal();

        while (true)
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Add a new entry");
            Console.WriteLine("2. Display journal entries");
            Console.WriteLine("3. Save journal to CSV");
            Console.WriteLine("4. Load journal from CSV");
            Console.WriteLine("5. Display a random past entry");
            Console.WriteLine("6. Exit");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    journal.AddEntry();
                    break;
                case "2":
                    journal.DisplayEntries();
                    break;
                case "3":
                    journal.SaveToCsv();
                    break;
                case "4":
                    journal.LoadFromCsv();
                    break;
                case "5":
                    journal.DisplayRandomEntry();
                    break;
                case "6":
                    return;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
    }
}
