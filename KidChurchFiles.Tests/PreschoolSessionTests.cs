namespace KidChurchFiles.Tests;

public class PreschoolSessionTests
{
    [Theory]
    [InlineData(5, 1, "05.1 Title for session 1")]
    [InlineData(16, 1, "16.1 Title for session 1")]
    [InlineData(16, 2, "16.2 Title for session 2")]
    public void GetDirectoryName_ShouldDisplayNumbersAndTitle(int unitNumber, int sessionNumber, string expectedDirectoryName)
    {
        // Arrange
        var title = $"Title for session {sessionNumber}";
        var session = new PreschoolSession(
            unitNumber,
            sessionNumber,
            title,
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty
        );

        // Act
        var directoryName = session.GetDirectoryName();

        // Assert
        Assert.Equal(expectedDirectoryName, directoryName);
    }
}
