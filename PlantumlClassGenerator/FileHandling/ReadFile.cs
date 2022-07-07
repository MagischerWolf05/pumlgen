using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using PlantumlClassGenerator.Interfaces.AccsesebilityInterface;
using PlantumlClassGenerator.Interfaces.AttributeInterface;

namespace PlantumlClassGenerator.FileHandling;

public static class ReadFile
{
    public static List<string> ClassList = new List<string>();

    public static List<IAccesebility> Listacesebility = (from t in Assembly.GetExecutingAssembly().GetTypes()
        where t.GetInterfaces().Contains(typeof(IAccesebility))
              && t.GetConstructor(Type.EmptyTypes) != null
        select Activator.CreateInstance(t) as IAccesebility).ToList();
        
    public static List<IAttribute> Listatribute = (from t in Assembly.GetExecutingAssembly().GetTypes()
        where t.GetInterfaces().Contains(typeof(IAttribute))
              && t.GetConstructor(Type.EmptyTypes) != null
        select Activator.CreateInstance(t) as IAttribute).ToList();

    public static void ReadClassFile(String pathToFile)
    {
        var FileInLines = File.ReadAllText(pathToFile);
        FindMethods(FileInLines);
       
        }
    private static void FindMethods(string input)
    {
        try
        {
            var listasstring =string.Join("|", Listacesebility.Select(X=>X.ToString()));
             string methodPattern = @"\b("+ listasstring + @")\s*" +
                                         @"\b("+string.Join("|", Listatribute.Select(X=>X.ToString()))+ @")?\s*[a-zA-Z]*(?<method>\s[a-zA-Z]+\s*)"; 
                                       // + @"\((([a-zA-Z\[\]\<\>]*\s*[a-zA-Z]*\s*)[,]?\s*)+(\)|})";
        
                new CreakeClassDiagramm(Regex.Matches(input, methodPattern, RegexOptions.IgnorePatternWhitespace));
            
        }
        catch (ArgumentException ex)
        {
            // Syntax error in the regular expression
        }
    }
}
