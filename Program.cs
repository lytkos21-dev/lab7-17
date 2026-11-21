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

            const int Capacity = 100;
            double[] trainingHours = new double[Capacity];
            int trainingCount = 0;

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

                    case "14":
                        RunAgeCalculation();
                        Pause();
                        break;

                    case "15":
                        Console.WriteLine("=== Додавання тривалості тренування ===");
                        double hours = ReadDouble("Введіть тривалість тренування (0.5 - 12 год): ", 0.5, 12.0);
                        AddValue(trainingHours, ref trainingCount, hours, Capacity);
                        Console.WriteLine($"Поточна кількість записів у Main: {trainingCount}");
                        Pause();
                        break;

                    case "16":
                        Console.WriteLine("=== Список зафіксованих тренувань ===");
                        PrintValues(trainingHours, trainingCount);
                        Pause();
                        break;

                    case "17":
                        Console.WriteLine("=== Статистика тренувань ===");
                        if (trainingCount == 0)
                        {
                            Console.WriteLine("Масив порожній. Спочатку додайте хоча б одне значення.");
                        }
                        else
                        {
                            FindMax(trainingHours, trainingCount, out double maxVal, out int maxIdx);
                            CalculateStatistics(trainingHours, trainingCount, out double sum, out double avg);

                            Console.WriteLine($"Кількість записів:    {trainingCount}");
                            Console.WriteLine($"Загальна тривалість:  {sum:F2} год");
                            Console.WriteLine($"Середня тривалість:   {avg:F2} год");
                            Console.WriteLine($"Найдовше тренування:  {maxVal:F2} год (запис №{maxIdx + 1})");
                        }
                        Pause();
                        break;

                    case "18":
                        Console.WriteLine("=== Дослід: значимий тип (int) та посилальний тип (масив) ===");
                        int sessions = 5;
                        Console.WriteLine($"1. Значимий тип (int): до виклику методу sessions = {sessions}");
                        TryModifyInt(sessions);
                        Console.WriteLine($"   Після виклику TryModifyInt(sessions): sessions = {sessions} (не змінилося, передалась копія)");

                        double[] sample = { 1.5, 2.0, 2.5 };
                        Console.WriteLine($"\n2. Посилальний тип (double[]): до виклику методу sample[0] = {sample[0]:F1}");
                        ModifyArrayElement(sample);
                        Console.WriteLine($"   Після виклику ModifyArrayElement(sample): sample[0] = {sample[0]:F1} (змінилося, передалась адреса)");
                        Pause();
                        break;

                    case "19":
                        Console.WriteLine("=== Видалення запису за номером ===");
                        PrintValues(trainingHours, trainingCount);
                        int recordNumber = ReadInt("\nВведіть номер запису для видалення: ", -10, Capacity + 10);
                        RemoveAt(trainingHours, ref trainingCount, recordNumber - 1);
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
            Console.WriteLine("14 — розрахунок віку вихованця");
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("15 — додати тривалість тренування ");
            Console.WriteLine("16 — показати список тренувань ");
            Console.WriteLine("17 — показати статистику та максимум");
            Console.WriteLine("18 — дослід: значимий та посилальний типи");
            Console.WriteLine("19 — видалити запис за номером");
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
                double cost = CalculateSubscriptionCost(hours);
                Console.WriteLine($"{hours,12:F2} | {cost,15:F2}");
            }
        }

        static double CalculateSubscriptionCost(double hoursPerWeek)
        {
            return hoursPerWeek * 400.0 * (1.0 - 0.03 * hoursPerWeek);
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

        static void RunAgeCalculation()
        {
            Console.WriteLine("=== Розрахунок віку вихованця ===");
            int currentYear = DateTime.Now.Year;
            int birthYear = ReadInt($"Введіть рік народження (1950 - {currentYear}): ", 1950, currentYear);

            int age = CalculateTraineeAge(birthYear, currentYear);
            Console.WriteLine($"Вік вихованця: {age} р.");
        }

        static int CalculateTraineeAge(int birthYear, int currentYear)
        {
            return currentYear - birthYear;
        }


        static void AddValue(double[] values, ref int count, double newValue, int capacity)
        {
            if (count >= capacity)
            {
                Console.WriteLine("Помилка: масив заповнений, місця більше немає.");
                return;
            }

            values[count] = newValue;
            count++;
            Console.WriteLine("Значення успішно додано.");
        }

        static void PrintValues(double[] values, int count)
        {
            if (count == 0)
            {
                Console.WriteLine("Масив порожній. Немає даних для відображення.");
                return;
            }

            Console.WriteLine($"{"№",4} | {"Тривалість, год",16}");
            Console.WriteLine(new string('-', 23));

            for (int i = 0; i < count; i++)
            {
                Console.WriteLine($"{i + 1,4} | {values[i],16:F2}");
            }
        }

        static void FindMax(double[] values, int count, out double maxValue, out int maxIndex)
        {
            if (count <= 0)
            {
                maxValue = 0;
                maxIndex = -1;
                return;
            }

            maxValue = values[0];
            maxIndex = 0;

            for (int i = 1; i < count; i++)
            {
                if (values[i] > maxValue)
                {
                    maxValue = values[i];
                    maxIndex = i;
                }
            }
        }

        static void CalculateStatistics(double[] values, int count, out double sum, out double average)
        {
            sum = 0;
            for (int i = 0; i < count; i++)
            {
                sum += values[i];
            }

            average = count > 0 ? sum / count : 0;
        }

        static void RemoveAt(double[] values, ref int count, int index)
        {
            if (index < 0 || index >= count)
            {
                Console.WriteLine("Помилка: некоректний номер запису. Видалення не виконано.");
                return;
            }

            for (int i = index; i < count - 1; i++)
            {
                values[i] = values[i + 1];
            }

            values[count - 1] = 0;
            count--;
            Console.WriteLine($"Запис №{index + 1} видалено.");
        }

        static void TryModifyInt(int value)
        {
            value += 100;
        }

        static void ModifyArrayElement(double[] values)
        {
            if (values.Length > 0)
            {
                values[0] += 10.0;
            }
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