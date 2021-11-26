namespace GodotTest
{
	using System;
	using Godot;


	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class TestGroupAttribute : Attribute
	{
		public readonly string _title;
		public TestGroupAttribute(string title){_title = title;}
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


	[AttributeUsage(AttributeTargets.Method)]
	public class SetupAttribute : Attribute{}


	[AttributeUsage(AttributeTargets.Method)]
	public class TearDownAttribute : Attribute{}

	public class TestFailedException : Exception
	{
		public TestFailedException(string message = "") : base(message)
		{}
	}

	public static class TestHelpers
	{
		static void IsTrue(bool boolean, string message = "IsTrue assert failed")
		{
			if(!boolean)
			{
				throw new TestFailedException(message);
			}
		}
		static void IsFalse(bool boolean, string message = "IsFalse assert failed")
		{
			if(!boolean)
			{
				throw new TestFailedException(message);
			}
		}
		static void IsEqual(object a, object b, string message = "IsEqual assert failed")
		{
			if(a != b)
			{
				throw new TestFailedException(message);
			}
		}
		static void IsNotEqual(object a, object b, string message = "IsNotEqual assert failed")
		{
			if(a == b)
			{
				throw new TestFailedException(message);
			}
		}
	}
}
