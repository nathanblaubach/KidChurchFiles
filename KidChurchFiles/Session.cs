namespace KidChurchFiles;

public class Session
{
    public required int UnitNumber { get; set; }
    public required int SessionNumber { get; set; }
    public required string SessionName { get; set; }

    public string GetDirectoryName()
    {
        return $"{UnitNumber.ToString().PadLeft(2, '0')}.{SessionNumber} {SessionName}";
    }
}
