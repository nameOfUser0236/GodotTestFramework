namespace GodotTest
{
	using System;
	using System.Reflection;
	using System.Collections.Generic;
	internal class Test : TestMethod
	{
		public readonly object?[] Args;
		public static Test Construct(MethodInfo method)
		{
			TestAttribute attribute = method.GetCustomAttribute<TestAttribute>()
				?? throw new NullReferenceException($"{nameof(method)} does not have a test attribute");
			return new Test(method, $"starteing test: {attribute.Title}", attribute.Args, attribute.Title);
		}
		private Test(MethodInfo method, string debugMessage, object?[] args, string title = "") : base(method, debugMessage, title)
		{
			this.Args = args;
		}

		public override void Run()
		{
			foreach(object? arg in this.Args)
			{
				_Run(arg);
			}
		}

		private void _Run(object? arg)
		{
			try
			{
				base.Run(arg);
				Godot.GD.Print($"{this.Title} succeed");
			}
			catch(TestFailedException e)
			{
				#if GODOT_TESTS_DEBUG
					Godot.GD.PrintErr($"test {this.Title}:{this.Method.Name} failed:\n{e}");
				#else
					Godot.GD.Print($"{this.Title} failed: {e.Message}");
				#endif
			}
			
		}
	}
}
