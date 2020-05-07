using Core.Utilities;
using DataLayer.ViewModels.User;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElevatorAdmin.TagHelpers
{
    [HtmlTargetElement("SelectSearch", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class SelectSearchTagHelper : TagHelper
    {
  

        /// <summary>
        /// 
        /// </summary>
        [HtmlAttributeName("selectClass")]
        public string SelectClass { get; set; }


        /// <summary>
        /// آیا آیتمی به صورتی که لطفا انتخاب منید داشته باشد یا خیر
        /// </summary>
        [HtmlAttributeName("hasNoneItem")]
        public bool HasNoneItem { get; set; } = false;

        /// <summary>
        /// آیا آیتم های این کوجودیت به صورت 
        /// enum
        /// است یا خیز که به صورت پیش فرض خیر است
        /// </summary>
        [HtmlAttributeName("isEnum")]
        public bool IsEnum { get; set; } = false;

        
        /// <summary>
        /// نام این موجودیت
        /// </summary>
        [HtmlAttributeName("name")]
        public string Name { get; set; }


        /// <summary>
        /// شماره آیتمی که باید به صورت پیش فرض انتخاب شده باشد
        /// </summary>
        [HtmlAttributeName("selectedItemId")]
        public int? SelectedItemId { get; set; }


        /// <summary>
        /// 
        /// </summary>
        [HtmlAttributeName("items")]
        public List<object> Items { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [HtmlAttributeName("itemsEnum")]
        public Dictionary<int, string> ItemsEnum { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [HtmlAttributeName("showText")]
        public string ShowText { get; set; }



        /// <summary>
        /// 
        /// </summary>
        [HtmlAttributeName("showValue")]
        public string ShowValue { get; set; }




        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }


        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {

            

            string Select = $"<select name='{this.Name}' class='form-control searchSelect {this.SelectClass}'>";

            Select += (this.HasNoneItem ? "<option value=''>انتخاب نمایید...</option>" : "");

            if (!IsEnum)
            {
                foreach (var item in Items)
                {
                    if (item.GetPropertyValue(this.ShowValue).ToString() == this.SelectedItemId.ToString())
                    {
                        Select += $"<option selected value='{item.GetPropertyValue(this.ShowValue)}'>{item.GetPropertyValue(this.ShowText)}</option>";
                    }
                    else
                    {
                        Select += $"<option value='{item.GetPropertyValue(this.ShowValue)}'>{item.GetPropertyValue(this.ShowText)}</option>";
                    }
                }
            }
            else
            {
                foreach (var item in ItemsEnum)
                {
                    if (this.SelectedItemId == item.Key)
                    {
                        Select += $"<option selected value='{item.Key}'>{item.Value}</option>";
                    }
                    else
                    {
                        Select += $"<option value='{item.Key}'>{item.Value}</option>";
                    }
                }
            }

            Select += "</select>";

            output.Content.AppendHtml(Select);
            return base.ProcessAsync(context, output);
        }




    }
}
