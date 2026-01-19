namespace KidChurchFiles.Adapters;

public class OlderPreschoolVolumeExporter(string rootDirectory)
{
    public void Export(Volume<OlderPreschoolSession> volume)
    {
        foreach (var session in volume.Sessions)
        {
            var sessionDirectory = GetSessionDirectory(volume, session);

            Directory.CreateDirectory(sessionDirectory);

            File.Copy(session.BibleStoryPictureSourcePath, $"{sessionDirectory}/Bible Story Picture.jpg");
            File.Copy(session.BibleStoryVideoSourcePath, $"{sessionDirectory}/Bible Story Video.mp4");
            File.Copy(session.BigPictureAnswerSourcePath, $"{sessionDirectory}/Big Picture Answer.jpg");
            File.Copy(session.BigPictureQuestionSourcePath, $"{sessionDirectory}/Big Picture Question.jpg");
            File.Copy(session.KeyPassageSourcePath, $"{sessionDirectory}/Key Passage.jpg");
            File.Copy(session.SongSourcePath, $"{sessionDirectory}/Song.mp4");
        }
    }

    public void PrintPlan(Volume<OlderPreschoolSession> volume)
    {
        Console.WriteLine($"# Volume {volume.VolumeNumber}");
        Console.WriteLine();
        foreach (var session in volume.Sessions)
        {
            Console.WriteLine($"## {GetSessionDirectory(volume, session)}");
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

    private string GetSessionDirectory(Volume<OlderPreschoolSession> volume, Session session)
    {
        return Path.Join(
            rootDirectory,
            volume.GetDirectoryName(),
            session.GetDirectoryName()
        );
    }
}
