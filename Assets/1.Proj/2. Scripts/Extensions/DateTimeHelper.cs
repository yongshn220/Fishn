using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public static class DateTimeHelper
{
    const string DATETIME_FORMAT = "yyyy-MM-dd HH:mm:ss";

    public static string GetDateTimeNowStr()
    {
        DateTime now = DateTime.Now;
        return now.ToString(DATETIME_FORMAT);
    }
}

public static class DateTimeExtension
{
    const string DATETIME_FORMAT = "yyyy-MM-dd HH:mm:ss";

    public static string ToFormatString(this DateTime dateTime)
    {
        return dateTime.ToString(DATETIME_FORMAT);
    }

    public static int GetDayPassedFromNow(this DateTime dateTime)
    {
        TimeSpan delta = DateTime.Now - dateTime;
        return (int) delta.TotalDays;
    }

    public static int GetHourPassedFromNow(this DateTime dateTime)
    {
        TimeSpan delta = DateTime.Now - dateTime;
        return (int) delta.TotalHours;
    }
}
