namespace KidChurchFiles;

public class Program
{
    public static void Main(string[] args)
    {
        var volumeNumber = int.Parse(args[0]);
        var downloadsFolder = $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}/Downloads";
        var preschoolVolumeReader = new LifewayPreschoolVolumeReader(downloadsFolder);
        var preschoolVolumeExporter = new RccPreschoolVolumeExporter($"{downloadsFolder}/dest-preschool");

        var preschoolVolume = preschoolVolumeReader.GetPreschoolVolume(volumeNumber);
        PrintRccPlan(preschoolVolume);
        preschoolVolumeExporter.ExportPreschoolVolume(preschoolVolume);
    }

    private static void PrintRccPlan(PreschoolVolume preschoolVolume)
    {
        Console.WriteLine($"# {preschoolVolume.GetDirectoryName()}");
        Console.WriteLine();
        foreach(var session in preschoolVolume.Sessions)
        {
            Console.WriteLine($"## {session.GetDirectoryName()}");
            Console.WriteLine();
            Console.WriteLine($"* Bible Story Picture.jpg: {session.BibleStoryPictureSourcePath}");
            Console.WriteLine($"* Bible Story Video.mp4: {session.BibleStoryVideoSourcePath}");
            Console.WriteLine($"* Big Picture Answer.jpg: {session.BigPictureAnswerSourcePath}");
            Console.WriteLine($"* Big Picture Question.jpg: {session.BigPictureQuestionSourcePath}");
            Console.WriteLine($"* Key Passage.jpg: {session.KeyPassageSourcePath}");
            Console.WriteLine($"* Song.mp4: {session.SongSourcePath}");
            Console.WriteLine();
        }
    }
}
