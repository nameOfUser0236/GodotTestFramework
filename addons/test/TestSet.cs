namespace GodotTest
{
	using System;
	using System.Reflection;
	using System.Collections.Generic;
	internal class TestGroup
	{
		private Type _type;
		public readonly string title;
		//private TestGroupAttribute _attribute;
		private TestGroup(Type type)
		{
			this._type = type;
			TestGroupAttribute? testGroupAttribute = type.GetCustomAttribute<TestGroupAttribute>();
			this.title = testGroupAttribute?._title ?? "";
		}

		public void RunAllTests()
		{
#if GODOT_TESTS_DEBUG
			Godot.GD.Print($"starting test group: {this.title}");
#endif
			foreach(Test test in GetTests())
			{
				test.Run();
			}
		}

		public static void RunAllGroups()
		{
			foreach(TestGroup testGroup in GetTestGroups())
			{
				testGroup.RunAllTests();
			}
		}

		public Test[] GetTests()
		{
			List<Test> tests = new();
			foreach(MethodInfo methodInfo in this._type.GetMethods())
			{
				TestAttribute? testAttribute = methodInfo.GetCustomAttribute<TestAttribute>();
				if(testAttribute != null)
				{
					Test test = new Test(methodInfo, testAttribute);
					if(!test.IsValid)
					{
#if GODOT_TESTS_DEBUG
					Godot.GD.PrintErr($"test: {test.Title}, is not valid");
#endif
					continue;
					}
					tests.Add(test);
				}
			}
			return tests.ToArray();
		}

		public static TestGroup[] GetTestGroups()
		{
			List<TestGroup> testGroups = new();
			Assembly assembly = Assembly.GetExecutingAssembly();
			foreach(Type type in assembly.GetTypes())
			{
				if(type.GetCustomAttribute<TestGroupAttribute>() != null)
				{
					testGroups.Add(new TestGroup(type));
				}
			}
			return testGroups.ToArray();
		}
	}
}