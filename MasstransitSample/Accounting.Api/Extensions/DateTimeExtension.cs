using System;
using System.Runtime.CompilerServices;

namespace Accounting.Api.Extensions;

public static class DateTimeExtensions
{
    public static DateTime SetKindUtc(this DateTime dt)
    {
        return dt.Kind == DateTimeKind.Utc ? dt : DateTime.SpecifyKind(dt, DateTimeKind.Utc);
    }
}