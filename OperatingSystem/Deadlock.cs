using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperatingSystem
{
    public static class Deadlock
    {
        #region Multiple Instance

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

        #endregion

        #region Single Resource

        public static void SingleResorce()
        {
            Console.Write("Number of resource: ");
            var nResource = int.Parse(Console.ReadLine());
            Console.Write("Number of Process: ");
            var nProcess = int.Parse(Console.ReadLine());
            var n = nResource + nProcess;
            var matrix = new bool[n][];
            Console.WriteLine("Input process resrource matrix: ");
            for (var i = 0; i < n; i++)
            {
                matrix[i] = new bool[n];
                var input = Console.ReadLine().Split(' ');

                for (var j = 0; j < n; j++)
                {
                    if (input[j].StartsWith("1")) input[j] = "true";
                    else input[j] = "false";
                    matrix[i][j] = bool.Parse(input[j]);
                }
            }

            var l = new List<int>();

            var res = false;
            for (var i = 0; i < n; i++)
            {
                l.Add(i);
                var marked = new bool[n];
                for (int j = 0; j < n; j++)
                {
                    if (matrix[i][j])
                        res = RecursiveSearch(matrix, ref l, ref marked, n, j);
                }

                if (res) break;
                l.Remove(i);
            }

            if (res) ShowOutput(l, nResource);
            else Console.WriteLine("No deadlock found");
        }

        private static bool RecursiveSearch(
            IReadOnlyList<bool[]> matrix,
            ref List<int> l,
            ref bool[] marked,
            int count, int currentNode)
        {
            if (l.Contains(currentNode)) return true;

            l.Add(currentNode);
            marked[currentNode] = true;

            for (var j = 0; j < count; j++)
            {
                if (marked[j] || !matrix[currentNode][j]) continue;
                if (RecursiveSearch(matrix, ref l, ref marked, count, j))
                {
                    return true;
                }
            }
            l.Remove(currentNode);
            return false;
        }

        private static void ShowOutput(IEnumerable<int> l, int nResource)
        {
            var r = new List<int>();
            var p = new List<int>();
            foreach (var rp in l)
            {
                if (rp < nResource)
                {
                    r.Add(rp);
                }
                else
                {
                    p.Add(rp);
                }
            }
            Console.Write("Resources: ");
            foreach (var rr in r)
            {
                Console.Write(rr + " ");
            }

            Console.Write("\nProcesses: ");
            foreach (var pp in p)
            {
                Console.Write(pp + " ");
            }
            Console.WriteLine();
        }

        #endregion
    }
}