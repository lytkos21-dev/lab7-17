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
                        Console.WriteLine("Поки не зроблено.");
                        break;

                    case "12":
                        Console.WriteLine("Поки не зроблено.");
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