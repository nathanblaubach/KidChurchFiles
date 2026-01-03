namespace KidChurchFiles.Interfaces;

public record PreschoolSession(
    int UnitNumber,
    int SessionNumber,
    string Title,
    string BibleStoryPictureFilePath,
    string BibleStoryVideoFilePath,
    string BigPictureAnswerFilePath,
    string BigPictureQuestionFilePath,
    string KeyPassageFilePath,
    string SongFilePath);
