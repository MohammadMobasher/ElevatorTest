using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ElevatorAdmin.TagHelpers
{
    [HtmlTargetElement("priceInput", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class PriceInputTagHelper : TagHelper
    {
        [HtmlAttributeName("name")]
        public string Name { get; set; }
        [HtmlAttributeName("title")]
        public string Title { get; set; }
        [HtmlAttributeName("required")]
        public bool Required { get; set; }
        [HtmlAttributeName("value")]
        public string Value { get; set; }


        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            string Input = @"<input name='" + this.Name + $@"' type='hidden' value='{Value}'>
                        <input name='" + this.Name + $@"_show' 
                               " + (this.Required ? "required='required'" : "") + @"
                               aria-describedby='" + this.Name + @"'
                               placeholder='" + this.Title + $@"' 
                               type='text' 
                               "+ (string.IsNullOrEmpty(this.Value) ? "" : "value='" + decimal.Parse(Value, CultureInfo.InvariantCulture).ToString("n0") + "'" )+ @"
                               class='form-control seperator-input'>";

            output.Content.AppendHtml(Input);
            return base.ProcessAsync(context, output);
        }
    }
}
