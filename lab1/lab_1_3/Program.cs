using System;
using System.IO;

class Program
{
    static void Main()
    {
        Random rand = new Random();
        int[] numbers = new int[rand.Next(20, 31)]; // Генеруємо випадкову кількість чисел від 20 до 30

        for (int i = 0; i < numbers.Length; i++)
        {
            numbers[i] = rand.Next(10, 101); // Генеруємо випадкове число від 10 до 100
        }

        // Записуємо числа у файл data.dat
        using (BinaryWriter writer = new BinaryWriter(File.Open("data.dat", FileMode.Create)))
        {
            foreach (int number in numbers)
            {
                writer.Write(number);
            }
        }

        Console.WriteLine("Числа були успішно згенеровані та записані у файл data.dat.");
    }
}
