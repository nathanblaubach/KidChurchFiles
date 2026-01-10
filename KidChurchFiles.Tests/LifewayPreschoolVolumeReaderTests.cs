using System;

namespace KidChurchFiles.Tests;

public class LifewayPreschoolVolumeReaderTests
{
    private readonly LifewayPreschoolVolumeReader reader = new(
        $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}/Downloads");

    [Theory]
    [InlineData(5)]
    [InlineData(6)]
    public void GetPreschoolVolume_ShouldFindBibleStoryPicture(int volumeNumber)
    {
        // Act
        var volume = reader.GetPreschoolVolume(volumeNumber);

        // Assert
        foreach (var session in volume.Sessions)
        {
            Assert.True(File.Exists(session.BibleStoryPictureSourcePath));
        }
    }

    [Theory]
    [InlineData(5)]
    [InlineData(6)]
    public void GetPreschoolVolume_ShouldFindBibleStoryVideo(int volumeNumber)
    {
        // Act
        var volume = reader.GetPreschoolVolume(volumeNumber);

        // Assert
        foreach (var session in volume.Sessions)
        {
            Assert.True(File.Exists(session.BibleStoryVideoSourcePath));
        }
    }

    [Theory]
    [InlineData(5)]
    [InlineData(6)]
    public void GetPreschoolVolume_ShouldFindBigPictureAnswer(int volumeNumber)
    {
        // Act
        var volume = reader.GetPreschoolVolume(volumeNumber);

        // Assert
        foreach (var session in volume.Sessions)
        {
            Assert.True(File.Exists(session.BigPictureAnswerSourcePath));
        }
    }

    [Theory]
    [InlineData(5)]
    [InlineData(6)]
    public void GetPreschoolVolume_ShouldFindBigPictureQuestion(int volumeNumber)
    {
        // Act
        var volume = reader.GetPreschoolVolume(volumeNumber);

        // Assert
        foreach (var session in volume.Sessions)
        {
            Assert.True(File.Exists(session.BigPictureQuestionSourcePath));
        }
    }

    [Theory]
    [InlineData(5)]
    [InlineData(6)]
    public void GetPreschoolVolume_ShouldFindKeyPassage(int volumeNumber)
    {
        // Act
        var volume = reader.GetPreschoolVolume(volumeNumber);

        // Assert
        foreach (var session in volume.Sessions)
        {
            Assert.True(File.Exists(session.KeyPassageSourcePath));
        }
    }

    [Theory]
    [InlineData(5)]
    [InlineData(6)]
    public void GetPreschoolVolume_ShouldFindSong(int volumeNumber)
    {
        // Act
        var volume = reader.GetPreschoolVolume(volumeNumber);

        // Assert
        foreach (var session in volume.Sessions)
        {
            Assert.True(File.Exists(session.SongSourcePath));
        }
    }
}
