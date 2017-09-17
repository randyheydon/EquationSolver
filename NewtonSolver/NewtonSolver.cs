using System;
namespace NewtonSolver
{
	public static class NewtonSolver
	{
		// Everything here deals with univariate mathematical functions (i.e. functions that
		// take one number and return another).  This delegate gives the type signature of
		// such functions.
		public delegate double MathFunc(double x);

		// Create an approximate derivative function of a given function, using a difference
		// of size epsilon.
		// Calculating analytic derivatives is a pain without a proper computer algebra
		// system.  A numerical approximation with the finite difference method is much
		// easier.
		public static MathFunc FiniteDifference(MathFunc function, double epsilon = 0.0001)
		{
			return (x) => (function(x + epsilon / 2.0) - function(x-epsilon/2.0)) / epsilon;
		}

		// Use Newton's method to find a root of a given function with a given derivative
		// function (also known as a Jacobian) and a given starting guess.
		// TODO Raise exception or return something else if it fails to converge.
		public static double NewtonSolve(MathFunc function, MathFunc jacobian, double guess)
		{
			double x = guess;
			// Limit iterations, since some cases will be unsolvable.
			// TODO Make the iteration limit configurable.
			for (int i = 0; i < 1000; i++)
			{
				// Check if we are sufficiently close to the solution yet.
				// TODO Make permissible error configurable.
				double y = function(x);
				if (Math.Abs(y) < 1e-6)
				{
					return x;
				}
				// If it's not solved yet, apply one step Newton's method and check again.
				x = x - y / jacobian(x);
			}
			// TODO If the iteration limit is reached, return or raise something meaningful.
			return -1e99;
		}

		// If no Jacobian is given, use FiniteDifference to generate an approximation.
		public static double NewtonSolve(MathFunc function, double guess)
		{
			return NewtonSolve(function, FiniteDifference(function), guess);
		}
	}
}
