@tool
extends Node

static var currentObject
static var currentFunction
static var currentArgs = []


#receive event
static func re(event):
	currentObject = event
#receive function
static func rf(function):
	currentFunction = function
#receive argument
static func rx(arg):	
	currentArgs.append(arg)
#finalize call
static func fc():
	if not currentObject.has_method(currentFunction):
		push_error("FmodGenericWrapper: Function in object " + type_string(typeof(currentObject)) + " not found: " + currentFunction)
		return null
	var result = currentObject.callv(currentFunction, currentArgs)
	currentArgs = []
	return result

#get property
static func gp(property_name : String):
	return currentObject[property_name]
	
#set property
static func sp(property_name : String, value):
	currentObject[property_name] = value
