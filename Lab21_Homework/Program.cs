using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading; //!!!

namespace Lab21
{
    class Program
    {
        /*Имеется пустой участок земли (двумерный массив) и план сада, который необходимо реализовать. 
         * Эту задачу выполняют два садовника, которые не хотят встречаться друг с другом. 
         * Первый садовник начинает работу с верхнего левого угла сада и перемещается слева направо, сделав ряд, он спускается вниз. 
         * Второй садовник начинает работу с нижнего правого угла сада и перемещается снизу вверх, сделав ряд, он перемещается влево. 
         * Если садовник видит, что участок сада уже выполнен другим садовником, он идет дальше. Садовники должны работать параллельно. 
         * Создать многопоточное приложение, моделирующее работу садовников.
         * */

        static int[,] field;
        static int width;
        static int height;
        static object locker = new object();

        static void Main()
        {
            Console.Write("Введите размер садового поля по вертикали: ");
            height = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите размер садового поля по горизонтали: ");
            width = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();

            field = new int[height, width];

            ThreadStart threadStart = new ThreadStart(Gardner1);
            Thread thread = new Thread(threadStart);
            thread.Start();
            Gardner2();

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Console.Write($"{field[i, j]} \t");
                }
                Console.WriteLine();
            }
            Console.ReadLine();
        }
        private static void Gardner1()
        {
            lock (locker)
            {
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        if (field[i, j] == 0)
                            field[i, j] = 1;
                        Thread.Sleep(1);
                    }
                }
            }
        }
        private static void Gardner2()
        {
            for (int i = width - 1; i >= 0; i--)
            {
                for (int j = height - 1; j >= 0; j--)
                {
                    if (field[j, i] == 0)
                        field[j, i] = 2;
                    Thread.Sleep(1);
                }
            }
        }
    }
}