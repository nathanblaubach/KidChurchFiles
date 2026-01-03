using KidChurchFiles.Adapters;

namespace KidChurchFiles.Tests.Adapters;

public class LocalFilePreschoolVolumeImportTests
{
    private readonly LocalFilePreschoolVolumeImport _reader = new(
        $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}/Downloads");
    
    [Fact]
    public void GetPreschoolSessions_ShouldReturnValidFilePaths()
    {
        // Arrange
        const int volumeNumber = 6;

        // Act
        var volume = _reader.ImportPreschoolVolume(volumeNumber);

        // Assert
        foreach (var session in volume.Sessions)
        {
            Assert.True(File.Exists(session.BibleStoryPictureFilePath));
            Assert.True(File.Exists(session.BibleStoryVideoFilePath));
            Assert.True(File.Exists(session.BigPictureAnswerFilePath));
            Assert.True(File.Exists(session.BigPictureQuestionFilePath));
            Assert.True(File.Exists(session.KeyPassageFilePath));
            Assert.True(File.Exists(session.SongFilePath));
        }
    }
}
