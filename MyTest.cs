using GodotTest;
using System;
using Godot;
using static GodotTest.TestHelpers;

[TestGroup("Main Test Set")]
public class MyTest
{
	[Test("Main test")]
	public static void _Test()
	{
		Print("testing...");
		IsTrue(true);
		IsEqual<int>(0, 1 - 1);
		Print("success");
	}

	[Setup]
	public static void _Setup()
	{
		Print("MyTest setup method");
	}

	[TearDown]
	public static void _TearDown()
	{
		Print("MyTest tear down method");
	}
}
