using DAL;
using Project.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BAL.MerchantApplication
{
    public class AplicationManager
    {

        public objResponse GetAppStage(string AppId)
        {
            objResponse Response = new objResponse();
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[1];

                sqlParameter[0] = new SqlParameter("@AppId", SqlDbType.BigInt, 10);
                sqlParameter[0].Value = Convert.ToInt64(AppId);


                DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetAppStage", sqlParameter, DB_CONSTANTS.ConnectionString_ICS);


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
                Response.ErrorMessage = ex.Message.ToString();
                BAL.Common.LogManager.LogError("GetAppStage", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
            }

            return Response;
        }

        public objResponse ResumeApp(string Email , string Password)
        {
            objResponse Response = new objResponse();
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[2];

                sqlParameter[0] = new SqlParameter("@MerAppUserName", SqlDbType.NVarChar, 80);
                sqlParameter[0].Value = Email;

                sqlParameter[1] = new SqlParameter("@MerAppPassword", SqlDbType.NVarChar, 40);
                sqlParameter[1].Value = Password;


                DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_ResumeApp", sqlParameter, DB_CONSTANTS.ConnectionString_ICS);


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
                Response.ErrorMessage = ex.Message.ToString();
                BAL.Common.LogManager.LogError("ResumeApp", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
            }

            return Response;
        }

        public objResponse GetAppData(string AppId)
        {
            objResponse Response = new objResponse();
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[1];

                sqlParameter[0] = new SqlParameter("@AppId", SqlDbType.BigInt, 10);
                sqlParameter[0].Value = Convert.ToInt64(AppId);


                DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_GetAppData", sqlParameter, DB_CONSTANTS.ConnectionString_ICS);


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
                Response.ErrorMessage = ex.Message.ToString();
                BAL.Common.LogManager.LogError("GetAppData", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
            }

            return Response;
        }

        public objResponse AddAppStep1(Project.Entity.Applications objApp)
        {
            objResponse Response = new objResponse();
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[3];

                sqlParameter[0] = new SqlParameter("@MerAppUserName", SqlDbType.NVarChar, 80);
                sqlParameter[0].Value = objApp.MerAppUserName;

                sqlParameter[1] = new SqlParameter("@MerAppPassword", SqlDbType.NVarChar, 40);
                sqlParameter[1].Value = objApp.MerAppPassword;

                sqlParameter[2] = new SqlParameter("@promoCode", SqlDbType.NVarChar, 50);
                sqlParameter[2].Value = objApp.promoCode;                

                DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_AddApplicationStep1", sqlParameter, DB_CONSTANTS.ConnectionString_ICS);


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
                Response.ErrorMessage = ex.Message.ToString();
                BAL.Common.LogManager.LogError("AddAppStep1", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
            }

            return Response;
        }

        public objResponse AddAppStep2(Project.Entity.Applications objApp)
        {
            objResponse Response = new objResponse();
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[22];

                sqlParameter[0] = new SqlParameter("@DBA", SqlDbType.NVarChar, 100);
                sqlParameter[0].Value = objApp.DBA;

                sqlParameter[1] = new SqlParameter("@LBN", SqlDbType.NVarChar, 100);
                sqlParameter[1].Value = objApp.LBN;

                sqlParameter[2] = new SqlParameter("@bDesc", SqlDbType.NVarChar, 100);
                sqlParameter[2].Value = objApp.bDesc;

                sqlParameter[3] = new SqlParameter("@bEmail", SqlDbType.NVarChar, 80);
                sqlParameter[3].Value = objApp.bEmail;

                sqlParameter[4] = new SqlParameter("@website", SqlDbType.NVarChar, 100);
                sqlParameter[4].Value = objApp.website;

                sqlParameter[5] = new SqlParameter("@OwnershipTyp", SqlDbType.NVarChar, 100);
                sqlParameter[5].Value = objApp.OwnershipTyp;

                sqlParameter[6] = new SqlParameter("@yr", SqlDbType.NVarChar, 3);
                sqlParameter[6].Value = objApp.yr;

                sqlParameter[7] = new SqlParameter("@mt", SqlDbType.NVarChar, 3);
                sqlParameter[7].Value = objApp.mt;

                sqlParameter[8] = new SqlParameter("@ftID", SqlDbType.NVarChar, 9);
                sqlParameter[8].Value = objApp.ftID;                

                sqlParameter[9] = new SqlParameter("@LAddress1", SqlDbType.NVarChar, 100);
                sqlParameter[9].Value = objApp.LAddress1;

                sqlParameter[10] = new SqlParameter("@LAddress2", SqlDbType.NVarChar, 60);
                sqlParameter[10].Value = objApp.LAddress2;

                sqlParameter[11] = new SqlParameter("@city", SqlDbType.NVarChar, 100);
                sqlParameter[11].Value = objApp.city;

                sqlParameter[12] = new SqlParameter("@state", SqlDbType.NVarChar, 100);
                sqlParameter[12].Value = objApp.state;

                sqlParameter[13] = new SqlParameter("@zip", SqlDbType.NVarChar, 7);
                sqlParameter[13].Value = objApp.zip;

                sqlParameter[14] = new SqlParameter("@LPhoneNo", SqlDbType.NVarChar, 13);
                sqlParameter[14].Value = objApp.LPhoneNo;

                sqlParameter[15] = new SqlParameter("@LFaxNo", SqlDbType.NVarChar, 13);
                sqlParameter[15].Value = objApp.LFaxNo;

                sqlParameter[16] = new SqlParameter("@MAddress1", SqlDbType.NVarChar, 100);
                sqlParameter[16].Value = objApp.MAddress1;

                sqlParameter[17] = new SqlParameter("@MAddress2", SqlDbType.NVarChar, 60);
                sqlParameter[17].Value = objApp.MAddress2;

                sqlParameter[18] = new SqlParameter("@mcity", SqlDbType.NVarChar, 100);
                sqlParameter[18].Value = objApp.mcity;

                sqlParameter[19] = new SqlParameter("@mstate", SqlDbType.NVarChar, 20);
                sqlParameter[19].Value = objApp.mstate;

                sqlParameter[20] = new SqlParameter("@mzip", SqlDbType.NVarChar, 7);
                sqlParameter[20].Value = objApp.mzip;

                sqlParameter[21] = new SqlParameter("@Mer_App_ID", SqlDbType.BigInt, 10);
                sqlParameter[21].Value = objApp.MerchantApp_ID;




                DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_AddApplicationStep2", sqlParameter, DB_CONSTANTS.ConnectionString_ICS);


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
                Response.ErrorMessage = ex.Message.ToString();
                BAL.Common.LogManager.LogError("AddAppStep2", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
            }

            return Response;
        }

        public objResponse AddAppStep3(Project.Entity.Applications objApp)
        {
            objResponse Response = new objResponse();
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[12];

                sqlParameter[0] = new SqlParameter("@FName", SqlDbType.NVarChar, 100);
                sqlParameter[0].Value = objApp.FName;

                sqlParameter[1] = new SqlParameter("@LName", SqlDbType.NVarChar, 100);
                sqlParameter[1].Value = objApp.LName;

                sqlParameter[2] = new SqlParameter("@DOB", SqlDbType.NVarChar, 100);
                sqlParameter[2].Value = objApp.DOB;

                sqlParameter[3] = new SqlParameter("@SocSecurity", SqlDbType.NVarChar, 50);
                sqlParameter[3].Value = objApp.SocSecurity;

                sqlParameter[4] = new SqlParameter("@DLicense", SqlDbType.NVarChar, 50);
                sqlParameter[4].Value = objApp.DLicense;

                sqlParameter[5] = new SqlParameter("@DLicenseState", SqlDbType.NVarChar, 100);
                sqlParameter[5].Value = objApp.DLicenseState;

                sqlParameter[6] = new SqlParameter("@RAddress1", SqlDbType.NVarChar, 100);
                sqlParameter[6].Value = objApp.RAddress1;

                sqlParameter[7] = new SqlParameter("@RAddress2", SqlDbType.NVarChar, 100);
                sqlParameter[7].Value = objApp.RAddress2;

                sqlParameter[8] = new SqlParameter("@rcity", SqlDbType.NVarChar, 100);
                sqlParameter[8].Value = objApp.rcity;

                sqlParameter[9] = new SqlParameter("@rstate", SqlDbType.NVarChar, 100);
                sqlParameter[9].Value = objApp.rstate;

                sqlParameter[10] = new SqlParameter("@rzip", SqlDbType.NVarChar, 10);
                sqlParameter[10].Value = objApp.rzip;

                sqlParameter[11] = new SqlParameter("@Mer_App_ID", SqlDbType.BigInt, 10);
                sqlParameter[11].Value = objApp.MerchantApp_ID;




                DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_AddApplicationStep3", sqlParameter, DB_CONSTANTS.ConnectionString_ICS);


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
                Response.ErrorMessage = ex.Message.ToString();
                BAL.Common.LogManager.LogError("AddAppStep3", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
            }

            return Response;
        }

        public objResponse AddAppStep4(Project.Entity.Applications objApp)
        {
            objResponse Response = new objResponse();
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[9];

                sqlParameter[0] = new SqlParameter("@MerchantType", SqlDbType.NVarChar, 50);
                sqlParameter[0].Value = objApp.MerchantType;

                sqlParameter[1] = new SqlParameter("@RSwiped", SqlDbType.NVarChar, 50);
                sqlParameter[1].Value = objApp.RSwiped;

                sqlParameter[2] = new SqlParameter("@RKeyed", SqlDbType.NVarChar, 50);
                sqlParameter[2].Value = objApp.RKeyed;

                sqlParameter[3] = new SqlParameter("@MTOrders", SqlDbType.NVarChar, 50);
                sqlParameter[3].Value = objApp.MTOrders;

                sqlParameter[4] = new SqlParameter("@internet", SqlDbType.NVarChar, 50);
                sqlParameter[4].Value = objApp.internet;

                sqlParameter[5] = new SqlParameter("@avgTicket", SqlDbType.NVarChar, 50);
                sqlParameter[5].Value = objApp.avgTicket;

                sqlParameter[6] = new SqlParameter("@highTicket", SqlDbType.NVarChar, 50);
                sqlParameter[6].Value = objApp.highTicket;

                sqlParameter[7] = new SqlParameter("@MVolume", SqlDbType.NVarChar, 50);
                sqlParameter[7].Value = objApp.MVolume;

                sqlParameter[8] = new SqlParameter("@Mer_App_ID", SqlDbType.BigInt, 10);
                sqlParameter[8].Value = objApp.MerchantApp_ID;




                DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_AddApplicationStep4", sqlParameter, DB_CONSTANTS.ConnectionString_ICS);


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
                Response.ErrorMessage = ex.Message.ToString();
                BAL.Common.LogManager.LogError("AddAppStep4", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
            }

            return Response;
        }

        public objResponse AddAppStep5(Project.Entity.Applications objApp)
        {
            objResponse Response = new objResponse();
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[7];

                sqlParameter[0] = new SqlParameter("@BName", SqlDbType.NVarChar, 100);
                sqlParameter[0].Value = objApp.BName;

                sqlParameter[1] = new SqlParameter("@BCity", SqlDbType.NVarChar, 100);
                sqlParameter[1].Value = objApp.BCity;

                sqlParameter[2] = new SqlParameter("@BState", SqlDbType.NVarChar, 100);
                sqlParameter[2].Value = objApp.BState;

                sqlParameter[3] = new SqlParameter("@BZip", SqlDbType.NVarChar, 100);
                sqlParameter[3].Value = objApp.BZip;

                sqlParameter[4] = new SqlParameter("@BRoutNumber", SqlDbType.NVarChar, 50);
                sqlParameter[4].Value = objApp.BRoutNumber;

                sqlParameter[5] = new SqlParameter("@BacNumber", SqlDbType.NVarChar, 30);
                sqlParameter[5].Value = objApp.BacNumber;

                sqlParameter[6] = new SqlParameter("@Mer_App_ID", SqlDbType.BigInt, 10);
                sqlParameter[6].Value = objApp.MerchantApp_ID;




                DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_AddApplicationStep5", sqlParameter, DB_CONSTANTS.ConnectionString_ICS);


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
                Response.ErrorMessage = ex.Message.ToString();
                BAL.Common.LogManager.LogError("AddAppStep5", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
            }

            return Response;
        }

        public objResponse AddAppStep6(Project.Entity.Applications objApp)
        {
            objResponse Response = new objResponse();
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[30];

                sqlParameter[0] = new SqlParameter("@DebitQual", SqlDbType.NVarChar, 50);
                sqlParameter[0].Value = objApp.DebitQual;

                sqlParameter[1] = new SqlParameter("@DebitMIDQual", SqlDbType.NVarChar, 50);
                sqlParameter[1].Value = objApp.DebitMIDQual;

                sqlParameter[2] = new SqlParameter("@DebitNonQual", SqlDbType.NVarChar, 50);
                sqlParameter[2].Value = objApp.DebitNonQual;

                sqlParameter[3] = new SqlParameter("@CreditQual", SqlDbType.NVarChar, 50);
                sqlParameter[3].Value = objApp.CreditQual;

                sqlParameter[4] = new SqlParameter("@CreditMIDQual", SqlDbType.NVarChar, 50);
                sqlParameter[4].Value = objApp.CreditMIDQual;

                sqlParameter[5] = new SqlParameter("@CreditNonQual", SqlDbType.NVarChar, 50);
                sqlParameter[5].Value = objApp.CreditNonQual;

                sqlParameter[6] = new SqlParameter("@DebitQualPerItem", SqlDbType.NVarChar, 50);
                sqlParameter[6].Value = objApp.DebitQualPerItem;

                sqlParameter[7] = new SqlParameter("@DebitMIDQualPerItem", SqlDbType.NVarChar, 50);
                sqlParameter[7].Value = objApp.DebitMIDQualPerItem;

                sqlParameter[8] = new SqlParameter("@DebitNonQualPerItem", SqlDbType.NVarChar, 50);
                sqlParameter[8].Value = objApp.DebitNonQualPerItem;

                sqlParameter[9] = new SqlParameter("@CreditQualPerItem", SqlDbType.NVarChar, 50);
                sqlParameter[9].Value = objApp.CreditQualPerItem;

                sqlParameter[10] = new SqlParameter("@CreditMIDQualPerItem", SqlDbType.NVarChar, 50);
                sqlParameter[10].Value = objApp.CreditMIDQualPerItem;

                sqlParameter[11] = new SqlParameter("@CreditNonQualPerItem", SqlDbType.NVarChar, 50);
                sqlParameter[11].Value = objApp.CreditNonQualPerItem;

                sqlParameter[12] = new SqlParameter("@DebitTransFee", SqlDbType.NVarChar, 50);
                sqlParameter[12].Value = objApp.DebitTransFee;

                sqlParameter[13] = new SqlParameter("@ReturnTransFee", SqlDbType.NVarChar, 50);
                sqlParameter[13].Value = objApp.ReturnTransFee;

                sqlParameter[14] = new SqlParameter("@EBTTransFee", SqlDbType.NVarChar, 50);
                sqlParameter[14].Value = objApp.EBTTransFee;

                sqlParameter[15] = new SqlParameter("@ElectroAVSFee", SqlDbType.NVarChar, 50);
                sqlParameter[15].Value = objApp.ElectroAVSFee;

                sqlParameter[16] = new SqlParameter("@AMEXTransFee", SqlDbType.NVarChar, 50);
                sqlParameter[16].Value = objApp.AMEXTransFee;

                sqlParameter[17] = new SqlParameter("@StatementFee", SqlDbType.NVarChar, 50);
                sqlParameter[17].Value = objApp.StatementFee;

                sqlParameter[18] = new SqlParameter("@MontMini", SqlDbType.NVarChar, 50);
                sqlParameter[18].Value = objApp.MontMini;

                sqlParameter[19] = new SqlParameter("@ChargeBackFee", SqlDbType.NVarChar, 50);
                sqlParameter[19].Value = objApp.ChargeBackFee;

                sqlParameter[20] = new SqlParameter("@BatchFee", SqlDbType.NVarChar, 50);
                sqlParameter[20].Value = objApp.BatchFee;

                sqlParameter[21] = new SqlParameter("@GatewayAccessFee", SqlDbType.NVarChar, 50);
                sqlParameter[21].Value = objApp.GatewayAccessFee;

                sqlParameter[22] = new SqlParameter("@GatewayPerAuthFee", SqlDbType.NVarChar, 50);
                sqlParameter[22].Value = objApp.GatewayPerAuthFee;

                sqlParameter[23] = new SqlParameter("@GatewaySetUpFee", SqlDbType.NVarChar, 50);
                sqlParameter[23].Value = objApp.GatewaySetUpFee;

                sqlParameter[24] = new SqlParameter("@WirelessAccessFee", SqlDbType.NVarChar, 50);
                sqlParameter[24].Value = objApp.WirelessAccessFee;

                sqlParameter[25] = new SqlParameter("@WirelessPerAuthFee", SqlDbType.NVarChar, 50);
                sqlParameter[25].Value = objApp.WirelessPerAuthFee;

                sqlParameter[26] = new SqlParameter("@WirelessSetupFee", SqlDbType.NVarChar, 50);
                sqlParameter[26].Value = objApp.WirelessSetupFee;

                sqlParameter[27] = new SqlParameter("@RetrievalFee", SqlDbType.NVarChar, 50);
                sqlParameter[27].Value = objApp.RetrievalFee;

                sqlParameter[28] = new SqlParameter("@Mer_App_ID", SqlDbType.BigInt, 10);
                sqlParameter[28].Value = objApp.MerchantApp_ID;

                sqlParameter[29] = new SqlParameter("@Equipment", SqlDbType.NVarChar, 80);
                sqlParameter[29].Value = objApp.Equipment;

                




                DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_AddApplicationStep6", sqlParameter, DB_CONSTANTS.ConnectionString_ICS);


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
                Response.ErrorMessage = ex.Message.ToString();
                BAL.Common.LogManager.LogError("AddAppStep6", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
            }

            return Response;
        }

        public objResponse LoadAppStep1(string AppId)
        {
            objResponse Response = new objResponse();
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[1];

                sqlParameter[0] = new SqlParameter("@AppId", SqlDbType.BigInt, 10);
                sqlParameter[0].Value = Convert.ToInt64(AppId);


                DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_LoadAppStep1", sqlParameter, DB_CONSTANTS.ConnectionString_ICS);


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
                Response.ErrorMessage = ex.Message.ToString();
                BAL.Common.LogManager.LogError("LoadAppStep1", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
            }

            return Response;
        }

        public objResponse LoadAppStep2(string AppId)
        {
            objResponse Response = new objResponse();
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[1];

                sqlParameter[0] = new SqlParameter("@AppId", SqlDbType.BigInt, 10);
                sqlParameter[0].Value = Convert.ToInt64(AppId);


                DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_LoadAppStep2", sqlParameter, DB_CONSTANTS.ConnectionString_ICS);


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
                Response.ErrorMessage = ex.Message.ToString();
                BAL.Common.LogManager.LogError("LoadAppStep2", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
            }

            return Response;
        }

        public objResponse LoadAppStep3(string AppId)
        {
            objResponse Response = new objResponse();
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[1];

                sqlParameter[0] = new SqlParameter("@AppId", SqlDbType.BigInt, 10);
                sqlParameter[0].Value = Convert.ToInt64(AppId);


                DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_LoadAppStep3", sqlParameter, DB_CONSTANTS.ConnectionString_ICS);


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
                Response.ErrorMessage = ex.Message.ToString();
                BAL.Common.LogManager.LogError("LoadAppStep3", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
            }

            return Response;
        }

        public objResponse LoadAppStep4(string AppId)
        {
            objResponse Response = new objResponse();
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[1];

                sqlParameter[0] = new SqlParameter("@AppId", SqlDbType.BigInt, 10);
                sqlParameter[0].Value = Convert.ToInt64(AppId);


                DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_LoadAppStep4", sqlParameter, DB_CONSTANTS.ConnectionString_ICS);


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
                Response.ErrorMessage = ex.Message.ToString();
                BAL.Common.LogManager.LogError("LoadAppStep4", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
            }

            return Response;
        }

        public objResponse LoadAppStep5(string AppId)
        {
            objResponse Response = new objResponse();
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[1];

                sqlParameter[0] = new SqlParameter("@AppId", SqlDbType.BigInt, 10);
                sqlParameter[0].Value = Convert.ToInt64(AppId);


                DATA_ACCESS_LAYER.Fill(Response.ResponseData, "usp_LoadAppStep5", sqlParameter, DB_CONSTANTS.ConnectionString_ICS);


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
                Response.ErrorMessage = ex.Message.ToString();
                BAL.Common.LogManager.LogError("LoadAppStep5", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
            }

            return Response;
        }

        
    }
}
