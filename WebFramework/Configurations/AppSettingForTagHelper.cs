using Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebFramework.Configurations
{
    public class AppSettingForTagHelper
    {
        public GoogleReCaptcha GoogleReCaptcha { get; }

        public AppSettingForTagHelper(GoogleReCaptcha googleReCaptcha)
        {
            GoogleReCaptcha = googleReCaptcha;
        }
    }
}
