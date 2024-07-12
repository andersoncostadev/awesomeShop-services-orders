using System.Collections.Generic;

namespace AwesomeShop.Services.Orders.Application.Dtos
{
    public class GenericHandlerResult
    {
        public GenericHandlerResult(object data, bool success, string message, List<ValidationsObject> validations)
        {
            Data = data;
            Success = success;
            Message = message;
            Validations = validations;
        }

        public object Data { private get; set; }
        public bool Success { private get; set; }
        public string Message { private get; set; }
        public List<ValidationsObject> Validations { private get; set; }
    }

    public class ValidationsObject
    {
        public ValidationsObject(string propertyName, string message)
        {
            PropertyName = propertyName;
            Message = message;
        }

        public string PropertyName { get; set; }
        public string Message { get; set; }
    }
}
