namespace GodotTest
{
	using System;
	using System.Reflection;
	using System.Collections.Generic;
	internal class Test
	{
		public MethodInfo method;
		private TestAttribute attribute;
		public Test(MethodInfo method, TestAttribute attribute)
		{
			this.attribute = attribute;
			this.method = method;
		}

		public void Run()
		{
#if GODOT_TESTS_DEBUG
			Godot.GD.Print($" starteing test: {this.attribute.Title}");
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


		public bool IsValid =>
				!method.IsAbstract &&
				!method.IsGenericMethod &&
				 method.IsStatic;

		public string Title => attribute.Title;
		public object[]? Args => attribute.Args;
	}
}
