tool
extends EditorPlugin

var dock;

func _enter_tree():
	#print("test plugin loaded");
	dock = preload("res://addons/test/scenes/test_gui.tscn").instance();
	add_control_to_bottom_panel(dock, "Tests");


func _exit_tree():
	remove_control_from_bottom_panel(dock);
	dock.queue_free();
