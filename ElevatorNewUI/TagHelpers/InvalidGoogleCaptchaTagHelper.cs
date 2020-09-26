using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElevatorNewUI.TagHelpers
{

    [HtmlTargetElement("InvalidGoogleCaptcha", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class InvalidGoogleCaptchaTagHelper : TagHelper
    {
    }
}
