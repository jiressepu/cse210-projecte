using System;
using System.Collections.Generic;
using System.Linq;

class Reference
{
    public string Book { get; }
    public int Chapter { get; }
    public int StartVerse { get; }
    public int? EndVerse { get; }

    public Reference(string book, int chapter, int startVerse, int? endVerse = null)
    {
        Book = book;
        Chapter = chapter;
        StartVerse = startVerse;
        EndVerse = endVerse;
    }

    public override string ToString()
    {
        return EndVerse == null ? $"{Book} {Chapter}:{StartVerse}" : $"{Book} {Chapter}:{StartVerse}-{EndVerse}";
    }
}

class Word
{
    private string text;
    private bool isHidden;

    public Word(string text)
    {
        this.text = text;
        isHidden = false;
    }

    public void Hide()
    {
        isHidden = true;
    }

    public bool IsHidden()
    {
        return isHidden;
    }

    public override string ToString()
    {
        return isHidden ? new string('_', text.Length) : text;
    }
}

class Scripture
{
    private Reference reference;
    private List<Word> words;
    private Random random;

    public Scripture(Reference reference, string text)
    {
        this.reference = reference;
        words = text.Split(' ').Select(word => new Word(word)).ToList();
        random = new Random();
    }

    public void HideWords(int count)
    {
        var visibleWords = words.Where(w => !w.IsHidden()).ToList();
        if (visibleWords.Count == 0) return;

        for (int i = 0; i < count && visibleWords.Count > 0; i++)
        {
            int index = random.Next(visibleWords.Count);
            visibleWords[index].Hide();
            visibleWords.RemoveAt(index);
        }
    }

    public bool AllWordsHidden()
    {
        return words.All(w => w.IsHidden());
    }

    public void Display()
    {
        Console.Clear();
        Console.WriteLine(reference);
        Console.WriteLine(string.Join(" ", words));
    }
}

class Program
{
    static void Main()
    {
        // Créer des écritures directement dans le code
        List<Scripture> scriptures = new List<Scripture>
        {
            new Scripture(new Reference("1 Nephi", 1, 1), "I, Nephi, having been born of goodly parents, therefore I was taught somewhat in all the learning of my father."),
            new Scripture(new Reference("Mosiah", 2, 17), "When ye are in the service of your fellow beings ye are only in the service of your God."),
            new Scripture(new Reference("John", 3, 16), "For God so loved the world that he gave his one and only Son, that whoever believes in him shall not perish but have eternal life."),
            new Scripture(new Reference("Proverbs", 3, 5), "Trust in the Lord with all your heart and lean not on your own understanding; in all your ways submit to him, and he will make your paths straight.")
        };

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Choose a scripture to display:");
            Console.WriteLine("1. 1 Nephi 1:1");
            Console.WriteLine("2. Mosiah 2:17");
            Console.WriteLine("3. John 3:16");
            Console.WriteLine("4. Proverbs 3:5");
            Console.WriteLine("Press 'q' to quit.");
            
            string input = Console.ReadLine().ToLower();

            if (input == "q")
                break;

            Scripture scriptureToDisplay = null;

            switch (input)
            {
                case "1":
                    scriptureToDisplay = scriptures[0];  // 1 Nephi 1:1
                    break;
                case "2":
                    scriptureToDisplay = scriptures[1];  // Mosiah 2:17
                    break;
                case "3":
                    scriptureToDisplay = scriptures[2];  // John 3:16
                    break;
                case "4":
                    scriptureToDisplay = scriptures[3];  // Proverbs 3:5
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    continue;
            }

            if (scriptureToDisplay != null)
            {
                Random random = new Random();
                Console.WriteLine("\nChoose a difficulty level: 1 (Easy), 2 (Medium), 3 (Hard)");
                if (!int.TryParse(Console.ReadLine(), out int difficulty) || difficulty < 1 || difficulty > 3)
                {
                    Console.WriteLine("Invalid input. Defaulting to Easy.");
                    difficulty = 1;
                }

                int wordsToHide = difficulty == 1 ? 2 : difficulty == 2 ? 4 : 6;

                while (true)
                {
                    scriptureToDisplay.Display();
                    Console.WriteLine("\nPress Enter to hide words, type 'recite' to test, or 'quit' to exit.");
                    string actionInput = Console.ReadLine().ToLower();

                    if (actionInput == "quit") break;

                    if (actionInput == "recite")
                    {
                        Console.WriteLine("\nTry to rewrite the scripture:");
                        string userResponse = Console.ReadLine();
                        Console.WriteLine(userResponse.Trim().Equals(scriptureToDisplay.ToString(), StringComparison.OrdinalIgnoreCase) ? "Well done!" : "Try again!");
                    }
                    else
                    {
                        scriptureToDisplay.HideWords(wordsToHide);
                        if (scriptureToDisplay.AllWordsHidden())
                        {
                            scriptureToDisplay.Display();
                            Console.WriteLine("\nAll words are hidden. Program finished.");
                            break;
                        }
                    }
                }
            }
        }
    }
}
