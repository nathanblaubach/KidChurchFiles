namespace KidChurchFiles.Adapters;

public class LocalFilePreschoolVolumeUnitSessionScanner(string basePath)
{
    /// <summary>
    /// Get the Volume, Unit, Session combinations for the given volume
    /// </summary>
    /// <param name="volumeNumber">Volume to map</param>
    /// <returns>Volume, Unit, Session combinations</returns>
    public IEnumerable<VolumeUnitSession> ReadVolumeUnitSessionMapping(int volumeNumber)
    {
        var bibleStoryVideosDirectory = Path.Join(
            basePath,
            $"TGP_Preschool_V{volumeNumber}_Bible_Story_Videos"
        );

        return Directory
            .GetFiles(bibleStoryVideosDirectory)
            .Where(filePath => filePath.EndsWith($"pre_bible_story.mp4"))
            .Select(filePath =>
            {
                var fileName = filePath.Split("/").Last();
                var fileNameParts = fileName.Split("_");
                return new VolumeUnitSession(
                    Volume: int.Parse(fileNameParts[1].Replace("v", "")),
                    Unit: int.Parse(fileNameParts[2].Replace("u", "")),
                    Session: int.Parse(fileNameParts[3].Replace("s", "")));
            })
            .OrderBy(session => session.Volume)
            .ThenBy(session => session.Unit)
            .ThenBy(session => session.Session);
    }
}
