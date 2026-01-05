namespace KidChurchFiles;

public class Program
{
    public static void Main(string[] args)
    {
        var volumeNumber = int.Parse(args[0]);
        var useCases = new UseCases($"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}/Downloads");
        useCases.ExtractPreschoolZips(volumeNumber);
        useCases.CopyPreschoolVolume(volumeNumber);
    }
}
