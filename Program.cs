using System;
using System.Globalization;
using System.Text;

namespace SportsSection
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            bool running = true;

            while (running)
            {
                ShowMenu();
                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                    case "2":
                    case "3":
                    case "4":
                    case "5":
                    case "6":
                    case "7":
                    case "8":
                    case "9":
                    case "10":
                        ShowNotImplemented();
                        Pause();
                        break;

                    case "11":
                        ShowAbout();
                        Pause();
                        break;

                    case "12":
                        RunTabulation();
                        Pause();
                        break;

                    case "13":
                        RunAttendanceTable();
                        Pause();
                        break;

                    case "0":
                        running = false;
                        Console.WriteLine("Завершення роботи. До побачення!");
                        break;

                    default:
                        Console.WriteLine("Немає такого пункту. Спробуйте ще раз.");
                        Pause();
                        break;
                }
            }
        }

        static void ShowMenu()
        {
            Console.WriteLine("=== Спортивна секція ===");
            Console.WriteLine("1  — показати всіх вихованців");
            Console.WriteLine("2  — додати вихованця");
            Console.WriteLine("3  — знайти за умовою");
            Console.WriteLine("4  — змінити запис");
            Console.WriteLine("5  — видалити запис");
            Console.WriteLine("6  — зведена таблиця (вікові групи)");
            Console.WriteLine("7  — зберегти у файл");
            Console.WriteLine("8  — завантажити з файлу");
            Console.WriteLine("9  — експорт у CSV");
            Console.WriteLine("10 — довідник видів спорту");
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("11 — довідка про програму");
            Console.WriteLine("12 — табулювання вартості занять");
            Console.WriteLine("13 — таблиця відвідуваності секцій");
            Console.WriteLine("0  — вихід");
            Console.Write("Ваш вибір: ");
        }

        static void Pause()
        {
            Console.WriteLine("\nНатисніть будь-яку клавішу...");
            Console.ReadKey(true);
            Console.Clear();
        }

        static void ShowNotImplemented()
        {
            Console.WriteLine("Цей пункт меню буде реалізовано в наступних роботах.");
        }

        static void ShowAbout()
        {
            Console.WriteLine("=== Довідка про застосунок ===");
            Console.WriteLine("Тема №12: Спортивна секція");
            Console.WriteLine("Сутності: Користувач, Вихованець (головна), Вид спорту (довідник)");
            Console.WriteLine("\nПоля сутності «Вихованець»:");
            Console.WriteLine("  1. Номер (int)");
            Console.WriteLine("  2. Прізвище та ім'я (string)");
            Console.WriteLine("  3. Рік народження (int)");
            Console.WriteLine("  4. Код виду спорту з довідника (int)");
            Console.WriteLine("  5. Розряд (string)");
            Console.WriteLine("  6. Дата вступу (DateTime)");
            Console.WriteLine("  7. Кількість тренувань на тиждень (int)");
        }

        static void RunTabulation()
        {
            Console.WriteLine("=== Розрахунок вартості абонемента залежно від годин на тиждень ===");
            double from = ReadDouble("Введіть початкову кількість годин (0.5 - 20): ", 0.5, 20.0);
            double to = ReadDouble($"Введіть кінцеву кількість годин ({from:F1} - 40): ", from, 40.0);
            double step = ReadDouble("Введіть крок зміни годин (0.1 - 5): ", 0.1, 5.0);

            PrintTabulationTable(from, to, step);
        }

        static void PrintTabulationTable(double from, double to, double step)
        {
            Console.WriteLine("\n(Базова ставка: 400 грн/год на тиждень, знижка 3% за кожну годину інтенсивності)");
            Console.WriteLine($"{"Години/тиж",12} | {"Вартість, грн",15}");
            Console.WriteLine(new string('-', 30));

            for (double hours = from; hours <= to + 1e-9; hours += step)
            {
                double cost = hours * 400.0 * (1.0 - 0.03 * hours);
                Console.WriteLine($"{hours,12:F2} | {cost,15:F2}");
            }
        }

        static void RunAttendanceTable()
        {
            Console.WriteLine("=== Звіт: кількість проведених тренувань по секціях і місяцях ===");
            int rows = ReadInt("Введіть кількість секцій (рядків, від 1 до 15): ", 1, 15);
            int cols = ReadInt("Введіть кількість місяців (стовпців, від 1 до 12): ", 1, 12);

            PrintAttendanceTable(rows, cols);
        }

        static void PrintAttendanceTable(int rows, int cols)
        {
            Console.WriteLine();
            int[] colTotals = new int[cols];
            int grandTotal = 0;

            Console.Write($"{"Секція \\ Місяць",-16}");
            for (int col = 1; col <= cols; col++)
            {
                Console.Write($"{$"Міс. {col}",9}");
            }
            Console.WriteLine($"{"Разом",10}");

            int lineWidth = 16 + cols * 9 + 10;
            Console.WriteLine(new string('-', lineWidth));

            for (int row = 1; row <= rows; row++)
            {
                Console.Write($"{"Секція #" + row,-16}");
                int rowSum = 0;

                for (int col = 1; col <= cols; col++)
                {
                    int trainings = 8 + (row * 2 + col) % 7;
                    rowSum += trainings;
                    colTotals[col - 1] += trainings;
                    Console.Write($"{trainings,9}");
                }

                Console.WriteLine($"{rowSum,10}");
            }

            Console.WriteLine(new string('-', lineWidth));
            Console.Write($"{"Разом",-16}");
            for (int col = 1; col <= cols; col++)
            {
                grandTotal += colTotals[col - 1];
                Console.Write($"{colTotals[col - 1],9}");
            }
            Console.WriteLine($"{grandTotal,10}");
        }

        static int ReadInt(string prompt, int min, int max)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (int.TryParse(input, out int result) && result >= min && result <= max)
                {
                    return result;
                }

                Console.WriteLine($"Помилка: введіть ціле число від {min} до {max}.");
            }
        }

        static double ReadDouble(string prompt, double min, double max)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine()?.Replace(',', '.');

                if (double.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out double result)
                    && result >= min && result <= max)
                {
                    return result;
                }

                Console.WriteLine($"Помилка: введіть дійсне число від {min} до {max}.");
            }
        }
    }
}