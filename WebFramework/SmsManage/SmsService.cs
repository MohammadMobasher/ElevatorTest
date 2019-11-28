using Core;
using Core.Utilities;
using DataLayer.SSOT;
using Microsoft.Extensions.Configuration;
using PayamakPanel.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebFramework.SmsManage
{
    public class SmsService
    {
        private readonly FaraApi _faraApi;
        private readonly SmsSettings _siteSetting;

        public SmsService(FaraApi faraApi, IConfiguration configuration)
        {
            _faraApi = faraApi;
            _siteSetting = configuration.GetSection(nameof(SmsSettings)).Get<SmsSettings>();
        }

        /// <summary>
        /// متد ارسال پیامک
        /// </summary>
        /// <param name="to">دریافت کننده</param>
        /// <param name="message">متن پیامک</param>
        /// <returns></returns>
        public SweetAlertExtenstion SendSms(string to, string message)
        {
            var model = _faraApi.SendSms
                (_siteSetting.UserName, _siteSetting.Password
                , _siteSetting.Number, to, message);

            return SweetAlertExtenstion.Ok();
        }

        /// <summary>
        /// اهتبار باقی مانده
        /// </summary>
        /// <returns></returns>
        public string Credit()
        {
            var model = _faraApi.GetMyCredit(_siteSetting.UserName, _siteSetting.Password);

            return model.Value;
        }

        /// <summary>
        /// مشاهده تمامی پیام های ارسالی/دریافتی
        /// </summary>
        /// <param name="type">دریافت/ارسال</param>
        /// <returns></returns>
        public MessageList AllSms(SmsTypeSSOT type = SmsTypeSSOT.Send)
        {
            return _faraApi.GetMyMessageList(_siteSetting.UserName, _siteSetting.Password, (int)type, 0, 100);
        }


        /// <summary>
        /// مبلغ باقی مانده
        /// </summary>
        /// <returns></returns>
        public string PriceRemain()
        {
            var model = _faraApi.GetBasePrice(_siteSetting.UserName, _siteSetting.Password);

            return model.Value;
        }
    }
}
