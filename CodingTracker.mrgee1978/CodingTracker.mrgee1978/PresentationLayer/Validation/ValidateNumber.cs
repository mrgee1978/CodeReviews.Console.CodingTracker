namespace CodingTracker.mrgee1978.PresentationLayer.Validation;

public static class ValidateNumber
{
    /// <summary>
    /// Used to make sure the user inputs a valid integer
    /// </summary>
    /// <param name="message"></param>
    /// <param name="errorMessage"></param>
    /// <returns>int</returns>
    public static int GetValidInteger(string message, string errorMessage)
    {
        Console.WriteLine(message);
        string? numberInput = Console.ReadLine();
        int validNumber;

        while (!int.TryParse(numberInput, out validNumber) || Convert.ToInt32(numberInput) < 0)
        {
            Console.WriteLine(errorMessage);
            numberInput = Console.ReadLine();
        }

        return validNumber;
    }
}
