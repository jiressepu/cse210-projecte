using System;

class Program
{
    static void Main()
    {
        // Demander le prénom
        Console.Write("What is your first name? ");
        string firstName = Console.ReadLine();

        // Demander le nom de famille
        Console.Write("What is your last name? ");
        string lastName = Console.ReadLine();

        // Afficher le message formaté
        Console.WriteLine($"Your name is {lastName}, {firstName} {lastName}.");
    }
}