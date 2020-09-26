using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebFramework.Configurations;

namespace ElevatorNewUI.TagHelpers
{
    [HtmlTargetElement("GoogleCaptcha", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class GoogleCaptchaTagHelper : TagHelper
    {

        private AppSettingForTagHelper _googleCaptchaConfig;

        public GoogleCaptchaTagHelper(AppSettingForTagHelper appSettingForTagHelper)
        {
            _googleCaptchaConfig = appSettingForTagHelper;
        }

        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            //=================================================
            string publicSiteKey = _googleCaptchaConfig.GoogleReCaptcha.Key;
            string googleCaptcha = $"<div class='g-recaptcha' data-sitekey='{publicSiteKey}'></div>";
            string googleCaptchaScript = "<script src='https://www.google.com/recaptcha/api.js'></script>";
            //=================================================

            output.Content.AppendHtml(googleCaptchaScript + googleCaptcha);
            return base.ProcessAsync(context, output);
        }

    }
}
