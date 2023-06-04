using System;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        //Генерація масиву
        int[] array = GenerateRandomArray(27, 10, 100);
        Console.WriteLine("Початковий масив:");
        PrintArray(array);

        //Швидке сортування масиву
        QuickSort(array, 0, array.Length - 1);
        Console.WriteLine("\nВiдсортований масив:");
        PrintArray(array);

        //Введення ключового значення
        int key;
        bool isValidInput = false;
        do
        {
            Console.Write("\nВведiть ключове значення: ");
            string input = Console.ReadLine();

            isValidInput = int.TryParse(input, out key);
            if (!isValidInput)
            {
                Console.WriteLine("Некоректне значення. Введiть цiле число.");
            }
        } while (!isValidInput);

        //Бінарний пошук та замір часу виконання
        Stopwatch binarySearchTimer = Stopwatch.StartNew();
        int binarySearchCount = BinarySearch(array, key);
        //Додаємо 1 мс до часу, бо без цього виводиться просто 0
        Thread.Sleep(1);
        binarySearchTimer.Stop();

        //Послідовний пошук та замір часу виконання
        Stopwatch linearSearchTimer = Stopwatch.StartNew();
        int linearSearchCount = LinearSearch(array, key);
        Thread.Sleep(1);
        linearSearchTimer.Stop();

        Console.WriteLine("\nБiнарний пошук:");
        Console.WriteLine("Значення зустрiчається {0} раз(iв)", binarySearchCount);
        Console.WriteLine("Час виконання: {0} мс", binarySearchTimer.ElapsedMilliseconds);

        Console.WriteLine("\nПослiдовний пошук:");
        Console.WriteLine("Значення зустрiчається {0} раз(iв)", linearSearchCount);
        Console.WriteLine("Час виконання: {0} мс", linearSearchTimer.ElapsedMilliseconds);

        Console.ReadLine();
    }

    //Генерація масиву
    static int[] GenerateRandomArray(int size, int min, int max)
    {
        Random random = new Random();
        int[] array = new int[size];
        for (int i = 0; i < size; i++)
        {
            array[i] = random.Next(min, max);
        }
        return array;
    }

    //Вивід масиву
    static void PrintArray(int[] array)
    {
        foreach (int element in array)
        {
            Console.Write(element + " ");
        }
        Console.WriteLine();
    }
    
    //Швидке сортування
    static void QuickSort(int[] array, int left, int right)
    {
        if (left < right)
        {
            int pivotIndex = Partition(array, left, right);
            QuickSort(array, left, pivotIndex - 1);
            QuickSort(array, pivotIndex + 1, right);
        }
    }

    //Повертання індексу опорного елементу
    static int Partition(int[] array, int left, int right)
    {
        int pivot = array[right];
        int i = left - 1;

        for (int j = left; j < right; j++)
        {
            if (array[j] < pivot)
            {
                i++;
                Swap(array, i, j);
            }
        }

        Swap(array, i + 1, right);
        return i + 1;
    }

    //Обмін елементів 
    static void Swap(int[] array, int i, int j)
    {
        int temp = array[i];
        array[i] = array[j];
        array[j] = temp;
    }

    //Бінарний пошук
    static int BinarySearch(int[] array, int key)
    {
        int count = 0;
        int left = 0;
        int right = array.Length - 1;

        while (left <= right)
        {
            int mid = left + (right - left) / 2;
            if (array[mid] == key)
            {
                count++;
                int index = mid - 1;
                while (index >= left && array[index] == key)
                {
                    count++;
                    index--;
                }
                index = mid + 1;
                while (index <= right && array[index] == key)
                {
                    count++;
                    index++;
                }
                break;
            }
            else if (array[mid] < key)
            {
                left = mid + 1;
            }
            else
            {
                right = mid - 1;
            }
        }
        return count;
    }

    //Послідовний пошук
    static int LinearSearch(int[] array, int key)
    {
        int count = 0;
        foreach (int element in array)
        {
            if (element == key)
            {
                count++;
            }
        }
        return count;
    }
}
