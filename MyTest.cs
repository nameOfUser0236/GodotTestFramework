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
		GD.Print("testing...");
		IsTrue(true);
		IsEqual<int>(0, 1 - 1);
		GD.Print("success");
	}

	[Setup]
	public static void _Setup()
	{
		GD.Print("MyTest setup method");
	}

	[TearDown]
	public static void _TearDown()
	{
		GD.Print("MyTest tear down method");
	}
}
