using Project.Web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace Project.Web.ApiHelper
{
    public class ApiHelper
    {
            HttpClient Apiclient = new HttpClient();
            private string url = System.Configuration.ConfigurationManager.AppSettings["apiUrl"].ToString();
            private string key = System.Configuration.ConfigurationManager.AppSettings["apikey"].ToString();
            private string secret = System.Configuration.ConfigurationManager.AppSettings["apisecret"].ToString();
            //public List<SalesRepModel> getSalesReps()
            //{
            //    List<SalesRepModel> agents = new List<SalesRepModel>();
            //    try
            //    {
            //        HttpClient client = new HttpClient();
            //        client.BaseAddress = new Uri(url);
            //        // Add an Accept header for JSON format.  
            //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //        client.DefaultRequestHeaders.Add("apikey", key);
            //        client.DefaultRequestHeaders.Add("apisecret", secret);
            //        // List all Names.  
            //        HttpResponseMessage response = client.GetAsync("agent/salesrep").Result;  // Blocking call!  
            //        if (response.IsSuccessStatusCode)
            //        {
            //            var products = response.Content.ReadAsStringAsync().Result;
            //        }
            //        else
            //        {
            //            Debug.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);                       
            //        }  
            //    }
            //    catch (Exception ex)
            //    {
            //        BAL.Common.LogManager.LogError("getSalesReps", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
            //    }
            //    return agents;
            //}
            
            
           
    }
}