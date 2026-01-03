using KidChurchFiles.Interfaces;

namespace KidChurchFiles.Adapters;

public class LocalFilePreschoolVolumeExport(string rootDirectory) : IPreschoolVolumeExport
{
    public void ExportPreschoolVolume(Volume<PreschoolSession> volume)
    {
        var unitNumbers = volume.Sessions
            .Select(session => session.UnitNumber)
            .Distinct()
            .OrderBy(unit => unit)
            .ToList();

        foreach (var session in volume.Sessions)
        {
            // Create the session file path
            var sessionDirectory = Path.Join(
                rootDirectory,
                $"V{NumberPad(volume.VolumeNumber)} (units {unitNumbers.First()}-{unitNumbers.Last()})",
                $"{NumberPad(session.UnitNumber)}.{session.SessionNumber} {session.Title}"
            );
            Directory.CreateDirectory(sessionDirectory);
            
            // Copy the files into the directory
            File.Copy(session.BibleStoryPictureFilePath,  Path.Join(sessionDirectory, "Bible Story Picture.jpg"));
            File.Copy(session.BibleStoryVideoFilePath,    Path.Join(sessionDirectory, "Bible Story Video.mp4"));
            File.Copy(session.BigPictureAnswerFilePath,   Path.Join(sessionDirectory, "Big Picture Answer.jpg"));
            File.Copy(session.BigPictureQuestionFilePath, Path.Join(sessionDirectory, "Big Picture Question.jpg"));
            File.Copy(session.KeyPassageFilePath,         Path.Join(sessionDirectory, "Key Passage.jpg"));
            File.Copy(session.SongFilePath,               Path.Join(sessionDirectory, "Song.mp4"));
        }
    }

    private string NumberPad(int value)
    {
        return value.ToString().PadLeft(2, '0');
    }
}
