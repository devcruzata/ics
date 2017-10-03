using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;


namespace BAL.Sms
{
   public class SmsManager
    {
       public async Task<string> SendSms(string toNo, string messages)
       {
          
           string baseUrl = ConfigurationManager.AppSettings["baseUrl"].ToString();
           string Username = ConfigurationManager.AppSettings["Username"].ToString();
           string Key = ConfigurationManager.AppSettings["Key"].ToString();

           var baseAddress = new Uri(baseUrl);
           string credstring = "username=" + Username + "&key=" + Key + "&to=" + toNo + "&message=" + messages;
           string responseData = "";
           
           using (var httpClient = new HttpClient { BaseAddress = baseAddress })
           {


               using (var content = new StringContent(credstring, System.Text.Encoding.Default, "application/x-www-form-urlencoded"))
               {
                   
                   using (var response = await httpClient.PostAsync("send.php", content))
                   {
                        responseData = await response.Content.ReadAsStringAsync();
                       
                   }
               }
               Debug.WriteLine(responseData.ToString());
           }
           return responseData;
          // return responseData;
       }


       public string PopulateBody(string body, string leadName)
       {
           string template = string.Empty;
        
           template = body;

           template = template.Replace("{name}", leadName);
           return template;
       }
       

       public List<SmsResponse> XMLResponseParser(string XmlResponse)
       {
          
           List<SmsResponse> Response = new List<SmsResponse>();
           try
           {
               XDocument xdoc = XDocument.Parse(XmlResponse);             

               List<XElement> pageElements = xdoc.Root.Element("messages").Elements("message").ToList();
               foreach (XElement pageElement in pageElements)
               {
                   SmsResponse response = new SmsResponse();
                   if (pageElement.Element("result").Value == "0000")
                   {
                       response.to = pageElement.Element("to").Value;
                       response.messageid = pageElement.Element("messageid").Value;
                       response.result = pageElement.Element("result").Value;
                       response.errortext = pageElement.Element("errortext").Value;
                       response.price = pageElement.Element("price").Value;
                       response.currency_symbol = pageElement.Element("currency_symbol").Value;
                       response.currency_type = pageElement.Element("currency_type").Value;
                       Response.Add(response);                       
                   }
                   else if (pageElement.Element("result").Value == "2018")
                   {
                       response.to = pageElement.Element("to").Value;
                       response.result = pageElement.Element("result").Value;
                       response.errortext = pageElement.Element("errortext").Value;
                       Response.Add(response);                       
                   }
                   else
                   {
                       response.result = "Whoops, Something Went Wrong";
                       response.errortext = "3001";
                       Response.Add(response);                       
                   }
                   string result = pageElement.Element("result").Value;
               }
           }
           catch (Exception ex)
           {
               return Response;
           }
           return Response;
       }

       //public void Send(string toNo, string messages)
       //{
       //    ClickSendClient client = new ClickSendClient(ConfigurationManager.AppSettings["Username"].ToString(), ConfigurationManager.AppSettings["Key"].ToString());

       //    // POST https://rest.clicksend.com/v3/sms/send

       //    // Create the SMS object and specify the SMS details
       //    SmsMessage sms = new SmsMessage();

       //    sms.Source = "c#"; //Your method of sending

       //    sms.To = "+918982951915"; // Recipient phone number in E.164 format.
       //    //sms.ListId = 428;   Your list ID if sending to a whole list. Can be used instead of 'to'.

       //    sms.Body = DateTime.Now.ToString() + ":\r\n Your message comes here";

       //    sms.From = "+61408518670"; //Your sender id - more info: https://help.clicksend.com/SMS/what-is-a-sender-id-or-sender-number.
       //    //sms.Schedule = 1477476000; //Leave blank for immediate delivery. Your schedule time in unix format 
       //    sms.CustomString = "Custom kn0ChLhwn6"; //Your reference. Will be passed back with all replies and delivery reports.

       //    SmsMessageCollection SMSs = new SmsMessageCollection();

       //    SMSs.Messages = new List<SmsMessage>();
       //    SMSs.Messages.Add(sms);

       //    string SMSresult = client.SMS.SendSms(SMSs);

       //    Console.WriteLine("SMS Result " + SMSresult);
       //}
    }

    
}
