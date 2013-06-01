using System;
using System.ComponentModel;
using System.Web.Http.Controllers;
using System.Web.Http.Description;

namespace WebApp.Infrastructure
{
    public class AttributesDocumentationProvider : IDocumentationProvider
    {
        public string GetDocumentation(HttpActionDescriptor actionDescriptor)
        {
            var descriptionAttributes = actionDescriptor.GetCustomAttributes<DescriptionAttribute>();
            var descriptionAttributesCount = descriptionAttributes.Count;
            if (descriptionAttributesCount == 0)
            {
                return string.Empty;
            }

            if (descriptionAttributesCount > 1)
            {
                throw new Exception();
            }

            var singleDescriptionAttribute = descriptionAttributes[0];
            return singleDescriptionAttribute.Description;
        }

        public string GetDocumentation(HttpParameterDescriptor parameterDescriptor)
        {
            var descriptionAttributes = parameterDescriptor.GetCustomAttributes<DescriptionAttribute>();
            var descriptionAttributesCount = descriptionAttributes.Count;
            if (descriptionAttributesCount == 0)
            {
                return string.Empty;
            }

            if (descriptionAttributesCount > 1)
            {
                throw new Exception();
            }

            var singleDescriptionAttribute = descriptionAttributes[0];
            return singleDescriptionAttribute.Description;
        }
    }
}