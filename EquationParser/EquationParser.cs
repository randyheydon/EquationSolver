using System;
namespace EquationParser
{
	public class UnsafeEquationParser
	{
		private const string code_template = @"
			public class EquationWrapper
			{{
				public static double Equation(double x)
				{{
					return {0};
				}}
			}}";

		private System.Reflection.MethodInfo func;

		public UnsafeEquationParser(string equation)
		{
			// Dynamically builds code based on the equation, using a technique adapted from
			// https://stackoverflow.com/questions/3188882/compile-and-run-dynamic-code-without-generating-exe
			var code = string.Format(code_template, equation);
			var compiled = new Microsoft.CSharp.CSharpCodeProvider().CompileAssemblyFromSource(
				new System.CodeDom.Compiler.CompilerParameters { GenerateInMemory = true },
				code);
			func = compiled.CompiledAssembly.GetType("EquationWrapper").GetMethod("Equation");
		}

		public double Evaluate(double x)
		{
			var result = func.Invoke(null, new object[] { x });
			return Convert.ToDouble(result);
		}
	}
}
