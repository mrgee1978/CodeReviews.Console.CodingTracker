using System.ComponentModel.Design;
using System.Globalization;

namespace CodingTracker.mrgee1978.PresentationLayer.Validation;

public static class ValidateDate
{

    /// <summary>
    /// Gets a valid date string and returns to the user
    /// does all the validation for creation of DateTime objects
    /// </summary>
    /// <param name="message"></param>
    /// <param name="errorMessage"></param>
    /// <param name="isUpdate"></param>
    /// <returns></returns>
    public static string GetValidDateString(string message, string errorMessage, string dateFormat, bool isUpdate = false, bool isError=false)
    {
        if (isError)
        {
            Console.WriteLine(errorMessage);
        }
        else
        {
            Console.WriteLine(message);
        }

        string? dateString = Console.ReadLine();

        if (dateString == "0")
        {
            return dateString;
        }
        else if (string.IsNullOrEmpty(dateString) && isUpdate) // optional parameter only used when updating a coding session in case the user wants to keep either date without having to re-enter the date therefore they can just press enter which will be a empty string so we have to check here
        {
            return dateString;
        }


        while (!DateTime.TryParseExact(dateString, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
        {
            Console.WriteLine(errorMessage);
            dateString = Console.ReadLine();
            if (dateString == "0")
            {
                return dateString;
            }
        }
        return dateString;
    }
    /// <summary>
    /// Get a valid starting date
    /// </summary>
    /// <param name="message"></param>
    /// <param name="errorMessage"></param>
    /// <returns>DateTime</returns>
    public static DateTime GetValidStartDate(string validDateString)
    {
        // The GetValidDateString() method already ensures that the date string is validated
        // so no need to check it again here
        return DateTime.Parse(validDateString);
    }

    /// <summary>
    /// Get a valid ending date
    /// </summary>
    /// <param name="message"></param>
    /// <param name="errorMessage"></param>
    /// <param name="startDate"></param>
    /// <returns>DateTime</returns>
    public static DateTime GetValidEndDate(string validDateString, string message, string errorMessage, string dateFormat, DateTime startDate)
    {
        DateTime endDate = DateTime.Parse(validDateString);

        while (startDate > endDate)
        {
            validDateString = GetValidDateString(message, errorMessage, dateFormat, false, true);
            try
            {
                // Have to use a Try/Catch block here because '0' is valid input in the
                // GetValidDateString() method, but is not a valid date time 
                endDate = DateTime.Parse(validDateString);
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        return endDate;
    }
}
