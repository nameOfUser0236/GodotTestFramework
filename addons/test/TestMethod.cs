namespace GodotTest
{
	using System;
	using System.Reflection;
	using System.Collections.Generic;
	using Godot;
	internal class TestMethod
	{
		public readonly string Title;
		public readonly string DebugMessage;
		public readonly MethodInfo Method;
		public TestMethod(MethodInfo method, string debugMessage, string title = "")
		{
			this.Method = method;
			this.Title = title;
			this.DebugMessage = debugMessage;
		}

		public virtual void Run()
		{
			#if GODOT_TESTS_DEBUG
				GD.Print($"	{DebugMessage}");
			#endif
			try
			{
				Method?.Invoke(null, null);
			}
			catch(TargetInvocationException e) when (e.InnerException is TestFailedException)
			{
				throw e.InnerException;
			}
			catch(Exception e)
			{
				GD.PrintErr(e);
			}
		}

		public static bool IsMethodValid(MethodInfo method) =>
			!method.IsAbstract &&
			!method.IsGenericMethod &&
			!method.IsConstructor &&
			 method.IsPublic &&
			 method.IsStatic;

		public bool IsValid => IsMethodValid(this.Method);
	}

	[AttributeUsage(AttributeTargets.Method)]
	public class TestMethodAttribute : Attribute{}
}