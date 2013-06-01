using System;
using System.Linq;
using System.Web;
using System.Web.Http.Description;
using Newtonsoft.Json;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;

namespace WebApp.Infrastructure
{
    public class ApiModelGenerator
    {
        public static ApiModel BuildApiModel(IApiExplorer apiExplorer)
        {
            return new ApiModel
            {
                Methods = apiExplorer.ApiDescriptions.Select(apiDescription => new MethodModel
                {
                    Name = apiDescription.ActionDescriptor.ActionName,
                    HttpVerb = apiDescription.HttpMethod.ToString(),
                    RelativeUrl = apiDescription.RelativePath,
                    Description = apiDescription.Documentation,
                    ReturnSample = MakeSampleReturn(apiDescription.ActionDescriptor.ReturnType),
                    Parameters = apiDescription.ParameterDescriptions.Select(parameterDescription => new ParameterModel
                    {
                        ParameterName = parameterDescription.Name,
                        Description = parameterDescription.Documentation,
                        Source = parameterDescription.Source.ToString(),
                        Sample = MakeParameterSampleString(parameterDescription.ParameterDescriptor.ParameterType, parameterDescription.Source)
                    }).ToList()
                }).ToList()
            };
        }

        private static string MakeSampleReturn(Type type)
        {
            if (type == null)
            {
                return string.Empty;
            }

            return MakeSampleJson(type);
        }

        private static string MakeParameterSampleString(Type type, ApiParameterSource source)
        {
            if (type == null)
            {
                return string.Empty;
            }

            if (source == ApiParameterSource.FromBody)
            {
                return MakeSampleJson(type);
            }

            if (source == ApiParameterSource.FromUri)
            {
                return MakeSampleUrlEncoded(type);
            }

            throw new Exception();
        }

        private static string MakeSampleUrlEncoded(Type type)
        {
            var sample = MakeSampleObject(type);
            return HttpUtility.UrlEncode(sample.ToString());
        }

        private static string MakeSampleJson(Type type)
        {
            var sample = MakeSampleObject(type);
            return JsonConvert.SerializeObject(sample, Formatting.Indented);
        }

        private static object MakeSampleObject(Type type)
        {
            var fixture = new Fixture();
            var sample = new SpecimenContext(fixture).Resolve(type);
            return sample;
        }
    }
}