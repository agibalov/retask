using System.Collections.Generic;
using Service.Exceptions;

namespace Service.DTO
{
    public class ServiceResult<T>
    {
        public bool Ok { get; set; }
        public T Payload { get; set; }
        public ServiceError Error { get; set; }
        public IDictionary<string, List<string>> FieldsInError;
    }
}