using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.Entity
{
   public class UserRoles
    {
        public long Role_ID { get; set; }

        public string RoleName { get; set; }

        public string AssociatedLeads { get; set; }

        public string SystemwideLeads { get; set; }

        public string Calendar { get; set; }

        public string EditTask { get; set; }

        public string LeadNotes { get; set; }

        public string Documents { get; set; }

        public string ManageDocuments { get; set; }

        public string UserManagement { get; set; }

        public string SiteManagement { get; set; }

        public string HelpDeskTickets { get; set; }

        public string LeadDistribution { get; set; }

        public string ResidualsAccess { get; set; }

        public string ResidualManagerView { get; set; }

        public string MerchantinfoAssociatedLead { get; set; }

        public string MerchantinfoSystemwideLeads { get; set; }

        public string ManageMerchant { get; set; }

        public string Statements { get; set; }

        public string TriggerStatementDownload { get; set; }

        public string PortfolioActivityAssociatedleads { get; set; }

        public string PortfolioActivitySystemwideleads { get; set; }

        public string ProposalGenerator { get; set; }

        public string MerchantApplication { get; set; }

        public string UnderwritngApplicationSubmissions { get; set; }

        public string Agenttracker { get; set; }

        public string MessagingSystem { get; set; }
    }
}
