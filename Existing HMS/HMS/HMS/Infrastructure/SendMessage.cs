using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace HMS.Infrastructure
{
    public class SendMessage
    {
        
        public string SMS(string phoneNo, string msg)
        {
            
            string url = "http://smsproadv.in:6005/api/v2/SendSMS?ApiKey="+ System.Web.Configuration.WebConfigurationManager.AppSettings.Get("ApiKey")+ "&ClientId="+ System.Web.Configuration.WebConfigurationManager.AppSettings.Get("ClientId") + "&SenderId="+ System.Web.Configuration.WebConfigurationManager.AppSettings.Get("SenderId");

            //String strPost = "?user=" + HttpUtility.UrlPathEncode("Sharanaya_UserName") + "&password=" + HttpUtility.UrlPathEncode("Sharanaya_Password") + "&sender=" + HttpUtility.UrlPathEncode("AIRSMS") + "&mobile=" + HttpUtility.UrlPathEncode(phoneNo) + "&type=" + HttpUtility.UrlPathEncode("3") + "&message=" + message;

            string content =url+"&Message="+ HttpUtility.UrlPathEncode(msg) + "&MobileNumbers="+ HttpUtility.UrlPathEncode(phoneNo);
            using (var client = new HttpClient())
            {

                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //HttpResponseMessage response = client.PostAsync(url + strPost, new StringContent(content, Encoding.UTF8, "application/json")).Result;
                
                    
                //var response =  client.GetStringAsync(content);
                WebRequest wrGETURL;
                wrGETURL = WebRequest.Create(content);
                //WebProxy myProxy = new WebProxy("myproxy", 80);
                //myProxy.BypassProxyOnLocal = true;

                //wrGETURL.Proxy = WebProxy.GetDefaultProxy();

                //Stream objStream;
                //objStream = wrGETURL.GetResponse().GetResponseStream();
                var response = wrGETURL.GetResponse();
            }
            return "Message sent";
        }
        public string Whatsapp(string phoneNo ,string msg)
        {
            // Find your Account Sid and Token at twilio.com/console
            // and set the environment variables. See http://twil.io/secure
            string accountSid = Environment.GetEnvironmentVariable("SHARANAYA_TWILIO_ACCOUNT_SID");
            string authToken = Environment.GetEnvironmentVariable("SHARANAYA_TWILIO_AUTH_TOKEN");

            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                from: new Twilio.Types.PhoneNumber("whatsapp:Sharanaya_Phone_Number(+14155238886)"),
                body: msg,
                to: new Twilio.Types.PhoneNumber("whatsapp:"+phoneNo)
            );

            return message.Sid;
        }
    }
}