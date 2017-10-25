using Project.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Web.Models
{
    public class LeadModel
    {
        public long Lead_ID { get; set; }

        public string DBA { get; set; }

        public string ContName { get; set; }

        public string bPhone { get; set; }

        public string Email { get; set; }

        public string website { get; set; }

        public string AltNo { get; set; }

        public string currProcessing { get; set; }

        public string EstMontVolume { get; set; }

        public string MerAppUsername { get; set; }

        public string MerAppPassword { get; set; }

        public string LBusiName { get; set; }

        public string OwnershipType { get; set; }

        public string YrInBusiness { get; set; }

        public string MtInBusiness { get; set; }

        public string BDescription { get; set; }

        public string LAddress1 { get; set; }

        public string LAddress2 { get; set; }

        public string city { get; set; }

        public string state { get; set; }

        public string zip { get; set; }

        public string LPhoneNo { get; set; }

        public string LFaxNo { get; set; }

        public string MAddress1 { get; set; }

        public string MAddress2 { get; set; }

        public string mcity { get; set; }

        public string mstate { get; set; }

        public string mzip { get; set; }

        public string FName { get; set; }

        public string LName { get; set; }

        public string DOB { get; set; }

        public string SocSecurity { get; set; }

        public string DLicense { get; set; }

        public string DLicenseState { get; set; }

        public string RAddress1 { get; set; }

        public string RAddress2 { get; set; }

        public string rcity { get; set; }

        public string rstate { get; set; }

        public string rzip { get; set; }

        public string OwnerPhone { get; set; }

        public string MerchantType { get; set; }

        public string RSwiped { get; set; }

        public string RKeyed { get; set; }

        public string MTOrders { get; set; }

        public string internet { get; set; }

        public string avgTicket { get; set; }

        public string highTicket { get; set; }

        public string MVolume { get; set; }

        public string BName { get; set; }

        public string BCity { get; set; }

        public string BState { get; set; }

        public string BZip { get; set; }

        public string BRoutNumber { get; set; }

        public string BacNumber { get; set; }

        public string Equipment { get; set; }

        public string DebitQual { get; set; }

        public string DebitMIDQual { get; set; }

        public string DebitNonQual { get; set; }

        public string CreditQual { get; set; }

        public string CreditMIDQual { get; set; }

        public string CreditNonQual { get; set; }

        public string DebitQualPerItem { get; set; }

        public string DebitMIDQualPerItem { get; set; }

        public string DebitNonQualPerItem { get; set; }

        public string CreditQualPerItem { get; set; }

        public string CreditMIDQualPerItem { get; set; }

        public string CreditNonQualPerItem { get; set; }

        public string DebitTransFee { get; set; }

        public string ReturnTransFee { get; set; }

        public string EBTTransFee { get; set; }

        public string ElectroAVSFee { get; set; }

        public string AMEXTransFee { get; set; }

        public string StatementFee { get; set; }

        public string MontMini { get; set; }

        public string ChargeBackFee { get; set; }

        public string BatchFee { get; set; }

        public string GatewayFee { get; set; }

        public string WirelessFee { get; set; }

        public string RetrievalFee { get; set; }

        public long CreatedBy { get; set; }


        public string CreatedDate { get; set; }


        public string UpdatedBy { get; set; }


        public string UpdatedDate { get; set; }

        public string LeadStatus { get; set; }

        public string LeadSource { get; set; }

        public string LeadGroup { get; set; }

        public List<Leads> leads { get; set; }

        public List<Notes> Notes { get; set; }

       // public List<Meetings> Meeting { get; set; }

        public List<Activity> Activity { get; set; }

        public List<Docs> Doc { get; set; }

        public List<Tasks> Task { get; set; }

       // public List<Mails> mails { get; set; }

        public string CreatedByName { get; set; }

        public long AssignToID { get; set; }

        public string AssignToName { get; set; }

        public string LeadEventStartDate { get; set; }

        public string LeadEventEndDate { get; set; }

        public string LeadEventStartTime { get; set; }

        public string LeadEventEndTime { get; set; }

        public string LeadEventDuration { get; set; }

        public string LastNote { get; set; }
    }

    public class LeadSubmisionModel
    {
       
        public long businessName { get; set; }

       
        public long contactName { get; set; }

       
        public long contactPhone { get; set; }

        
        public long secondaryPhone { get; set; }

        
        public long email { get; set; }

       
        public long cooments { get; set; }
    }
}