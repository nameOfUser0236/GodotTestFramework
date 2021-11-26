namespace GodotTest
{
	using System;
	using System.Reflection;
	using System.Collections.Generic;
	using Godot;

	internal abstract class TestBase
	{
		public string Title;
		public readonly string DebugMessage;
		public TestBase(string title, string? debugMessage = null)
		{
			this.Title = title;
			this.DebugMessage = debugMessage ?? $"running {this.Title}";
		}

		public abstract void Run();

		public static void SafeRun(MethodInfo? method, string debugMessage = "")
		{
			#if GODOT_TESTS_DEBUG
				GD.Print(debugMessage);
			#endif
			try
			{
				method?.Invoke(null, null);
			}
			catch(TestFailedException e)
			{
				GD.PrintErr($"test {method?.Name} failed:\n{e}");
			}
			catch(Exception e)
			{
				GD.PrintErr(e);
			}
		}

		public static bool IsMethodValid(MethodInfo method) =>
				!method.IsAbstract &&
				!method.IsGenericMethod &&
				 method.IsStatic;

		public static bool HasAttribute<T>(MemberInfo member) where T : Attribute
		{
			return member.GetCustomAttribute<T>() != null;
		}
	}
}