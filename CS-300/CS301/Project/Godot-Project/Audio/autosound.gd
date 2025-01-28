extends FmodEventEmitter3D

# plays the sound on load
func _ready() -> void:
	print("AutoSound: playing " + event_name)
	play()

# deletes the sound when finished to prevent looping / memory leak
func _on_stopped() -> void:
	print("AutoSound: stopping " + event_name)
	get_parent().queue_free()
