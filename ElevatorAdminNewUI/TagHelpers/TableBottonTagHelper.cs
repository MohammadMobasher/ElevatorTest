using DataLayer.ViewModels.User;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElevatorAdminNewUI.TagHelpers
{
    [HtmlTargetElement("TableBotton", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class TableBottonTagHelper : TagHelper
    {
        /// <summary>
        /// 
        /// </summary>
        [HtmlAttributeName("querystring")]
        public string QueryString { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [HtmlAttributeName("buttonClass")]
        public string ButtonClass { get; set; }

        /// <summary>
        /// ایا این دکمه برای مودال است یا خبر
        /// </summary>
        [HtmlAttributeName("isModal")]
        public bool IsModal { get; set; } = false;

        /// <summary>
        /// نام مربوط به Area
        /// </summary>
        [HtmlAttributeName("area")]
        public string Area { get; set; }

        /// <summary>
        /// نام مربوط به Controller
        /// </summary>
        [HtmlAttributeName("controller")]
        public string Controller { get; set; }

        /// <summary>
        /// نام مربوط به Action
        /// </summary>
        [HtmlAttributeName("action")]
        public string Action { get; set; }


        /// <summary>
        /// درصورتی که این دکمه برای باز کردن مودال باشد، عنوان مودال در این فیلد قرار میگیرد
        /// </summary>
        [HtmlAttributeName("modalTitle")]
        public string ModalTitle { get; set; }

        /// <summary>
        /// متن داخل دکمه
        /// </summary>
        [HtmlAttributeName("title")]
        public string Title { get; set; }


        /// <summary>
        ///  کلاس مربوط به آیکون داخل دکمه
        /// </summary>
        [HtmlAttributeName("icon")]
        public string Icon { get; set; }


        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }


        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            //===================================================================================
            string Button = "";
            List<UserAccessListViewModel> ListAccess = ViewContext.ViewBag.ListAccess;
            //===================================================================================

            if ((ListAccess != null && ListAccess.Count > 0 
                && ListAccess.Where(x => x.Controller == this.Controller + "Controller" 
                && x.Action == this.Action).ToList().Count() > 0) 
                || ListAccess.Any(a=>a.IsAdmin == true))
            {
                Button = @"
                    <button data-style='zoom-out'
                            " + (!string.IsNullOrEmpty(this.QueryString) ? "data-role-querystring='" + this.QueryString + "'" : "") + @"  
                            data-role-href='" + (!string.IsNullOrEmpty(this.Area) ? "/" + this.Area : "") + "/" + this.Controller + "/" + this.Action + @"' 
                            class='ladda-button btn " + this.ButtonClass + @" data-role-table-btn'
                            data-toggle='tooltip'
                            data-placement='bottom'
                            title ='" + Title + @"'
                            " + (!string.IsNullOrEmpty(this.ModalTitle) ? "modal-title='" + this.ModalTitle + "'" : "") + @"
                            " + (this.IsModal ? "ismodal" : "") + @"
                            data-role='confirm'>

                            <span class='ladda-label'>

                                  <i class='fa " + this.Icon + @" btn-icon' aria-hidden='true'></i>&nbsp;&nbsp;
                                  " + Title + @"
                            
                            </span>
                    </button>
                ";
            }

            output.Content.AppendHtml(Button);
            return base.ProcessAsync(context, output);
        }




    }
}
