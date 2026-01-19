namespace KidChurchFiles.Tests;

public class VolumeTests
{
    [Fact]
    public void GetDirectoryName_ShouldDisplayVolumeAndUnitNumbers()
    {
        // Arrange
        var unitNumbers = new List<int> { 2, 2, 2, 3, 3, 4, 4, 5, 5, 5, 5 };
        var volume = new Volume<Session>
        {
            VolumeNumber = 6,
            Sessions = unitNumbers.Select(unitNumber => new Session
            {
                UnitNumber = unitNumber,
                SessionNumber = 1,
                SessionName = string.Empty
            })
        };
        var expectedDirectoryName = "V06 (units 2-5)";

        // Act
        var directoryName = volume.GetDirectoryName();

        // Assert
        Assert.Equal(expectedDirectoryName, directoryName);
    }
}
