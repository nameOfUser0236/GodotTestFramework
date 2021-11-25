using GodotTest;
using System;
using Godot;

[TestGroup("my test set")]
public class MyTest
{
	[Test("my test")]
	public static void _Test()
	{
		GD.Print("success");
	}

	[Setup]
	public static void _Setup()
	{
		GD.Print("seting up MyTest");
	}

	[TearDown]
	public static void _TearDown()
	{
		GD.Print("tearing down MyTest");
	}
}
