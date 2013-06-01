using NUnit.Framework;
using Service.DTO;
using Service.Exceptions;

namespace ServiceTests.Extensions
{
    public static class FluentAssertExtensions
    {
        public static ServiceResult<T> Error<T>(this ServiceResult<T> result, ServiceError error)
        {
            Assert.IsFalse(result.Ok, "Expected OK to be false");
            Assert.AreEqual(error, result.Error, "Expected error to be {0}", error);
            return result;
        }

        public static T Ok<T>(this ServiceResult<T> result)
        {
            Assert.IsTrue(result.Ok, "Expected OK to be true. Error: {0}", result.Error);
            return result.Payload;
        }

        public static ServiceResult<T> FieldsInError<T>(this ServiceResult<T> result, params string[] fieldNames)
        {
            foreach (string fieldName in fieldNames)
            {
                Assert.IsTrue(result.FieldsInError.ContainsKey(fieldName));
            }
            
            return result;
        }

        public static string AssertNotEmptyString(this string s, string message, params object[] args)
        {
            Assert.IsFalse(string.IsNullOrEmpty(s), message, args);
            return s;
        }

        public static int AssertAdequateId(this int id, string message, params object[] args)
        {
            Assert.AreNotEqual(0, id, message, args);
            return id;
        }
    }
}