namespace KidChurchFiles;

public class LifewayPreschoolVolumeReader(string rootDirectory)
{
    private readonly LifewayPreschoolVolumeScanner scanner = new(rootDirectory);

    public PreschoolVolume GetPreschoolVolume(int volumeNumber)
    {
        var preschoolVolumeStructure = scanner.GetPreschoolVolumeStructure(volumeNumber);
        
        var preschoolSessions = new List<PreschoolSession>();
        foreach(var preschoolUnitSessions in preschoolVolumeStructure)
        {
            var unitNumber = preschoolUnitSessions.Key;
            var sessionNumbers = preschoolUnitSessions.Value;
            foreach(var sessionNumber in sessionNumbers)
            {
                preschoolSessions.Add(new(
                    UnitNumber: unitNumber,
                    SessionNumber: sessionNumber,
                    SessionTitle: "Session Title Here",
                    BibleStoryPictureSourcePath: GetPreschoolBibleStoryPicturePath(volumeNumber, unitNumber, sessionNumber),
                    BibleStoryVideoSourcePath: GetPreschoolBibleStoryVideoPath(volumeNumber, unitNumber, sessionNumber),
                    BigPictureAnswerSourcePath: GetPreschoolBigPictureAnswerPath(volumeNumber, unitNumber),
                    BigPictureQuestionSourcePath: GetPreschoolBigPictureQuestionPath(volumeNumber, unitNumber),
                    KeyPassageSourcePath: GetPreschoolKeyPassagePath(volumeNumber, unitNumber),
                    SongSourcePath: GetPreschoolKeyPassageSongPath(volumeNumber, unitNumber)));
            }
        }
        return new PreschoolVolume(volumeNumber, preschoolSessions);
    }

    private string GetPreschoolBibleStoryVideoPath(int volumeNumber, int unitNumber, int sessionNumber)
    {
        return Directory.GetFiles(
            $"{rootDirectory}/TGP_Preschool_V{volumeNumber}_Bible_Story_Videos",
            $"*{volumeNumber}*{unitNumber}*{sessionNumber}*.mp4").SingleOrDefault()
            ?? throw new FileNotFoundException($"Could not find Bible Story Video for Volume: {volumeNumber}, Unit: {unitNumber}, Session: {sessionNumber}");
    }

    private string GetPreschoolKeyPassageSongPath(int volumeNumber, int unitNumber)
    {
        var keyPassageSongPathFiles = Directory
            .GetFiles(
                $"{rootDirectory}/TGP_Preschool_V{volumeNumber}_Key_Passage_Songs",
                $"*.mp4")
            .OrderBy(filePath => filePath)
            .ToArray();

        var lowestUnitInCurrentVolume = scanner
            .GetPreschoolVolumeStructure(volumeNumber).Keys
            .OrderBy(unit => unit)
            .First();
        var unitIndex = unitNumber - lowestUnitInCurrentVolume;

        if (unitIndex < keyPassageSongPathFiles.Length)
        {
            return keyPassageSongPathFiles[unitIndex];
        }

        throw new FileNotFoundException($"Could not find Key Passage Song for Volume: {volumeNumber}, Unit: {unitNumber}");
    }

    private string GetPreschoolBibleStoryPicturePath(int volumeNumber, int unitNumber, int sessionNumber)
    {
        return FindPresentationFile(volumeNumber, $"Bible Story Picture U{unitNumber}S{sessionNumber}.jpg")
            ?? throw new FileNotFoundException($"Could not find Bible Story Picture for Volume: {volumeNumber}, Unit: {unitNumber}, Session: {sessionNumber}");
    }
    private string GetPreschoolBigPictureAnswerPath(int volumeNumber, int unitNumber)
    {
        return FindPresentationFile(volumeNumber, $"Big Picture Answer U{unitNumber}.jpg")
            ?? throw new FileNotFoundException($"Could not find Big Picture Answer for Volume: {volumeNumber}, Unit: {unitNumber}");
    }
    private string GetPreschoolBigPictureQuestionPath(int volumeNumber, int unitNumber)
    {
        return FindPresentationFile(volumeNumber, $"Big Picture Question U{unitNumber}.jpg")
            ?? throw new FileNotFoundException($"Could not find Big Picture Question for Volume: {volumeNumber}, Unit: {unitNumber}");
    }
    private string GetPreschoolKeyPassagePath(int volumeNumber, int unitNumber)
    {
        return FindPresentationFile(volumeNumber, $"Key Passage CSB *U{unitNumber}.jpg")
            ?? throw new FileNotFoundException($"Could not find Key Passage for Volume: {volumeNumber}, Unit: {unitNumber}");
    }

    private string? FindPresentationFile(int volumeNumber, string filename)
    {
        return Directory.GetFiles(
            $"{rootDirectory}/TGP_Preschool_V{volumeNumber}_Presentation_Files",
            filename,
            SearchOption.AllDirectories).SingleOrDefault();
    }
}
