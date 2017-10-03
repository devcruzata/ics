namespace Project.Web.Common
{
    public class UserSession
    {
        public long UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }       
        public string UserType { get; set; }
        public string Subscription_ID { get; set; }
       // public string Group_ID { get; set; }
        

    }

    public class UserTransactionSession
    {
        public long CustomerID { get; set; }
        public System.Int32 ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string SubscriptionTYpe { get; set; }
        public string TransactionError { get; set; }

    }

    public class UserPermissionsSession
    {
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