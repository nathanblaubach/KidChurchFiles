namespace KidChurchFiles;

public class Volume<T> where T : Session
{
    public required int VolumeNumber { get; set; }
    public required IEnumerable<T> Sessions;

    public string GetDirectoryName()
    {
        var lowestUnitNumber = Sessions.Min(session => session.UnitNumber);
        var highestUnitNumber = Sessions.Max(session => session.UnitNumber);
        return $"V{VolumeNumber.ToString().PadLeft(2, '0')} (units {lowestUnitNumber}-{highestUnitNumber})";
    }
}
