﻿using DataLayer.ViewModels.User;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElevatorAdmin.TagHelpers
{
    [HtmlTargetElement("TableBotton", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class TableBottonTagHelper : TagHelper
    {

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
            string Button = "";
            List<UserAccessListViewModel> ListAccess = ViewContext.ViewBag.ListAccess;

            if (ListAccess.Where(x => x.Controller == this.Controller && x.Action == this.Action) != null)
            {
                Button = @"
                    <button data-role-href='/" + this.Area + "/" + this.Controller + "/" + this.Action + @"' 
                            class='btn btn-lg " + this.ButtonClass + @" data-role-table-btn'
                            data-toggle='tooltip'
                            title='" + Title + @"'
                            modal-title='" + this.ModalTitle + @"'
                            ismodal='" + (this.IsModal ? "true" : "false") + @"'
                            data-role='confirm'>
                            <i class='fa " + this.Icon + @" btn-icon' aria-hidden='true'></i>&nbsp;&nbsp;
                            " + Title + @"
                    </button>
                ";
                

            }


            output.Content.AppendHtml(Button);
            return base.ProcessAsync(context, output);
        }




        }
}
