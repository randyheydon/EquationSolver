using System;
namespace NewtonSolver
{
	public class SolveFailureException : Exception
	{
		public SolveFailureException(string message) : base(message) { }
	}

	public static class NewtonSolver
	{
		// Everything here deals with univariate mathematical functions (i.e. functions that
		// take one number and return another).  This delegate gives the type signature of
		// such functions.
		public delegate double MathFunc(double x);

		/// <summary>
		/// Create an approximate derivative function (a.k.a. Jacobian) of a given function.
		/// </summary>
		/// <remarks>
		/// Calculating analytic derivatives is a pain without a proper computer algebra
		/// system.  A numerical approximation with the finite difference method is much
		/// easier.
		/// </remarks>
		/// <returns>A MathFunc representing the approximated derivative function.</returns>
		/// <param name="function">The MathFunc to differentiate.</param>
		/// <param name="epsilon">
		/// The size of the finite difference.  Smaller values will give results closer to
		/// the analytic derivative, but with more risk of numerical error.
		/// </param>
		public static MathFunc FiniteDifference(MathFunc function, double epsilon = 0.0001)
		{
			return (x) => (function(x + epsilon / 2.0) - function(x-epsilon/2.0)) / epsilon;
		}

		/// <summary>
		/// Finds a root of the given function (i.e. the input needed to set the function to
		/// zero) using Newton's method.
		/// </summary>
		/// <returns>A value that will set the given function to approximately zero.</returns>
		/// <param name="function">The MathFunc to be solved.</param>
		/// <param name="jacobian">
		/// The derivative of the given function.  Also known as the Jacobian of the
		/// function.
		/// </param>
		/// <param name="guess">
		/// Starting point for the solving process.  The closer it is to a solution, the
		/// quicker the solution process will be.
		/// </param>
		/// <param name="maxerr">
		/// Maximum acceptable error in the solution.  The final result of the input
		/// function must be zero plus or minus this error value.
		/// </param>
		/// <param name="maxiters">
		/// Maximum number of iterations to perform before giving up.  Newton's method is
		/// an iterative process, but it can't always find a solution, so it must be
		/// allowed to fail eventually.
		/// </param>
		/// <exception cref="SolveFailureException">
		/// Thrown if a solution cannot be found to the desired error level within the
		/// desired number of iterations.
		/// </exception>
		public static double NewtonSolve(MathFunc function, MathFunc jacobian, double guess,
		                                 double maxerr = 1e-6, int maxiters = 1000)
		{
			double x = guess;
			// Limit iterations, since some cases will be unsolvable.
			for (int i = 0; i < maxiters; i++)
			{
				// Check if we are sufficiently close to the solution yet.
				double y = function(x);
				if (Math.Abs(y) < maxerr)
				{
					return x;
				}
				// If it's not solved yet, apply one step Newton's method and check again.
				x = x - y / jacobian(x);
			}
			// If the iteration limit is reached without getting within the error limit,
			// throw a custom exception.
			throw new SolveFailureException("Could not find solution");
		}

		/// <summary>
		/// Find a root of the given function using Newton's method, with a derivative
		/// approximated using the FiniteDifference method.
		/// </summary>
		/// <returns>A value that will set the given function to approximately zero.</returns>
		/// <param name="function">The MathFunc to be solved.</param>
		/// <param name="guess">
		/// Starting point for the solving process.  The closer it is to a solution, the
		/// quicker the solution process will be.
		/// </param>
		/// <exception cref="SolveFailureException">
		/// Thrown if a solution cannot be found.
		/// </exception>
		public static double NewtonSolve(MathFunc function, double guess)
		{
			return NewtonSolve(function, FiniteDifference(function), guess);
		}
	}
}
