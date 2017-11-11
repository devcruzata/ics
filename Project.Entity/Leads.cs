using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.Entity
{
   public class Leads
    {

        public long Lead_ID { get; set; }

        /// <summary>
        /// Business Info
        /// </summary>
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


        /// <summary>
        /// Owner Info
        /// </summary>

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


        /// <summary>
        /// Financial Info
        /// </summary>

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

        /// <summary>
        /// Pricing Info
        /// </summary>



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

        public string CreditDisocuntRate { get; set; }

        public string DebitDiscountRate { get; set; }

        public string ERRSurcharge { get; set; }

        public string InterchangeRate { get; set; }

        public string DiscountRate { get; set; }

        public string ReserveAccountFee { get; set; }

        public string FDRHelpDeskFee { get; set; }

        public string FDRAsstServiceFee { get; set; }

        public string ACHChangeFee { get; set; }

        public string RetrivalRequestFee { get; set; }

        public string VoiceAuthFee { get; set; }

        public string AnnualFee { get; set; }

        public string PCINonActionFee { get; set; }

        public string RegulatoryFee { get; set; }

        public string RegulatoryNonComplienceFee { get; set; }

        public string EarlyTerminationBefore1Year { get; set; }

        public string EarlyTerminationAfter1Year { get; set; }

        public string EarlyTerminationBefore2Year { get; set; }

        public string InterchangeClearFee { get; set; }

        public string WirelessSetupFee { get; set; }

        public string WirelessMonthlyFee { get; set; }

        public string WirelessAuthFee { get; set; }

        public string GatewaySetupFee { get; set; }

        public string GatewayMonthlyFee { get; set; }

        public string GatewayAuthFee { get; set; }

        /// <summary>
        /// Equipment Section
        /// </summary>        
        public string EquipmentNo { get; set; }

        public string EPNReaders { get; set; }

        public string Terminals { get; set; }

        public string EquipmentType1 { get; set; }

        public string TerminalType { get; set; }

        public string FreeTerminal { get; set; }

        public string SoftwareName { get; set; }

        public string GatewayName { get; set; }

        public string EquipmentSerial { get; set; }

        public string Pinpad { get; set; }

        public string PinPadPurchasePrice { get; set; }

        public string PinpadSerial { get; set; }

        public string ShippingMethod { get; set; }

        public string Shipped { get; set; }

        public string TrackingNumber { get; set; }

        public string EquipmentType2 { get; set; }

        public string PurchasePrice { get; set; }

        public string ApplicableTaxes { get; set; }


        /// <summary>
        /// Genral Info
        /// </summary>
        public long CreatedBy { get; set; }

        public string CreatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public string UpdatedDate { get; set; }

        public string LeadStatus { get; set; }

        public string LeadColour { get; set; }

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

        //Rates and fees section

        public string RatesType { get; set; }
    }

   public class LeadSubmision
   {

       public long businessName { get; set; }


       public long contactName { get; set; }


       public long contactPhone { get; set; }


       public long secondaryPhone { get; set; }


       public long email { get; set; }


       public long cooments { get; set; }
   }
}
