using HotelBookingManager.domain.constatnt;
using HotelBookingManager.domain.dto;
using HotelBookingManager.domain.exception;
using System.Text.RegularExpressions;

namespace HotelBookingManager.util.validation
{
    class ValidationUtils
    {
        public static bool ValidateClient(Client client)
        {
            Match clientNameMatchResult = Regex.Match(client.Name, RegExp.clientName);
            Match clientAddressMatchResult = Regex.Match(client.Address, RegExp.clientAddress);


            if (!clientNameMatchResult.Success)
            {
                throw new ValidationException(ExceptionCode.WRONG_CLIENT_NAME, "Некорректное имя клиента.");
            }
            if (!clientAddressMatchResult.Success)
            {
                throw new ValidationException(ExceptionCode.WRONG_CLIENT_ADDRESS, "Некорректный адрес клиента.");
            }

            return true;
        }
    }
}
