using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperatingSystem
{
    public static class Deadlock
    {
        public static void MultipleResource()
        {
            Console.Write("Number of resources: ");
            var numResource = int.Parse(Console.ReadLine());
            Console.Write("Number of processes: ");
            var numProcess = int.Parse(Console.ReadLine());

            var E = new int[numResource];
            var A = new int[numResource];
            var C = new int[numProcess][];
            var R = new int[numProcess][];

            TakeInput(numProcess, C, numResource, R, E, A);

            var marked = new bool[numProcess];
            for (var i = 0; i < numProcess; i++)
            {
                if (marked[i]) continue;
                var matched = true;
                for (var j = 0; j < numResource; j++)
                {
                    if (R[i][j] <= A[j]) continue;
                    matched = false;
                    break;
                }

                if (!matched) continue;
                for (var j = 0; j < numResource; j++)
                {
                    A[j] += C[i][j];
                }
                marked[i] = true;
                i = -1;
            }

            var deadLocked = false;
            for (var i = 0; i < numProcess; i++)
            {
                if (marked[i]) continue;
                Console.Write(i + " ");
                deadLocked = true;
            }

            Console.WriteLine(deadLocked ? "create a deadlock" : "No deadlock found");
        }

        
        private static void TakeInput(
            int numProcess,
            IList<int[]> c,
            int numResource,
            IList<int[]> r,
            IList<int> e,
            IList<int> a
        )
        {
            for (var i = 0; i < numProcess; i++)
            {
                c[i] = new int[numResource];
                r[i] = new int[numResource];
            }

            Console.Write("Resource Existing Vector: ");
            var inputStr = Console.ReadLine().Split(' ');
            for (int i = 0; i < numResource; i++)
            {
                e[i] = int.Parse(inputStr[i]);
            }

            for (var i = 0; i < numResource; i++)
            {
                a[i] = e[i];
            }
 
            Console.WriteLine("Current Allocation Matrix: ");
            for (var i = 0; i < numProcess; i++)
            {
                
                inputStr = Console.ReadLine().Split(' ');
                for (var j = 0; j < numResource; j++)
                {
                    c[i][j] = int.Parse(inputStr[j]);
                    a[j] -= c[i][j];
                }
            }

            Console.WriteLine("Request Matrix: ");
            for (var i = 0; i < numProcess; i++)
            {
                inputStr = Console.ReadLine().Split(' ');
                for (var j = 0; j < numResource; j++)
                {
                    r[i][j] = int.Parse(inputStr[j]);
                }
            }
        }

        
    }
}