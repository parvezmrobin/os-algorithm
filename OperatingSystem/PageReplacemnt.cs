using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace OperatingSystem
{
    public static class PageReplacemnt
    {
        public static int FiFo(int pageFrameCount)
        {
            var pageFrames = new Queue<int>();
            var count = 0;
            while (true)
            {
                Console.Write("Page Number: ");
                var input = Console.ReadLine();
                var requestedPage = int.Parse(input == ""
                    ? ShowInvalidArgumentWarning()
                    : input ?? throw new InvalidOperationException());
                if (requestedPage == -1) break;
                if (pageFrames.Contains(requestedPage))
                {
                    Console.WriteLine(requestedPage + " already exists");
                }
                else if (pageFrames.Count < pageFrameCount)
                {
                    Console.WriteLine("Loading " + requestedPage);
                    count++;
                    pageFrames.Enqueue(requestedPage);
                }
                else
                {
                    var oldPage = pageFrames.Dequeue();
                    Console.WriteLine("Replacing " + oldPage + " by " + requestedPage);
                    count++;
                    pageFrames.Enqueue(requestedPage);
                }
            }

            ShowMemoryStatus(pageFrames.AsEnumerable());

            return count;
        }

        public static int SecondChance(int pageFrameCount)
        {
            var pageFrames = new List<int>();
            var rBits = new List<int>();
            var pageFaultCount = 0;
            while (true)
            {
                Console.Write("Page Number: ");

                var requestedPage = int.Parse(Console.ReadLine() ?? ShowInvalidArgumentWarning());
                if (requestedPage == -1) break;

                if (pageFrames.Contains(requestedPage))
                {
                    Console.WriteLine(requestedPage + " already Exists");
                }
                else if (pageFrames.Count < pageFrameCount)
                {
                    Console.WriteLine("Loading " + requestedPage);
                    pageFrames.Add(requestedPage);
                    pageFaultCount++;
                }
                else
                {
                    pageFaultCount++;
                    rBits.Clear();
                    Console.Write("R bits: ");
                    var rBitsStr = Console.ReadLine()?.Split(' ');
                    Debug.Assert(rBitsStr != null, nameof(rBitsStr) + " != null");

                    for (var i = 0; i < pageFrameCount; i++)
                    {
                        try
                        {
                            rBits.Add(int.Parse(rBitsStr[i]));
                        }
                        catch (IndexOutOfRangeException)
                        {
                            ShowInvalidArgumentWarning();
                            break;
                        }
                    }

                    for (var i = 0; i < pageFrameCount; i++)
                    {
                        if (rBits[i] == 1)
                        {
                            rBits.RemoveAt(i);
                            rBits.Add(0);
                            var p = pageFrames[i];
                            pageFrames.RemoveAt(i);
                            pageFrames.Add(p);
                            i--;
                        }
                        else
                        {
                            Console.WriteLine("Replacing " + pageFrames[i] + " by " + requestedPage);
                            pageFrames[i] = requestedPage;
                            break;
                        }
                    }
                }
            }
            ShowMemoryStatus(pageFrames);
            return pageFaultCount;
        }

        public static int NotRecentlyUsed(int pageFrameCount)
        {
            var pageFrames = new List<int>();
            var rBits = new List<int>();
            var mBits = new List<int>();
            var count = 0;
            while (true)
            {
                Console.Write("Page Numbe: ");

                var requestedPage = int.Parse(Console.ReadLine() ?? ShowInvalidArgumentWarning());
                if (requestedPage == -1)
                    break;
               

                if (pageFrames.Contains(requestedPage))
                {
                    Console.WriteLine(requestedPage + " already exists");
                }
                else if (pageFrames.Count < pageFrameCount)
                {
                    Console.WriteLine("Loading " + requestedPage);
                    pageFrames.Add(requestedPage);
                    count++;
                }
                else
                {
                    rBits.Clear();
                    mBits.Clear();
                    Console.Write("R bits: ");
                    var rBitsStr = Console.ReadLine()?.Split(' ');
                    Console.Write("M bits: ");
                    var mBitsStr = Console.ReadLine()?.Split(' ');
                    Debug.Assert(rBitsStr != null, nameof(rBitsStr) + " != null");
                    Debug.Assert(mBitsStr != null, nameof(mBitsStr) + " != null");

                    for (var i = 0; i < pageFrameCount; i++)
                    {
                        try
                        {
                            rBits.Add(int.Parse(rBitsStr[i]));
                            mBits.Add(int.Parse(mBitsStr[i]));
                        }
                        catch (IndexOutOfRangeException)
                        {
                            ShowInvalidArgumentWarning();
                            break;
                        }
                    }
                    var min = rBits[0] + mBits[0];
                    var minIndex = 0;
                    for (var i = 1; i < pageFrames.Count; i++)
                    {
                        if (rBits[i] + mBits[i] >= min) continue;
                        min = rBits[i] + mBits[i];
                        minIndex = i;
                    }

                    Console.WriteLine("Replacing " + pageFrames[minIndex] + " by " + requestedPage);
                    pageFrames[minIndex] = requestedPage;

                    count++;
                }
            }

            ShowMemoryStatus(pageFrames);

            return count;
        }

        public static int Clock(int pageFrameCount)
        {
            var pageFrames = new List<int>();
            var rBits = new List<int>();
            var count = 0;
            var index = -1;

            while (true)
            {
                Console.Write("Page Number");

                var requestedPage = int.Parse(Console.ReadLine() ?? ShowInvalidArgumentWarning());
                if (requestedPage == -1)
                    break;

                if (pageFrames.Contains(requestedPage))
                {
                    Console.WriteLine(requestedPage + " already exists");
                }
                else if (pageFrames.Count < pageFrameCount)
                {
                    Console.WriteLine("Loading " + requestedPage);
                    pageFrames.Add(requestedPage);
                    count++;
                }
                else
                {
                    rBits.Clear();
                    Console.Write("R bits: ");
                    var rBitsStr = Console.ReadLine()?.Split(' ');
                    Debug.Assert(rBitsStr != null, nameof(rBitsStr) + " != null");

                    for (var i = 0; i < pageFrameCount; i++)
                    {
                        try
                        {
                            rBits.Add(int.Parse(rBitsStr[i]));
                        }
                        catch (IndexOutOfRangeException)
                        {
                            ShowInvalidArgumentWarning();
                            break;
                        }
                    }
                    for (var i = index + 1; i != index; i++)
                    {
                        if (i == pageFrames.Count) i = 0;
                        if (rBits[i] == 0)
                        {
                            Console.WriteLine("Replacing " + pageFrames[i] + " by " + requestedPage);
                            pageFrames[i] = requestedPage;
                            index = i;
                            break;
                        }
                        rBits[i] = 0;
                    }

                    count++;
                }
            }

            ShowMemoryStatus(pageFrames);

            return count;
        }

        private static string ShowInvalidArgumentWarning()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid input. Exiting...");
            Console.ResetColor();

            return "-1";
        }

        private static void ShowMemoryStatus(IEnumerable<int> list)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("Memory Status: [");
            foreach (var item in list)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine("\b]");
            Console.ResetColor();
        }
    }
}