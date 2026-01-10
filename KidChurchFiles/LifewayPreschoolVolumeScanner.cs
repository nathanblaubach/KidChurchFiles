using System.IO.Compression;
using System.Text.RegularExpressions;

namespace KidChurchFiles;

public class LifewayPreschoolVolumeScanner(string rootDirectory)
{
    public Dictionary<int, List<int>> GetPreschoolVolumeStructure(int volumeNumber)
    {
        EnsureZipFilesAreExtractedToDirectories(volumeNumber);
        return ScanVolumeForUnitAndSessionNumbers(volumeNumber);
    }

    private void EnsureZipFilesAreExtractedToDirectories(int volumeNumber)
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

        static void ExtractZipsInPlace(IEnumerable<string> zipFilePaths)
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
    }

    private Dictionary<int, List<int>> ScanVolumeForUnitAndSessionNumbers(int volumeNumber)
    {
        var bibleStoryVideoFileNames = Directory
            .GetFiles($"{rootDirectory}/TGP_Preschool_V{volumeNumber}_Bible_Story_Videos")
            .Where(filePath => filePath.EndsWith(".mp4"))
            .Select(filePath => filePath.Split("/").Last());

        var unitSessionMapping = new Dictionary<int, List<int>>();
        foreach (var fileName in bibleStoryVideoFileNames)
        {
            var unitNumber = GetNumberByIdentifier(fileName, "u");
            var sessionNumber = GetNumberByIdentifier(fileName, "s");
            if (unitNumber.HasValue && sessionNumber.HasValue)
            {
                if (unitSessionMapping.TryGetValue(unitNumber.Value, out List<int>? value))
                {
                    value.Add(sessionNumber.Value);
                }
                else
                {
                    unitSessionMapping.Add(unitNumber.Value, [sessionNumber.Value]);
                }
            }
        }

        return unitSessionMapping
            .OrderBy(unitSessionMapping => unitSessionMapping.Key)
            .ToDictionary(
                unitSessionMapping => unitSessionMapping.Key,
                unitSessionMapping => unitSessionMapping.Value
                    .OrderBy(sessionNumber => sessionNumber)
                    .ToList()
            );

        static int? GetNumberByIdentifier(string fileName, string identifier)
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
}
