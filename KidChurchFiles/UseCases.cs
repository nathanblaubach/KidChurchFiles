using System.Globalization;
using KidChurchFiles.Adapters;
using KidChurchFiles.Interfaces;

namespace KidChurchFiles;

public class UseCases
{
    private readonly IPreschoolVolumeImport _import = new LocalFilePreschoolVolumeImport(
        $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}/Downloads");

    private readonly IPreschoolVolumeExport _export = new LocalFilePreschoolVolumeExport(
        $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}/Downloads/{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss", CultureInfo.InvariantCulture)}");
    
    public void CopyPreschoolFiles(int volumeNumber)
    {
        var volume = _import.ImportPreschoolVolume(volumeNumber);
        _export.ExportPreschoolVolume(volume);
    }
}