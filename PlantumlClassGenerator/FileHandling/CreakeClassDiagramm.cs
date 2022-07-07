using System.Reflection;
using System.Text.RegularExpressions;

namespace PlantumlClassGenerator.FileHandling;

public class CreakeClassDiagramm
{
    public CreakeClassDiagramm(MatchCollection matchCollection)
    {
        CreateClassDiagrammForPlantuml(matchCollection);
    }
    public static string plantumlstring ="";
    public static void CreateClassDiagrammForPlantuml(MatchCollection matchCollection)
    {
        plantumlstring = $"@startuml\n class {matchCollection[0].Groups["method"].Value}{{ ";
        plantumlstring=plantumlstring.Replace("\r\n{", "{\r\n");
        for (int index = 1; index < matchCollection.Count - 1; index++)
        {
            plantumlstring += matchCollection[index].Value + "\n";
        }

        plantumlstring += "}\n@enduml";
        using (StreamWriter writer = new StreamWriter(@"D:\Dokumente\01_bbw\m326\PlantumlClassGenerator\PlantumlClassGenerator\FileHandling\test.puml"))
        {
            writer.WriteLine(plantumlstring);
        }
    }
}

