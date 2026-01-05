namespace KidChurchFiles;

public record PreschoolVolume(
    int VolumeNumber,
    List<PreschoolSession> Sessions
)
{
    public string GetDirectoryName()
    {
        var lowestUnitNumber = Sessions.Min(session => session.UnitNumber);
        var highestUnitNumber = Sessions.Max(session => session.UnitNumber);
        return $"V{VolumeNumber.ToString().PadLeft(2, '0')} (units {lowestUnitNumber}-{highestUnitNumber})";
    }
}
