using KidChurchFiles.Adapters;

namespace KidChurchFiles.Tests.Adapters;

public class LocalFilePreschoolVolumeUnitSessionScannerTests
{
    private readonly LocalFilePreschoolVolumeUnitSessionScanner _repository = new(
        $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}/Downloads");

    [Fact]
    public void ShouldFindVolumeSessions()
    {
        // Arrange
        const int volumeNumber = 6;
        IEnumerable<VolumeUnitSession> expectedSessions = [
            new(6, 16, 1),
            new(6, 16, 2),
            new(6, 16, 3),
            new(6, 16, 4),
            new(6, 17, 1),
            new(6, 17, 2),
            new(6, 17, 3),
            new(6, 17, 4),
            new(6, 18, 1),
            new(6, 18, 2),
            new(6, 18, 3),
            new(6, 18, 4),
            new(6, 18, 5)
        ];

        // Act
        var sessions = _repository.ReadVolumeUnitSessionMapping(volumeNumber);

        // Assert
        Assert.Equal(expectedSessions, sessions);
    }
}
