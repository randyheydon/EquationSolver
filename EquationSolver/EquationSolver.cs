using System;
using static NewtonSolver.NewtonSolver;

namespace EquationSolver
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			var solution = NewtonSolve((x) => x * x * x + 5 * x * x - 17 * x + 7, 0.0);
			Console.WriteLine(solution.ToString());
		}
	}
}
