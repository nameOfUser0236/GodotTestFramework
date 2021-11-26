namespace GodotTest
{
	using System;
	using System.Reflection;
	using System.Collections.Generic;
	internal class TestGroup
	{
		public readonly string Title;
		public readonly Test[] Tests;
		public readonly TestMethod? Setup;
		public readonly TestMethod? TearDown;
		//private TestGroupAttribute _attribute;
		private TestGroup(Type type, string title)
		{
			this.Title = title;
			(Tests, Setup, TearDown) = GetTests(type);
		}

		public void Run()
		{
			RunSelect( x => true);
		}

		public void RunSelect(Func<Test, bool> testFilter)
		{
			#if GODOT_TESTS_DEBUG
				Godot.GD.Print($"starting test group: {this.Title}");
			#endif

			Setup?.Run();
			foreach(Test test in Tests)
			{
				if(testFilter(test))
				{
					test.Run();
				}
			}
			TearDown?.Run();
		}

		public static void RunAllGroups()
		{
			RunSelectGroups( x => true, x => true);
		}

		public static void RunSelectGroups(Func<TestGroup, bool> groupFilter, Func<Test, bool> testFilter)
		{
			foreach(TestGroup testGroup in GetTestGroups())
			{
				if(groupFilter(testGroup))
				{
					testGroup.RunSelect(testFilter);
				}
			}
		}

		private (Test[] tests, TestMethod? setup, TestMethod? tearDown) GetTests(Type type)
		{
			List<Test> tests = new();
			TestMethod? setup = null;
			TestMethod? tearDown = null;
			foreach(MethodInfo method in type.GetMethods())
			{
				bool isValid = TestMethod.IsMethodValid(method);
				if(!isValid && (
					HasAttribute<TestAttribute>(method) || 
					HasAttribute<SetupAttribute>(method)||
					HasAttribute<TearDownAttribute>(method))
				){
					Godot.GD.PrintErr($"method: {method.Name} is not valid for testing");
					continue;
				}

				if(HasAttribute<TestAttribute>(method))
				{
					Test test = Test.Construct(method);
					tests.Add(test);
					continue;
				}

				if(HasAttribute<SetupAttribute>(method))
				{
					setup = new TestMethod(method, $"seting up test group: {this.Title}");
					continue;
				}

				if(HasAttribute<TearDownAttribute>(method))
				{
					tearDown = new TestMethod(method, $"tearing down test group: {this.Title}");
					continue;
				}
			}
			return (tests.ToArray(), setup, tearDown);
		}

		public static TestGroup[] GetTestGroups(Assembly? _assembly = null)
		{
			List<TestGroup> testGroups = new();
			Assembly assembly = _assembly ?? Assembly.GetExecutingAssembly();

			foreach(Type type in assembly.GetTypes())
			{
				TestGroupAttribute? groupAttribute = type.GetCustomAttribute<TestGroupAttribute>();
				if(groupAttribute != null)
				{
					testGroups.Add(new TestGroup(type, groupAttribute._title));
				}
			}
			return testGroups.ToArray();
		}

		public static bool HasAttribute<T>(MemberInfo member) where T : Attribute
		{
			return member.GetCustomAttribute<T>() != null;
		}
	}
}