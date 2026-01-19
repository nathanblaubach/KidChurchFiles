namespace KidChurchFiles;

public class OlderPreschoolSession : Session
{
    public required string BibleStoryPictureSourcePath { get; set; }
    public required string BibleStoryVideoSourcePath { get; set; }
    public required string BigPictureAnswerSourcePath { get; set; }
    public required string BigPictureQuestionSourcePath { get; set; }
    public required string KeyPassageSourcePath { get; set; }
    public string? MissionsVideoSourcePath { get; set; }
    public required string SongSourcePath { get; set; }
}
