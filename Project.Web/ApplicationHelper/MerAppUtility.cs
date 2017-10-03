using Project.Entity;
using Project.Web.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Xml;

namespace Project.Web.ApplicationHelper
{
   public class MerAppUtility
    {
       public static DataSet GetTemplatedDataSet()
       {
           //string[] templates = {
           //                  "freeterminal",
           //                  "wireless",
           //                  "reprogram",
           //                  "gateway",
           //                  "quickbooksfinancial",
           //                  "quickbookspos",
           //                  "freeipterminal",
           //                  "freefd200terminal",
           //                  "roamdata",
           //                  "vx510gprsterminal",
           //                  "smartswipe"
           //                  };

          // string templateName = templates[templateId - 1];

           string templatePath = "~\\Template\\i_pay_template.xml";

           // Load the template that will be used
           string xmlPath = HttpContext.Current.Server.MapPath(templatePath);

           DataSet ds = new DataSet();

           XmlReaderSettings settings = new XmlReaderSettings();

           settings.ConformanceLevel = ConformanceLevel.Auto;

           // input is a stream or filename
           using (XmlReader reader = XmlReader.Create(xmlPath, settings))
           {
               ds.ReadXml(reader);
           }

           return ds;
       }

       public static string PopulateXml(ApplicationModel objAppView, string templatePath , string CreatedTime)
       {
           string AppXml = string.Empty;
           using (StreamReader reader = new StreamReader(HostingEnvironment.MapPath(templatePath)))
           {
               AppXml = reader.ReadToEnd();
           }

           AppXml = AppXml.Replace("{{ApplicationId}}",objAppView.MerchantApp_ID.ToString());
           AppXml = AppXml.Replace("{{CreatedWhen}}", CreatedTime);
           AppXml = AppXml.Replace("{{UpdatedWhen}}", CreatedTime);
           
           AppXml = AppXml.Replace("{{Legal Business Name}}", objAppView.LBN);
           AppXml = AppXml.Replace("{{DBA}}", objAppView.DBA);
           AppXml = AppXml.Replace("{{Ownership Type}}", objAppView.OwnershipTyp);
           AppXml = AppXml.Replace("{{Years in Busi}}", objAppView.yr);
           AppXml = AppXml.Replace("{{Month in Busi}}", objAppView.mt);
           AppXml = AppXml.Replace("{{Main Web}}", objAppView.website);
           AppXml = AppXml.Replace("{{Fed Tax}}", objAppView.ftID);
           AppXml = AppXml.Replace("{{Verify Fed Tax}}", objAppView.ftID);
           AppXml = AppXml.Replace("{{Location Address}}", objAppView.LAddress1 + " " + objAppView.LAddress2);
           AppXml = AppXml.Replace("{{Location City}}", objAppView.city);
           AppXml = AppXml.Replace("{{Location Zip}}", objAppView.zip);
           AppXml = AppXml.Replace("{{Location State}}", objAppView.state);
           AppXml = AppXml.Replace("{{MainEmail}}", objAppView.bEmail);
           AppXml = AppXml.Replace("{{Main Phone}}", objAppView.LPhoneNo);
           AppXml = AppXml.Replace("{{Main Fax}}", objAppView.LFaxNo);
           AppXml = AppXml.Replace("{{Mailing Add}}", objAppView.MAddress1 + " " + objAppView.MAddress2);
           AppXml = AppXml.Replace("{{Mailing City}}", objAppView.mcity);
           AppXml = AppXml.Replace("{{Mailing Zip}}", objAppView.mzip);
           AppXml = AppXml.Replace("{{Mailing State}}", objAppView.mstate);

           AppXml = AppXml.Replace("{{firstname}}", objAppView.FName);
           AppXml = AppXml.Replace("{{lastname}}", objAppView.LName);
           AppXml = AppXml.Replace("{{Princ DOB}}", objAppView.DOB);
           AppXml = AppXml.Replace("{{prici SSN}}", objAppView.SocSecurity);
           AppXml = AppXml.Replace("{{Princi verify SSN#}}", objAppView.SocSecurity);
           AppXml = AppXml.Replace("{{RAdd}}", objAppView.RAddress1 + " " + objAppView.RAddress2);
           AppXml = AppXml.Replace("{{RCity}}", objAppView.rcity);
           AppXml = AppXml.Replace("{{RState}}", objAppView.rzip);
           AppXml = AppXml.Replace("{{RZip}}", objAppView.rstate);

           AppXml = AppXml.Replace("{{Merchant Type}}", objAppView.MerchantType);
           AppXml = AppXml.Replace("{{Retail Swiped}}", objAppView.RSwiped);
           AppXml = AppXml.Replace("{{Retail Keyed}}", objAppView.RKeyed);
           AppXml = AppXml.Replace("{{Internet Percent}}", objAppView.internet);
           AppXml = AppXml.Replace("{{Mail Order Percent}}", objAppView.MTOrders);
           AppXml = AppXml.Replace("{{Month Avg}}", objAppView.avgTicket);
           AppXml = AppXml.Replace("{{Month High}}", objAppView.highTicket);
           AppXml = AppXml.Replace("{{Month Process}}", objAppView.MVolume);

           AppXml = AppXml.Replace("{{Bank Name}}", objAppView.BName);
           AppXml = AppXml.Replace("{{Bank City}}", objAppView.BCity);
           AppXml = AppXml.Replace("{{Bank State}}", objAppView.BState);
           AppXml = AppXml.Replace("{{Routing Number}}", objAppView.BRoutNumber);
           AppXml = AppXml.Replace("{{Ac No}}", objAppView.BacNumber);

           AppXml = AppXml.Replace("{{3-tier CrdQual}}", objAppView.CreditQual);
           AppXml = AppXml.Replace("{{3-tier CrdMidQual}}", objAppView.CreditMIDQual);
           AppXml = AppXml.Replace("{{3-tier CrdNonQual}}", objAppView.CreditNonQual);
           AppXml = AppXml.Replace("{{3-tier DbtQual}}", objAppView.DebitQual);
           AppXml = AppXml.Replace("{{3-tier DbtMidQual}}", objAppView.DebitMIDQual);
           AppXml = AppXml.Replace("{{3-tier DbtNonQual}}", objAppView.DebitNonQual);
           AppXml = AppXml.Replace("{{3-tier CrdMidQualPer}}", objAppView.CreditMIDQualPerItem);
           AppXml = AppXml.Replace("{{3-tier CrdNonQualPer}}", objAppView.CreditNonQualPerItem);
           AppXml = AppXml.Replace("{{3-tier DbtMidQualPer}}", objAppView.DebitMIDQualPerItem);
           AppXml = AppXml.Replace("{{3-tier DbtNonQualPe}}", objAppView.DebitNonQualPerItem);
           AppXml = AppXml.Replace("{{Gateway AccesFee}}", objAppView.GatewayAccessFee);
           AppXml = AppXml.Replace("{{Gateway PerAuthFee}}", objAppView.GatewayPerAuthFee);
           AppXml = AppXml.Replace("{{Gateway SetupFee}}", objAppView.GatewaySetUpFee);
           AppXml = AppXml.Replace("{{Wireless AccessFee}}", objAppView.WirelessAccessFee);
           AppXml = AppXml.Replace("{{Wireless PerAuthFee}}", objAppView.WirelessPerAuthFee);
           AppXml = AppXml.Replace("{{Wireless SetupFee}}", objAppView.WirelessSetupFee);
           AppXml = AppXml.Replace("{{Ach ReturnFee}}", objAppView.ReturnTransFee);
           AppXml = AppXml.Replace("{{ChargeBack Fee}}", objAppView.ChargeBackFee);
           AppXml = AppXml.Replace("{{Ebt Trans}}", objAppView.EBTTransFee);
           AppXml = AppXml.Replace("{{Transection Fee}}", objAppView.DebitTransFee);
           AppXml = AppXml.Replace("{{Retrieval Req}}", objAppView.RetrievalFee);
           AppXml = AppXml.Replace("{{Statement Fee}}", objAppView.StatementFee);
           AppXml = AppXml.Replace("{{Monthly Mini}}", objAppView.MontMini);
           AppXml = AppXml.Replace("{{Avs}}", objAppView.ElectroAVSFee);
           AppXml = AppXml.Replace("{{Batch}}", objAppView.BatchFee);
           AppXml = AppXml.Replace("{{Amex Transaction fee}}", objAppView.AMEXTransFee);

           return AppXml;
       }
       

       
    }
}
