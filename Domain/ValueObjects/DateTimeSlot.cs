
namespace Domain.ValueObjects;

public class DateTimeSlot
{
    public DateTime EntryTimestamp { get; private set; }
    public DateTime? ExitTimestamp { get; private set; }
    public decimal? TotalHours
    {
        get
        {
            if (ExitTimestamp.HasValue)
            {
                return (decimal?)(ExitTimestamp.Value - EntryTimestamp).TotalHours;
            }
            return null;
        }
    }

    public DateTimeSlot(DateTime entryTimestamp)
    {
        if (entryTimestamp == default)
            throw new ArgumentException("Entry timestamp must be set.", nameof(entryTimestamp));

        EntryTimestamp = entryTimestamp;
    }

    public void EndSession(DateTime exitTimestamp)
    {
        if (exitTimestamp <= EntryTimestamp)
            throw new ArgumentException("Exit timestamp must be after entry timestamp.");

        ExitTimestamp = exitTimestamp;
    }

    public override string ToString()
    {
        return $"{EntryTimestamp:yyyy-MM-dd} - {ExitTimestamp:yyyy-MM-dd}";
    }
}
