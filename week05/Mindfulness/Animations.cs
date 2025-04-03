using System;
using System.Threading;

public class Animations
{
    // Affiche un spinner comme animation
    public static void ShowSpinner(int durationInSeconds)
    {
        string[] spinner = { "|", "/", "-", "\\" };
        int counter = 0;

        for (int i = 0; i < durationInSeconds; i++)
        {
            Console.Write(spinner[counter]);
            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop); // Retour à la position précédente
            Thread.Sleep(500);
            counter = (counter + 1) % spinner.Length;
        }
        Console.WriteLine();  // Passe à la ligne suivante
    }

    // Affiche un compte à rebours pendant une pause
    public static void Countdown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.Write($"{i}... ");
            Thread.Sleep(1000); // Pause d'une seconde
        }
        Console.WriteLine("Time's up!");
    }

    // Affiche des points pour simuler une pause visuelle
    public static void ShowDots(int durationInSeconds)
    {
        for (int i = 0; i < durationInSeconds; i++)
        {
            Console.Write(".");
            Thread.Sleep(1000); // Pause d'une seconde
        }
        Console.WriteLine();
    }
}
