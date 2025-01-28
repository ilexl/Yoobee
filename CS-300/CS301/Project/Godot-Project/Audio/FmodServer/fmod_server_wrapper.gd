
@tool
extends Node

static var currentFunction
static var currentArgs = []

#receive function
static func rf(function):
	currentFunction = function
#receive argument
static func rx(arg):	
	currentArgs.append(arg)
#finalize call
static func fc():
	if not FmodServer.has_method(currentFunction):
		push_error("FmodServerWrapper: Function not found: " + currentFunction)
	var result = FmodServer.callv(currentFunction, currentArgs)
	currentArgs = []
	return result
