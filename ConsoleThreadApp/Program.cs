using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleThreadApp
{
    class Program
    {
        static object __SynchronizationObj = new object();
        static int __N;

        static void Main(string[] args)
        {
            int choice = 1;
            #region Обновление заголовка консоли в отдельном потоке
            Thread TimerThread = new Thread(ConsoleTitleTimer);
            TimerThread.IsBackground = true;
            TimerThread.Start();
            #endregion

            Console.WriteLine("Выберите запускаемую задачу.");
            Console.WriteLine("\t1. Задача считает в отдельных потоках факториал числа N и сумму целых чисел до N.");
            Console.WriteLine("\t2. Задача парсит CSV-файл и сохраняет результат в текстовый файл.");
            Console.WriteLine("Введите номер задачи (по-умолчанию 1):");

            if (!int.TryParse(Console.ReadLine(), out choice))
                choice = 1;

            switch (choice)
            {

                case 2:
                    Thread Task2Thread = new Thread(Task2) { IsBackground = true };
                    Task2Thread.Start();
                    break;

                case 1:
                default:
                    Console.WriteLine("Введите произвольное положительное целое число:");
                    while (!int.TryParse(Console.ReadLine(), out __N) || __N <= 0)
                        Console.Write("Введено некорректное значение. Повторите ввод:");

                    Thread FactorialThread = new Thread(Factorial) { IsBackground = true };
                    FactorialThread.Start();

                    Thread SummThread = new Thread(Summ) { IsBackground = true };
                    SummThread.Start();
                    break;
            }     


            Console.WriteLine("Фоновая задача запущена...");
            Console.ReadLine();
        }

        static void ConsoleTitleTimer()
        {
            while (__TimerThreadEnable)
            {
                Console.Title = DateTime.Now.ToString("HH:mm:ss.ffff");
                Thread.Sleep(10);
            }
        }

        static void Factorial()
        {
            int local_N = __N;
            long result = 1;
            lock(__SynchronizationObj)
                Console.WriteLine("Факториал числа {0} вычисляется...", __N);

            while (local_N>1)
            {
                result *= local_N--;
            }
            lock (__SynchronizationObj)
                Console.WriteLine("Результат факториала от {0} = {1}", __N, result);
        }

        static void Summ()
        {
            int local_N = __N;
            long result = 1;
            lock (__SynchronizationObj)
                Console.WriteLine("Сумма целых чисел от 1 до {0} вычисляется...", __N);

            while (local_N > 1)
            {
                result += local_N--;
            }
            lock (__SynchronizationObj)
                Console.WriteLine("Сумма целых чисел от 1 до {0} = {1}", __N, result);
        }

        static void Task2()
        {
            string csvHeader= @"""No"";""Name"";""Text"";""Number""";
            Random rnd = new Random();
            using (var file = File.CreateText("some.csv"))
            {
                file.WriteLine(csvHeader);
                for (long i = 1; i < 10000; i++)
                {
                    file.WriteLine(@"{0};""Name_{0}"";""Text_{0}"";{1}",i,rnd.Next(0,999999));
                }
            }
            Console.WriteLine("Файл csv записан");

            string line;
            List<string> listStrings = new List<string>();
            StringBuilder sb = new StringBuilder();
            using (var sr = new StreamReader("some.csv"))
            using (var file = File.CreateText("result.txt"))
            {
                while ((line= sr.ReadLine())!=null)
                {
                    listStrings.Clear();
                       listStrings.AddRange(line.Split(';'));
                    sb.Append(listStrings).Append(" ");
                    string s = sb.ToString();
                    file.WriteLine(string.Join(" ", listStrings.ToArray()));
                }

            }
            Console.WriteLine("Файл txt записан");
        }
    }
}
