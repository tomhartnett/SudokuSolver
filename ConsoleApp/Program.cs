using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Begin program.");

            Puzzle puzzle = new Puzzle("Puzzle1.txt");
            puzzle.Display();

            DateTime start = DateTime.Now;
            puzzle.Solve();
            TimeSpan elapsed = DateTime.Now.Subtract(start);

            Console.WriteLine("{0} solutions found in {1} seconds.", puzzle.Solutions.Count, elapsed.TotalSeconds);

            string solution = puzzle.Solutions[0];
            Console.WriteLine(solution.Substring(0, 9));
            Console.WriteLine(solution.Substring(9, 9));
            Console.WriteLine(solution.Substring(18, 9));
            Console.WriteLine(solution.Substring(27, 9));
            Console.WriteLine(solution.Substring(36, 9));
            Console.WriteLine(solution.Substring(45, 9));
            Console.WriteLine(solution.Substring(54, 9));
            Console.WriteLine(solution.Substring(63, 9));
            Console.WriteLine(solution.Substring(72, 9));

            Console.WriteLine("End program.");
        }
    }
}
