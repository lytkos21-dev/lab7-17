using System;
using System.Globalization;
using System.Text;

namespace TaxiService
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            bool running = true;

            const int Capacity = 100;
            int[] ids = new int[Capacity];
            string[] drivers = new string[Capacity];
            DateTime[] tripDates = new DateTime[Capacity];
            string[] pickupAddresses = new string[Capacity];
            string[] destinationAddresses = new string[Capacity];
            double[] distances = new double[Capacity];
            double[] ratesPerKm = new double[Capacity];
            int[] paymentMethodIds = new int[Capacity];
            int tripsCount = 0;
            int nextTripId = 1;

            double[] sampleDistances = new double[Capacity];
            int sampleDistancesCount = 0;

            while (running)
            {
                ShowMenu();
                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        ShowAllTrips(ids, drivers, tripDates, pickupAddresses, destinationAddresses, distances, ratesPerKm, paymentMethodIds, tripsCount);
                        Pause();
                        break;
                    case "2":
                        AddTrip(ids, drivers, tripDates, pickupAddresses, destinationAddresses, distances, ratesPerKm, paymentMethodIds, ref tripsCount, ref nextTripId, Capacity);
                        Pause();
                        break;
                    case "3":
                        SearchTrips(ids, drivers, tripDates, pickupAddresses, destinationAddresses, distances, ratesPerKm, paymentMethodIds, tripsCount);
                        Pause();
                        break;
                    case "4":
                        EditTrip(ids, drivers, tripDates, pickupAddresses, destinationAddresses, distances, ratesPerKm, paymentMethodIds, tripsCount);
                        Pause();
                        break;
                    case "5":
                        DeleteTrip(ids, drivers, tripDates, pickupAddresses, destinationAddresses, distances, ratesPerKm, paymentMethodIds, ref tripsCount);
                        Pause();
                        break;
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
                        RunDriversTable();
                        Pause();
                        break;
                    case "14":
                        RunSingleTripCostCalculation();
                        Pause();
                        break;
                    case "15":
                        Console.WriteLine("=== Додавання відстані поїздки ===");
                        double dist = ReadDouble("Введіть відстань поїздки (0.5 - 500 км): ", 0.5, 500.0);
                        AddValue(sampleDistances, ref sampleDistancesCount, dist, Capacity);
                        Console.WriteLine($"Поточна кількість записів у Main: {sampleDistancesCount}");
                        Pause();
                        break;
                    case "16":
                        Console.WriteLine("=== Список зафіксованих відстаней ===");
                        PrintValues(sampleDistances, sampleDistancesCount);
                        Pause();
                        break;
                    case "17":
                        Console.WriteLine("=== Статистика відстаней ===");
                        if (sampleDistancesCount == 0)
                        {
                            Console.WriteLine("Масив порожній. Спочатку додайте хоча б одне значення.");
                        }
                        else
                        {
                            FindMax(sampleDistances, sampleDistancesCount, out double maxVal, out int maxIdx);
                            CalculateStatistics(sampleDistances, sampleDistancesCount, out double sum, out double avg);
                            Console.WriteLine($"Кількість записів:    {sampleDistancesCount}");
                            Console.WriteLine($"Загальний пробіг:     {sum:F2} км");
                            Console.WriteLine($"Середня відстань:     {avg:F2} км");
                            Console.WriteLine($"Найдовша поїздка:     {maxVal:F2} км (запис №{maxIdx + 1})");
                        }
                        Pause();
                        break;
                    case "18":
                        Console.WriteLine("=== Дослід: значимий тип (int) та посилальний тип (масив) ===");
                        int orders = 5;
                        Console.WriteLine($"1. Значимий тип (int): до виклику методу orders = {orders}");
                        TryModifyInt(orders);
                        Console.WriteLine($"   Після виклику TryModifyInt(orders): orders = {orders} (не змінилося, передалась копія)");
                        double[] sample = { 3.5, 7.0, 12.2 };
                        Console.WriteLine($"\n2. Посилальний тип (double[]): до виклику методу sample[0] = {sample[0]:F1}");
                        ModifyArrayElement(sample);
                        Console.WriteLine($"   Після виклику ModifyArrayElement(sample): sample[0] = {sample[0]:F1} (змінилося, передалась адреса)");
                        Pause();
                        break;
                    case "19":
                        Console.WriteLine("=== Видалення запису за номером ===");
                        PrintValues(sampleDistances, sampleDistancesCount);
                        int recordNumber = ReadInt("\nВведіть номер запису для видалення: ", -10, Capacity + 10);
                        RemoveAt(sampleDistances, ref sampleDistancesCount, recordNumber - 1);
                        Pause();
                        break;
                    case "20":
                        SeedTestData(ids, drivers, tripDates, pickupAddresses, destinationAddresses, distances, ratesPerKm, paymentMethodIds, ref tripsCount, ref nextTripId, Capacity);
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
            Console.WriteLine("=== Служба таксі ===");
            Console.WriteLine("1  — показати всі поїздки");
            Console.WriteLine("2  — додати поїздку");
            Console.WriteLine("3  — знайти за умовою");
            Console.WriteLine("4  — змінити запис");
            Console.WriteLine("5  — видалити запис");
            Console.WriteLine("6  — зведена таблиця");
            Console.WriteLine("7  — зберегти у файл");
            Console.WriteLine("8  — завантажити з файлу");
            Console.WriteLine("9  — експорт у CSV");
            Console.WriteLine("10 — довідник способів оплати");
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("11 — довідка про програму");
            Console.WriteLine("12 — табулювання вартості поїздки");
            Console.WriteLine("13 — таблиця замовлень водіїв по днях");
            Console.WriteLine("14 — розрахунок вартості однієї поїздки");
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("15 — додати відстань поїздки (Лаб 9)");
            Console.WriteLine("16 — показати список відстаней (Лаб 9)");
            Console.WriteLine("17 — показати статистику та максимум (Лаб 9)");
            Console.WriteLine("18 — дослід: значимий та посилальний типи (Лаб 9)");
            Console.WriteLine("19 — видалити запис за номером (Лаб 9)");
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("20 — заповнити тестовими поїздками (Лаб 10)");
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

        static int FindIndexById(int[] ids, int count, int id)
        {
            for (int i = 0; i < count; i++)
            {
                if (ids[i] == id)
                {
                    return i;
                }
            }
            return -1;
        }

        static void PrintTripTableHeader()
        {
            Console.WriteLine($"{"№",4} {"Водій",-18} {"Дата й час",-17} {"Звідки",-18} {"Куди",-18} {"Км",7} {"Тариф",8} {"Опл.",5}");
            Console.WriteLine(new string('-', 102));
        }

        static void PrintTripRow(int id, string driver, DateTime tripDate, string pickup, string destination, double distance, double rate, int paymentId)
        {
            Console.WriteLine($"{id,4} {driver,-18} {tripDate,-17:dd.MM.yyyy HH:mm} {pickup,-18} {destination,-18} {distance,7:F1} {rate,8:F2} {paymentId,5}");
        }

        static void SearchTrips(int[] ids, string[] drivers, DateTime[] tripDates, string[] pickupAddresses, string[] destinationAddresses, double[] distances, double[] ratesPerKm, int[] paymentMethodIds, int count)
        {
            Console.WriteLine("=== Пошук поїздок за умовою ===");
            if (count == 0)
            {
                Console.WriteLine("Сховище порожнє. Немає записів для пошуку.");
                return;
            }

            Console.WriteLine("1 — за частиною тексту (водій або адреса)");
            Console.WriteLine("2 — за діапазоном відстані (км)");
            int mode = ReadInt("Оберіть спосіб пошуку (1 - 2): ", 1, 2);
            Console.WriteLine();

            int foundCount = 0;

            if (mode == 1)
            {
                string query = ReadText("Введіть частину ПІБ водія або адреси: ");
                Console.WriteLine();

                for (int i = 0; i < count; i++)
                {
                    if (drivers[i].Contains(query) || pickupAddresses[i].Contains(query) || destinationAddresses[i].Contains(query))
                    {
                        if (foundCount == 0)
                        {
                            PrintTripTableHeader();
                        }
                        PrintTripRow(ids[i], drivers[i], tripDates[i], pickupAddresses[i], destinationAddresses[i], distances[i], ratesPerKm[i], paymentMethodIds[i]);
                        foundCount++;
                    }
                }
            }
            else
            {
                double from = ReadDouble("Мінімальна відстань у км (0 - 500): ", 0.0, 500.0);
                double to = ReadDouble("Максимальна відстань у км (0 - 500): ", 0.0, 500.0);

                if (from > to)
                {
                    Console.WriteLine("Помилка: нижня межа не може бути більшою за верхню.");
                    return;
                }

                Console.WriteLine();

                for (int i = 0; i < count; i++)
                {
                    if (distances[i] >= from && distances[i] <= to)
                    {
                        if (foundCount == 0)
                        {
                            PrintTripTableHeader();
                        }
                        PrintTripRow(ids[i], drivers[i], tripDates[i], pickupAddresses[i], destinationAddresses[i], distances[i], ratesPerKm[i], paymentMethodIds[i]);
                        foundCount++;
                    }
                }
            }

            if (foundCount == 0)
            {
                Console.WriteLine("За вашим запитом нічого не знайдено.");
            }
            else
            {
                Console.WriteLine(new string('-', 102));
                Console.WriteLine($"Знайдено записів: {foundCount}.");
            }
        }

        static void EditTrip(int[] ids, string[] drivers, DateTime[] tripDates, string[] pickupAddresses, string[] destinationAddresses, double[] distances, double[] ratesPerKm, int[] paymentMethodIds, int count)
        {
            Console.WriteLine("=== Зміна даних поїздки ===");
            if (count == 0)
            {
                Console.WriteLine("Сховище порожнє.");
                return;
            }

            int id = ReadInt("Введіть номер поїздки для зміни: ", 1, int.MaxValue);
            int index = FindIndexById(ids, count, id);

            if (index == -1)
            {
                Console.WriteLine($"Поїздки №{id} немає.");
                return;
            }

            Console.WriteLine("\nПоточні дані:");
            PrintTripTableHeader();
            PrintTripRow(ids[index], drivers[index], tripDates[index], pickupAddresses[index], destinationAddresses[index], distances[index], ratesPerKm[index], paymentMethodIds[index]);

            Console.WriteLine("\nВведіть нові значення (порожній Enter залишає поле без змін):");

            Console.Write($"Водій [{drivers[index]}]: ");
            string driverInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(driverInput))
            {
                drivers[index] = driverInput.Trim();
            }

            while (true)
            {
                Console.Write($"Дата й час [{tripDates[index]:dd.MM.yyyy HH:mm}]: ");
                string dateInput = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(dateInput))
                {
                    break;
                }

                if (DateTime.TryParseExact(dateInput.Trim(), "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime newDate)
                    && newDate >= new DateTime(2020, 1, 1) && newDate <= DateTime.Now.AddDays(1))
                {
                    tripDates[index] = newDate;
                    break;
                }

                Console.WriteLine("Помилка: введіть дату й час у форматі дд.мм.рррр гг:хх або натисніть Enter.");
            }

            Console.Write($"Адреса подачі [{pickupAddresses[index]}]: ");
            string pickupInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(pickupInput))
            {
                pickupAddresses[index] = pickupInput.Trim();
            }

            Console.Write($"Адреса призначення [{destinationAddresses[index]}]: ");
            string destInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(destInput))
            {
                destinationAddresses[index] = destInput.Trim();
            }

            while (true)
            {
                Console.Write($"Відстань у км [{distances[index]:F1}]: ");
                string distInput = Console.ReadLine()?.Replace(',', '.');
                if (string.IsNullOrWhiteSpace(distInput))
                {
                    break;
                }

                if (double.TryParse(distInput.Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out double newDist)
                    && newDist >= 0.5 && newDist <= 500.0)
                {
                    distances[index] = newDist;
                    break;
                }

                Console.WriteLine("Помилка: введіть число від 0.5 до 500 або натисніть Enter.");
            }

            while (true)
            {
                Console.Write($"Тариф за км [{ratesPerKm[index]:F2}]: ");
                string rateInput = Console.ReadLine()?.Replace(',', '.');
                if (string.IsNullOrWhiteSpace(rateInput))
                {
                    break;
                }

                if (double.TryParse(rateInput.Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out double newRate)
                    && newRate >= 5.0 && newRate <= 200.0)
                {
                    ratesPerKm[index] = newRate;
                    break;
                }

                Console.WriteLine("Помилка: введіть число від 5 до 200 або натисніть Enter.");
            }

            while (true)
            {
                Console.Write($"Код способу оплати (1-3) [{paymentMethodIds[index]}]: ");
                string payInput = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(payInput))
                {
                    break;
                }

                if (int.TryParse(payInput.Trim(), out int newPay) && newPay >= 1 && newPay <= 3)
                {
                    paymentMethodIds[index] = newPay;
                    break;
                }

                Console.WriteLine("Помилка: введіть ціле число від 1 до 3 або натисніть Enter.");
            }

            Console.WriteLine($"\nПоїздку №{id} успішно оновлено.");
        }

        static void DeleteTrip(int[] ids, string[] drivers, DateTime[] tripDates, string[] pickupAddresses, string[] destinationAddresses, double[] distances, double[] ratesPerKm, int[] paymentMethodIds, ref int count)
        {
            Console.WriteLine("=== Видалення поїздки ===");
            if (count == 0)
            {
                Console.WriteLine("Сховище порожнє.");
                return;
            }

            int id = ReadInt("Введіть номер поїздки для видалення: ", 1, int.MaxValue);
            int index = FindIndexById(ids, count, id);

            if (index == -1)
            {
                Console.WriteLine($"Поїздки №{id} немає.");
                return;
            }

            Console.Write($"Справді видалити поїздку №{id} (водій: {drivers[index]})? (так/ні): ");
            if (Console.ReadLine()?.Trim().ToLower() != "так")
            {
                Console.WriteLine("Видалення скасовано.");
                return;
            }

            for (int i = index; i < count - 1; i++)
            {
                ids[i] = ids[i + 1];
                drivers[i] = drivers[i + 1];
                tripDates[i] = tripDates[i + 1];
                pickupAddresses[i] = pickupAddresses[i + 1];
                destinationAddresses[i] = destinationAddresses[i + 1];
                distances[i] = distances[i + 1];
                ratesPerKm[i] = ratesPerKm[i + 1];
                paymentMethodIds[i] = paymentMethodIds[i + 1];
            }

            count--;
            Console.WriteLine($"Поїздку №{id} видалено. Лишилося записів: {count}.");
        }

        static void ShowAllTrips(int[] ids, string[] drivers, DateTime[] tripDates, string[] pickupAddresses, string[] destinationAddresses, double[] distances, double[] ratesPerKm, int[] paymentMethodIds, int count)
        {
            Console.WriteLine("=== Список поїздок служби таксі ===");
            if (count == 0)
            {
                Console.WriteLine("Записів поки немає.");
                return;
            }

            PrintTripTableHeader();

            for (int i = 0; i < count; i++)
            {
                PrintTripRow(ids[i], drivers[i], tripDates[i], pickupAddresses[i], destinationAddresses[i], distances[i], ratesPerKm[i], paymentMethodIds[i]);
            }

            double totalRevenue = CalculateTotalRevenue(distances, ratesPerKm, count);
            double totalDistance = CalculateTotalDistance(distances, count);

            Console.WriteLine(new string('-', 102));
            Console.WriteLine($"Записів: {count}. Загальний пробіг: {totalDistance:F1} км. Загальна вартість поїздок: {totalRevenue:F2} грн.");
        }

        static double CalculateTotalRevenue(double[] distances, double[] ratesPerKm, int count)
        {
            double total = 0;
            for (int i = 0; i < count; i++)
            {
                total += distances[i] * ratesPerKm[i];
            }
            return total;
        }

        static double CalculateTotalDistance(double[] distances, int count)
        {
            double total = 0;
            for (int i = 0; i < count; i++)
            {
                total += distances[i];
            }
            return total;
        }

        static void AddTrip(int[] ids, string[] drivers, DateTime[] tripDates, string[] pickupAddresses, string[] destinationAddresses, double[] distances, double[] ratesPerKm, int[] paymentMethodIds, ref int count, ref int nextId, int capacity)
        {
            Console.WriteLine("=== Додавання нової поїздки ===");
            if (count >= capacity)
            {
                Console.WriteLine("Сховище заповнене (максимум 100 записів), видаліть щось зайве.");
                return;
            }

            string driver = ReadText("Водій (прізвище та ім'я): ");
            DateTime tripDate = ReadDateTime("Дата й час поїздки (дд.мм.рррр гг:хх): ", new DateTime(2020, 1, 1), DateTime.Now.AddDays(1));
            string pickup = ReadText("Адреса подачі: ");
            string destination = ReadText("Адреса призначення: ");
            double distance = ReadDouble("Відстань у км (0.5 - 500): ", 0.5, 500.0);
            double rate = ReadDouble("Тариф за км, грн (5 - 200): ", 5.0, 200.0);
            int paymentId = ReadInt("Код способу оплати (1 - Готівка, 2 - Картка, 3 - Apple/Google Pay): ", 1, 3);

            ids[count] = nextId;
            drivers[count] = driver;
            tripDates[count] = tripDate;
            pickupAddresses[count] = pickup;
            destinationAddresses[count] = destination;
            distances[count] = distance;
            ratesPerKm[count] = rate;
            paymentMethodIds[count] = paymentId;

            count++;
            nextId++;

            Console.WriteLine($"\nДодано поїздку №{ids[count - 1]} (водій: {driver}).");
        }

        static void SeedTestData(int[] ids, string[] drivers, DateTime[] tripDates, string[] pickupAddresses, string[] destinationAddresses, double[] distances, double[] ratesPerKm, int[] paymentMethodIds, ref int count, ref int nextId, int capacity)
        {
            Console.WriteLine("=== Заповнення тестовими даними ===");
            string[] sampleDrivers = { "Ковач Василь", "Мельник Андрій", "Бондар Олег", "Ткачук Ігор", "Савченко Тарас", "Гнатюк Роман" };
            DateTime[] sampleDates =
            {
                new DateTime(2026, 11, 20, 8, 30, 0),
                new DateTime(2026, 11, 20, 10, 15, 0),
                new DateTime(2026, 11, 21, 14, 45, 0),
                new DateTime(2026, 11, 22, 18, 10, 0),
                new DateTime(2026, 11, 23, 21, 05, 0),
                new DateTime(2026, 11, 24, 9, 20, 0)
            };
            string[] samplePickups = { "пл. Петефі, 14", "вул. Собранецька, 89", "Залізничний вокзал", "пр. Свободи, 22", "вул. Корзо, 5", "вул. Минайська, 16" };
            string[] sampleDestinations = { "Боздоський парк", "вул. Капушанська, 40", "пл. Корятовича, 2", "вул. Заньковецької, 11", "Аеропорт Ужгород", "вул. Грушевського, 33" };
            double[] sampleDist = { 4.2, 6.5, 3.8, 5.1, 7.4, 2.9 };
            double[] sampleRates = { 18.0, 18.0, 20.0, 18.0, 22.0, 18.0 };
            int[] samplePayments = { 2, 1, 3, 2, 3, 1 };

            int added = 0;
            for (int i = 0; i < sampleDrivers.Length; i++)
            {
                if (count >= capacity)
                {
                    Console.WriteLine("Сховище заповнилося під час додавання тестових записів.");
                    break;
                }

                ids[count] = nextId;
                drivers[count] = sampleDrivers[i];
                tripDates[count] = sampleDates[i];
                pickupAddresses[count] = samplePickups[i];
                destinationAddresses[count] = sampleDestinations[i];
                distances[count] = sampleDist[i];
                ratesPerKm[count] = sampleRates[i];
                paymentMethodIds[count] = samplePayments[i];

                count++;
                nextId++;
                added++;
            }

            Console.WriteLine($"Додано тестових поїздок: {added}. Усього в сховищі: {count}.");
        }

        static string ReadText(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                {
                    return input.Trim();
                }
                Console.WriteLine("Помилка: рядок не може бути порожнім.");
            }
        }

        static DateTime ReadDateTime(string prompt, DateTime minDate, DateTime maxDate)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine()?.Trim();
                if (DateTime.TryParseExact(input, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateTime)
                    && dateTime >= minDate && dateTime <= maxDate)
                {
                    return dateTime;
                }
                Console.WriteLine($"Помилка: введіть коректні дату й час у форматі дд.мм.рррр гг:хх від {minDate:dd.MM.yyyy HH:mm} до {maxDate:dd.MM.yyyy HH:mm}.");
            }
        }

        static void ShowAbout()
        {
            Console.WriteLine("=== Довідка про застосунок ===");
            Console.WriteLine("Тема №13: Служба таксі");
            Console.WriteLine("Сутності: Користувач, Поїздка (головна), Спосіб оплати (довідник)");
            Console.WriteLine("\nПоля сутності «Поїздка»:");
            Console.WriteLine("  1. Номер (int)");
            Console.WriteLine("  2. Водій (string)");
            Console.WriteLine("  3. Дата й час (DateTime)");
            Console.WriteLine("  4. Адреса подачі (string)");
            Console.WriteLine("  5. Адреса призначення (string)");
            Console.WriteLine("  6. Відстань у км (double)");
            Console.WriteLine("  7. Тариф за км (double)");
            Console.WriteLine("  8. Код способу оплати з довідника (int)");
        }

        static void RunTabulation()
        {
            Console.WriteLine("=== Табулювання вартості поїздки залежно від відстані ===");
            double from = ReadDouble("Введіть початкову відстань у км (1 - 50): ", 1.0, 50.0);
            double to = ReadDouble($"Введіть кінцеву відстань у км ({from:F1} - 150): ", from, 150.0);
            double step = ReadDouble("Введіть крок зміни відстані у км (0.5 - 20): ", 0.5, 20.0);
            PrintTabulationTable(from, to, step);
        }

        static void PrintTabulationTable(double from, double to, double step)
        {
            Console.WriteLine("\n(Подача авто: 50 грн, базовий тариф: 18 грн/км)");
            Console.WriteLine($"{"Відстань, км",14} | {"Вартість, грн",15}");
            Console.WriteLine(new string('-', 32));
            for (double km = from; km <= to + 1e-9; km += step)
            {
                double cost = CalculateTariffCost(km);
                Console.WriteLine($"{km,14:F2} | {cost,15:F2}");
            }
        }

        static double CalculateTariffCost(double distanceKm)
        {
            return 50.0 + distanceKm * 18.0;
        }

        static void RunDriversTable()
        {
            Console.WriteLine("=== Звіт: кількість виконаних замовлень водіями по днях ===");
            int rows = ReadInt("Введіть кількість водіїв (рядків, від 1 до 15): ", 1, 15);
            int cols = ReadInt("Введіть кількість днів (стовпців, від 1 до 14): ", 1, 14);
            PrintDriversTable(rows, cols);
        }

        static void PrintDriversTable(int rows, int cols)
        {
            Console.WriteLine();
            int[] colTotals = new int[cols];
            int grandTotal = 0;

            Console.Write($"{"Водій \\ День",-16}");
            for (int col = 1; col <= cols; col++)
            {
                Console.Write($"{$"День {col}",9}");
            }
            Console.WriteLine($"{"Разом",10}");

            int lineWidth = 16 + cols * 9 + 10;
            Console.WriteLine(new string('-', lineWidth));

            for (int row = 1; row <= rows; row++)
            {
                Console.Write($"{"Водій #" + row,-16}");
                int rowSum = 0;
                for (int col = 1; col <= cols; col++)
                {
                    int orders = 6 + (row * 3 + col) % 8;
                    rowSum += orders;
                    colTotals[col - 1] += orders;
                    Console.Write($"{orders,9}");
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

        static void RunSingleTripCostCalculation()
        {
            Console.WriteLine("=== Розрахунок вартості поїздки ===");
            double distance = ReadDouble("Введіть відстань у км (0.5 - 500): ", 0.5, 500.0);
            double rate = ReadDouble("Введіть тариф за км, грн (5 - 200): ", 5.0, 200.0);
            double cost = CalculateSingleTripCost(distance, rate);
            Console.WriteLine($"Вартість поїздки: {cost:F2} грн.");
        }

        static double CalculateSingleTripCost(double distance, double ratePerKm)
        {
            return distance * ratePerKm;
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
            Console.WriteLine($"{"№",4} | {"Відстань, км",14}");
            Console.WriteLine(new string('-', 21));
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine($"{i + 1,4} | {values[i],14:F2}");
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
                if (double.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out double result) && result >= min && result <= max)
                {
                    return result;
                }
                Console.WriteLine($"Помилка: введіть дійсне число від {min} до {max}.");
            }
        }
    }
}