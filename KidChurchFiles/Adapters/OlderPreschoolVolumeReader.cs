namespace KidChurchFiles.Adapters;

public class OlderPreschoolVolumeReader(string rootDirectory)
{
    private readonly VolumeSessionScanner scanner = new(rootDirectory);

    public Volume<OlderPreschoolSession> GetVolume(int volumeNumber)
    {
        var volumeStructure = scanner.GetVolumeStructure(volumeNumber);
        return new Volume<OlderPreschoolSession>
        {
            VolumeNumber = volumeStructure.VolumeNumber,
            Sessions = volumeStructure.Sessions.Select(session => new OlderPreschoolSession
            {
                UnitNumber = session.UnitNumber,
                SessionNumber = session.SessionNumber,
                SessionName = session.SessionName,
                BibleStoryPictureSourcePath = GetPreschoolBibleStoryPicturePath(volumeNumber, session.UnitNumber, session.SessionNumber),
                BibleStoryVideoSourcePath = GetPreschoolBibleStoryVideoPath(volumeNumber, session.UnitNumber, session.SessionNumber),
                BigPictureAnswerSourcePath = GetPreschoolBigPictureAnswerPath(volumeNumber, session.UnitNumber),
                BigPictureQuestionSourcePath = GetPreschoolBigPictureQuestionPath(volumeNumber, session.UnitNumber),
                KeyPassageSourcePath = GetPreschoolKeyPassagePath(volumeNumber, session.UnitNumber),
                SongSourcePath = GetPreschoolKeyPassageSongPath(volumeNumber, session.UnitNumber)
            })
        };
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
            .GetVolumeStructure(volumeNumber).Sessions
            .Select(session => session.UnitNumber)
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
