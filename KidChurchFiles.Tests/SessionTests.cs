namespace KidChurchFiles.Tests;

public class SessionTests
{
    [Theory]
    [InlineData(5, 1, "05.1 Name for session 1")]
    [InlineData(16, 1, "16.1 Name for session 1")]
    [InlineData(16, 2, "16.2 Name for session 2")]
    public void GetDirectoryName_ShouldDisplayNumbersAndTitle(int unitNumber, int sessionNumber, string expectedDirectoryName)
    {
        // Arrange
        var session = new Session
        {
            UnitNumber = unitNumber,
            SessionNumber = sessionNumber,
            SessionName = $"Name for session {sessionNumber}"
        };

        // Act
        var directoryName = session.GetDirectoryName();

        // Assert
        Assert.Equal(expectedDirectoryName, directoryName);
    }
}
