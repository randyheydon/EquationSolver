using System;

namespace EquationSolver
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			var outfunc = NewtonSolver.NewtonSolver.FiniteDifference(Math.Sin, 0.0000001);
			Console.WriteLine(outfunc(Math.PI).ToString());
		}
	}
}
