# meta-name: Custom Task
# meta-description: Custom task to be used in a BehaviorTree
# meta-default: true
@tool
extends BTAction



# Display a customized name (requires @tool).
func _generate_name() -> String:
	return "Wait to detect"


# Called once during initialization.
func _setup() -> void:
	pass


# Called each time this task is entered.
func _enter() -> void:
	
	pass


# Called each time this task is exited.
func _exit() -> void:
	pass


# Called each time this task is ticked (aka executed).
func _tick(delta: float) -> Status:
	return SUCCESS


# Strings returned from this method are displayed as warnings in the behavior tree editor (requires @tool).
