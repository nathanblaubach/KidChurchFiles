namespace KidChurchFiles.Interfaces;

public interface IPreschoolVolumeImport
{
    Volume<PreschoolSession> ImportPreschoolVolume(int volumeNumber);
}
