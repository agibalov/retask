using System;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace WebApp
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString AddClassIfPropertyInError<TModel, TProperty>(
            this HtmlHelper<TModel> htmlHelper, 
            Expression<Func<TModel, TProperty>> expression, 
            string errorClassName)
        {
            var expressionText = ExpressionHelper.GetExpressionText(expression);
            var fullHtmlFieldName = htmlHelper.ViewContext.ViewData
                                              .TemplateInfo.GetFullHtmlFieldName(expressionText);
            var state = htmlHelper.ViewData.ModelState[fullHtmlFieldName];
            if (state == null)
            {
                return MvcHtmlString.Empty;
            }

            if (state.Errors.Count == 0)
            {
                return MvcHtmlString.Empty;
            }

            return new MvcHtmlString(errorClassName);
        }
    }
}