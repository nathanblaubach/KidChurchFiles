namespace KidChurchFiles.Interfaces;

public record Volume<T>(
    int VolumeNumber,
    IEnumerable<T> Sessions);
