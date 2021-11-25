namespace GodotTest
{
	using System;
	using System.Reflection;
	using System.Collections.Generic;
	internal class TestGroup
	{
		private Type _type;
		public readonly string Title;
		public readonly MethodInfo? Setup;
		public readonly MethodInfo? TearDown;
		//private TestGroupAttribute _attribute;
		private TestGroup(Type type)
		{
			this._type = type;
			TestGroupAttribute? testGroupAttribute = type.GetCustomAttribute<TestGroupAttribute>();
			this.Title = testGroupAttribute?._title ?? "";
			// get setup & tear down methods
			foreach(MethodInfo method in type.GetMethods())
			{

				if(method.GetCustomAttribute<SetupAttribute>() != null)
				{
					if(!Test.IsValidMethod(method))
					{
						Godot.GD.PrintErr($"setup/tear down method: {method.Name}, is not valid");
						continue;
					}
					this.Setup = method;
					continue;
				}
				if(method.GetCustomAttribute<TearDownAttribute>() != null)
				{
					if(!Test.IsValidMethod(method))
					{
						Godot.GD.PrintErr($"setup/tear down method: {method.Name}, is not valid");
						continue;
					}
					this.TearDown = method;
				}
			}
		}

		public void RunSetup()
		{
#if GODOT_TESTS_DEBUG
			Godot.GD.Print($"seting up test group: {this.Title}");
#endif
			try
			{
				Setup?.Invoke(null, null);
			}
			catch(Exception e)
			{
				Godot.GD.PrintErr(e.ToString());
			}
		}
		
		public void RunTearDown()
		{
#if GODOT_TESTS_DEBUG
			Godot.GD.Print($"tearing down test group: {this.Title}");
#endif
			try
			{
				TearDown?.Invoke(null, null);
			}
			catch(Exception e)
			{
				Godot.GD.PrintErr(e.ToString());
			}
		}

		public void RunAllTests()
		{
#if GODOT_TESTS_DEBUG
			Godot.GD.Print($"starting test group: {this.Title}");
#endif
			RunSetup();
			foreach(Test test in GetTests())
			{
				test.Run();
			}
			RunTearDown();
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
					Test test = new Test(methodInfo);
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