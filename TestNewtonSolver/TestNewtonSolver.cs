using NUnit.Framework;
using System;
using static NewtonSolver.NewtonSolver;

namespace TestNewtonSolver
{
	[TestFixture()]
	public class Test
	{
		// Helper function to check if calculations are correct within floating point error.
		public static bool IsClose(double a, double b)
		{
			return Math.Abs(a - b) < 1e-8;
		}

		[Test()]
		public void TestFiniteDifference()
		{
			MathFunc diff;
			diff = FiniteDifference((x) => x * x - x);
			Assert.That(IsClose(diff(0.5), 0));
			Assert.That(IsClose(diff(0.6), 0.2));
			Assert.That(IsClose(diff(0), -1));
			diff = FiniteDifference(Math.Sin, 0.0000001);
			Assert.That(IsClose(diff(0), 1));
			Assert.That(IsClose(diff(Math.PI/2), 0));
			Assert.That(IsClose(diff(Math.PI), -1));
		}

		[Test()]
		public void TestNewtonSolve()
		{
			Assert.That(IsClose(NewtonSolve(Math.Sin, Math.Cos, 1.0), 0));
			Assert.That(IsClose(NewtonSolve(Math.Sin, Math.Cos, 3.0), Math.PI));
			Assert.That(IsClose(NewtonSolve(Math.Sin, Math.Cos, 7.0), 2*Math.PI));
			Assert.That(IsClose(NewtonSolve((x) => x * x - x, 0.3), 0));
			Assert.That(IsClose(NewtonSolve((x) => x * x - x, 0.6), 1));
			Assert.That(IsClose(NewtonSolve((x) => x * x - x, 2.0), 1));
			Assert.That(IsClose(NewtonSolve((x) => x * x - x, -1.0), 0));
		}

		[Test()]
		public void TestNewtonSolveFailures()
		{
			// Equation with no real roots.
			Assert.Throws(typeof(NewtonSolver.SolveFailureException),
						  () => NewtonSolve((x) => x * x + 2, (x) => 2 * x, 1));
			// Bad guess makes solution impossible.
			Assert.Throws(typeof(NewtonSolver.SolveFailureException),
						  () => NewtonSolve((x) => x * x - 2, (x) => 2 * x, 0));
		}
	}
}
