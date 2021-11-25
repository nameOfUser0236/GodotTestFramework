using GodotTest;
using System;
using Godot;

[TestGroup("my test set")]
public class MyTest
{
	[Test("my test")]
	public void _Test()
	{
		GD.Print("success");
	}
}
