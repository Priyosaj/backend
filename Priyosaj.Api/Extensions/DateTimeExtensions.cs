namespace Priyosaj.Api.Extensions;

public static class DateTimeExtensions
{
    public static int CalculateAge(this DateTime dob)
    {
        DateTime today = DateTime.Now;
        int age = today.Year - dob.Year;
        if (dob.Date > today.AddYears(-age)) age--;
        return age;
    }
}