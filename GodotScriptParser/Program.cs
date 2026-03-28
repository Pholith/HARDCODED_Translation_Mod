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
            sw.WriteLine(writeAsGetText(message));
        }
        // Idk why but this is not formatted like others dialogues messages. So I just add them here.
        sw.WriteLine(writeAsGetText("The Acorn diner, A dirty little burger joint which serves as a hangout for locals and a hub for passer-throughs."));
        sw.WriteLine(writeAsGetText("Stairs to some old apartment units."));
        sw.WriteLine(writeAsGetText("It looks like a vending machine."));
        sw.WriteLine(writeAsGetText("The classic back ally, a prime spot for shady dealings or just keeping out of sight."));
        sw.WriteLine(writeAsGetText("The local Robotech shop, for all your droid maintenance needs."));
        sw.WriteLine(writeAsGetText("She\'s staring at you intently, unabashedly even, from the booth where she\'s been sipping her coffee."));
        sw.WriteLine(writeAsGetText("The missionary is huge, easily twice your height, but exudes an aura of peacefulness."));
        sw.WriteLine(writeAsGetText("The greasy waiter does not offer you a smile when he sees you approach."));
        sw.WriteLine(writeAsGetText("The sight of another android makes you feel weird, especially since this one is just a mindless automaton."));
        sw.WriteLine(writeAsGetText("She looks excited to see you come in."));
    }
}

static string writeAsGetText(string text)
{
    return $"gettext('{text}')";
}


Main();