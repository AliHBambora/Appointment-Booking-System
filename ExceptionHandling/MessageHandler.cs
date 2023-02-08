namespace Appointment_booking_system.ExceptionHandling
{
    public static class MessageHandler
    {
        private static Dictionary<ErrorCode, string> errors = new Dictionary<ErrorCode, string>();

        static MessageHandler()
        {
            errors.Add(ErrorCode.AB_US_001, "Email address not registered in the system. Please register.");
            errors.Add(ErrorCode.AB_US_002, "Incorrect password. Please enter the correct credentials.");
            errors.Add(ErrorCode.AB_US_003, "Failed to login please contact system administrator");
            errors.Add(ErrorCode.AB_US_004, "Failed to register new user.Please contact system administrator");
            errors.Add(ErrorCode.AB_US_005, "Email already exists in the system.");
            errors.Add(ErrorCode.AB_US_006, "Failed to log out. Please contact system administrator");

            errors.Add(ErrorCode.AB_BK_001, "Booking failed.Please contact system administrator");
            errors.Add(ErrorCode.AB_BK_002, "Failed to retrieve the booking information. Please contact system administrator");
            errors.Add(ErrorCode.AB_BK_003, "Booking cannot be cancelled as it has less than 24 hrs left");
            errors.Add(ErrorCode.AB_BK_004, "Failed to cancle the booking. Please contact system administrator");
            errors.Add(ErrorCode.AB_BK_005, "Failed to retrieve the bookings for the given date. Please contact system administrator");
            errors.Add(ErrorCode.AB_BK_006, "Cannot book for a date before than todays date. Please select a date greater or equal to todays date");
            errors.Add(ErrorCode.AB_BK_007, "Booking already exists for the user.");

        }

        public static string getMessage(ErrorCode code)
        {
            string result = "";
            errors.TryGetValue(code, out result);
            return result;
        }
    }
}
