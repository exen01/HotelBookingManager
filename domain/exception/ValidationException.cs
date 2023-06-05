using HotelBookingManager.domain.constatnt;
using System;

namespace HotelBookingManager.domain.exception
{
    class ValidationException : Exception
    {
        private readonly ExceptionCode code;
        private readonly string message;

        public ValidationException(ExceptionCode code, string message)
        {
            this.code = code;
            this.message = message;
        }

        public ExceptionCode Code => code;
        public string UserMessage => message;
    }
}
