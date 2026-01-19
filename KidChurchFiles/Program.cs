namespace KidChurchFiles;

using KidChurchFiles.Adapters;

public class Program
{
    public static void Main(string[] args)
    {
        var volumeNumber = int.Parse(args[0]);
        var userDownloadsFolder = $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}/Downloads";
        var olderPreschoolVolumeReader = new OlderPreschoolVolumeReader(userDownloadsFolder);
        var olderPreschoolVolumeExporter = new OlderPreschoolVolumeExporter($"{userDownloadsFolder}/dest-preschool");
        var olderPreschoolVolume = olderPreschoolVolumeReader.GetVolume(volumeNumber);
        olderPreschoolVolumeExporter.PrintPlan(olderPreschoolVolume);
        olderPreschoolVolumeExporter.Export(olderPreschoolVolume);
    }
}
