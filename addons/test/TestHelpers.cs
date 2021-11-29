namespace GodotTest
{
	using System;
	using Godot;
	public static class TestHelpers
	{
		public static void Print(string what)
		{
			GD.Print($"	{what}");
		}
		public static void IsTrue(bool boolean, string message = "IsTrue assert failed")
		{
			if(!boolean)
			{
				throw new TestFailedException(message);
			}
		}
		public static void IsFalse(bool boolean, string message = "IsFalse assert failed")
		{
			if(!boolean)
			{
				throw new TestFailedException(message);
			}
		}
		public static void IsEqual(object a, object b, string message = "IsEqual assert failed")
		{
			if(a != b)
			{
				throw new TestFailedException(message);
			}
		}
		public static void IsEqual<T>(T a, T b, string message = "IsEqual<T> assert failed") where T : notnull, IEquatable<T>
		{
			if(!a.Equals(b))
			{
				throw new TestFailedException(message);
			}
		}
		public static void IsNotEqual(object a, object b, string message = "IsNotEqual assert failed")
		{
			if(a == b)
			{
				throw new TestFailedException(message);
			}
		}
	}
}