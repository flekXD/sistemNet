using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;
using System.Threading;

class Program
{
    static Mutex mutex = new Mutex();

    static void Main()
    {
        Console.WriteLine("Натисніть пробіл, щоб почати сортування даних...");

        while (Console.ReadKey().Key != ConsoleKey.Spacebar) { }

        int[] numbers;

        // Проектуємо файл в пам'ять
        try
        {
            using (BinaryReader reader = new BinaryReader(File.Open("C:\\Users\\Fleks\\source\\repos\\lab_1_1\\lab_1_1\\data.dat", FileMode.Open)))
            {
                numbers = new int[reader.BaseStream.Length / sizeof(int)];

                for (int i = 0; i < numbers.Length; i++)
                {
                    numbers[i] = reader.ReadInt32();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка при читанні файлу: {ex.Message}");
            return;
        }

        // Сортуємо масив
        try
        {
            mutex.WaitOne();

            using (MemoryMappedFile mmf = MemoryMappedFile.CreateNew("sort_iterations", 10000))
            {
                for (int i = 0; i < numbers.Length - 1; i++)
                {
                    for (int j = 0; j < numbers.Length - i - 1; j++)
                    {
                        if (numbers[j] > numbers[j + 1])
                        {
                            // Обмін елементів
                            int temp = numbers[j];
                            numbers[j] = numbers[j + 1];
                            numbers[j + 1] = temp;
                        }
                    }

                    // Записуємо поточний стан масиву після кожної ітерації
                    using (MemoryMappedViewStream stream = mmf.CreateViewStream())
                    {
                        BinaryWriter writer = new BinaryWriter(stream);
                        writer.Write($"Iteration {i + 1}: {string.Join(", ", numbers)}\n");
                    }

                    // Затримка на 1 секунду
                    Thread.Sleep(1000);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка при сортуванні: {ex.Message}");
        }
        finally
        {
            mutex.ReleaseMutex();
        }

        Console.WriteLine("Робота завершена");
    }
}
