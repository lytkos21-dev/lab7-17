using System;
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
                Console.WriteLine("11 — довідка про програму (Лаб 7)");
                Console.WriteLine("12 — табулювання вартості занять (Лаб 7)");
                Console.WriteLine("13 — таблиця відвідуваності секцій (Лаб 7)");
                Console.WriteLine("0  — вихід");
                Console.Write("Ваш вибір: ");

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
                        Console.WriteLine("Цей пункт меню буде реалізовано в наступних роботах.");
                        break;

                    case "11":
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
                        break;

                    case "12":
                        Console.WriteLine("=== Розрахунок вартості абонемента залежно від годин на тиждень ===");
                        Console.WriteLine("(Базова ставка: 400 грн/год на тиждень, знижка 3% за кожну годину інтенсивності)\n");

                        double from = 1.0;
                        double to = 5.0;
                        double step = 0.5;

                        Console.WriteLine($"{"Години/тиж",12} | {"Вартість, грн",15}");
                        Console.WriteLine(new string('-', 30));

                        for (double hours = from; hours <= to + 1e-9; hours += step)
                        {
                            double cost = hours * 400.0 * (1.0 - 0.03 * hours);
                            Console.WriteLine($"{hours,12:F2} | {cost,15:F2}");
                        }
                        break;

                    case "13":
                        Console.WriteLine("Поки не зроблено.");
                        break;

                    case "0":
                        running = false;
                        Console.WriteLine("Завершення роботи. До побачення!");
                        continue;

                    default:
                        Console.WriteLine("Немає такого пункту. Спробуйте ще раз.");
                        break;
                }

                Console.WriteLine("\nНатисніть будь-яку клавішу...");
                Console.ReadKey(true);
                Console.Clear();
            }
        }
    }
}