using DAL;
using Project.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BAL.Leads
{
  public  class LeadsManager
    {
      public objResponse AddLeadStep1(Project.Entity.Leads objLeads)
      {
          objResponse Response = new objResponse();
          try
          {
              SqlParameter[] sqlParameter = new SqlParameter[32];

              sqlParameter[0] = new SqlParameter("@DBA", SqlDbType.NVarChar, 100);
              sqlParameter[0].Value = objLeads.DBA;

              sqlParameter[1] = new SqlParameter("@ContName", SqlDbType.NVarChar, 100);
              sqlParameter[1].Value = objLeads.ContName;

              sqlParameter[2] = new SqlParameter("@bPhone", SqlDbType.NVarChar, 13);
              sqlParameter[2].Value = objLeads.bPhone;

              sqlParameter[3] = new SqlParameter("@Email", SqlDbType.NVarChar, 100);
              sqlParameter[3].Value = objLeads.Email;

              sqlParameter[4] = new SqlParameter("@website", SqlDbType.NVarChar, 100);
              sqlParameter[4].Value = objLeads.website;

              sqlParameter[5] = new SqlParameter("@currProcessing", SqlDbType.NVarChar, 20);
              sqlParameter[5].Value = objLeads.currProcessing;

              sqlParameter[6] = new SqlParameter("@EstMontVolume", SqlDbType.NVarChar, 50);
              sqlParameter[6].Value = objLeads.EstMontVolume;

              sqlParameter[7] = new SqlParameter("@MerAppUsername", SqlDbType.NVarChar, 100);
              sqlParameter[7].Value = objLeads.MerAppUsername;

              sqlParameter[8] = new SqlParameter("@MerAppPassword", SqlDbType.NVarChar, 10);
              sqlParameter[8].Value = objLeads.MerAppPassword;

              sqlParameter[9] = new SqlParameter("@LBusiName", SqlDbType.NVarChar, 10);
              sqlParameter[9].Value = objLeads.LBusiName;

              sqlParameter[10] = new SqlParameter("@OwnershipType", SqlDbType.NVarChar, 100);
              sqlParameter[10].Value = objLeads.OwnershipType;

              sqlParameter[11] = new SqlParameter("@YrInBusiness", SqlDbType.NVarChar, 10);
              sqlParameter[11].Value = objLeads.YrInBusiness;

              sqlParameter[12] = new SqlParameter("@MtInBusiness", SqlDbType.NVarChar, 10);
              sqlParameter[12].Value = objLeads.MtInBusiness;

              sqlParameter[13] = new SqlParameter("@BDescription", SqlDbType.NVarChar, 500);
              sqlParameter[13].Value = objLeads.BDescription;

              sqlParameter[14] = new SqlParameter("@LAddress1", SqlDbType.NVarChar, 100);
              sqlParameter[14].Value = objLeads.LAddress1;

              sqlParameter[15] = new SqlParameter("@LAddress2", SqlDbType.NVarChar, 100);
              sqlParameter[15].Value = objLeads.LAddress2;

              sqlParameter[16] = new SqlParameter("@city", SqlDbType.NVarChar, 100);
              sqlParameter[16].Value = objLeads.city;

              sqlParameter[17] = new SqlParameter("@state", SqlDbType.NVarChar, 100);
              sqlParameter[17].Value = objLeads.state;

              sqlParameter[18] = new SqlParameter("@zip", SqlDbType.NVarChar, 10);
              sqlParameter[18].Value = objLeads.zip;

              sqlParameter[19] = new SqlParameter("@LPhoneNo", SqlDbType.NVarChar, 13);
              sqlParameter[19].Value = objLeads.LPhoneNo;

              sqlParameter[20] = new SqlParameter("@LFaxNo", SqlDbType.NVarChar, 13);
              sqlParameter[20].Value = objLeads.LFaxNo;

              sqlParameter[21] = new SqlParameter("@MAddress1", SqlDbType.NVarChar, 100);
              sqlParameter[21].Value = objLeads.MAddress1;

              sqlParameter[22] = new SqlParameter("@MAddress2", SqlDbType.NVarChar, 100);
              sqlParameter[22].Value = objLeads.MAddress2;

              sqlParameter[23] = new SqlParameter("@mcity", SqlDbType.NVarChar, 100);
              sqlParameter[23].Value = objLeads.mcity;

              sqlParameter[24] = new SqlParameter("@mstate", SqlDbType.NVarChar, 100);
              sqlParameter[24].Value = objLeads.mstate;

              sqlParameter[25] = new SqlParameter("@mzip", SqlDbType.NVarChar, 10);
              sqlParameter[25].Value = objLeads.mzip;

              sqlParameter[26] = new SqlParameter("@CreatedBy", SqlDbType.BigInt, 10);
              sqlParameter[26].Value = objLeads.CreatedBy;

              sqlParameter[27] = new SqlParameter("@Status", SqlDbType.NVarChar, 80);
              sqlParameter[27].Value = objLeads.LeadStatus;

              sqlParameter[28] = new SqlParameter("@LeadSource", SqlDbType.NVarChar, 80);
              sqlParameter[28].Value = objLeads.LeadSource;

              sqlParameter[29] = new SqlParameter("@LeadGroup", SqlDbType.NVarChar, 80);
              sqlParameter[29].Value = objLeads.LeadGroup;

              sqlParameter[30] = new SqlParameter("@AltNo", SqlDbType.NVarChar, 13);
              sqlParameter[30].Value = objLeads.AltNo;

              sqlParameter[31] = new SqlParameter("@AssignToID", SqlDbType.BigInt, 13);
              sqlParameter[31].Value = objLeads.AssignToID;


              DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_AddLeadStep1", sqlParameter, DB_CONSTANTS.ConnectionString_ICS);


              if (Response.ResponseData.Tables[0].Rows.Count > 0)
              {
                  Response.ErrorCode = 0;
                  Response.ErrorMessage = Response.ResponseData.Tables[0].Rows[0][0].ToString(); ;
              }
              else
              {
                  Response.ErrorCode = 2001;
                  Response.ErrorMessage = "There is an Error. Please Try After some time.";
              }
          }
          catch (Exception ex)
          {
              Response.ErrorMessage = ex.Message.ToString();
              BAL.Common.LogManager.LogError("AddLeadStep1", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
          }

          return Response;
      }

      public objResponse AddLeadStep2(Project.Entity.Leads objLeads)
      {
          objResponse Response = new objResponse();
          try
          {
              SqlParameter[] sqlParameter = new SqlParameter[13];

              sqlParameter[0] = new SqlParameter("@FName", SqlDbType.NVarChar, 100);
              sqlParameter[0].Value = objLeads.FName;

              sqlParameter[1] = new SqlParameter("@LName", SqlDbType.NVarChar, 100);
              sqlParameter[1].Value = objLeads.LName;

              sqlParameter[2] = new SqlParameter("@DOB", SqlDbType.NVarChar, 100);
              sqlParameter[2].Value = objLeads.DOB;

              sqlParameter[3] = new SqlParameter("@SocSecurity", SqlDbType.NVarChar, 50);
              sqlParameter[3].Value = objLeads.SocSecurity;

              sqlParameter[4] = new SqlParameter("@DLicense", SqlDbType.NVarChar, 50);
              sqlParameter[4].Value = objLeads.DLicense;

              sqlParameter[5] = new SqlParameter("@DLicenseState", SqlDbType.NVarChar, 100);
              sqlParameter[5].Value = objLeads.DLicenseState;

              sqlParameter[6] = new SqlParameter("@RAddress1", SqlDbType.NVarChar, 100);
              sqlParameter[6].Value = objLeads.RAddress1;

              sqlParameter[7] = new SqlParameter("@RAddress2", SqlDbType.NVarChar, 100);
              sqlParameter[7].Value = objLeads.RAddress2;

              sqlParameter[8] = new SqlParameter("@rcity", SqlDbType.NVarChar, 100);
              sqlParameter[8].Value = objLeads.rcity;

              sqlParameter[9] = new SqlParameter("@rstate", SqlDbType.NVarChar, 100);
              sqlParameter[9].Value = objLeads.rstate;

              sqlParameter[10] = new SqlParameter("@rzip", SqlDbType.NVarChar, 10);
              sqlParameter[10].Value = objLeads.rzip;

              sqlParameter[11] = new SqlParameter("@OwnerPhone", SqlDbType.NVarChar, 13);
              sqlParameter[11].Value = objLeads.OwnerPhone;

              sqlParameter[12] = new SqlParameter("@Lead_ID", SqlDbType.BigInt, 10);
              sqlParameter[12].Value = objLeads.Lead_ID;

              DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_AddLeadStep2", sqlParameter, DB_CONSTANTS.ConnectionString_ICS);


              if (Response.ResponseData.Tables[0].Rows.Count > 0)
              {
                  Response.ErrorCode = 0;
                  Response.ErrorMessage = Response.ResponseData.Tables[0].Rows[0][0].ToString(); ;
              }
              else
              {
                  Response.ErrorCode = 2001;
                  Response.ErrorMessage = "There is an Error. Please Try After some time.";
              }
          }
          catch (Exception ex)
          {
              Response.ErrorMessage = ex.Message.ToString();
              BAL.Common.LogManager.LogError("AddLeadStep2", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
              
          }
          return Response;
      }

      public objResponse AddLeadStep3(Project.Entity.Leads objLeads)
      {
          objResponse Response = new objResponse();
          try
          {
              SqlParameter[] sqlParameter = new SqlParameter[15];

              sqlParameter[0] = new SqlParameter("@MerchantType", SqlDbType.NVarChar, 50);
              sqlParameter[0].Value = objLeads.MerchantType;

              sqlParameter[1] = new SqlParameter("@RSwiped", SqlDbType.NVarChar, 50);
              sqlParameter[1].Value = objLeads.RSwiped;

              sqlParameter[2] = new SqlParameter("@RKeyed", SqlDbType.NVarChar, 50);
              sqlParameter[2].Value = objLeads.RKeyed;

              sqlParameter[3] = new SqlParameter("@MTOrders", SqlDbType.NVarChar, 50);
              sqlParameter[3].Value = objLeads.MTOrders;

              sqlParameter[4] = new SqlParameter("@internet", SqlDbType.NVarChar, 50);
              sqlParameter[4].Value = objLeads.internet;

              sqlParameter[5] = new SqlParameter("@avgTicket", SqlDbType.NVarChar, 50);
              sqlParameter[5].Value = objLeads.avgTicket;

              sqlParameter[6] = new SqlParameter("@highTicket", SqlDbType.NVarChar, 50);
              sqlParameter[6].Value = objLeads.highTicket;

              sqlParameter[7] = new SqlParameter("@MVolume", SqlDbType.NVarChar, 50);
              sqlParameter[7].Value = objLeads.MVolume;

              sqlParameter[8] = new SqlParameter("@BName", SqlDbType.NVarChar, 100);
              sqlParameter[8].Value = objLeads.BName;

              sqlParameter[9] = new SqlParameter("@BCity", SqlDbType.NVarChar, 100);
              sqlParameter[9].Value = objLeads.BCity;

              sqlParameter[10] = new SqlParameter("@BState", SqlDbType.NVarChar, 100);
              sqlParameter[10].Value = objLeads.BState;

              sqlParameter[11] = new SqlParameter("@BZip", SqlDbType.NVarChar, 100);
              sqlParameter[11].Value = objLeads.BZip;

              sqlParameter[12] = new SqlParameter("@BRoutNumber", SqlDbType.NVarChar, 50);
              sqlParameter[12].Value = objLeads.BRoutNumber;

              sqlParameter[13] = new SqlParameter("@BacNumber", SqlDbType.NVarChar, 30);
              sqlParameter[13].Value = objLeads.BacNumber;

              sqlParameter[14] = new SqlParameter("@Lead_ID", SqlDbType.BigInt, 10);
              sqlParameter[14].Value = objLeads.Lead_ID;

              DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_AddLeadStep3", sqlParameter, DB_CONSTANTS.ConnectionString_ICS);


              if (Response.ResponseData.Tables[0].Rows.Count > 0)
              {
                  Response.ErrorCode = 0;
                  Response.ErrorMessage = Response.ResponseData.Tables[0].Rows[0][0].ToString(); ;
              }
              else
              {
                  Response.ErrorCode = 2001;
                  Response.ErrorMessage = "There is an Error. Please Try After some time.";
              }
          }
          catch (Exception ex)
          {
              Response.ErrorMessage = ex.Message.ToString();
              BAL.Common.LogManager.LogError("AddLeadStep3", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
          }
          return Response;
      }      

        public objResponse AddLeadStep4(Project.Entity.Leads objLeads)
        {
            objResponse Response = new objResponse();
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[49];

                sqlParameter[0] = new SqlParameter("@DebitQual", SqlDbType.NVarChar, 50);
                sqlParameter[0].Value = objLeads.DebitQual;

                sqlParameter[1] = new SqlParameter("@DebitMIDQual", SqlDbType.NVarChar, 50);
                sqlParameter[1].Value = objLeads.DebitMIDQual;

                sqlParameter[2] = new SqlParameter("@DebitNonQual", SqlDbType.NVarChar, 50);
                sqlParameter[2].Value = objLeads.DebitNonQual;

                sqlParameter[3] = new SqlParameter("@CreditQual", SqlDbType.NVarChar, 50);
                sqlParameter[3].Value = objLeads.CreditQual;

                sqlParameter[4] = new SqlParameter("@CreditMIDQual", SqlDbType.NVarChar, 50);
                sqlParameter[4].Value = objLeads.CreditMIDQual;

                sqlParameter[5] = new SqlParameter("@CreditNonQual", SqlDbType.NVarChar, 50);
                sqlParameter[5].Value = objLeads.CreditNonQual;

                sqlParameter[6] = new SqlParameter("@DebitQualPerItem", SqlDbType.NVarChar, 50);
                sqlParameter[6].Value = objLeads.DebitQualPerItem;

                sqlParameter[7] = new SqlParameter("@DebitMIDQualPerItem", SqlDbType.NVarChar, 50);
                sqlParameter[7].Value = objLeads.DebitMIDQualPerItem;

                sqlParameter[8] = new SqlParameter("@DebitNonQualPerItem", SqlDbType.NVarChar, 50);
                sqlParameter[8].Value = objLeads.DebitNonQualPerItem;

                sqlParameter[9] = new SqlParameter("@CreditQualPerItem", SqlDbType.NVarChar, 50);
                sqlParameter[9].Value = objLeads.CreditQualPerItem;

                sqlParameter[10] = new SqlParameter("@CreditMIDQualPerItem", SqlDbType.NVarChar, 50);
                sqlParameter[10].Value = objLeads.CreditMIDQualPerItem;

                sqlParameter[11] = new SqlParameter("@CreditNonQualPerItem", SqlDbType.NVarChar, 50);
                sqlParameter[11].Value = objLeads.CreditNonQualPerItem;

                sqlParameter[12] = new SqlParameter("@DebitTransFee", SqlDbType.NVarChar, 50);
                sqlParameter[12].Value = objLeads.DebitTransFee;

                sqlParameter[13] = new SqlParameter("@ReturnTransFee", SqlDbType.NVarChar, 50);
                sqlParameter[13].Value = objLeads.ReturnTransFee;

                sqlParameter[14] = new SqlParameter("@EBTTransFee", SqlDbType.NVarChar, 50);
                sqlParameter[14].Value = objLeads.EBTTransFee;

                sqlParameter[15] = new SqlParameter("@ElectroAVSFee", SqlDbType.NVarChar, 50);
                sqlParameter[15].Value = objLeads.ElectroAVSFee;

                sqlParameter[16] = new SqlParameter("@AMEXTransFee", SqlDbType.NVarChar, 50);
                sqlParameter[16].Value = objLeads.AMEXTransFee;

                sqlParameter[17] = new SqlParameter("@StatementFee", SqlDbType.NVarChar, 50);
                sqlParameter[17].Value = objLeads.StatementFee;

                sqlParameter[18] = new SqlParameter("@MontMini", SqlDbType.NVarChar, 50);
                sqlParameter[18].Value = objLeads.MontMini;

                sqlParameter[19] = new SqlParameter("@ChargeBackFee", SqlDbType.NVarChar, 50);
                sqlParameter[19].Value = objLeads.ChargeBackFee;

                sqlParameter[20] = new SqlParameter("@BatchFee", SqlDbType.NVarChar, 50);
                sqlParameter[20].Value = objLeads.BatchFee;

                sqlParameter[21] = new SqlParameter("@Lead_ID", SqlDbType.BigInt, 10);
                sqlParameter[21].Value = objLeads.Lead_ID;

                sqlParameter[22] = new SqlParameter("@Equipment", SqlDbType.NVarChar, 80);
                sqlParameter[22].Value = "";

                sqlParameter[23] = new SqlParameter("@RatesType", SqlDbType.NVarChar, 50);
                sqlParameter[23].Value = objLeads.RatesType;

                sqlParameter[24] = new SqlParameter("@CreditDisocuntRate", SqlDbType.NVarChar, 50);
                sqlParameter[24].Value = objLeads.CreditDisocuntRate;

                sqlParameter[25] = new SqlParameter("@DebitDiscountRate", SqlDbType.NVarChar, 50);
                sqlParameter[25].Value = objLeads.DebitDiscountRate;

                sqlParameter[26] = new SqlParameter("@ERRSurcharge", SqlDbType.NVarChar, 50);
                sqlParameter[26].Value = objLeads.ERRSurcharge;

                sqlParameter[27] = new SqlParameter("@InterchangeRate", SqlDbType.NVarChar, 50);
                sqlParameter[27].Value = objLeads.InterchangeRate;

                sqlParameter[28] = new SqlParameter("@DiscountRate", SqlDbType.NVarChar, 50);
                sqlParameter[28].Value = objLeads.DiscountRate;

                sqlParameter[29] = new SqlParameter("@ReserveAccountFee", SqlDbType.NVarChar, 50);
                sqlParameter[29].Value = objLeads.ReserveAccountFee;

                sqlParameter[30] = new SqlParameter("@FDRHelpDeskFee", SqlDbType.NVarChar, 50);
                sqlParameter[30].Value = objLeads.FDRHelpDeskFee;

                sqlParameter[31] = new SqlParameter("@FDRAsstServiceFee", SqlDbType.NVarChar, 50);
                sqlParameter[31].Value = objLeads.FDRAsstServiceFee;

                sqlParameter[32] = new SqlParameter("@ACHChangeFee", SqlDbType.NVarChar, 50);
                sqlParameter[32].Value = objLeads.ACHChangeFee;

                sqlParameter[33] = new SqlParameter("@RetrivalRequestFee", SqlDbType.NVarChar, 50);
                sqlParameter[33].Value = objLeads.RetrivalRequestFee;

                sqlParameter[34] = new SqlParameter("@VoiceAuthFee", SqlDbType.NVarChar, 50);
                sqlParameter[34].Value = objLeads.VoiceAuthFee;

                sqlParameter[35] = new SqlParameter("@AnnualFee", SqlDbType.NVarChar, 50);
                sqlParameter[35].Value = objLeads.AnnualFee;

                sqlParameter[36] = new SqlParameter("@PCINonActionFee", SqlDbType.NVarChar, 50);
                sqlParameter[36].Value = objLeads.PCINonActionFee;

                sqlParameter[37] = new SqlParameter("@RegulatoryFee", SqlDbType.NVarChar, 50);
                sqlParameter[37].Value = objLeads.RegulatoryFee;

                sqlParameter[38] = new SqlParameter("@RegulatoryNonComplienceFee", SqlDbType.NVarChar, 50);
                sqlParameter[38].Value = objLeads.RegulatoryNonComplienceFee;

                sqlParameter[39] = new SqlParameter("@EarlyTerminationBefore1Year", SqlDbType.NVarChar, 50);
                sqlParameter[39].Value = objLeads.EarlyTerminationBefore1Year;

                sqlParameter[40] = new SqlParameter("@EarlyTerminationAfter1Year", SqlDbType.NVarChar, 50);
                sqlParameter[40].Value = objLeads.EarlyTerminationAfter1Year;

                sqlParameter[41] = new SqlParameter("@EarlyTerminationBefore2Year", SqlDbType.NVarChar, 50);
                sqlParameter[41].Value = objLeads.EarlyTerminationBefore2Year;

                sqlParameter[42] = new SqlParameter("@InterchangeClearFee", SqlDbType.NVarChar, 50);
                sqlParameter[42].Value = objLeads.InterchangeClearFee;

                sqlParameter[43] = new SqlParameter("@WirelessSetupFee", SqlDbType.NVarChar, 50);
                sqlParameter[43].Value = objLeads.WirelessSetupFee;

                sqlParameter[44] = new SqlParameter("@WirelessMonthlyFee", SqlDbType.NVarChar, 50);
                sqlParameter[44].Value = objLeads.WirelessMonthlyFee;

                sqlParameter[45] = new SqlParameter("@WirelessAuthFee", SqlDbType.NVarChar, 50);
                sqlParameter[45].Value = objLeads.WirelessAuthFee;

                sqlParameter[46] = new SqlParameter("@GatewaySetupFee", SqlDbType.NVarChar, 50);
                sqlParameter[46].Value = objLeads.GatewaySetupFee;

                sqlParameter[47] = new SqlParameter("@GatewayMonthlyFee", SqlDbType.NVarChar, 50);
                sqlParameter[47].Value = objLeads.GatewayMonthlyFee;

                sqlParameter[48] = new SqlParameter("@GatewayAuthFee", SqlDbType.NVarChar, 50);
                sqlParameter[48].Value = objLeads.GatewayAuthFee;


                DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_AddLeadStep4", sqlParameter, DB_CONSTANTS.ConnectionString_ICS);


                if (Response.ResponseData.Tables[0].Rows.Count > 0)
                {
                    Response.ErrorCode = 0;
                    Response.ErrorMessage = Response.ResponseData.Tables[0].Rows[0][0].ToString(); ;
                }
                else
                {
                    Response.ErrorCode = 2001;
                    Response.ErrorMessage = "There is an Error. Please Try After some time.";
                }
            }
            catch (Exception ex)
            {
                Response.ErrorMessage = ex.Message.ToString();
                BAL.Common.LogManager.LogError("AddLeadStep4", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
            }
            return Response;
        }

        public objResponse AddLeadStep5(Project.Entity.Leads objLeads)
        {
            objResponse Response = new objResponse();
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[17];

                sqlParameter[0] = new SqlParameter("@EquipmentNo", SqlDbType.NVarChar, 50);
                sqlParameter[0].Value = objLeads.EquipmentNo;

                sqlParameter[1] = new SqlParameter("@EPNReaders", SqlDbType.NVarChar, 50);
                sqlParameter[1].Value = objLeads.EPNReaders;

                sqlParameter[2] = new SqlParameter("@Terminals", SqlDbType.NVarChar, 50);
                sqlParameter[2].Value = objLeads.Terminals;

                sqlParameter[3] = new SqlParameter("@EquipmentType1", SqlDbType.NVarChar, 50);
                sqlParameter[3].Value = objLeads.EquipmentType1;

                sqlParameter[4] = new SqlParameter("@TerminalType", SqlDbType.NVarChar, 50);
                sqlParameter[4].Value = objLeads.TerminalType;

                sqlParameter[5] = new SqlParameter("@FreeTerminal", SqlDbType.NVarChar, 50);
                sqlParameter[5].Value = objLeads.FreeTerminal;

                sqlParameter[6] = new SqlParameter("@SoftwareName", SqlDbType.NVarChar, 50);
                sqlParameter[6].Value = objLeads.SoftwareName;

                sqlParameter[7] = new SqlParameter("@GatewayName", SqlDbType.NVarChar, 50);
                sqlParameter[7].Value = objLeads.GatewayName;

                sqlParameter[8] = new SqlParameter("@EquipmentSerial", SqlDbType.NVarChar, 50);
                sqlParameter[8].Value = objLeads.EquipmentSerial;

                sqlParameter[9] = new SqlParameter("@Pinpad", SqlDbType.NVarChar, 50);
                sqlParameter[9].Value = objLeads.Pinpad;

                sqlParameter[10] = new SqlParameter("@PinPadPurchasePrice", SqlDbType.NVarChar, 50);
                sqlParameter[10].Value = objLeads.PinPadPurchasePrice;

                sqlParameter[11] = new SqlParameter("@PinpadSerial", SqlDbType.NVarChar, 50);
                sqlParameter[11].Value = objLeads.PinpadSerial;

                sqlParameter[12] = new SqlParameter("@ShippingMethod", SqlDbType.NVarChar, 50);
                sqlParameter[12].Value = objLeads.ShippingMethod;

                sqlParameter[13] = new SqlParameter("@Shipped", SqlDbType.NVarChar, 50);
                sqlParameter[13].Value = objLeads.Shipped;

                sqlParameter[14] = new SqlParameter("@TrackingNumber", SqlDbType.NVarChar, 50);
                sqlParameter[14].Value = objLeads.TrackingNumber;

                sqlParameter[15] = new SqlParameter("@EquipmentType2", SqlDbType.NVarChar, 50);
                sqlParameter[15].Value = objLeads.EquipmentType2;

                sqlParameter[16] = new SqlParameter("@PurchasePrice", SqlDbType.NVarChar, 50);
                sqlParameter[16].Value = objLeads.PurchasePrice;

                sqlParameter[17] = new SqlParameter("@ApplicableTaxes", SqlDbType.NVarChar, 50);
                sqlParameter[17].Value = objLeads.ApplicableTaxes;

                sqlParameter[18] = new SqlParameter("@Lead_ID", SqlDbType.BigInt, 10);
                sqlParameter[18].Value = objLeads.Lead_ID;




                DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_AddLeadStep5", sqlParameter, DB_CONSTANTS.ConnectionString_ICS);


                if (Response.ResponseData.Tables[0].Rows.Count > 0)
                {
                    Response.ErrorCode = 0;
                    Response.ErrorMessage = Response.ResponseData.Tables[0].Rows[0][0].ToString(); ;
                }
                else
                {
                    Response.ErrorCode = 2001;
                    Response.ErrorMessage = "There is an Error. Please Try After some time.";
                }
            }
            catch (Exception ex)
            {
                Response.ErrorMessage = ex.Message.ToString();
                BAL.Common.LogManager.LogError("AddLeadStep5", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
            }
            return Response;
        }

      public List<Project.Entity.Leads> getAllLeads(string logedUser , string UserRole)
      {
          objResponse Response = new objResponse();
          List<Project.Entity.Leads> leads = new List<Project.Entity.Leads>();
          try
          {
              SqlParameter[] sqlParameter = new SqlParameter[2];

              sqlParameter[0] = new SqlParameter("@logedUser", SqlDbType.BigInt, 10);
              sqlParameter[0].Value = Convert.ToInt64(logedUser);

              sqlParameter[1] = new SqlParameter("@UserRole", SqlDbType.NVarChar, 50);
              sqlParameter[1].Value = UserRole;

              DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetAllLeads", sqlParameter, DB_CONSTANTS.ConnectionString_ICS);


              if (Response.ResponseData.Tables[0].Rows.Count > 0)
              {
                  Response.ErrorCode = 0;
                  foreach (DataRow dr in Response.ResponseData.Tables[0].Rows)
                  {
                      Project.Entity.Leads objLeads = new Project.Entity.Leads();
                      objLeads.Lead_ID = Convert.ToInt64(dr["Lead_ID_Auto_PK"]);
                      objLeads.DBA = dr["DBA"].ToString();
                      objLeads.ContName = dr["ContactName"].ToString();
                      objLeads.bPhone = dr["BusinessPhone"].ToString();
                      objLeads.Email = dr["Email"].ToString();
                      objLeads.LeadStatus = dr["Status_Name"].ToString();
                      objLeads.LeadColour = dr["StatusColour"].ToString();
                      objLeads.AssignToName = Convert.ToString(dr["AssignToName"]);
                      objLeads.CreatedByName = dr["CreatedBy"].ToString();
                        if(dr["AssignTo"].ToString() != "")
                        {
                            objLeads.AssignToID = Convert.ToInt64(dr["AssignTo"]);
                        }

                        if (dr["CreatedDate"].ToString() != "")
                        {
                            objLeads.CreatedDate = Convert.ToDateTime(dr["CreatedDate"]).ToString("MM/dd/yyyy");
                        }

                        
                        objLeads.LeadSource = dr["LeadSource"].ToString();
                      leads.Add(objLeads);
                  }
              }
              else
              {
                  Response.ErrorCode = 2001;
                  Response.ErrorMessage = "There is an Error. Please Try After some time.";
              }
          }
          catch (Exception ex)
          {
              Response.ErrorMessage = ex.Message.ToString();
              BAL.Common.LogManager.LogError("getAllLeads", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
          }
          return leads;
      }

      public string DeleteLead(Int64 Lead_id_pk)
      {
          objResponse response = new objResponse();
          DataTable dt = new DataTable();
          string result = "";
          try
          {
              SqlParameter[] sqlParameter = new SqlParameter[1];

              sqlParameter[0] = new SqlParameter("@Lead_id_pk", SqlDbType.BigInt, 0);
              sqlParameter[0].Value = Lead_id_pk;

              DATA_ACCESS_LAYER.Fill(response.ResponseData, "usp_DeleteLead", sqlParameter, DB_CONSTANTS.ConnectionString_ICS);

              dt = response.ResponseData.Tables[0];
              if (dt.Rows.Count > 0)
              {
                  response.ErrorCode = 0;
                  response.ErrorMessage = "Success";
                  result = Convert.ToString(dt.Rows[0][0]);
              }

          }
          catch (Exception ex)
          {
              response.ErrorCode = 2001;
              response.ErrorMessage = "Error while processing: " + ex.Message;
              BAL.Common.LogManager.LogError("DeleteLead", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
          }
          return result;
      }

      public objResponse getLeadsByID(long Lead_ID)
      {
          objResponse Response = new objResponse();          
          try
          {
              SqlParameter[] sqlParameter = new SqlParameter[1];

              sqlParameter[0] = new SqlParameter("@Lead_ID", SqlDbType.BigInt, 10);
              sqlParameter[0].Value = Lead_ID;


              DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetLeadByID", sqlParameter, DB_CONSTANTS.ConnectionString_ICS);


              if (Response.ResponseData.Tables[0].Rows.Count > 0)
              {
                  Response.ErrorCode = 0;
                  Response.ErrorMessage = "Suceess";                  
              }
              else
              {
                  Response.ErrorCode = 2001;
                  Response.ErrorMessage = "There is an Error. Please Try After some time.";
              }
          }
          catch (Exception ex)
          {
              Response.ErrorMessage = ex.Message.ToString();
              BAL.Common.LogManager.LogError("getLeadsByID", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
          }
          return Response;
      }

      public objResponse getLeadForEditByID(long Lead_ID)
      {
          objResponse Response = new objResponse();
          try
          {
              SqlParameter[] sqlParameter = new SqlParameter[1];

              sqlParameter[0] = new SqlParameter("@Lead_ID", SqlDbType.BigInt, 10);
              sqlParameter[0].Value = Lead_ID;


              DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetLeadForEditByID", sqlParameter, DB_CONSTANTS.ConnectionString_ICS);


              if (Response.ResponseData.Tables[0].Rows.Count > 0)
              {
                  Response.ErrorCode = 0;
                  Response.ErrorMessage = "Suceess";
              }
              else
              {
                  Response.ErrorCode = 2001;
                  Response.ErrorMessage = "There is an Error. Please Try After some time.";
              }
          }
          catch (Exception ex)
          {
              Response.ErrorMessage = ex.Message.ToString();
              BAL.Common.LogManager.LogError("getLeadForEditByID", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
          }
          return Response;
      }

      public objResponse getsaleRepByID(long srcId)
      {
          objResponse Response = new objResponse();
          try
          {
              SqlParameter[] sqlParameter = new SqlParameter[1];

              sqlParameter[0] = new SqlParameter("@srcId", SqlDbType.BigInt, 10);
              sqlParameter[0].Value = srcId;


              DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_getSalesrepIdBySource", sqlParameter, DB_CONSTANTS.ConnectionString_ICS);


              if (Response.ResponseData.Tables[0].Rows.Count > 0)
              {
                  Response.ErrorCode = 0;
                  Response.ErrorMessage = "Suceess";
              }
              else
              {
                  Response.ErrorCode = 2001;
                  Response.ErrorMessage = "There is an Error. Please Try After some time.";
              }
          }
          catch (Exception ex)
          {
              Response.ErrorMessage = ex.Message.ToString();
              BAL.Common.LogManager.LogError("getLeadForEditByID", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
          }
          return Response;
      }

        public objResponse getDispositionModalData(long LeadId)
        {
            objResponse Response = new objResponse();
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[1];

                sqlParameter[0] = new SqlParameter("@LeadId", SqlDbType.BigInt, 10);
                sqlParameter[0].Value = LeadId;


                DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_getDispoModalData", sqlParameter, DB_CONSTANTS.ConnectionString_ICS);


                if (Response.ResponseData.Tables[0].Rows.Count > 0)
                {
                    Response.ErrorCode = 0;
                    Response.ErrorMessage = "Suceess";
                }
                else
                {
                    Response.ErrorCode = 2001;
                    Response.ErrorMessage = "There is an Error. Please Try After some time.";
                }
            }
            catch (Exception ex)
            {
                Response.ErrorMessage = ex.Message.ToString();
                BAL.Common.LogManager.LogError("getDispositionModalData", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
            }
            return Response;
        }

        public objResponse changeStatus(string Lead_ID , string DispId , string Notes , string LogedUser)
      {
          objResponse Response = new objResponse();
          try
          {
              SqlParameter[] sqlParameter = new SqlParameter[4];

              sqlParameter[0] = new SqlParameter("@Lead_ID", SqlDbType.BigInt, 10);
              sqlParameter[0].Value = Convert.ToInt64(Lead_ID);

              sqlParameter[1] = new SqlParameter("@DispId", SqlDbType.BigInt, 10);
              sqlParameter[1].Value = Convert.ToInt64(DispId);

              sqlParameter[2] = new SqlParameter("@Notes", SqlDbType.NVarChar, 400);
              sqlParameter[2].Value = Notes;

              sqlParameter[3] = new SqlParameter("@LogedUser", SqlDbType.BigInt, 10);
              sqlParameter[3].Value = Convert.ToInt64(LogedUser);


              DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_changeLeadStatus", sqlParameter, DB_CONSTANTS.ConnectionString_ICS);


              if (Response.ResponseData.Tables[0].Rows.Count > 0)
              {
                  Response.ErrorCode = 0;
                  Response.ErrorMessage = "Suceess";
              }
              else
              {
                  Response.ErrorCode = 2001;
                  Response.ErrorMessage = "There is an Error. Please Try After some time.";
              }
          }
          catch (Exception ex)
          {
              Response.ErrorMessage = ex.Message.ToString();
              BAL.Common.LogManager.LogError("changeStatus", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
          }
          return Response;
      }

      public objResponse changeAssignToId(string Lead_ID, string AssignID)
      {
          objResponse Response = new objResponse();
          try
          {
              SqlParameter[] sqlParameter = new SqlParameter[2];

              sqlParameter[0] = new SqlParameter("@Lead_ID", SqlDbType.BigInt, 10);
              sqlParameter[0].Value = Convert.ToInt64(Lead_ID);

              sqlParameter[1] = new SqlParameter("@AssignID", SqlDbType.BigInt, 10);
              sqlParameter[1].Value = Convert.ToInt64(AssignID);


              DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_changeLeadAssignToId", sqlParameter, DB_CONSTANTS.ConnectionString_ICS);


              if (Response.ResponseData.Tables[0].Rows.Count > 0)
              {
                  Response.ErrorCode = 0;
                  Response.ErrorMessage = "Suceess";
              }
              else
              {
                  Response.ErrorCode = 2001;
                  Response.ErrorMessage = "There is an Error. Please Try After some time.";
              }
          }
          catch (Exception ex)
          {
              Response.ErrorMessage = ex.Message.ToString();
              BAL.Common.LogManager.LogError("changeAssignToId", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
          }
          return Response;
      }

      public objResponse editLeadStep1(Project.Entity.Leads objLeads)
      {
          objResponse Response = new objResponse();
          try
          {
              SqlParameter[] sqlParameter = new SqlParameter[33];

              sqlParameter[0] = new SqlParameter("@DBA", SqlDbType.NVarChar, 100);
              sqlParameter[0].Value = objLeads.DBA;

              sqlParameter[1] = new SqlParameter("@ContName", SqlDbType.NVarChar, 100);
              sqlParameter[1].Value = objLeads.ContName;

              sqlParameter[2] = new SqlParameter("@bPhone", SqlDbType.NVarChar, 13);
              sqlParameter[2].Value = objLeads.bPhone;

              sqlParameter[3] = new SqlParameter("@Email", SqlDbType.NVarChar, 100);
              sqlParameter[3].Value = objLeads.Email;

              sqlParameter[4] = new SqlParameter("@website", SqlDbType.NVarChar, 100);
              sqlParameter[4].Value = objLeads.website;

              sqlParameter[5] = new SqlParameter("@currProcessing", SqlDbType.NVarChar, 20);
              sqlParameter[5].Value = objLeads.currProcessing;

              sqlParameter[6] = new SqlParameter("@EstMontVolume", SqlDbType.NVarChar, 50);
              sqlParameter[6].Value = objLeads.EstMontVolume;

              sqlParameter[7] = new SqlParameter("@MerAppUsername", SqlDbType.NVarChar, 100);
              sqlParameter[7].Value = objLeads.MerAppUsername;

              sqlParameter[8] = new SqlParameter("@MerAppPassword", SqlDbType.NVarChar, 10);
              sqlParameter[8].Value = objLeads.MerAppPassword;

              sqlParameter[9] = new SqlParameter("@LBusiName", SqlDbType.NVarChar, 10);
              sqlParameter[9].Value = objLeads.LBusiName;

              sqlParameter[10] = new SqlParameter("@OwnershipType", SqlDbType.NVarChar, 100);
              sqlParameter[10].Value = objLeads.OwnershipType;

              sqlParameter[11] = new SqlParameter("@YrInBusiness", SqlDbType.NVarChar, 10);
              sqlParameter[11].Value = objLeads.YrInBusiness;

              sqlParameter[12] = new SqlParameter("@MtInBusiness", SqlDbType.NVarChar, 10);
              sqlParameter[12].Value = objLeads.MtInBusiness;

              sqlParameter[13] = new SqlParameter("@BDescription", SqlDbType.NVarChar, 500);
              sqlParameter[13].Value = objLeads.BDescription;

              sqlParameter[14] = new SqlParameter("@LAddress1", SqlDbType.NVarChar, 100);
              sqlParameter[14].Value = objLeads.LAddress1;

              sqlParameter[15] = new SqlParameter("@LAddress2", SqlDbType.NVarChar, 100);
              sqlParameter[15].Value = objLeads.LAddress2;

              sqlParameter[16] = new SqlParameter("@city", SqlDbType.NVarChar, 100);
              sqlParameter[16].Value = objLeads.city;

              sqlParameter[17] = new SqlParameter("@state", SqlDbType.NVarChar, 100);
              sqlParameter[17].Value = objLeads.state;

              sqlParameter[18] = new SqlParameter("@zip", SqlDbType.NVarChar, 10);
              sqlParameter[18].Value = objLeads.zip;

              sqlParameter[19] = new SqlParameter("@LPhoneNo", SqlDbType.NVarChar, 13);
              sqlParameter[19].Value = objLeads.LPhoneNo;

              sqlParameter[20] = new SqlParameter("@LFaxNo", SqlDbType.NVarChar, 13);
              sqlParameter[20].Value = objLeads.LFaxNo;

              sqlParameter[21] = new SqlParameter("@MAddress1", SqlDbType.NVarChar, 100);
              sqlParameter[21].Value = objLeads.MAddress1;

              sqlParameter[22] = new SqlParameter("@MAddress2", SqlDbType.NVarChar, 100);
              sqlParameter[22].Value = objLeads.MAddress2;

              sqlParameter[23] = new SqlParameter("@mcity", SqlDbType.NVarChar, 100);
              sqlParameter[23].Value = objLeads.mcity;

              sqlParameter[24] = new SqlParameter("@mstate", SqlDbType.NVarChar, 100);
              sqlParameter[24].Value = objLeads.mstate;

              sqlParameter[25] = new SqlParameter("@mzip", SqlDbType.NVarChar, 10);
              sqlParameter[25].Value = objLeads.mzip;

              sqlParameter[26] = new SqlParameter("@CreatedBy", SqlDbType.BigInt, 10);
              sqlParameter[26].Value = objLeads.CreatedBy;

              sqlParameter[27] = new SqlParameter("@Status", SqlDbType.NVarChar, 80);
              sqlParameter[27].Value = objLeads.LeadStatus;

              sqlParameter[28] = new SqlParameter("@LeadSource", SqlDbType.NVarChar, 80);
              sqlParameter[28].Value = objLeads.LeadSource;

              sqlParameter[29] = new SqlParameter("@LeadGroup", SqlDbType.NVarChar, 80);
              sqlParameter[29].Value = objLeads.LeadGroup;

              sqlParameter[30] = new SqlParameter("@AltNo", SqlDbType.NVarChar, 13);
              sqlParameter[30].Value = objLeads.AltNo;

              sqlParameter[31] = new SqlParameter("@Lead_ID", SqlDbType.BigInt, 13);
              sqlParameter[31].Value = objLeads.Lead_ID;

              sqlParameter[32] = new SqlParameter("@AssignToID", SqlDbType.BigInt, 13);
              sqlParameter[32].Value = objLeads.AssignToID;


              DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_EditLeadStep1", sqlParameter, DB_CONSTANTS.ConnectionString_ICS);


              if (Response.ResponseData.Tables[0].Rows.Count > 0)
              {
                  Response.ErrorCode = 0;
                  Response.ErrorMessage = Response.ResponseData.Tables[0].Rows[0][0].ToString(); ;
              }
              else
              {
                  Response.ErrorCode = 2001;
                  Response.ErrorMessage = "There is an Error. Please Try After some time.";
              }
          }
          catch (Exception ex)
          {
              Response.ErrorMessage = ex.Message.ToString();
              BAL.Common.LogManager.LogError("editLeadStep1", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
          }
          return Response;
      }

      public objResponse AssignLead(long Lead_ID, long UserID)
      {
          objResponse Response = new objResponse();
          try
          {
              SqlParameter[] sqlParameter = new SqlParameter[2];

              sqlParameter[0] = new SqlParameter("@Lead_ID", SqlDbType.BigInt, 10);
              sqlParameter[0].Value = Lead_ID;

              sqlParameter[1] = new SqlParameter("@UserID", SqlDbType.BigInt, 10);
              sqlParameter[1].Value = UserID;

             

              DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_AssignLead", sqlParameter, DB_CONSTANTS.ConnectionString_ICS);


              if (Response.ResponseData.Tables[0].Rows.Count > 0)
              {
                  Response.ErrorCode = 0;
                  Response.ErrorMessage = Response.ResponseData.Tables[0].Rows[0][0].ToString();
              }
              else
              {
                  Response.ErrorCode = 2001;
                  Response.ErrorMessage = "There is an Error. Please Try After some time.";
              }
          }
          catch (Exception ex)
          {
              Response.ErrorCode = 3001;
              Response.ErrorMessage = ex.Message.ToString();
              BAL.Common.LogManager.LogError("AssignLead", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
          }
          return Response;
      }

      public objResponse SubmitData(LeadSubmision objLeads)
      {
          objResponse Response = new objResponse();
          try
          {
              SqlParameter[] sqlParameter = new SqlParameter[32];

              sqlParameter[0] = new SqlParameter("@businessName", SqlDbType.NVarChar, 100);
              sqlParameter[0].Value = objLeads.businessName;

              sqlParameter[1] = new SqlParameter("@contactName", SqlDbType.NVarChar, 100);
              sqlParameter[1].Value = objLeads.contactName;

              sqlParameter[2] = new SqlParameter("@contactPhone", SqlDbType.NVarChar, 13);
              sqlParameter[2].Value = objLeads.contactPhone;

              sqlParameter[3] = new SqlParameter("@Email", SqlDbType.NVarChar, 100);
              sqlParameter[3].Value = objLeads.email;

              sqlParameter[4] = new SqlParameter("@secondaryPhone", SqlDbType.NVarChar, 13);
              sqlParameter[4].Value = objLeads.secondaryPhone;

              sqlParameter[9] = new SqlParameter("@cooments", SqlDbType.NVarChar, 200);
              sqlParameter[9].Value = objLeads.cooments;



              DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_SubmitData", sqlParameter, DB_CONSTANTS.ConnectionString_ICS);


              if (Response.ResponseData.Tables[0].Rows.Count > 0)
              {
                  Response.ErrorCode = 0;
                  Response.ErrorMessage = Response.ResponseData.Tables[0].Rows[0][0].ToString(); ;
              }
              else
              {
                  Response.ErrorCode = 2001;
                  Response.ErrorMessage = "There is an Error. Please Try After some time.";
              }
          }
          catch (Exception ex)
          {
              Response.ErrorMessage = ex.Message.ToString();
              BAL.Common.LogManager.LogError("SubmitData", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
          }

          return Response;
      }
    }
}
