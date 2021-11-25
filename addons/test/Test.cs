namespace GodotTest
{
	using System;
	using System.Reflection;
	using System.Collections.Generic;
	internal class Test
	{
		public MethodInfo method;
		private TestAttribute attribute;
		public Test(MethodInfo method)
		{
			this.attribute = method.GetCustomAttribute<TestAttribute>() ?? throw new NullReferenceException($"{nameof(method)} does not have a test attribute");
			this.method = method;
		}

		public void Run()
		{
#if GODOT_TESTS_DEBUG
			Godot.GD.Print($"starteing test: {this.attribute.Title}");
#endif
			try
			{
				method.Invoke(null, null);
			}
			catch(Exception e)
			{
				Godot.GD.PrintErr(e.ToString());
			}
		}


		public bool IsValid => IsValidMethod(this.method);

		internal static bool IsValidMethod(MethodInfo method) =>
				!method.IsAbstract &&
				!method.IsGenericMethod &&
				 method.IsStatic;

		public string Title => attribute.Title;
		public object[]? Args => attribute.Args;
	}
}
