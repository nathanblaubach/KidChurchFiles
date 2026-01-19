namespace KidChurchFiles.Tests.Adapters;

using KidChurchFiles.Adapters;

public class VolumeSessionScannerTests
{
    private readonly VolumeSessionScanner scanner = new(
        $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}/Downloads");

    [Fact]
    public void GetVolumeStructure_ShouldFindVolumeFiveUnitsAndSessions()
    {
        // Arrange
        var volumeNumber = 5;
        var expectedVolumeStructure = new Volume<Session>()
        {
            VolumeNumber = volumeNumber,
            Sessions = [
                CreateExpectedSession(13, 1),
                CreateExpectedSession(13, 2),
                CreateExpectedSession(13, 3),
                CreateExpectedSession(13, 4),
                CreateExpectedSession(14, 1),
                CreateExpectedSession(14, 2),
                CreateExpectedSession(14, 3),
                CreateExpectedSession(14, 4),
                CreateExpectedSession(15, 1),
                CreateExpectedSession(15, 2),
                CreateExpectedSession(15, 3),
                CreateExpectedSession(15, 4),
                CreateExpectedSession(15, 5)
            ]
        };

        // Act
        var detectedVolumeStructure = scanner.GetVolumeStructure(volumeNumber);

        // Assert
        Assert.True(AreEqual(expectedVolumeStructure, detectedVolumeStructure));
    }

    [Fact]
    public void GetVolumeStructure_ShouldFindVolumeSixUnitsAndSessions()
    {
        // Arrange
        var volumeNumber = 6;
        var expectedVolumeStructure = new Volume<Session>()
        {
            VolumeNumber = volumeNumber,
            Sessions = [
                CreateExpectedSession(16, 1),
                CreateExpectedSession(16, 2),
                CreateExpectedSession(16, 3),
                CreateExpectedSession(16, 4),
                CreateExpectedSession(17, 1),
                CreateExpectedSession(17, 2),
                CreateExpectedSession(17, 3),
                CreateExpectedSession(17, 4),
                CreateExpectedSession(18, 1),
                CreateExpectedSession(18, 2),
                CreateExpectedSession(18, 3),
                CreateExpectedSession(18, 4),
                CreateExpectedSession(18, 5)
            ]
        };

        // Act
        var detectedVolumeStructure = scanner.GetVolumeStructure(volumeNumber);

        // Assert
        Assert.True(AreEqual(expectedVolumeStructure, detectedVolumeStructure));
    }

    private static Session CreateExpectedSession(int unitNumber, int sessionNumber) => new()
    {
        UnitNumber = unitNumber,
        SessionNumber = sessionNumber,
        SessionName = "Session Title Here"
    };

    private static bool AreEqual(Volume<Session> left, Volume<Session> right)
    {
        return left.VolumeNumber == right.VolumeNumber
            && left.Sessions.All(leftSession => right.Sessions.Any(rightSession => Equal(leftSession, rightSession)))
            && right.Sessions.All(rightSession => left.Sessions.Any(leftSession => Equal(leftSession, rightSession)));
    }

    private static bool Equal(Session left, Session right)
        => left.UnitNumber == right.UnitNumber
        && left.SessionNumber == right.SessionNumber
        && left.SessionName == right.SessionName;
}
