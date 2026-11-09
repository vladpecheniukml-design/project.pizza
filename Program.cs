using System;

class PizzaDeliveryApp
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        int n = 20;

        int[] orderIds = new int[n];
        string[] addresses = new string[n];
        int[] deliveryTimes = new int[n];
        string[] assortments = new string[n];
        double[] prices = new double[n];
        double[] profits = new double[n];

        string[] possibleAddresses = { 
            "вул. Соборна, 12", "вул. Київська, 45", "вул. Чорновола, 8", 
            "вул. Грушевського, 99", "пр. Миру, 5", "вул. Відінська, 14" 
        };
        string[] possibleAssortment = { 
            "Піца Пепероні", "Піца Маргарита", "Піца Чотири Сири", 
            "Піца М'ясна", "Піца Барбекю", "Піца Гавайська" 
        };

        Random rand = new Random(42);

        for (int i = 0; i < n; i++)
        {
            orderIds[i] = 1001 + i;
            addresses[i] = possibleAddresses[rand.Next(possibleAddresses.Length)];
            deliveryTimes[i] = rand.Next(15, 55);
            assortments[i] = possibleAssortment[rand.Next(possibleAssortment.Length)];
            prices[i] = Math.Round(220 + rand.NextDouble() * 350, 2);
            profits[i] = Math.Round(prices[i] * 0.4, 2);
        }

        Console.WriteLine("==================================================================================");
        Console.WriteLine("                         ПОЧАТКОВИЙ СПИСОК 20 ЗАМОВЛЕНЬ                           ");
        Console.WriteLine("==================================================================================");
        PrintOrders(orderIds, addresses, deliveryTimes, assortments, prices, profits);

        CalculateDailyStatistics(deliveryTimes, prices, profits, addresses);

        SortByDeliveryTime(orderIds, addresses, deliveryTimes, assortments, prices, profits);
        Console.WriteLine("\n==================================================================================");
        Console.WriteLine("                  СОРТУВАННЯ ЗА ЧАСОМ ДОСТАВКИ (ЗРОСТАННЯ)        ");
        Console.WriteLine("==================================================================================");
        PrintOrders(orderIds, addresses, deliveryTimes, assortments, prices, profits);

        SortByAddress(orderIds, addresses, deliveryTimes, assortments, prices, profits);
        Console.WriteLine("\n==================================================================================");
        Console.WriteLine("                    СОРТУВАННЯ ЗА АДРЕСОЮ (АЛФАВІТНО)             ");
        Console.WriteLine("==================================================================================");
        PrintOrders(orderIds, addresses, deliveryTimes, assortments, prices, profits);

        SortByAssortment(orderIds, addresses, deliveryTimes, assortments, prices, profits);
        Console.WriteLine("\n==================================================================================");
        Console.WriteLine("                 СОРТУВАННЯ ЗА АСОРТИМЕНТОМ ПРОДУКТІВ             ");
        Console.WriteLine("==================================================================================");
        PrintOrders(orderIds, addresses, deliveryTimes, assortments, prices, profits);

        SortByPriceAndProfit(orderIds, addresses, deliveryTimes, assortments, prices, profits);
        Console.WriteLine("\n==================================================================================");
        Console.WriteLine("               СОРТУВАННЯ ЗА ЦІНОЮ ТА ПРИБУТКОМ (СПАДАННЯ)        ");
        Console.WriteLine("==================================================================================");
        PrintOrders(orderIds, addresses, deliveryTimes, assortments, prices, profits);
    }

    static void PrintOrders(int[] ids, string[] addrs, int[] times, string[] items, double[] prcs, double[] prfts)
    {
        Console.WriteLine("{0,-6} | {1,-24} | {2,-15} | {3,-20} | {4,-12} | {5,-10}", 
            "ID", "Адреса (Куди)", "Час (хв)", "Асортимент (Товар)", "Ціна (грн)", "Прибуток");
        Console.WriteLine(new string('-', 98));
        
        for (int i = 0; i < ids.Length; i++)
        {
            Console.WriteLine("{0,-6} | {1,-24} | {2,-15} | {3,-20} | {4,-12:F2} | {5,-10:F2}", 
                ids[i], addrs[i], times[i], items[i], prcs[i], prfts[i]);
        }
    }

    static void CalculateDailyStatistics(int[] times, double[] prices, double[] profits, string[] addresses)
    {
        int totalOrders = times.Length;
        int totalMinutes = 0;
        double totalPrice = 0;
        double totalProfit = 0;

        for (int i = 0; i < totalOrders; i++)
        {
            totalMinutes += times[i];
            totalPrice += prices[i];
            totalProfit += profits[i];
        }

        Console.WriteLine("\n==================================================================================");
        Console.WriteLine("                        АНАЛІТИКА ТА СТАТИСТИКА ЗА ДЕНЬ                            ");
        Console.WriteLine("==================================================================================");
        Console.WriteLine($"* Всього замовлень оброблено за день: {totalOrders}");
        Console.WriteLine($"* Загальний час, витрачений на доставку: {totalMinutes} хв ({totalMinutes / 60.0:F2} годин)");
        Console.WriteLine($"* Середній час доставки одного замовлення: {(double)totalMinutes / totalOrders:F1} хв");
        Console.WriteLine($"* Загальна вартість проданих товарів (продуктів): {totalPrice:F2} грн");
        Console.WriteLine($"* Загальний чистий прибуток за день: {totalProfit:F2} грн");
        Console.WriteLine("==================================================================================");
    }

    static void Swap(int i, int j, int[] ids, string[] addrs, int[] times, string[] items, double[] prcs, double[] prfts)
    {
        int tempId = ids[i]; ids[i] = ids[j]; ids[j] = tempId;
        string tempAddr = addrs[i]; addrs[i] = addrs[j]; addrs[j] = tempAddr;
        int tempTime = times[i]; times[i] = times[j]; times[j] = tempTime;
        string tempItem = items[i]; items[i] = items[j]; items[j] = tempItem;
        double tempPrc = prcs[i]; prcs[i] = prcs[j]; prcs[j] = tempPrc;
        double tempPrft = prfts[i]; prfts[i] = prfts[j]; prfts[j] = tempPrft;
    }

    static void SortByDeliveryTime(int[] ids, string[] addrs, int[] times, string[] items, double[] prcs, double[] prfts)
    {
        for (int i = 0; i < times.Length - 1; i++)
        {
            for (int j = 0; j < times.Length - i - 1; j++)
            {
                if (times[j] > times[j + 1])
                {
                    Swap(j, j + 1, ids, addrs, times, items, prcs, prfts);
                }
            }
        }
    }

    static void SortByAddress(int[] ids, string[] addrs, int[] times, string[] items, double[] prcs, double[] prfts)
    {
        for (int i = 0; i < addrs.Length - 1; i++)
        {
            for (int j = 0; j < addrs.Length - i - 1; j++)
            {
                if (string.Compare(addrs[j], addrs[j + 1]) > 0)
                {
                    Swap(j, j + 1, ids, addrs, times, items, prcs, prfts);
                }
            }
        }
    }

    static void SortByAssortment(int[] ids, string[] addrs, int[] times, string[] items, double[] prcs, double[] prfts)
    {
        for (int i = 0; i < items.Length - 1; i++)
        {
            for (int j = 0; j < items.Length - i - 1; j++)
            {
                if (string.Compare(items[j], items[j + 1]) > 0)
                {
                    Swap(j, j + 1, ids, addrs, times, items, prcs, prfts);
                }
            }
        }
    }

    static void SortByPriceAndProfit(int[] ids, string[] addrs, int[] times, string[] items, double[] prcs, double[] prfts)
    {
        for (int i = 0; i < prcs.Length - 1; i++)
        {
            for (int j = 0; j < prcs.Length - i - 1; j++)
            {
                if (prcs[j] < prcs[j + 1])
                {
                    Swap(j, j + 1, ids, addrs, times, items, prcs, prfts);
                }
            }
        }
    }
}
