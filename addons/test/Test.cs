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
			return new Test(method, $"starteing test: {attribute.Title}");
		}
		private Test(MethodInfo method, string debugMessage, string title = "") : base(method, debugMessage, title){}
	}
}
