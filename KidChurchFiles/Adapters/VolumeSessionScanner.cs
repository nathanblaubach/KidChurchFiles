using System.IO.Compression;
using System.Text.RegularExpressions;

namespace KidChurchFiles.Adapters;

public class VolumeSessionScanner(string rootDirectory)
{
    public Volume<Session> GetVolumeStructure(int volumeNumber)
    {
        var topLevelDirectories = Directory.GetFiles(
            rootDirectory,
            $"TGP_Preschool_V{volumeNumber}_*.zip"
        );

        ExtractZipsInPlace(topLevelDirectories);

        var presentationFileDirectory = Directory.GetFiles(
            $"{rootDirectory}/TGP_Preschool_V{volumeNumber}_Presentation_Files",
            "*.zip"
        );

        ExtractZipsInPlace(presentationFileDirectory);

        var bibleStoryVideoFileNames = Directory
            .GetFiles($"{rootDirectory}/TGP_Preschool_V{volumeNumber}_Bible_Story_Videos")
            .Where(filePath => filePath.EndsWith(".mp4"))
            .Select(filePath => filePath.Split("/").Last());

        return new Volume<Session>
        {
            VolumeNumber = volumeNumber,
            Sessions = bibleStoryVideoFileNames
                .Select(fileName => new Session
                {
                    UnitNumber = GetNumberByIdentifier(fileName, "u") ?? -1,
                    SessionNumber = GetNumberByIdentifier(fileName, "s") ?? -1,
                    SessionName = "Session Title Here"
                })
                .Where(session => session.UnitNumber != -1 && session.SessionNumber != -1)
                .OrderBy(session => session.UnitNumber)
                .ThenBy(session => session.SessionNumber)
        };
    }

    private static void ExtractZipsInPlace(IEnumerable<string> zipFilePaths)
    {
        foreach (var zipFilePath in zipFilePaths)
        {
            var extractDirectoryPath = zipFilePath.Replace(".zip", string.Empty);
            if (!Directory.Exists(extractDirectoryPath))
            {
                ZipFile.ExtractToDirectory(zipFilePath, extractDirectoryPath);
            }
        }
    }

    private static int? GetNumberByIdentifier(string fileName, string identifier)
    {
        var numberString = Regex
            .Matches(fileName.ToLower(), identifier + "(\\d{1,3})")
            .SingleOrDefault()?
            .Value
            .Replace(identifier, string.Empty);

        return int.TryParse(numberString, out int parseNumber)
            ? parseNumber
            : null;
    }
}
