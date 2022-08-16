using System.Text;
using Microsoft.VisualBasic;

namespace Application.Extensions;

public static class TimespanExtensions
{
    public static string ToHumanReadableString (this TimeSpan t)
    {
        var sb = new StringBuilder();
        if (t.TotalSeconds <= 1) {
            return $@"{t:s\.ff} seconds";
        }
        if (t.TotalMinutes <= 1) {
            return $@"{t:%s} seconds";
        }
        if (t.TotalHours <= 1) {
            return $@"{t:%m} minutes";
        }
        if (t.TotalDays <= 1) {
            return $@"{t:%h} hours";
        }

        return $@"{t:%d} days";
    }
}