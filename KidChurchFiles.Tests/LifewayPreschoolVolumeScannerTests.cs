namespace KidChurchFiles.Tests;

public class LifewayPreschoolVolumeScannerTests
{
    private readonly LifewayPreschoolVolumeScanner scanner = new(
        $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}/Downloads");

    [Fact]
    public void GetPreschoolVolumeStructure_ShouldFindVolumeFiveUnitsAndSessions()
    {
        var volumeNumber = 5;
        var expectedUnitsAndSessions = new Dictionary<int, List<int>>
        {
            { 13, [1, 2, 3, 4] },
            { 14, [1, 2, 3, 4] },
            { 15, [1, 2, 3, 4, 5] }
        };

        var detectedUnitsAndSessions = scanner.GetPreschoolVolumeStructure(volumeNumber);

        Assert.Equal(expectedUnitsAndSessions, detectedUnitsAndSessions);
    }

    [Fact]
    public void GetPreschoolVolumeStructure_ShouldFindVolumeSixUnitsAndSessions()
    {
        var volumeNumber = 6;
        var expectedUnitsAndSessions = new Dictionary<int, List<int>>
        {
            { 16, [1, 2, 3, 4] },
            { 17, [1, 2, 3, 4] },
            { 18, [1, 2, 3, 4, 5] }
        };

        var detectedUnitsAndSessions = scanner.GetPreschoolVolumeStructure(volumeNumber);

        Assert.Equal(expectedUnitsAndSessions, detectedUnitsAndSessions);
    }
}
