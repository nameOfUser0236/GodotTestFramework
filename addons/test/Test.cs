namespace GodotTest
{
	using System;
	using System.Reflection;
	using System.Collections.Generic;
	internal class Test : TestMethod
	{
		public static Test Construct(MethodInfo method)
		{
			TestAttribute attribute = method.GetCustomAttribute<TestAttribute>()
				?? throw new NullReferenceException($"{nameof(method)} does not have a test attribute");
			return new Test(method, $"	starteing test: {attribute.Title}", attribute.Title);
		}
		private Test(MethodInfo method, string debugMessage, string title = "") : base(method, debugMessage, title){}

		public override void Run()
		{
			try
			{
				base.Run();
				Godot.GD.Print($"	{this.Title} succeed");
			}
			catch(TestFailedException e)
			{
				#if GODOT_TESTS_DEBUG
					Godot.GD.PrintErr($"	test {this.Title}:{this.Method.Name} failed:\n{e}");
				#else
					Godot.GD.Print($"	{this.Title} failed: {e.Message}");
				#endif
			}
			
		}
	}

	[AttributeUsage(AttributeTargets.Method)]
	public class TestAttribute : Attribute
	{
		public readonly string Title;
		public readonly object[]? Args;
		public TestAttribute(string title, params object[]? args)
		{
			Title = title;
			Args = args;
		}
	}

	public class TestFailedException : Exception
	{
		public TestFailedException(string message = "") : base(message)
		{}
	}
}
