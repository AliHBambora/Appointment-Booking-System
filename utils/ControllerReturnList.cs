namespace Appointment_booking_system.utils
{
    public class ControllerReturnList<T> : ControllerReturn<T>
    {
        public ControllerReturnList(string errorCode, string errorMessage) : base(errorCode, errorMessage)
        {
        }

        public ControllerReturnList()
        {

        }
        public List<T> data { get; set; }
    }
}

