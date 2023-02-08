namespace Appointment_booking_system.ExceptionHandling
{
    public enum ErrorCode
    {
        //template 
        //AB_{MF}_{000} , MF main function
        
        AB_US_001,  //User email not found in the system
        AB_US_002,  //Incorrect user password
        AB_US_003,  //Unable to login.
        AB_US_004,  //Unable to register new user,
        AB_US_005,  //User email already exists in the system,
        AB_US_006,  //Failed to log out. 

        AB_BK_001,  //Booking failed
        AB_BK_002,  //Failed to retrieve the booking information
        AB_BK_003,  //Failed to cancle the booking as it was made less than 24 hrs ago
        AB_BK_004,  //Failed to cancle the booking due to Exception
        AB_BK_005,  //Failed to retrieve the bookings for the particular date
        AB_BK_006,  //Cannot book for a date previous than todays date
        AB_BK_007,  //Booking already exists for the user
    }
}
