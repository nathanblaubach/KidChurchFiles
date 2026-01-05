namespace KidChurchFiles.Tests;

public class LifewayFileScannerTests
{
    private readonly LifewayFileScanner scanner = new(
        $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}/Downloads");
    
    [Fact]
    public void GetTopLevelZips_ShouldReturnExistingZipPaths()
    {
        // Arrange
        const int volumeNumber = 6;

        // Act
        var zips = scanner.GetTopLevelZips(volumeNumber);

        // Assert
        Assert.True(zips.Any());
        foreach (var zip in zips)
        {
            Assert.EndsWith(".zip", zip);
            Assert.True(File.Exists(zip));
        }
    }

    [Fact]
    public void GetPresentationFileZips_ShouldReturnExistingZipPaths()
    {
        // Arrange
        const int volumeNumber = 6;

        // Act
        var zips = scanner.GetPresentationFileZips(volumeNumber);

        // Assert
        Assert.True(zips.Any());
        foreach (var zip in zips)
        {
            Assert.EndsWith(".zip", zip);
            Assert.True(File.Exists(zip));
        }
    }

    [Fact]
    public void GetPreschoolVolume_ShouldReturnValidFilePaths()
    {
        // Arrange
        const int volumeNumber = 6;

        // Act
        var volume = scanner.GetPreschoolVolume(volumeNumber);

        // Assert
        foreach (var session in volume.Sessions)
        {
            Assert.True(File.Exists(session.BibleStoryPictureSourcePath));
            Assert.True(File.Exists(session.BibleStoryVideoSourcePath));
            Assert.True(File.Exists(session.BigPictureAnswerSourcePath));
            Assert.True(File.Exists(session.BigPictureQuestionSourcePath));
            Assert.True(File.Exists(session.KeyPassageSourcePath));
            Assert.True(File.Exists(session.MissionsVideoSourcePath));
            Assert.True(File.Exists(session.SongSourcePath));
        }
    }
}
