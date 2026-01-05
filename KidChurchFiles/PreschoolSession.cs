namespace KidChurchFiles;

public record PreschoolSession(
    int UnitNumber,
    int SessionNumber,
    string SessionTitle,
    string BibleStoryPictureSourcePath,
    string BibleStoryVideoSourcePath,
    string BigPictureAnswerSourcePath,
    string BigPictureQuestionSourcePath,
    string KeyPassageSourcePath,
    string? MissionsVideoSourcePath,
    string SongSourcePath
)
{
    public string GetDirectoryName()
    {
        return $"{UnitNumber.ToString().PadLeft(2, '0')}.{SessionNumber} {SessionTitle}";
    }
}
