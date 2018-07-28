using System.Collections.Generic;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EntityFrameworkCoreDemo.Helpers
{
    public static class HtmlHelpers
    {
        public static IHtmlContent CustomHiddenId(this IHtmlHelper htmlHelper, string name)
        {
            var inputBuilder = new TagBuilder("input");
            inputBuilder.Attributes.Add("type", "hidden");
            inputBuilder.Attributes.Add("name", name);
            return inputBuilder;
        }

        public static IHtmlContent CustomDropDownListForLanguage(this IHtmlHelper htmlHelper,  string name, IEnumerable<SelectListItem> options)
        {
            var selectBuilder = new TagBuilder("select");
            selectBuilder.Attributes.Add("name", name);

            var optionsBuilder = new TagBuilder("option");
            foreach (var option in options)
            {
                optionsBuilder.MergeAttribute("value", option.Value);
                optionsBuilder.InnerHtml.Append(option.Text);
            }
            selectBuilder.InnerHtml.Append(optionsBuilder.ToString());
            
            return selectBuilder;
        }
    }
}
