namespace GodotTest
{
	using System;
	using System.Reflection;
	using System.Collections.Generic;
	using Godot;
	internal class TestMethod : TestBase
	{
		public readonly MethodInfo Method;
		public TestMethod(MethodInfo method, string runMessage, string title = "") : base(title, runMessage)
		{
			this.Method = method;
		}

		public override void Run()
		{
			SafeRun(this.Method, this.DebugMessage);
		}

		public bool IsValid => IsMethodValid(this.Method);
	}
}