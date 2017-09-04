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
		public static MathFunc FiniteDifference(MathFunc function, double epsilon)
		{
			return (x) => (function(x + epsilon / 2.0) - function(x-epsilon/2.0)) / epsilon;
		}

		// If epsilon is unspecified, use default value.
		public static MathFunc FiniteDifference(MathFunc function)
		{
			return FiniteDifference(function, 0.0001);
		}
	}
}
