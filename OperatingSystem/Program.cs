using System;

namespace OperatingSystem
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
//            Console.WriteLine("Choose Method");
//            Console.WriteLine("1. FiFo");
//            Console.WriteLine("2. Second Chance");
//            Console.WriteLine("3. Not Recently Used");
//            Console.WriteLine("4. Clock");
//
//            var selection = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
//            Console.Write("Number of Page Frames: ");
//            var pageFrames = int.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
//
//            int count;
//
//            switch (selection)
//            {
//                case 1:
//                    count = PageReplacemnt.FiFo(pageFrames);
//                    break;
//                case 2:
//                    count = PageReplacemnt.SecondChance(pageFrames);
//                    break;
//                case 3:
//                    count = PageReplacemnt.NotRecentlyUsed(pageFrames);
//                    break;
//                case 4:
//                    count = PageReplacemnt.Clock(pageFrames);
//                    break;
//                default:
//                    Console.WriteLine("Invalid selection");
//                    return;
//            }
//            Console.WriteLine("Total " + count + " page fault occured");
//            Console.WriteLine();
//            Console.ForegroundColor = ConsoleColor.DarkBlue;
//            Console.Write("Press any key to exit...");

            Deadlock.MultipleResource();
            Console.ReadKey();
        }
    }
}