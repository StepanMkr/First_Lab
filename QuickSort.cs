using System;
using System.Drawing.Text;
using System.IO;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Xml;

public class QuickSort
{
    public static BigInteger swapCount; // Счётчик обменов
    public static BigInteger ifCount;   // Счётчик операторов if

    // Основной метод для вызова сортировки
    public static void Sort(int[] array)
    {
        if (array == null || array.Length == 0)
            return;

        swapCount = 0; // Сбрасываем счётчик перед сортировкой
        ifCount = 0;
        QuickSortAlgorithm(array, 0, array.Length - 1);

        //Записываем результаты в файл
        LogSortResults(array.Length, swapCount, ifCount);
    }

    // Рекурсивный алгоритм быстрой сортировки
    private static void QuickSortAlgorithm(int[] array, int left, int right)
    {
        ifCount++;
        if (left < right)
        {
            int pivotIndex = Partition(array, left, right);
            QuickSortAlgorithm(array, left, pivotIndex - 1);
            QuickSortAlgorithm(array, pivotIndex + 1, right);
        }
    }

    // Метод для разделения массива
    private static int Partition(int[] array, int left, int right)
    {
        int pivot = array[right];
        int i = left - 1;

        for (int j = left; j < right; j++)
        {
            ifCount++;
            if (array[j] <= pivot)
            {
                i++;
                Swap(array, i, j);
            }
        }

        Swap(array, i + 1, right);
        return i + 1;
    }

    // Вспомогательный метод для обмена элементов
    private static void Swap(int[] array, int i, int j)
    {
        int temp = array[i];
        array[i] = array[j];
        array[j] = temp;
        swapCount++; // Увеличиваем счётчик при каждом обмене
    }

    //Метод для логирования результатов
    private static void LogSortResults(int n, BigInteger swaps, BigInteger ifs)
    {
        string logPath = "sort_results.txt";
        string header = "n\tSwaps\tIfs";
        string logEntry = $"{n}\t{swaps}\t{ifs}";

        // Если файл не существует, создаём его и добавляем заголовок
        if (!File.Exists(logPath))
        {
            File.WriteAllText(logPath, header + Environment.NewLine);
        }

        // Добавляем новую запись в файл
        File.AppendAllText(logPath, logEntry + Environment.NewLine);
    }
}

public class Program
{
    public static void Main()
    {
        XmlDocument xDoc = new XmlDocument();
        xDoc.Load("sort_config.xml");
        // получим корневой элемент
        XmlElement? xRoot = xDoc.DocumentElement;

        if (xRoot != null)
        {
            // обход всех узлов в корневом элементе
            foreach (XmlElement xnode in xRoot)
            {
                // получаем атрибут name
                string? name = xnode.Attributes.GetNamedItem("name")?.Value;
                int minElement = int.Parse(xnode.Attributes.GetNamedItem("minElement")?.Value);
                int maxElement = int.Parse(xnode.Attributes.GetNamedItem("maxElement")?.Value);
                int startLength = int.Parse(xnode.Attributes.GetNamedItem("startLength")?.Value);
                int maxLength = int.Parse(xnode.Attributes.GetNamedItem("maxLength")?.Value);
                int diff = int.Parse(xnode.Attributes.GetNamedItem("diff")?.Value);
                int? repeat = int.Parse(xnode.Attributes.GetNamedItem("repeat")?.Value);
                int znam = int.Parse(xnode.Attributes.GetNamedItem("Znamen")?.Value);

                if (name == "Arithmetic")
                {
                    for (int i = startLength; i <= maxLength; i += diff)
                    {
                        var array = GenerateRandomArray(minElement, maxElement, i);
                        QuickSort.Sort(array);
                    }
                }
                else if (name == "Geometric") {
                    for (int i = startLength; i <= maxLength; i *= znam) {
                        var array = GenerateRandomArray(minElement, maxElement, i);
                        QuickSort.Sort(array);
                    }
                }


                // Console.Write(name + " ");
                // Console.Write(minElement + " ");
                // Console.Write(maxElement + " ");
                // Console.Write(startLength + " ");
                // Console.Write(maxLength + " ");
                // Console.Write(diff + " ");
                // Console.Write(znam + " ");
                // Console.Write(repeat + " ");
                // Console.WriteLine();
            }
        }
    }
    private static int[] GenerateRandomArray(int minElement, int maxElement, int len)
    {
        Random rand = new Random();
        int[] array = new int[len];

        for (int i = 0; i < len; i++)
        {
            // Генерация случайного double от minElement до maxElement
            array[i] = rand.Next(minElement, maxElement + 1);
        }
        return array;
    }

}