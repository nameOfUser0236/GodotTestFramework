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

	[Test("Pram Test", new[]{5, 6})]
	public static void _PramTest(int[] a)
	{
		GD.Print(a[0]);
		GD.Print(a[1]);
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
