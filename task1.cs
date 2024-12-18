using System;
using System.Text;
using System.Net;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace lab5_6
{
    internal class Task1
    {
        static readonly int arraySizeLimit = Array.MaxLength;

        /// <summary>
        /// Определение принадлежности числа заданному диапазону
        /// </summary>
        /// <param name="number">Целое число</param>
        /// <param name="startNumber">Начало диапазона (включительно)</param>
        /// <param name="endNumber">Нонец диапазона (включительно)</param>
        /// <returns>Принадлежность числа заданному диапазону</returns>
        static bool CheckDiapasonBelonging(int number, int startNumber, int endNumber) => (startNumber <= number && number <= endNumber);

        /// <summary>
        /// Вывод текстового меню (главное меню)
        /// </summary>
        static void PrintMainMenu(out string message)
        {
            Console.WriteLine(@"1. Работа с двумерными массивами (матрицами).
2. Работа с рваными массивами.
3. Работа со строкой.
4. Завершение работы программы.");
            message = "Введите число, соответствующее нужному вам пункту меню:"; // сообщение для вывода пользователю при вводе пункта меню
        }

        /// <summary>
        /// Вывод текстового меню (меню для рваных массивов)
        /// </summary>
        static void PrintSubMenuRaggedArray(out string message)
        {
            Console.WriteLine(@"1. Создать рваный массив.
2. Вывести рваный массив в консоль.
3. Удалить из рваного массива все строки с четными номерами.
4. Перейти к главному меню.");
            message = "Введите число, соответствующее нужному вам пункту меню:"; // сообщение для вывода пользователю при вводе пункта меню
        }

        /// <summary>
        /// Вывод текстового меню (меню для строк)
        /// </summary>
        static void PrintSubMenuString(out string message)
        {
            Console.WriteLine(@"1. Ввести строку.
2. Определить, есть ли в строке идентификаторы; если есть, то напечатать самый короткий 
идентификатор (идентификаторы - имена объектов в программе; идентификатор начинается с буквы или 
знака подчеркивания, включает только буквы и цифры).
3. Перейти к главному меню.");
            message = "Введите число, соответствующее нужному вам пункту меню:"; // сообщение для вывода пользователю при вводе пункта меню
        }

        /// <summary>
        /// Вывод текстового меню (меню для создания массива)
        /// </summary>
        static void PrintMenuArrayCreatingWays(out string message)
        {
            Console.WriteLine(@"1. Создать массив вручную (вводом с клавиатуры).
2. Создать массив с помощью датчика случайных чисел.
3. Назад");
            message = "Введите число, соответствующее нужному вам пункту меню:"; // сообщение для вывода пользователю при вводе пункта меню
        }

        /// <summary>
        /// Вывод текстового меню (меню для добавления столбцов в двумерный массив)
        /// </summary>
        /// <param name="message">сообщение для пользователя при вводе пункта меню</param>
        static void PrintMenuArrayExpandingWays(out string message)
        {
            Console.WriteLine(@"1. Заполнить добавляемые столбцы вручную (вводом с клавиатуры).
2. Заполнить добавляемые столбцы с помощью датчика случайных чисел.
3. Назад");
            message = "Введите число, соответствующее нужному вам пункту меню:";
        }

        /// <summary>
        /// Считывание введенного пользователем значения, проверка корректности ввода числа типа Int32
        /// </summary>
        /// <param name="inputValue">Возвращаемое значение - целое число</param>
        static void ReadInput(string message, ref int inputValue)
        {
            bool isAppropriateNumber = false;
            do
            {
                try
                {
                    Console.WriteLine(message);
                    inputValue = int.Parse(Console.ReadLine());
                    isAppropriateNumber = true;
                }
                catch (FormatException)
                {
                    isAppropriateNumber = false;
                    Console.WriteLine($"Введенное значение не является целым числом.");
                }
                catch (OverflowException)
                {
                    isAppropriateNumber = false;
                    Console.WriteLine($"Вы ввели слишком большое по модулю целое число.");
                }
                catch (OutOfMemoryException)
                {
                    Console.WriteLine("Ошибка при выделении памяти.");
                }
            } while (!isAppropriateNumber);
        }

        /// <summary>
        /// Проверка считанного числа от ввода пользователя на принадлежность диапазону
        /// </summary>
        /// <param name="startPoint">Нижняя граница допустимого диапазона (включительно)</param>
        /// <param name="endPoint">Верхняя граница допустимого диапазона (включительно)</param>
        static int CheckReadNumber(string message, int startPoint, int endPoint)
        {
            int number = 0;
            do
            {
                ReadInput(message, ref number);
                if (!CheckDiapasonBelonging(number, startPoint, endPoint))
                {
                    Console.WriteLine($"Введенное вами число не входит в диапазон от {startPoint} до {endPoint}.");
                }
            } while (number < startPoint || number > endPoint);
            return number;
        }

        /// <summary>
        /// вывод меню пользователю и получение от пользователя пункта меню с необходимой задачей
        /// </summary>
        /// <param name="mainMenuPoint">Номер выбранного пункта меню</param>
        /// <param name="firstMenuPoint">Номер первого пункта меню</param>
        /// <param name="lastMenuPoint">Номер последнего пункта меню</param>
        static void ChooseMainMenuPoint(int firstMenuPoint, int lastMenuPoint, out int mainMenuPoint)
        {
            PrintMainMenu(out string messageMenuInput);
            mainMenuPoint = CheckReadNumber(messageMenuInput, firstMenuPoint, lastMenuPoint);
        }

        /// <summary>
        /// Вывод меню второго уровня и возврат выбранного пункта меню
        /// </summary>
        /// <param name="mainMenuPoint">Номер пункта главного меню (двумерные/рваные массивы или строки)</param>
        /// <param name="subMenuPoint">Номер выбранного пункта меню второго уровня</param>
        /// <param name="firstMenuPoint">номер первого элемента меню второго уровня</param>
        /// <param name="lastMenuPoint">номер последнего элемента меню второго уровня</param>
        static void ChooseSubMenuPoint(int mainMenuPoint, int firstMenuPoint, int lastMenuPoint, out int subMenuPoint)
        {
            string messageMenuInput = "";
            switch (mainMenuPoint)
            {
                case 1:
                    PrintSubMenuTwoDimensionalArray(out messageMenuInput);
                    break;
                case 2:
                    PrintSubMenuRaggedArray(out messageMenuInput);
                    break;
                case 3:
                    PrintSubMenuString(out messageMenuInput);
                    break;
            };
            subMenuPoint = CheckReadNumber(messageMenuInput, firstMenuPoint, lastMenuPoint);
        }

        /// <summary>
        /// Выбор способа создания массива из меню
        /// </summary>
        /// <param name="firstMenuPoint">Номер первого пункта меню</param>
        /// <param name="lastMenuPoint">Номер последнего пункта меню</param>
        /// <param name="arrayCreatingWaysMenuPoint">Номер выбранного пункта меню</param>
        static void ChooseArrayCreatingWaysMenuPoint(int firstMenuPoint, int lastMenuPoint, out int arrayCreatingWaysMenuPoint)
        {
            PrintMenuArrayCreatingWays(out string messageMenuArrayFillingWays);
            arrayCreatingWaysMenuPoint = CheckReadNumber(messageMenuArrayFillingWays, firstMenuPoint, lastMenuPoint);
        }

        /// <summary>
        /// Возвращает выбранный пользователем пункт меню
        /// </summary>
        /// <param name="firstMenuPoint">Первый пункт меню</param>
        /// <param name="lastMenuPoint">Последний пункт меню</param>
        /// <param name="arrayExpandingWaysMenuPoint">Выбранный пункт меню</param>
        static void ChooseArrayExpandingWaysMenuPoint(int firstMenuPoint, int lastMenuPoint, out int arrayExpandingWaysMenuPoint)
        {
            PrintMenuArrayExpandingWays(out string messageMenuArrayFillingWays);
            arrayExpandingWaysMenuPoint = CheckReadNumber(messageMenuArrayFillingWays, firstMenuPoint, lastMenuPoint);
        }

        /// <summary>
        /// Проверка, является ли одномерный массив пустым
        /// </summary>
        /// <param name="oneDimensionalArray">одномерный массив</param>
        /// <returns>пустой ли массив</returns>
        static bool CheckIfArrayIsEmpty(int[] oneDimensionalArray) => (oneDimensionalArray is null || oneDimensionalArray.Length == 0);

        #region Работа с двумерными массивами

        /// <summary>
        /// Проверка двумерного массива на пустоту
        /// </summary>
        /// <param name="twoDimensionalArray">двумерный массив</param>
        /// <returns>Пустой ли массив</returns>
        static bool CheckIfArrayIsEmpty(int[,] twoDimensionalArray) => (twoDimensionalArray is null || twoDimensionalArray.Length == 0);

        /// <summary>
        /// Вывод текстового меню (меню для двумерных массивов)
        /// </summary>
        static void PrintSubMenuTwoDimensionalArray(out string message)
        {
            Console.WriteLine(@"1. Создать двумерный массив (матрицу).
2. Вывести двумерный массив (матрицу) в консоль.
3. Добавить заданное число столбцов в начало матрицы (двумерного массива).
4. Перейти к главному меню.");
            message = "Введите число, соответствующее нужному вам пункту меню:";
        }

        /// <summary>
        /// Считывание количества строк и столбцов в матрице и создание массива, заполненного нулями, с учетом заданных размеров
        /// </summary>
        /// <param name="twoDimensionalArray">Двумерный массив (матрица)</param>
        static void GetArraySize(out int[,] twoDimensionalArray)
        {
            string messageRows = $"Введите количество строк в матрице (целое число от 0 до {arraySizeLimit})";
            int twoDimensionalArrayRowsNumber = CheckReadNumber(messageRows, 0, arraySizeLimit);
            if (twoDimensionalArrayRowsNumber != 0)
            {
                string messageColumns = $"Введите количество столбцов в матрице (целое число от 0 до {arraySizeLimit / twoDimensionalArrayRowsNumber})";
                int twoDimensionalArrayColumnsNumber = CheckReadNumber(messageColumns, 0, arraySizeLimit / twoDimensionalArrayRowsNumber);
                twoDimensionalArray = new int[twoDimensionalArrayRowsNumber, twoDimensionalArrayColumnsNumber];
            }
            else
            {
                twoDimensionalArray = new int[0, 0];
            }
        }

        /// <summary>
        /// Заполнение двумерного массива вводом с клавиатуры
        /// </summary>
        /// <param name="twoDimensionalArray">Двумерный массив</param>
        static void CreateArrayKeyBoard(ref int[,] twoDimensionalArray)
        {
            string messageEnterElement;
            GetArraySize(out twoDimensionalArray);
            if (!CheckIfArrayIsEmpty(twoDimensionalArray))
            {
                for (int i = 0; i < twoDimensionalArray.GetLength(0); i++)
                {
                    for (int j = 0; j < twoDimensionalArray.GetLength(1); j++)
                    {
                        messageEnterElement = $"Введите целое число - элемент в строке {i + 1}, столбце {j + 1} матрицы.";
                        twoDimensionalArray[i, j] = CheckReadNumber(messageEnterElement, Int32.MinValue, Int32.MaxValue);
                    }
                }
            }
        }

        /// <summary>
        /// Заполнение двумерного массива с помощью ДСЧ
        /// </summary>
        /// <param name="twoDimensionalArray">Двумерный массив</param>
        static void CreateArrayRandom(ref int[,] twoDimensionalArray)
        {
            GetArraySize(out twoDimensionalArray);
            if (!CheckIfArrayIsEmpty(twoDimensionalArray))
            {
                string messageEnterMinimum = "Введите целое число - минимальное значение для элементов матрицы.";
                string messageEnterMaximum = "Введите целое число - максимальное значение для элементов матрицы (не меньше минимального значения).";
                int minimumValue = CheckReadNumber(messageEnterMinimum, Int32.MinValue, Int32.MaxValue);
                int maximumValue = CheckReadNumber(messageEnterMaximum, minimumValue, Int32.MaxValue);
                Random randomElement = new Random();
                for (int i = 0; i < twoDimensionalArray.GetLength(0); i++)
                {
                    for (int j = 0; j < twoDimensionalArray.GetLength(1); j++)
                    {
                        twoDimensionalArray[i, j] = randomElement.Next(minimumValue, maximumValue);
                    }
                }
            }
        }

        /// <summary>
        /// Создание двумерного массива (матрицы)
        /// </summary>
        /// <param name="array">Двумерный массив (матрица)</param>
        static void CreateArray(ref int[,] array)
        {
            ChooseArrayCreatingWaysMenuPoint(1, 3, out int arrayCreatingWaysMenuPoint);
            switch (arrayCreatingWaysMenuPoint)
            {
                case 1:
                    CreateArrayKeyBoard(ref array);
                    PrintArray(array);
                    break;
                case 2:
                    CreateArrayRandom(ref array);
                    PrintArray(array);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Вывод двумерного массива
        /// </summary>
        /// <param name="twoDimensionalArray">Двумерный массив</param>
        static void PrintArray(int[,] twoDimensionalArray)
        {
            Console.WriteLine("Состав массива на данный момент:");
            if (CheckIfArrayIsEmpty(twoDimensionalArray))
            {
                Console.WriteLine("Массив пустой");
            }
            else
            {
                for (int i = 0; i < twoDimensionalArray.GetLength(0); i++)
                {
                    for (int j = 0; j < twoDimensionalArray.GetLength(1); j++)
                    {
                        Console.Write($"{twoDimensionalArray[i, j],4}");
                    }
                    Console.WriteLine();
                }
            }
        }

        /// <summary>
        /// Возврат массива с увеличенным числом столбцов с учетом числа добавляемых
        /// </summary>
        /// <param name="twoDimensionalArray">Двумерный массив</param>
        /// <param name="temporaryArray">Вспомогательный массив</param>
        /// <param name="addedColumnsNumber">Количество добавляемых стобцов</param>
        static void GetExpandedArraySize(int[,] twoDimensionalArray, out int[,] temporaryArray, out int addedColumnsNumber)
        {
            string messageNewColumns = $"Введите целое число - количество столбцов, которое вы хотите добавить в начало массива (не больше {arraySizeLimit / twoDimensionalArray.GetLength(0) - twoDimensionalArray.GetLength(1)}).";
            addedColumnsNumber = CheckReadNumber(messageNewColumns, 0, arraySizeLimit / twoDimensionalArray.GetLength(0) - twoDimensionalArray.GetLength(1));
            temporaryArray = new int[twoDimensionalArray.GetLength(0), twoDimensionalArray.GetLength(1) + addedColumnsNumber];
        }

        /// <summary>
        /// Добавление в двумерный массив новых стобцов и заполнение их элементами с помощью клавиатуры
        /// </summary>
        /// <param name="twoDimensionalArray">Двумерный массив</param>
        static void AddColumnsUsingKeyBoard(ref int[,] twoDimensionalArray)
        {
            string messageEnterElement;
            GetExpandedArraySize(twoDimensionalArray, out int[,] temporaryArray, out int addedColumnsNumber);
            if (addedColumnsNumber > 0)
            {
                for (int i = 0; i < twoDimensionalArray.GetLength(0); i++)
                {
                    int j;
                    for (j = 0; j < addedColumnsNumber; j++)
                    {
                        messageEnterElement = $"Введите целое число - элемент в строке {i + 1}, столбце {j + 1} матрицы.";
                        temporaryArray[i, j] = CheckReadNumber(messageEnterElement, Int32.MinValue, Int32.MaxValue);
                    }
                    for (j = addedColumnsNumber; j < twoDimensionalArray.GetLength(1) + addedColumnsNumber; j++)
                    {
                        temporaryArray[i, j] = twoDimensionalArray[i, j - addedColumnsNumber];
                    }
                }
                twoDimensionalArray = new int[temporaryArray.GetLength(0), temporaryArray.GetLength(1)];
                Array.Copy(temporaryArray, twoDimensionalArray, temporaryArray.Length);
            }
            else
            {
                Console.WriteLine("В массив не будут добавлены новые столбцы, поэтому размер и состав массива не изменятся.");
            }
        }

        /// <summary>
        /// Добавление в двумерный массив новых стобцов и заполнение их элементами с помощью ДСЧ
        /// </summary>
        /// <param name="twoDimensionalArray">Двумерный массив</param>
        static void AddColumnsUsingRandom(ref int[,] twoDimensionalArray)
        {
            GetExpandedArraySize(twoDimensionalArray, out int[,] temporaryArray, out int addedColumnsNumber);
            Random randomElement = new Random();
            string messageEnterMinimum = "Введите целое число - минимальное значение для добавляемых элементов матрицы.";
            string messageEnterMaximum = "Введите целое число - максимальное значение для добавляемых элементов матрицы (не меньше минимального значения).";
            int minimumValue = CheckReadNumber(messageEnterMinimum, Int32.MinValue, Int32.MaxValue);
            int maximumValue = CheckReadNumber(messageEnterMaximum, minimumValue, Int32.MaxValue);
            if (addedColumnsNumber > 0)
            {
                for (int i = 0; i < twoDimensionalArray.GetLength(0); i++)
                {
                    int j;
                    for (j = 0; j < addedColumnsNumber; j++)
                    {
                        temporaryArray[i, j] = randomElement.Next(minimumValue, maximumValue);
                    }
                    for (j = addedColumnsNumber; j < twoDimensionalArray.GetLength(1) + addedColumnsNumber; j++)
                    {
                        temporaryArray[i, j] = twoDimensionalArray[i, j - addedColumnsNumber];
                    }
                }
                twoDimensionalArray = new int[temporaryArray.GetLength(0), temporaryArray.GetLength(1)];
                Array.Copy(temporaryArray, twoDimensionalArray, temporaryArray.Length);
            }
            else
            {
                Console.WriteLine("В массив не будут добавлены новые столбцы, поэтому размер и состав массива не изменятся.");
            }
        }

        /// <summary>
        /// Вывод меню для добавления столбцов в начало двумерного массива
        /// </summary>
        /// <param name="twoDimensionalArray">Двумерный массив</param>
        static void AddNewColumnsArrayBeginning(ref int[,] twoDimensionalArray)
        {
            if (!(twoDimensionalArray.GetLength(0) == 0))
            {
                ChooseArrayExpandingWaysMenuPoint(1, 3, out int arrayExpandingWaysMenuPoint);
                switch (arrayExpandingWaysMenuPoint)
                {
                    case 1:
                        AddColumnsUsingKeyBoard(ref twoDimensionalArray);
                        PrintArray(twoDimensionalArray);
                        break;
                    case 2:
                        AddColumnsUsingRandom(ref twoDimensionalArray);
                        PrintArray(twoDimensionalArray);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                Console.WriteLine("Количество строк в массиве равно 0, поэтому при добавлении в него новых столбцов массив останется пустым.");
            }
        }

        #endregion

        #region Работа с рваными массивами

        /// <summary>
        /// Проверка, пустой ли рваный массив
        /// </summary>
        /// <param name="raggedArray">Рваный массив</param>
        /// <returns>Истинность/ложность пустоты массива</returns>
        static bool CheckIfArrayIsEmpty(int[][] raggedArray) => (raggedArray is null || raggedArray.Length == 0);

        /// <summary>
        /// Возврат размера рваного массива (массив, заполненный нулями)
        /// </summary>
        /// <param name="raggedArray">Рваный массив</param>
        static void GetArraySize(out int[][] raggedArray)
        {
            string messageRows = $"Введите количество строк в рваном массиве (целое число от 0 до 500)";
            int raggedArrayRowsNumber = CheckReadNumber(messageRows, 0, 500);
            raggedArray = new int[raggedArrayRowsNumber][];
        }

        /// <summary>
        /// Создать рваный массив с помощью ввода с клавиатуры
        /// </summary>
        /// <param name="raggedArray">Рваный массив</param>
        static void CreateArrayKeyBoard(ref int[][] raggedArray)
        {
            string messageEnterElement;
            string messageColumns;
            int raggedArrayColumnsNumber;
            GetArraySize(out raggedArray);
            if (!CheckIfArrayIsEmpty(raggedArray))
            {
                for (int i = 0; i < raggedArray.GetLength(0); i++)
                {
                    messageColumns = $"Введите целое число - количество столбцов в строке {i + 1} рваного массива.";
                    raggedArrayColumnsNumber = CheckReadNumber(messageColumns, 0, 500);
                    raggedArray[i] = new int[raggedArrayColumnsNumber];
                    for (int j = 0; j < raggedArrayColumnsNumber; j++)
                    {
                        messageEnterElement = $"Введите целое число - элемент в строке {i + 1}, столбце {j + 1} матрицы.";
                        raggedArray[i][j] = CheckReadNumber(messageEnterElement, Int32.MinValue, Int32.MaxValue);
                    }
                }
            }
        }

        /// <summary>
        /// Создать рваный массив с помощью ДСЧ
        /// </summary>
        /// <param name="raggedArray">Рваный массив</param>
        static void CreateArrayRandom(ref int[][] raggedArray)
        {
            string messageColumns;
            int raggedArrayColumnsNumber;
            GetArraySize(out raggedArray);
            if (!CheckIfArrayIsEmpty(raggedArray))
            {
                string messageEnterMinimum = "Введите целое число - минимальное значение для элементов матрицы.";
                string messageEnterMaximum = "Введите целое число - максимальное значение для элементов матрицы (не меньше минимального значения).";
                int minimumValue = CheckReadNumber(messageEnterMinimum, Int32.MinValue, Int32.MaxValue);
                int maximumValue = CheckReadNumber(messageEnterMaximum, minimumValue, Int32.MaxValue);
                Random randomElement = new Random();
                for (int i = 0; i < raggedArray.GetLength(0); i++)
                {
                    messageColumns = $"Введите целое число - количество столбцов в строке {i + 1} рваного массива.";
                    raggedArrayColumnsNumber = CheckReadNumber(messageColumns, 0, 500);
                    raggedArray[i] = new int[raggedArrayColumnsNumber];
                    for (int j = 0; j < raggedArrayColumnsNumber; j++)
                    {
                        raggedArray[i][j] = randomElement.Next(minimumValue, maximumValue);
                    }
                }
            }
        }

        /// <summary>
        /// Вывод рваного массива
        /// </summary>
        /// <param name="raggedArray">Рваный массив</param>
        static void PrintArray(int[][] raggedArray)
        {
            Console.WriteLine("Состав массива на данный момент:");
            if (CheckIfArrayIsEmpty(raggedArray))
            {
                Console.WriteLine("Массив пустой");
            }
            else
            {
                for (int i = 0; i < raggedArray.GetLength(0); i++)
                {
                    if (CheckIfArrayIsEmpty(raggedArray[i]))
                    {
                        Console.WriteLine($"Массив в строке {i + 1} пустой.");
                    }
                    else
                    {
                        for (int j = 0; j < raggedArray[i].Length; j++)
                        {
                            Console.Write($"{raggedArray[i][j],4}");
                        }
                        Console.WriteLine();
                    }
                }
            }
        }

        /// <summary>
        /// Меню для способов создания рваного массива
        /// </summary>
        /// <param name="raggedArray">Рваный массив</param>
        static void CreateArray(ref int[][] raggedArray)
        {
            ChooseArrayCreatingWaysMenuPoint(1, 3, out int arrayCreatingWaysMenuPoint);
            switch (arrayCreatingWaysMenuPoint)
            {
                case 1:
                    CreateArrayKeyBoard(ref raggedArray);
                    PrintArray(raggedArray);
                    break;
                case 2:
                    CreateArrayRandom(ref raggedArray);
                    PrintArray(raggedArray);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Удаление строк с четным номером из рваного массива
        /// </summary>
        /// <param name="raggedArray">Рваный массив</param>
        static void DeleteEvenNumberedRows(ref int[][] raggedArray)
        {
            int temporaryArrayIndexCounter = 0;
            if (!CheckIfArrayIsEmpty(raggedArray))
            {
                if (raggedArray.GetLength(0) < 2)
                {
                    Console.WriteLine("В массиве нет строк с четными номерами.");
                }
                else
                {
                    int unevenNumberedRowsQuantity = raggedArray.GetLength(0) - (raggedArray.GetLength(0) / 2);
                    int[][] temporaryArray = new int[unevenNumberedRowsQuantity][];
                    for (int i = 0; i < raggedArray.GetLength(0); i += 2)
                    {
                        temporaryArray[temporaryArrayIndexCounter] = new int[raggedArray[i].GetLength(0)];
                        Array.Copy(raggedArray[i], temporaryArray[temporaryArrayIndexCounter++], raggedArray[i].Length);
                    }
                    raggedArray = new int[unevenNumberedRowsQuantity][];
                    for (int i = 0; i < temporaryArray.GetLength(0); i++)
                    {
                        raggedArray[i] = new int[temporaryArray[i].Length];
                        Array.Copy(temporaryArray[i], raggedArray[i], temporaryArray[i].Length);
                    }
                    Console.WriteLine("Строки с четными номерами были удалены из массива.");
                }
                PrintArray(raggedArray);
            }
            else
            {
                Console.WriteLine("Массив пустой, поэтому из него невозможно удалить строки с четными номерами.");
            }
        }

        #endregion

        #region Работа со строками

        /// <summary>
        /// Массив строк для быстрого ввода
        /// </summary>
        static string[] testStringsArray = ["static void PrintUpper string info12346: WriteLine ToUpper info, 1234info."];

        /// <summary>
        /// Вывод строки
        /// </summary>
        /// <param name="userString">Строка</param>
        static void PrintString(string userString)
        {
            Console.WriteLine("Состав строки на данный момент:");
            if (System.String.IsNullOrEmpty(userString))
            {
                Console.WriteLine("Строка пустая");
            }
            else
            {
                Console.WriteLine(userString);
            }
        }

        /// <summary>
        /// Вывод меню способов ввода строки
        /// </summary>
        /// <param name="message">сообщение для пользователя при выборе пункта меню</param>
        static void PrintMenuStringCreatingWays(out string message)
        {
            Console.WriteLine(@"1. Ввести строку с клавиатуры.
2. Выбрать строку из заданного тестового набора строк.
3. Назад");
            message = "Введите число, соответствующее нужному вам пункту меню:";
        }

        /// <summary>
        /// Выбор пункта меню с нужным способом ввода
        /// </summary>
        /// <param name="firstMenuPoint">Первый пункт меню</param>
        /// <param name="lastMenuPoint">Последний пункт меню</param>
        /// <param name="stringCreatingWaysMenuPoint">Выбранный пункт меню</param>
        static void ChooseStringCreatingWaysMenuPoint(int firstMenuPoint, int lastMenuPoint, out int stringCreatingWaysMenuPoint)
        {
            PrintMenuStringCreatingWays(out string messageMenuStringCreatingWays);
            stringCreatingWaysMenuPoint = CheckReadNumber(messageMenuStringCreatingWays, firstMenuPoint, lastMenuPoint);
        }

        /// <summary>
        /// Считать строку из консоли
        /// </summary>
        /// <param name="message">Сообщение для пользователя при вводе строки</param>
        /// <param name="userString">Строка</param>
        static void ReadString(string message, ref string userString)
        {
            bool isStringCorrect = false;
            do
            {
                try
                {
                    Console.WriteLine(message);
                    userString = Console.ReadLine();
                    isStringCorrect = true;
                }
                catch (OutOfMemoryException)
                {
                    Console.WriteLine("Ошибка при выделении памяти.");
                }
            } while (!isStringCorrect);
        }

        /// <summary>
        /// Знаки препинания на конце предложения
        /// </summary>
        static readonly char[] sentenceSeparators = { '.', '!', '?' };

        /// <summary>
        /// Знаки препинания в предложении + пробел
        /// </summary>
        static readonly char[] wordSeparatorsAndSpace = { ',', ':', ';', ' ' };

        /// <summary>
        /// Знаки препинания в предложении
        /// </summary>
        static readonly char[] wordSeparators = { ',', ':', ';' };

        /// <summary>
        /// Является ли символ каким-либо знаком препинания либо пробелом
        /// </summary>
        /// <param name="stringElement">Символ в строке</param>
        /// <returns>Истинность/ложность того, является ли символ допустимым</returns>
        static bool IsAnySeparator(char stringElement)
        {
            foreach (char currentElement1 in wordSeparatorsAndSpace)
                if (currentElement1 == stringElement)
                    return true;
            foreach (char currentElement2 in sentenceSeparators)
                if (currentElement2 == stringElement)
                    return true;
            return false;
        }

        /// <summary>
        /// Является ли символ знаком препинания на конце предложения
        /// </summary>
        /// <param name="stringElement">Строка</param>
        /// <returns>Истинность/ложность того, является ли символ знаком препинания на конце предложения</returns>
        static bool IsSentenceSeparator(char stringElement)
        {
            foreach (char currentElement in sentenceSeparators)
                if (currentElement == stringElement)
                    return true;
            return false;
        }

        static bool IsWordSeparator(char stringElement)
        {
            foreach (char currentElement in wordSeparators)
                if (currentElement == stringElement)
                    return true;
            return false;
        }

        static void ConvertTabsIntoWhitespaces(ref string userString)
        {
            StringBuilder temporaryString = new StringBuilder(userString);
            temporaryString.Replace("\t", " ");
            userString = temporaryString.ToString();
        }

        static bool CheckStringSeparators(string userString)
        {
            ConvertTabsIntoWhitespaces(ref userString);
            foreach (char currentElement in userString)
            {
                if (!(Char.IsLetterOrDigit(currentElement) || currentElement is '_' || IsAnySeparator(currentElement)))
                    return false;
            }
            if (!IsSentenceSeparator(userString[^1]))
                return false;
            return true;
        }

        static bool CheckDoubleSeparators(string userString)
        {
            for (int i = 0; i < userString.Length - 1; i++)
            {
                if ((IsSentenceSeparator(userString[i]) || IsWordSeparator(userString[i]) || userString[i] is ' ')
                    && (IsSentenceSeparator(userString[i + 1]) || IsWordSeparator(userString[i + 1])) 
                    || (userString[i] is '_' && (IsSentenceSeparator(userString[i + 1]) || IsWordSeparator(userString[i + 1]))))
                {
                    return false;
                }
            }
            return true;
        }

        static string CheckReadString(string message)
        {
            string userString = "";
            bool isStringAppropriate = false;
            do
            {
                ReadString(message, ref userString);
                if (System.String.IsNullOrEmpty(userString))
                {
                    Console.WriteLine("Строка пустая");
                    isStringAppropriate = true;
                }
                else
                {
                    if (CheckStringSeparators(userString) && CheckDoubleSeparators(userString) && !IsAnySeparator(userString[0]))
                    {
                        isStringAppropriate = true;
                        Console.WriteLine("Строка сохранена.");
                        PrintString(userString);
                    }
                    else
                    {
                        Console.WriteLine($"Введенная вами строка не удовлетворяет требованиям к строке.");
                    }
                }
            } while (!isStringAppropriate);
            return userString;
        }

        static void CreateStringUsingKeyboard(ref string userString)
        {
            string message = @"Введите строку, удовлетворяющую следующим требованиям:
строка состоит из слов, разделенных пробелами (пробелов может быть несколько подряд) и знаками препинания (, ; :);
пробелы не стоят перед знаками препинания;
в строке может быть несколько предложений, в конце каждого предложения стоит ОДИН знак препинания (. ! ?).
Не используйте в строке переносы на следующий абзац с помощью клавиши Enter.";
            userString = CheckReadString(message);
        }

        static void PrintStringsList()
        {
            Console.WriteLine("Список имеющихся строк:");
            for (int i = 0; i < testStringsArray.Length; i++)
            {
                Console.WriteLine($"Строка {i + 1}: {testStringsArray[i]}");
            }
        }

        static void PrintMenuStringsListActions(out string message)
        {
            Console.WriteLine(@"1. Выбрать одну из имеющихся строк.
2. Добавить новую строку в заданный тестовый набор строк.
3. Назад");
            message = "Введите число, соответствующее нужному вам пункту меню:";
        }

        static void ChooseStringListActions(int firstMenuPoint, int lastMenuPoint, out int stringListActionsPoint)
        {
            PrintMenuStringsListActions(out string messageMenuStringsListActions);
            stringListActionsPoint = CheckReadNumber(messageMenuStringsListActions, firstMenuPoint, lastMenuPoint);
        }

        static void ChooseStringListElement(ref string userString)
        {
            PrintStringsList();
            string message = "Выберите номер нужной вам строки из списка";
            int stringsListElement = CheckReadNumber(message, 1, testStringsArray.Length);
            userString = testStringsArray[stringsListElement-1];
        }

        static void AddNewStringsListElement(ref string userString)
        {
            if (testStringsArray.Length < Array.MaxLength)
            {
                PrintStringsList();
                CreateStringUsingKeyboard(ref userString);
                string[] temporaryArray = new string[testStringsArray.Length + 1];
                testStringsArray.CopyTo(temporaryArray, 0);
                temporaryArray[^1] = userString;
                testStringsArray = new string[temporaryArray.Length];
                temporaryArray.CopyTo(testStringsArray, 0);
                Console.WriteLine("Введенная вами строка была добавлена в заданный тестовый набор строк. Вы можете продолжить работу с ней или выбрать новую строку.");
            }
            else
            {
                Console.WriteLine("В массив не могут быть добавлены новые строки, поскольку в массиве сейчас максимальное количество элементов");
            }
        }

        static void GoToStringList(ref string userString)
        {
            PrintStringsList();
            ChooseStringListActions(1, 3, out int stringListActionsPoint);
            switch (stringListActionsPoint)
            {
                case 1:
                    ChooseStringListElement(ref userString);
                    break;
                case 2:
                    AddNewStringsListElement(ref userString);
                    break;
                default:
                    break;
            }
        }

        static void CreateString(ref string userString)
        {
            ChooseStringCreatingWaysMenuPoint(1, 3, out int stringCreatingWaysMenuPoint);
            switch (stringCreatingWaysMenuPoint)
            {
                case 1:
                    CreateStringUsingKeyboard(ref userString);
                    break;
                case 2:
                    GoToStringList(ref userString);
                    PrintString(userString);
                    break;
                default:
                    break;
            }
        }

        static void FindShortestStringIdentificators(string userString)
        {
            bool isIdentificatorsListEmpty = true;
            if (System.String.IsNullOrEmpty(userString))
            {
                Console.WriteLine("Строка пустая");
            }
            else
            {
                PrintString(userString);
                string pattern = @"[!]|[\.]|[\?]|[ ]|[,]";
                int minLength = Int32.MaxValue;
                string shortestIdentificator = "";
                //Regex regex = new Regex(pattern);
                string[] words = Regex.Split(userString, pattern);
                foreach (var word in words)
                {
                    if (!System.String.IsNullOrEmpty(word) && !Char.IsDigit(word[0]) && !(word.Substring(1, word.Length-1).Contains('_')))
                    {
                        isIdentificatorsListEmpty = false;
                        if (word.Length < minLength)
                        {
                            minLength = word.Length;
                            shortestIdentificator = word;
                        }
                        else
                        {
                            if (word.Length == minLength)
                            {
                                if (System.String.Compare(word, shortestIdentificator) < 0)
                                    shortestIdentificator = word;
                            }
                        }
                    }
                }
                if (isIdentificatorsListEmpty)
                {
                    Console.WriteLine("В данной строке нет идентификаторов.");
                }
                else
                {
                    Console.WriteLine($"Среди идентификаторов с наименьшей длиной {shortestIdentificator.Length} первый по алфавиту - {shortestIdentificator}.");
                }
            }
        }

        #endregion

        static void Main(string[] args)
        {
            int mainMenuPoint; // выбранный пункт в главном меню
            int subMenuPointTwoDimensionalArray; // выбранный пункт в меню для двумерных массивов
            int subMenuPointRaggedArray; // выбранный пункт в меню для рваных массивов
            int subMenuPointString; // выбранный пункт в меню для строк
            int[,] twoDimensionalArray = new int[0, 0]; // двумерный массив
            int[][] raggedArray = new int[0][]; // рваный массив
            string userString = ""; // строка
            Console.WriteLine("Добро пожаловать! Данная программа поможет вам провести обработку двумерных массивов, рваных массивов и строк.");
            do
            {
                ChooseMainMenuPoint(1, 4, out mainMenuPoint);
                switch (mainMenuPoint)
                {
                    case 1:
                        do
                        {
                            ChooseSubMenuPoint(mainMenuPoint, 1, 4, out subMenuPointTwoDimensionalArray);
                            switch (subMenuPointTwoDimensionalArray)
                            {
                                case 1:
                                    CreateArray(ref twoDimensionalArray);
                                    break;
                                case 2:
                                    PrintArray(twoDimensionalArray);
                                    break;
                                case 3:
                                    AddNewColumnsArrayBeginning(ref twoDimensionalArray);
                                    break;
                                default:
                                    Console.WriteLine("Вы выбрали переход обратно к главному меню");
                                    break;
                            }
                        } while (subMenuPointTwoDimensionalArray != 4);
                        break;
                    case 2:
                        do
                        {
                            ChooseSubMenuPoint(mainMenuPoint, 1, 4, out subMenuPointRaggedArray);
                            switch (subMenuPointRaggedArray)
                            {
                                case 1:
                                    CreateArray(ref raggedArray);
                                    break;
                                case 2:
                                    PrintArray(raggedArray);
                                    break;
                                case 3:
                                    DeleteEvenNumberedRows(ref raggedArray);
                                    break;
                                default:
                                    Console.WriteLine("Вы выбрали переход обратно к главному меню");
                                    break;
                            }
                        } while (subMenuPointRaggedArray != 4);
                        break;
                    case 3:
                        do
                        {
                            ChooseSubMenuPoint(mainMenuPoint, 1, 3, out subMenuPointString);
                            switch (subMenuPointString)
                            {
                                case 1:
                                    CreateString(ref userString);
                                    break;
                                case 2:
                                    FindShortestStringIdentificators(userString);
                                    break;
                                default:
                                    Console.WriteLine("Вы выбрали переход обратно к главному меню");
                                    break;
                            }
                        } while (subMenuPointString != 3);
                        break;
                    default:
                        Console.WriteLine("Вы выбрали завершение работы программы.");
                        break;
                }
            } while (mainMenuPoint != 4);
        }
    }
}

