using System;
using ClassLibrary1;
using Laba10;
using MyCollectionNamespace;

namespace Laba13
{
    class Program
    {
        static MyObservableCollection<ControlElement> collection1 = new MyObservableCollection<ControlElement>();
        static MyObservableCollection<ControlElement> collection2 = new MyObservableCollection<ControlElement>();
        static Journal journal1 = new Journal();
        static Journal journal2 = new Journal();

        static void Main()
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("1. Добавить элемент в коллекцию 1");
                Console.WriteLine("2. Добавить элемент в коллекцию 2");
                Console.WriteLine("3. Удалить элемент из коллекции 1");
                Console.WriteLine("4. Обновить элемент в коллекции 2");
                Console.WriteLine("5. Вывести журналы");
                Console.WriteLine("6. Выйти");
                Console.Write("Выберите действие: ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        AddElement(collection1);
                        break;
                    case "2":
                        AddElement(collection2);
                        break;
                    case "3":
                        RemoveElement(collection1);
                        break;
                    case "4":
                        UpdateElement(collection2);
                        break;
                    case "5":
                        PrintJournals();
                        break;
                    case "6":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Некорректный выбор. Пожалуйста, выберите существующее действие.");
                        break;
                }

                Console.WriteLine();
            }
        }

        static void AddElement(MyObservableCollection<ControlElement> collection)
        {
            Console.Write("Введите название элемента: ");
            string name = Console.ReadLine();
            Console.Write("Введите координату X: ");
            double x = double.Parse(Console.ReadLine());
            Console.Write("Введите координату Y: ");
            double y = double.Parse(Console.ReadLine());

            collection.AddPoint(new ControlElement(name, x, y, collection.Count + 1));
        }

        static void RemoveElement(MyObservableCollection<ControlElement> collection)
        {
            if (collection.Count > 0)
            {
                Console.Write("Введите номер элемента, который хотите удалить: ");
                int index = int.Parse(Console.ReadLine());

                if (index >= 1 && index <= collection.Count)
                {
                    collection.RemoveData(collection[index - 1]);
                }
                else
                {
                    Console.WriteLine("Некорректный номер элемента.");
                }
            }
            else
            {
                Console.WriteLine("Коллекция пуста.");
            }
        }

        static void UpdateElement(MyObservableCollection<ControlElement> collection)
        {
            if (collection.Count > 1)
            {
                Console.Write("Введите номер элемента, который хотите обновить: ");
                int index = int.Parse(Console.ReadLine());

                if (index >= 1 && index <= collection.Count)
                {
                    Console.Write("Введите новое название элемента: ");
                    string name = Console.ReadLine();
                    Console.Write("Введите новую координату X: ");
                    double x = double.Parse(Console.ReadLine());
                    Console.Write("Введите новую координату Y: ");
                    double y = double.Parse(Console.ReadLine());

                    collection[index - 1] = new ControlElement(name, x, y, collection.Count + 1);
                }
                else
                {
                    Console.WriteLine("Некорректный номер элемента.");
                }
            }
            else
            {
                Console.WriteLine("В коллекции недостаточно элементов для обновления.");
            }
        }

        static void PrintJournals()
        {
            Console.WriteLine("Journal 1:");
            Console.WriteLine(journal1);

            Console.WriteLine("Journal 2:");
            Console.WriteLine(journal2);
        }
    }
}
