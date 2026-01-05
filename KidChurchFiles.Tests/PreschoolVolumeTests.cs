namespace KidChurchFiles.Tests;

public class PreschoolVolumeTests
{
    [Fact]
    public void GetDirectoryName_ShouldDisplayVolumeAndUnitNumbers()
    {
        // Arrange
        var volume = new PreschoolVolume(6, [
            CreateSessionWithUnitNumber(2),
            CreateSessionWithUnitNumber(2),
            CreateSessionWithUnitNumber(2),
            CreateSessionWithUnitNumber(3),
            CreateSessionWithUnitNumber(3),
            CreateSessionWithUnitNumber(4),
            CreateSessionWithUnitNumber(4),
            CreateSessionWithUnitNumber(5),
            CreateSessionWithUnitNumber(5),
            CreateSessionWithUnitNumber(5),
            CreateSessionWithUnitNumber(5),
        ]);
        var expectedDirectoryName = "V06 (units 2-5)";

        // Act
        var directoryName = volume.GetDirectoryName();

        // Assert
        Assert.Equal(expectedDirectoryName, directoryName);
    }

    private PreschoolSession CreateSessionWithUnitNumber(int unitNumber)
        => new PreschoolSession(unitNumber, 1, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
}
