namespace KidChurchFiles;

public class RccPreschoolVolumeExporter(string rootDirectory)
{
    public void ExportPreschoolVolume(PreschoolVolume volume)
    {
        foreach (var session in volume.Sessions)
        {
            var sessionDirectory = Path.Join(
                rootDirectory,
                volume.GetDirectoryName(),
                session.GetDirectoryName()
            );
            
            Directory.CreateDirectory(sessionDirectory);

            File.Copy(session.BibleStoryPictureSourcePath, $"{sessionDirectory}/Bible Story Picture.jpg");
            File.Copy(session.BibleStoryVideoSourcePath, $"{sessionDirectory}/Bible Story Video.mp4");
            File.Copy(session.BigPictureAnswerSourcePath, $"{sessionDirectory}/Big Picture Answer.jpg");
            File.Copy(session.BigPictureQuestionSourcePath, $"{sessionDirectory}/Big Picture Question.jpg");
            File.Copy(session.KeyPassageSourcePath, $"{sessionDirectory}/Key Passage.jpg");
            File.Copy(session.SongSourcePath, $"{sessionDirectory}/Song.mp4");
        }
    }
}
