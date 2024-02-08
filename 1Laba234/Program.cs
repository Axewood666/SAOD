using System;

struct StackItem
{
    public int Value;
}

class Program
{
    
    static StackItem[] mainStack = new StackItem[0];
    static StackItem[] secondStack = new StackItem[0];
    static int mainTop = -1;
    static int secondTop = -1;
    

    static void Main(string[] args)
    {
        Menu();
    }

    static bool IsEmpty()
    {
        return mainTop == -1;
    }

    static bool IsSecondEmpty()
    {
        return secondTop == -1;
    }

    static void ShowStack(StackItem[] stack, int top)
    {
        if (top != -1)
        {
            Console.WriteLine();
            for (int i = 0; i <= top; i++)
            {
                Console.WriteLine($"{top - i + 1}) {stack[i].Value}");
            }
        }
        else
        {
            Console.WriteLine("Стек пуст");
        }
    }

    static void Push(ref StackItem[] stack, ref int top, int value)
    {
        if (top + 1 >= stack.Length)
        {
            Array.Resize(ref stack, stack.Length + 1);
        }
        stack[++top].Value = value;
    }

    static int Pop(ref StackItem[] stack, ref int top)
    {
        if (top != -1)
        {
            int value = stack[top].Value;
            if (top > 0)
            {
                Array.Resize(ref stack, stack.Length - 1);
            }
            top--;
            return value;
        }
        else
        {
            return -1;
        }
    }

    static void PushRand()
    {
        Console.Write("Сколько чисел добавить? ");
        int rcount = checkInt();
        Random rand = new Random();
        for (int i = 0; i < rcount; i++)
        {
            Push(ref mainStack, ref mainTop, rand.Next());
        }
    }

    static void MoveToSecondStack()
    {
        if (mainTop != -1)
        {
            int value = Pop(ref mainStack, ref mainTop);
            Push(ref secondStack, ref secondTop, value);
        }
    }

    static void AddFromSecond()
    {
        if (secondTop != -1)
        {
            int value = Pop(ref secondStack, ref secondTop);
            Push(ref mainStack, ref mainTop, value);
        }
    }

    static void Menu()
    {
        int number, value, addMethod;
        while (true)
        {
            Console.WriteLine("========================================================");
            Console.WriteLine("\nВведите число:");
            Console.WriteLine("1. Вывести главный стек");
            Console.WriteLine("2. Добавить элемент");
            Console.WriteLine("3. Удалить элемент");
            Console.WriteLine("4. Заполнить рандомными числами");
            Console.WriteLine("5. Вывести вспомогательный стек");
            Console.WriteLine("0. Завершить работу программы");
            Console.WriteLine("========================================================");
            Console.Write("Ваш выбор: ");
            number = checkInt();

            switch (number)
            {
                case 1:
                    Console.WriteLine("Главный стек: ");
                    ShowStack(mainStack, mainTop);
                    break;
                case 2:
                    Console.WriteLine("Уточните откуда взять элемент: \n 1. Ручной ввод \n 2. С вершины вспомогательного стека \n Ваш Выбор: ");
                    addMethod = checkInt();
                    if (addMethod == 1)
                    {
                        Console.Write("Введите целое число: ");
                        value = checkInt();
                        Push(ref mainStack, ref mainTop, value);
                    }
                    else if (addMethod == 2)
                    {
                        if (!IsSecondEmpty()) AddFromSecond();
                        else Console.WriteLine("Вспомогательный стек пуст");
                    }
                    else
                    {
                        Console.WriteLine("Ошибка ввода");
                    }
                    break;
                case 3:
                    if (!IsEmpty())
                    {
                        Console.WriteLine("Уточните команду: \n 1. Действительно удалить элемент с полным освобождением памяти \n 2. Включить его в вершину вспомогательного стека удаленных элементов\nВаш Выбор: ");
                        addMethod = checkInt();
                        if (addMethod == 1)
                        {
                            value = Pop(ref mainStack, ref mainTop);
                            if (value != -1)
                            {
                                Console.WriteLine($"Элемент '{value}' удален");
                            }
                            else
                            {
                                Console.WriteLine("Стек пуст.");
                            }
                        }
                        else if (addMethod == 2)
                        {
                            MoveToSecondStack();
                        }
                        else
                        {
                            Console.WriteLine("Ошибка ввода. Повторите ввод команды");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Стек пуст. Удалять нечего");
                    }
                    break;
                case 4:
                    PushRand();
                    break;
                case 5:
                    Console.WriteLine("Состояние вспомогательного стека удаленных элементов: ");
                    ShowStack(secondStack, secondTop);
                    break;
                case 0:
                    return;
                default:
                    Console.WriteLine("Ошибка ввода. Повторите ввод команды");
                    break;
            }
        }
    }

    static int checkInt()
    {
        int result;
        while (!int.TryParse(Console.ReadLine(), out result))
        {
            Console.WriteLine("\nОШИБКА ВВОДА. ПОВТОРИТЕ ВВОД");
        }
        return result;
    }
}
