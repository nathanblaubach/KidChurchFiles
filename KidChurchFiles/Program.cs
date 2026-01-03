namespace KidChurchFiles;

public class Program
{
    public static void Main(string[] args)
    {
        var volumeNumber = int.Parse(args[0]);
        var useCases = new UseCases();
        useCases.CopyPreschoolFiles(volumeNumber);
    }
}
