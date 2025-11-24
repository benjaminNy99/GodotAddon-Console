extends Control

@onready var rtl_output: RichTextLabel = $pc_background/rtl_output
@onready var le_input: LineEdit = $pc_background/le_input

var manager_node

func _ready() -> void:
	load_commands()

## load commands
func load_commands() -> void:
	var scriptManager = load("res://addons/GodotConsole/Scripts/Manager.cs")
	manager_node = scriptManager.new(rtl_output, le_input)
	add_child(manager_node)


func _on_le_input_text_submitted(new_text: String) -> void:
	le_input.clear()
	le_input.focus_mode = Control.FOCUS_CLICK
	manager_node.Command(new_text)
