using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Project.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

           routes.MapRoute(
           name: "MyAccount",
           url: "ics/MyAccount",
           defaults: new { controller = "Home", action = "AdminHome" }
           );

            routes.MapRoute(
            name: "AdminDashboard",
            url: "Home/Admin",
            defaults: new { controller = "Home", action = "AdminDashboard" }
            );

            routes.MapRoute(
            name: "CrmUserHome",
            url: "CRM/User",
            defaults: new { controller = "Home", action = "CRM_User_Dashboard" }
            );

            routes.MapRoute(
            name: "EmailTemplates",
            url: "Templates/Email-Template",
            defaults: new { controller = "EmailTemplate", action = "TemplateHome" }
            );

            routes.MapRoute(
            name: "Campaigns",
            url: "Marketing/Campaign",
            defaults: new { controller = "MarketingCampaign", action = "CampaignHome" }
            );

            routes.MapRoute(
            name: "RoundRobin",
            url: "setings/round-robin",
            defaults: new { controller = "RoundRobin", action = "Index" }
            );

            routes.MapRoute(
            name: "AdminSeting",
            url: "Setings/Admin-Setings",
            defaults: new { controller = "AdminSeting", action = "SetingHome" }
            );

            routes.MapRoute(
            name: "GroupSeting",
            url: "Setings/Group-Setings",
            defaults: new { controller = "AdminSeting", action = "GroupsHome" }
            );

            routes.MapRoute(
            name: "LeadSourceSeting",
            url: "Setings/Lead-Source-Setings",
            defaults: new { controller = "AdminSeting", action = "LeadSourceHome" }
            );

            routes.MapRoute(
            name: "MailSeting",
            url: "Setings/Mail-Setings",
            defaults: new { controller = "AdminSeting", action = "MailSetingHome" }
            );

            routes.MapRoute(
            name: "LeadStatusSeting",
            url: "Setings/Lead-Status-Setings",
            defaults: new { controller = "AdminSeting", action = "LeadStatusHome" }
            );


            routes.MapRoute(
            name: "Manageuser",
            url: "UserManagement/users",
            defaults: new { controller = "UserManagement", action = "UserHome" }
            );

            routes.MapRoute(
            name: "ImportUser",
            url: "UserManagement/ImportUsers",
            defaults: new { controller = "UserManagement", action = "UploadUserExcel" }
            );

            routes.MapRoute(
            name: "ManageLead",
            url: "LeadManagement/leads",
            defaults: new { controller = "Leads", action = "LeadHome" }
            );

            routes.MapRoute(
            name: "AddLead",
            url: "LeadManagement/add",
            defaults: new { controller = "Leads", action = "ManageLead" }
            );

            routes.MapRoute(
            name: "ImportLead",
            url: "Leads/ImportLeads",
            defaults: new { controller = "Leads", action = "UploadLeadExcel" }
            );

            routes.MapRoute(
            name: "MerchantApplication",
            url: "Applications/ManageApplications",
            defaults: new { controller = "Application", action = "ApplicationHome" }
            );

            //routes.MapRoute(
            //name: "Calender",
            //url: "Leads/ManageCalender",
            //defaults: new { controller = "Calender", action = "ManageCalender" }
            //);

            routes.MapRoute(
            name: "Calender",
            url: "Leads/ManageCalender",
            defaults: new { controller = "LeadEvents", action = "Index" }
            );

            routes.MapRoute(
            name: "Reports",
            url: "Reports/home",
            defaults: new { controller = "Reports", action = "ReportHome" }
            );

            routes.MapRoute(
            name: "Docs",
            url: "Docs/list",
            defaults: new { controller = "Doc", action = "Index" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Authentication", action = "Login", id = UrlParameter.Optional }


            );
        }
    }
}