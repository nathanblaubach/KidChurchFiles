using KidChurchFiles.Interfaces;

namespace KidChurchFiles.Adapters;

public class LocalFilePreschoolVolumeImport(string rootDirectory) : IPreschoolVolumeImport
{
    private readonly LocalFilePreschoolVolumeUnitSessionScanner _scanner = new(rootDirectory);

    public Volume<PreschoolSession> ImportPreschoolVolume(int volumeNumber)
    {
        var preschoolSessions = _scanner
            .ReadVolumeUnitSessionMapping(volumeNumber)
            .Select(session =>
            {
                var presentationFilesDirectory = Path.Join(
                    rootDirectory,
                    $"TGP_Preschool_V{session.Volume}_Presentation_Files",
                    $"TGP5_v{session.Volume}_unit{session.Unit}_ps_presentation_files");

                return new PreschoolSession(
                    UnitNumber: session.Unit,
                    SessionNumber: session.Session,
                    Title: "Replace this with the lesson title",
                    BibleStoryPictureFilePath: Path.Join(
                        presentationFilesDirectory,
                        $"Bible Story Picture U{session.Unit}S{session.Session}.jpg"),
                    BibleStoryVideoFilePath: Path.Join(
                        rootDirectory,
                        $"TGP_Preschool_V{session.Volume}_Bible_Story_Videos",
                        $"tgp5_v{session.Volume}_u{session.Unit}_s{session.Session}_pre_bible_story.mp4"),
                    BigPictureAnswerFilePath: Path.Join(
                        presentationFilesDirectory,
                        $"Big Picture Answer U{session.Unit}.jpg"),
                    BigPictureQuestionFilePath: Path.Join(
                        presentationFilesDirectory,
                        $"Big Picture Question U{session.Unit}.jpg"),
                    KeyPassageFilePath: Path.Join(
                        presentationFilesDirectory,
                        $"Key Passage CSB OP U{session.Unit}.jpg"),
                    SongFilePath: GetSongFilePath(session));
            });

        return new Volume<PreschoolSession>(volumeNumber, preschoolSessions);
    }

    /// <summary>
    /// Find the key passage song file path for the unit the session is in
    /// </summary>
    /// <remarks>
    /// Songs are numbered by unit, but use 1-x within the volume rather than the actual unit numbers
    /// The song file names include a title
    /// </remarks>
    /// <param name="session"></param>
    /// <returns></returns>
    private string GetSongFilePath(VolumeUnitSession session)
    {
        var lowestVolumeUnitNumber = _scanner
            // Get units in the volume
            .ReadVolumeUnitSessionMapping(session.Volume).Select(volumeUnitSession => volumeUnitSession.Unit).Distinct()
            // Arrange from lowest to highest and pick the lowest one
            .OrderBy(unit => unit).First();

        var songUnitNumber = session.Unit - lowestVolumeUnitNumber + 1;

        return Directory
            // Get files in the song directory
            .GetFiles(Path.Join(rootDirectory, $"TGP_Preschool_V{session.Volume}_Key_Passage_Songs")) 
            // Filter down to mp4 files
            .Where(filePath => filePath.EndsWith(".mp4"))
            // Pick the file that starts with the song unit number
            .Single(filePath =>
            {
                var fileName = filePath.Split("/").Last();
                return fileName.StartsWith(songUnitNumber.ToString());
            });
    }
}
