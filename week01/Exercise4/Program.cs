using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        List<int> numbers = new List<int>();
        Console.WriteLine("Enter a list of numbers, type 0 when finished.");
        
        while (true)
        {
            Console.Write("Enter number: ");
            int number = int.Parse(Console.ReadLine());
            
            if (number == 0)
                break;
            
            numbers.Add(number);
        }
        
        int sum = 0;
        foreach (int num in numbers)
        {
            sum += num;
        }
        double average = numbers.Count > 0 ? (double)sum / numbers.Count : 0;
        int max = numbers.Count > 0 ? numbers[0] : 0;
        int? minPositive = null;
        
        foreach (int num in numbers)
        {
            if (num > max)
                max = num;
            if (num > 0 && (minPositive == null || num < minPositive))
                minPositive = num;
        }
        
        numbers.Sort();
        
        Console.WriteLine($"The sum is: {sum}");
        Console.WriteLine($"The average is: {average}");
        Console.WriteLine($"The largest number is: {max}");
        if (minPositive != null)
            Console.WriteLine($"The smallest positive number is: {minPositive}");
        
        Console.WriteLine("The sorted list is:");
        foreach (int num in numbers)
        {
            Console.WriteLine(num);
        }
    }
}
