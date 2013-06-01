using RazorEngine.Configuration;
using RazorEngine.Templating;

namespace Service.Infrastructure
{
    public class RazorTemplateRenderer
    {
        private readonly TemplateService _templateService;

        public RazorTemplateRenderer()
        {
            var templateServiceConfiguration = new TemplateServiceConfiguration
                {
                    BaseTemplateType = typeof (HtmlTemplateBase<>)
                };

            _templateService = new TemplateService(templateServiceConfiguration);
        }

        public string Render(string template, object model)
        {
            var renderedHtml = _templateService.Parse(
                template, 
                model, 
                null, 
                null);
            return renderedHtml;
        }
    }
}