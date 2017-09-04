using NUnit.Framework;
using System;

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
			NewtonSolver.NewtonSolver.MathFunc diff;
			diff = NewtonSolver.NewtonSolver.FiniteDifference((x) => x * x - x);
			Assert.That(IsClose(diff(0.5), 0));
			Assert.That(IsClose(diff(0.6), 0.2));
			Assert.That(IsClose(diff(0), -1));
			diff = NewtonSolver.NewtonSolver.FiniteDifference(Math.Sin, 0.0000001);
			Assert.That(IsClose(diff(0), 1));
			Assert.That(IsClose(diff(Math.PI/2), 0));
			Assert.That(IsClose(diff(Math.PI), -1));
		}
	}
}
