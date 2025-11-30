extends Node
class_name MouseManager
var captured : bool = false;  
var fullscreen : bool = false;
var window : Window;
func _ready() -> void:
	window = Window.get_focused_window()
func _process(delta: float) -> void:
	if(Input.mouse_mode == Input.MOUSE_MODE_CAPTURED):
		captured = true;
	if(window.mode == Window.MODE_FULLSCREEN):
		fullscreen = true;
		
	if(Input.is_action_just_pressed("f5")):
		if(captured):
			Input.mouse_mode = Input.MOUSE_MODE_VISIBLE
		else:
			Input.mouse_mode = Input.MOUSE_MODE_CAPTURED
		captured = not captured
	if(Input.is_action_just_pressed("f11")):
		if(fullscreen):
			window.mode = Window.MODE_WINDOWED;
		else:
			window.mode = Window.MODE_FULLSCREEN
		fullscreen = not fullscreen
	if(Input.is_action_just_pressed("quit")):
		get_tree().quit()
