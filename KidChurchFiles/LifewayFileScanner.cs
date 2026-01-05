namespace KidChurchFiles;

public class LifewayFileScanner(string rootDirectory)
{
    // Zips
    public IEnumerable<string> GetTopLevelZips(int volumeNumber) =>
    [
        $"{GetPreschoolBibleStoryVideosDirectory(volumeNumber)}.zip",
        $"{GetPreschoolKeyPassageSongsDirectory(volumeNumber)}.zip",
        $"{GetPreschoolMissionsVideosDirectory(volumeNumber)}.zip",
        $"{GetOlderPreschoolDirectory(volumeNumber)}.zip",
        $"{GetPreschoolPresentationFilesDirectory(volumeNumber)}.zip",
        $"{GetPreschoolVolumeResourcesDirectory(volumeNumber)}.zip",
    ];

    public IEnumerable<string> GetPresentationFileZips(int volumeNumber)
        => Directory.GetFiles($"{GetPreschoolPresentationFilesDirectory(volumeNumber)}", "*.zip");

    // Scanner
    private IDictionary<int, List<int>> GetUnitSessionNumberMappings(int volumeNumber)
    {
        var unitSessionMapping = new Dictionary<int, List<int>>();

        var bibleStoryVideoPaths = Directory
            .GetFiles(GetPreschoolBibleStoryVideosDirectory(volumeNumber))
            .Where(filePath => filePath.EndsWith($"pre_bible_story.mp4"));

        foreach (var bibleStoryVideoPath in bibleStoryVideoPaths)
        {
            var fileName = bibleStoryVideoPath.Split("/").Last();
            var fileNameParts = fileName.Split("_");
            var unit = int.Parse(fileNameParts[2].Replace("u", ""));
            var session = int.Parse(fileNameParts[3].Replace("s", ""));
            if (unitSessionMapping.TryGetValue(unit, out List<int>? value))
            {
                value.Add(session);
            }
            else
            {
                unitSessionMapping.Add(unit, new List<int>{session});
            }
        }

        return unitSessionMapping;
    }

    public PreschoolVolume GetPreschoolVolume(int volumeNumber)
    {
        var preschoolSessions = new List<PreschoolSession>();
        foreach(var unitSessionNumberMapping in GetUnitSessionNumberMappings(volumeNumber))
        {
            var unitNumber = unitSessionNumberMapping.Key;
            var sessionNumbers = unitSessionNumberMapping.Value;
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
                    MissionsVideoSourcePath: GetPreschoolMissionsVideoPath(volumeNumber, unitNumber),
                    SongSourcePath: GetPreschoolKeyPassageSongPath(volumeNumber, unitNumber)));
            }
        }
        return new PreschoolVolume(volumeNumber, preschoolSessions);
    }

    // Files
    private string GetPreschoolBibleStoryVideoPath(int volumeNumber, int unitNumber, int sessionNumber)
        => Directory.GetFiles(GetPreschoolBibleStoryVideosDirectory(volumeNumber), $"*v{volumeNumber}_u{unitNumber}_s{sessionNumber}*.mp4")[0];
    private string GetPreschoolKeyPassageSongPath(int volumeNumber, int unitNumber)
    {
        var lowestUnit = GetUnitSessionNumberMappings(volumeNumber).Keys.OrderBy(unit => unit).First();
        var songVideoUnitNumber = unitNumber - lowestUnit + 1;
        return Directory.GetFiles(GetPreschoolKeyPassageSongsDirectory(volumeNumber), $"{songVideoUnitNumber}_*.mp4")[0];
    }
    private string GetPreschoolMissionsVideoPath(int volumeNumber, int unitNumber)
        => Directory.GetFiles(GetPreschoolMissionsVideosDirectory(volumeNumber), $"u{unitNumber}_missions_*.mp4")[0];
    private string GetPreschoolBibleStoryPicturePath(int volumeNumber, int unitNumber, int sessionNumber)
        => $"{GetPreschoolPresentationFilesSubdirectory(volumeNumber, unitNumber)}/Bible Story Picture U{unitNumber}S{sessionNumber}.jpg";
    private string GetPreschoolBigPictureAnswerPath(int volumeNumber, int unitNumber)
        => $"{GetPreschoolPresentationFilesSubdirectory(volumeNumber, unitNumber)}/Big Picture Answer U{unitNumber}.jpg";
    private string GetPreschoolBigPictureQuestionPath(int volumeNumber, int unitNumber)
        => $"{GetPreschoolPresentationFilesSubdirectory(volumeNumber, unitNumber)}/Big Picture Question U{unitNumber}.jpg";
    private string GetPreschoolKeyPassagePath(int volumeNumber, int unitNumber)
        => $"{GetPreschoolPresentationFilesSubdirectory(volumeNumber, unitNumber)}/Key Passage CSB OP U{unitNumber}.jpg";

    // Directories
    private string GetPreschoolBibleStoryVideosDirectory(int volumeNumber)
        => $"{rootDirectory}/TGP_Preschool_V{volumeNumber}_Bible_Story_Videos";
    private string GetPreschoolKeyPassageSongsDirectory(int volumeNumber)
        => $"{rootDirectory}/TGP_Preschool_V{volumeNumber}_Key_Passage_Songs";
    private string GetPreschoolMissionsVideosDirectory(int volumeNumber)
        => $"{rootDirectory}/TGP_Preschool_V{volumeNumber}_Missions_Videos";
    private string GetOlderPreschoolDirectory(int volumeNumber)
        => $"{rootDirectory}/TGP_Preschool_V{volumeNumber}_Older_Preschool";
    private string GetPreschoolPresentationFilesDirectory(int volumeNumber)
        => $"{rootDirectory}/TGP_Preschool_V{volumeNumber}_Presentation_Files";
    private string GetPreschoolPresentationFilesSubdirectory(int volumeNumber, int unitNumber)
        => $"{GetPreschoolPresentationFilesDirectory(volumeNumber)}/TGP5_v{volumeNumber}_unit{unitNumber}_ps_presentation_files";
    private string GetPreschoolVolumeResourcesDirectory(int volumeNumber)
        => $"{rootDirectory}/TGP_Preschool_V{volumeNumber}_Volume_Resources";

}
