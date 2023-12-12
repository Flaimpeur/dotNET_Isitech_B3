namespace consoleProject;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        Console.WriteLine("Press any key to exist.");
    }

    // Exo prof corriger
    public static int Sum(IEnumerable<object> values)
{
    var sum = 0;
    foreach (var item in values)
    {
        switch (item)
        {
            case 0:
                break;
            case int val:
                sum += val;
                break;
            case IEnumerable<object> subList when subList.Any():
                sum += Sum(subList);
                break; //Il manquait de quoi sortir
            case IEnumerable<object> subList:
                break;
            case null:
                break;
            
        }
    }
    return sum; // Et il ne retournais rien
}
}


