@tool
extends Node

# i have no idea what this script does or if we need it

func _ready():
	var temp = _add_inspector_buttons()
	for t in temp:
		print(type_string(typeof(t)))		
		print(type_string(typeof(t.get("name"))))
		print(type_string(typeof(t.get("icon"))))
		print(type_string(typeof(t.get("pressed"))))
	

func _add_inspector_buttons() -> Array:
	var buttons = []
	buttons.push_back({
		"name": "Test button",
		"icon": preload("res://Testing/InspectorButtons/icon.svg"),
		"pressed": _on_test_button_pressed
	})
	buttons.push_back({
		"name": "Another button",
		"pressed": _on_another_button_pressed
	})
	buttons.push_back({
		"name": "Other button with lambda",
		"pressed": func(): print('Lambda callback')
	})
	return buttons


func _on_test_button_pressed() -> void:
	print('Test pressed')


func _on_another_button_pressed() -> void:
	print('Another button pressed')
