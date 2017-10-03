using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BAL.Reports;
using Project.Entity;
using Project.Web.Models;

namespace Project.Web.Controllers.Reports
{
    public class ReportsController : Controller
    {
        ReportsManager objRepManager = new ReportsManager();
        //
        // GET: /Reports/

        [Authorize]
        public ActionResult ReportHome()
        {
            ReportModel objModel = new ReportModel();
            return View(objModel);
        }


        [Authorize]
        [HttpPost]
        public ActionResult ReportHome(ReportModel objModel)
        {
            ReportItem objReportItem = new ReportItem();
            objResponse response = new objResponse();
            try
            {
                objModel.eDate = BAL.Helper.Helper.ConvertToDateNullable(objModel.eDateString, "MM/dd/yy");

                //model.sDate = TimeZoneInfo.ConvertTime(BAL.Helper.Helper.ConvertToDateNullable(model.sDateString, "dd/MM/yyyy"), timeZoneInfo);
                objModel.sDate = BAL.Helper.Helper.ConvertToDateNullable(objModel.sDateString, "MM/dd/yy");


                response = objRepManager.MyReports(objModel.sDate, objModel.eDate, objModel.reportType);
                if (response != null && response.ErrorCode == 0 && response.ResponseData != null && response.ResponseData.Tables.Count > 0 && response.ResponseData.Tables[0].Rows.Count > 0)
                {
                    objModel.Report = new List<ReportItem>();
                    int Counter = 0;
                    foreach (DataRow item in response.ResponseData.Tables[0].Rows)
                    {
                        if (objModel.reportType == "Lead Per Sales Rep.")
                        {
                            objModel.Report.Add(new ReportItem()
                            {
                                SeriolNo = (++Counter).ToString(),
                                noOfLeads = item[1].ToString(),
                                salesRepName = item[0].ToString()                             

                            });
                        }
                        if(objModel.reportType == "Closed Lead Per Sales Rep.")
                        {
                            objModel.Report.Add(new ReportItem()
                            {
                                SeriolNo = (++Counter).ToString(),
                                noOfLeads = item[1].ToString(),
                                salesRepName = item[0].ToString() 
                            });
                        }

                        if (objModel.reportType == "Lead Per Source")
                        {
                            objModel.Report.Add(new ReportItem()
                            {
                                SeriolNo = (++Counter).ToString(),
                                noOfLeads = item[1].ToString(),
                                sourceName = item[0].ToString()
                            });
                        }

                        if (objModel.reportType == "Closed Lead Per Source")
                        {
                            objModel.Report.Add(new ReportItem()
                            {
                                SeriolNo = (++Counter).ToString(),
                                noOfLeads = item[1].ToString(),
                                sourceName = item[0].ToString()
                            });
                        }
                        
                    }


                    objModel.hasReport = true;
                    objModel.errorMessage = string.Empty;
                }
                else
                {
                    objModel.hasReport = false;
                    objModel.errorMessage = "Request report did not found.";
                }
            }
            catch (Exception ex)
            {
                BAL.Common.LogManager.LogError("ReportHome Post Method", 1, Convert.ToString(ex.Source), Convert.ToString(ex.Message), Convert.ToString(ex.StackTrace));
            }
            return View(objModel);
        }

    }
}
