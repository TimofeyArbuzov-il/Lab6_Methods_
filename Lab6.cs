using System;
using System.IO;

namespace Lab6
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("ЛАБОРАТОРНАЯ РАБОТА №6 — МАССИВЫ");
            Console.WriteLine("1 — Подсчёт гласных и согласных (Упр. 6.1)");
            Console.WriteLine("2 — Умножение матриц (Упр. 6.2)");
            Console.Write("\nВведите номер задания: ");

            int choice = int.Parse(Console.ReadLine() ?? "0");
            switch (choice)
            {
                case 1: Ex6_1(args); break;
                case 2: Ex6_2(); break;
                default: Console.WriteLine("Неверный выбор."); break;
            }
        }

        // УПРАЖНЕНИЕ 6.1 — Подсчёт гласных и согласных. Пример ввода: (глас.5 согл.7)
        static void Ex6_1(string[] args)
        {
            Console.WriteLine("\nУПРАЖНЕНИЕ 6.1 — Подсчёт гласных и согласных в файле");

            string fileName;
            if (args.Length > 0)
                fileName = args[0];
            else
            {
                Console.Write("Введите имя текстового файла: ");
                fileName = Console.ReadLine();
            }

            if (!File.Exists(fileName))
            {
                Console.WriteLine("Ошибка: файл не найден!");
                return;
            }
          
            string text = File.ReadAllText(fileName);
            char[] chars = text.ToCharArray();

            CountLetters(chars, out int vowels, out int consonants);

            Console.WriteLine($"\nГласных букв: {vowels}");
            Console.WriteLine($"Согласных букв: {consonants}");
        }

        static void CountLetters(char[] chars, out int vowels, out int consonants)
        {
            vowels = consonants = 0;
            char[] vowelChars = {
                'a','e','i','o','u','y','A','E','I','O','U','Y',
                'а','е','ё','и','о','у','ы','э','ю','я',
                'А','Е','Ё','И','О','У','Ы','Э','Ю','Я'
            };

            foreach (char c in chars)
            {
                if (char.IsLetter(c))
                {
                    if (Array.IndexOf(vowelChars, c) >= 0)
                        vowels++;
                    else
                        consonants++;
                }
            }
        }

        // УПРАЖНЕНИЕ 6.2 — Умножение двух матриц Пример ввода: (ввестиномер задания 1 или 2)
        
        static void Ex6_2()
        {
            Console.WriteLine("\nУПРАЖНЕНИЕ 6.2 — Умножение матриц");

            int[,] A = { { 1, 2, 3 }, { 4, 5, 6 } };
            int[,] B = { { 7, 8 }, { 9, 10 }, { 11, 12 } };

            Console.WriteLine("\nМатрица A:");
            PrintMatrix(A);

            Console.WriteLine("Матрица B:");
            PrintMatrix(B);

            int[,] C = MultiplyMatrices(A, B);

            Console.WriteLine("Результат A × B:");
            PrintMatrix(C);
        }

        static void PrintMatrix(int[,] m)
        {
            for (int i = 0; i < m.GetLength(0); i++)
            {
                for (int j = 0; j < m.GetLength(1); j++)
                    Console.Write($"{m[i, j],4}");
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        static int[,] MultiplyMatrices(int[,] A, int[,] B)
        {
            int rowsA = A.GetLength(0), colsA = A.GetLength(1);
            int rowsB = B.GetLength(0), colsB = B.GetLength(1);

            if (colsA != rowsB)
                throw new Exception("Невозможно перемножить матрицы: несовпадение размерностей.");

            int[,] result = new int[rowsA, colsB];

            for (int i = 0; i < rowsA; i++)
                for (int j = 0; j < colsB; j++)
                    for (int k = 0; k < colsA; k++)
                        result[i, j] += A[i, k] * B[k, j];

            return result;
        }
    }
}
