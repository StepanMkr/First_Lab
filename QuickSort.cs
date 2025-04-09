using System;
using System.IO;
using System.Xml.Serialization;

public class QuickSort
{
    public static int swapCount; // Счётчик обменов
    public static int ifCount;   // Счётчик операторов if

    // Основной метод для вызова сортировки
    public static void Sort(int[] array)
    {
        if (array == null || array.Length == 0)
            return;
        
        swapCount = 0; // Сбрасываем счётчик перед сортировкой
        ifCount = 0;
        QuickSortAlgorithm(array, 0, array.Length - 1);
        
        // Записываем результаты в файл
        // LogSortResults(array.Length, swapCount);
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

    // Метод для логирования результатов
    // private static void LogSortResults(int n, int swaps)
    // {
    //     string logPath = "sort_log.txt";
    //     string header = "n\tSwaps";
    //     string logEntry = $"{n}\t{swaps}";

    //     // Если файл не существует, создаём его и добавляем заголовок
    //     if (!File.Exists(logPath))
    //     {
    //         File.WriteAllText(logPath, header + Environment.NewLine);
    //     }

    //     // Добавляем новую запись в файл
    //     File.AppendAllText(logPath, logEntry + Environment.NewLine);
    // }
}

public class Program
{
    public static void Main()
    {
        // Загрузка конфигурации из XML
        SortingConfig config = LoadConfig("sort_config.xml");
        
        // Инициализация логгера
        if (File.Exists(config.Settings.LogFilePath))
        {
            File.WriteAllText(config.Settings.LogFilePath, "n\tSwaps\tIfs\tTime(ms)\n");
        }
        
        // Обработка статических массивов
        foreach (var arrayConfig in config.Arrays.StaticArrays)
        {
            TestArray(arrayConfig.GetIntArray(), config.Settings.LogFilePath);
        }
        
        // Обработка случайных массивов
        if (config.Settings.GenerateRandomArrays)
        {
            var random = new Random();
            foreach (int size in config.Arrays.RandomArrays.Sizes)
            {
                int[] randomArray = GenerateRandomArray(
                    size, 
                    config.Arrays.RandomArrays.MinValue, 
                    config.Arrays.RandomArrays.MaxValue);
                
                TestArray(randomArray, config.Settings.LogFilePath);
            }
        }
        
        // Тестирование предварительно отсортированных массивов
        if (config.Settings.TestPresorted)
        {
            foreach (int size in new[] { 100, 1000, 10000 })
            {
                int[] sortedArray = GenerateSortedArray(size, ascending: true);
                TestArray(sortedArray, config.Settings.LogFilePath);
                
                int[] reverseSortedArray = GenerateSortedArray(size, ascending: false);
                TestArray(reverseSortedArray, config.Settings.LogFilePath);
            }
        }
    }
    
    private static SortingConfig LoadConfig(string path)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(SortingConfig));
        using (FileStream stream = new FileStream(path, FileMode.Open))
        {
            return (SortingConfig)serializer.Deserialize(stream);
        }
    }
    
    private static void TestArray(int[] array, string logPath)
    {
        var watch = System.Diagnostics.Stopwatch.StartNew();
        QuickSort.Sort(array);
        watch.Stop();
        
        string logEntry = $"{array.Length}\t{QuickSort.swapCount}\t{QuickSort.ifCount}\t{watch.ElapsedMilliseconds}";
        File.AppendAllText(logPath, logEntry + Environment.NewLine);
    }
    
    private static int[] GenerateRandomArray(int size, int min, int max)
    {
        Random rnd = new Random();
        int[] array = new int[size];
        for (int i = 0; i < size; i++)
        {
            array[i] = rnd.Next(min, max);
        }
        return array;
    }
    
    private static int[] GenerateSortedArray(int size, bool ascending)
    {
        int[] array = new int[size];
        for (int i = 0; i < size; i++)
        {
            array[i] = ascending ? i : size - i;
        }
        return array;
    }
}