using System.Diagnostics;
using System.Text.RegularExpressions;



static void Main()
{
    List<string> allMessages = new List<string>();
    string hardcoded_path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "HARDCODED");

    List<string> dirs = Directory.EnumerateDirectories(hardcoded_path, "*", searchOption: SearchOption.AllDirectories).ToList();

    // Add root directory that also contains some dialogues.
    dirs.Add(hardcoded_path);

    foreach (string dirPath in dirs)
    {
        // Avoid unwanted folders
        if (dirPath.Contains("addons")) continue;
        if (dirPath.Contains(".godot")) continue;

        foreach (var filePath in Directory.EnumerateFiles(dirPath))
        {
            if (!filePath.EndsWith(".gd")) continue;

            string readedFile = File.ReadAllText(filePath);

            // All dialogues are HARDCODED (lol) in class that extends DialogueSceneClass
            if (readedFile.Contains("extends DialogueSceneClass"))
            {
                // Big and efficient regex to match all case 💪💪💪💪💪
                foreach (Match match in Regex.Matches(readedFile, @"'message':.'((?:\\'|[^'])*?)'"))
                {
                    allMessages.Add(match.Groups[1].Value);
                }
            }
        }
    }
    // Compile text to a fake python file with regex so POedit can read it and generate a .pot
    using (StreamWriter sw = new StreamWriter(Path.Combine(Environment.CurrentDirectory, "result.py")))
    {
        foreach (var message in allMessages)
        {
            if (string.IsNullOrWhiteSpace(message)) continue;
            sw.WriteLine($"gettext('{message}')");
        }
    }

}

Main();