using System;

class Program
{
    static void Main()
    {
        // Demander la note à l'utilisateur
        Console.Write("Enter your grade percentage: ");
        string input = Console.ReadLine();
        int grade = int.Parse(input);
        
        string letter = "";
        string sign = "";

        // Déterminer la lettre de la note
        if (grade >= 90)
        {
            letter = "A";
        }
        else if (grade >= 80)
        {
            letter = "B";
        }
        else if (grade >= 70)
        {
            letter = "C";
        }
        else if (grade >= 60)
        {
            letter = "D";
        }
        else
        {
            letter = "F";
        }

        // Ajouter un signe + ou - si nécessaire
        int lastDigit = grade % 10;
        if (lastDigit >= 7 && grade >= 60 && letter != "A")
        {
            sign = "+";
        }
        else if (lastDigit < 3 && letter != "F")
        {
            sign = "-";
        }

        // Gérer les cas spéciaux pour A+, F+ et F-
        if (letter == "A" && sign == "+")
        {
            sign = ""; // Pas de A+
        }
        else if (letter == "F")
        {
            sign = ""; // Pas de F+ ou F-
        }

        // Afficher la note finale
        Console.WriteLine($"Your grade is {letter}{sign}.");

        // Vérifier si l'utilisateur a réussi
        if (grade >= 70)
        {
            Console.WriteLine("Congratulations! You passed the course.");
        }
        else
        {
            Console.WriteLine("Keep working hard! You'll do better next time.");
        }
    }
}
