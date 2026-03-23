# Our base script is the original game script.
extends "res://dialoguesystem.gd"

var translation : Translation;
const MOD_LOG_NAME := "Victoire-Translator:"

func _ready() -> void:
	# That's located in 
	# C:\Users\*******\AppData\Roaming\Godot\app_userdata\HARDCODED
	translation = load("user://fr.po") as Translation
	ModLoaderLog.info("Loading translation...", MOD_LOG_NAME)
	ModLoaderLog.info(translation.to_string(), MOD_LOG_NAME)
	
	
# This overrides the method with the same name, changing the value of its argument:
func typetext(text):
	
	var translatedText: String;
	
	if (translation != null):
		translatedText = translation.get_message(text)
	
	# Do nothing is file is not loaded, or translation not done yet.
	if (translatedText == null or translatedText == ""):
		super(text)
	else:
		super(translatedText)
		
	# Calling the base method will call the original game method:

	# Note that if the vanilla script returned something, we would do this instead:
	#return super(modded_path)
