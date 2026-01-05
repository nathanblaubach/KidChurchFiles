using System.Globalization;
using System.IO.Compression;

namespace KidChurchFiles;

public class UseCases(string rootDirectory)
{
    private readonly LifewayFileScanner lifewayFileScanner = new(
        rootDirectory);

    private readonly PreschoolVolumeExporter preschoolVolumeExporter = new(
        $"{rootDirectory}/{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss", CultureInfo.InvariantCulture)}");

    public void ExtractPreschoolZips(int volumeNumber)
    {
        ExtractZips(lifewayFileScanner.GetTopLevelZips(volumeNumber));
        ExtractZips(lifewayFileScanner.GetPresentationFileZips(volumeNumber));

        static void ExtractZips(IEnumerable<string> zipFilePaths)
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

    public void CopyPreschoolVolume(int volumeNumber)
    {
        var preschoolVolume = lifewayFileScanner.GetPreschoolVolume(volumeNumber);
        preschoolVolumeExporter.ExportPreschoolVolume(preschoolVolume);
    }
}
