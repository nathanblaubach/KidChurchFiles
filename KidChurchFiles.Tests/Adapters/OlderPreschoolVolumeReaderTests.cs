namespace KidChurchFiles.Tests.Adapters;

using KidChurchFiles.Adapters;

public class OlderPreschoolVolumeReaderTests
{
    private readonly OlderPreschoolVolumeReader reader = new(
        $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}/Downloads");

    [Theory]
    [InlineData(5)]
    [InlineData(6)]
    public void GetVolume_ShouldFindBibleStoryPicture(int volumeNumber)
    {
        // Act
        var volume = reader.GetVolume(volumeNumber);

        // Assert
        foreach (var session in volume.Sessions)
        {
            Assert.True(File.Exists(session.BibleStoryPictureSourcePath));
        }
    }

    [Theory]
    [InlineData(5)]
    [InlineData(6)]
    public void GetVolume_ShouldFindBibleStoryVideo(int volumeNumber)
    {
        // Act
        var volume = reader.GetVolume(volumeNumber);

        // Assert
        foreach (var session in volume.Sessions)
        {
            Assert.True(File.Exists(session.BibleStoryVideoSourcePath));
        }
    }

    [Theory]
    [InlineData(5)]
    [InlineData(6)]
    public void GetVolume_ShouldFindBigPictureAnswer(int volumeNumber)
    {
        // Act
        var volume = reader.GetVolume(volumeNumber);

        // Assert
        foreach (var session in volume.Sessions)
        {
            Assert.True(File.Exists(session.BigPictureAnswerSourcePath));
        }
    }

    [Theory]
    [InlineData(5)]
    [InlineData(6)]
    public void GetVolume_ShouldFindBigPictureQuestion(int volumeNumber)
    {
        // Act
        var volume = reader.GetVolume(volumeNumber);

        // Assert
        foreach (var session in volume.Sessions)
        {
            Assert.True(File.Exists(session.BigPictureQuestionSourcePath));
        }
    }

    [Theory]
    [InlineData(5)]
    [InlineData(6)]
    public void GetVolume_ShouldFindKeyPassage(int volumeNumber)
    {
        // Act
        var volume = reader.GetVolume(volumeNumber);

        // Assert
        foreach (var session in volume.Sessions)
        {
            Assert.True(File.Exists(session.KeyPassageSourcePath));
        }
    }

    [Theory]
    [InlineData(5)]
    [InlineData(6)]
    public void GetVolume_ShouldFindSong(int volumeNumber)
    {
        // Act
        var volume = reader.GetVolume(volumeNumber);

        // Assert
        foreach (var session in volume.Sessions)
        {
            Assert.True(File.Exists(session.SongSourcePath));
        }
    }
}
