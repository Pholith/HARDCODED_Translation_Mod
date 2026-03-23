extends Node

const MOD_DIR := "Victoire-Translator"
const MOD_LOG_NAME := "Victoire-Translator:"

var mod_dir_path := ""
var extensions_dir_path := ""
var translations_dir_path := ""


func _init() -> void:
	mod_dir_path = ModLoaderMod.get_unpacked_dir().path_join(MOD_DIR)
	# Add extensions
	
	install_script_extensions()

func install_script_extensions() -> void:
	extensions_dir_path = mod_dir_path.path_join("extensions")
	ModLoaderLog.info("Installing dialogue extension...", MOD_LOG_NAME)
	ModLoaderMod.install_script_extension(extensions_dir_path.path_join("dialoguesystem.gd"))


func _ready() -> void:
	ModLoaderLog.info("Ready!", MOD_LOG_NAME)
		
	
# Useless code. I found a better way to get strings by parsing scripts.
func export_all_text() -> void: 
	
	var scriptsDict: Array[Dictionary] = ProjectSettings.get_global_class_list();
	for dict in scriptsDict:
		#ResourceLoader.get()
		
		var loadedResource: Resource = ResourceLoader.load(dict.path)
		if (loadedResource as GDScript):
			print(loadedResource)
			if loadedResource.get_base_script() == DialogueSceneClass:
				print("ok !")
			
