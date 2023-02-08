using Appointment_booking_system.ExceptionHandling;
using GEAweb_API.Common.GEAException;
using GEAWebAPI.Controllers.Service;
using System.Net;

namespace Appointment_booking_system.utils
{
    public class ControllerReturn<T>
    {
        private static string FAILED = "failed";
        private static string SUCCESS = "success";
        public string status { get; set; }

        public string errorCode { get; set; }

        public string errorMessage { get; set; }

        public static ControllerReturnSingle<T> ReturnSingle(T data)
        {
            return new ControllerReturnSingle<T>
            {
                data = data,
                status = SUCCESS
            };
        }

        public static ControllerReturnList<T> ReturnList(List<T> data)
        {
            return new ControllerReturnList<T>
            {
                data = data,
                status = SUCCESS
            };
        }

        public ControllerReturn(string errorCode, string errorMessage)
        {
            this.errorCode = errorCode;
            this.errorMessage = errorMessage;
            this.status = FAILED;
        }

        public ControllerReturn()
        {

        }

        public static ControllerReturn<T> ReturnError(ReturnStatus.Status status,
        ErrorCode code, Exception orgEx, string customMsg)
        {
            String erroCode = code != 0 ? code.ToString() : "";
            //var msg = customMsg + ", " + orgEx?.Message;
            customMsg = orgEx?.Message;

            ControllerReturn<T> returnedValue = new ControllerReturn<T>(erroCode, customMsg);
            returnedValue.status = FAILED;
            return returnedValue;
        }

        public static ControllerReturn<T> ReturnErrorMessage(ReturnStatus.Status status,
       ErrorCode code, string customMsg)
        {
            String erroCode = code != 0 ? code.ToString() : "";
            ControllerReturn<T> returnedValue = new ControllerReturn<T>(erroCode, customMsg);
            returnedValue.status = FAILED;
            return returnedValue;
        }

    }
}
