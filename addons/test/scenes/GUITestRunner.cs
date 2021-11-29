namespace GodotTest
{
	using System;
	using Godot;
	[Tool]
	public class GUITestRunner : Node
	{
		public void RunAllTests()
		{
#if GODOT_TESTS_DEBUG
			GD.Print("running all tests");
#endif
			TestGroup.RunAllGroups();
		}

		public void RefreshTests()
		{
			#if GODOT_TESTS_DEBUG
				GD.Print("refreshing tests");
			#endif
			TestGroup.RefreshGroups();
		}
	}
}
