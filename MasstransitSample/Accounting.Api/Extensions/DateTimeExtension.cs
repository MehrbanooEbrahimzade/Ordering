using System;
using System.Runtime.CompilerServices;

namespace Accounting.Api.Extensions;

public static class DateTimeExtensions
{
    public static DateTime? SetKindUtc(this DateTime? dateTime)
    {
        return dateTime?.SetKindUtc();
    }

    public static DateTime SetKindUtc(this DateTime dt)
    {
        if (dt.Kind == DateTimeKind.Utc)
            return dt;

        DateTime.SpecifyKind(dt, DateTimeKind.Utc);
        return DateTime.SpecifyKind(dt, DateTimeKind.Utc);
    }
}