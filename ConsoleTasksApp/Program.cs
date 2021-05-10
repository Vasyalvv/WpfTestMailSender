using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleTasksApp
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] A = new int[100,100];
            int[,] B = new int[100, 100];
            int[,] Result = new int[100, 100];

            GenerateArray( A);
            GenerateArray( B);

            Parallel.For(0, A.GetLength(0), new ParallelOptions { MaxDegreeOfParallelism=10},
                (i) =>
                {
                    Console.WriteLine("i={0,2}\tThread ID:{1,2}",i,Thread.CurrentThread.ManagedThreadId);
                    for (int j = 0; j < B.GetLength(1); j++)
                    {
                        for (int k = 0; k < A.GetLength(1); k++)
                        {
                            Result[i, j] += A[i, k] * B[k, j];
                        }
                    }
                });


            Console.ReadLine();
        }

        static void GenerateArray( int[,] Arr)
        {
            Random rnd = new Random();
            for(int i=0;i<Arr.GetLength(0);i++)
            {
                for (int j = 0; j < Arr.GetLength(1); j++)
                {
                    Arr[i, j] = rnd.Next(0, 10);
                }
            }
        }
    }
}
