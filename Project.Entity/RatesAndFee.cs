using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Entity
{
   public class RatesAndFee
    {
        public Int64 Rate_ID_Auto_PK { get; set; }

        public string Rate_Type { get; set; }

        public string Debit_Qual { get; set; }

        public string Debit_MID_Qual { get; set; }

        public string Debit_MID_Qual_Per_Item { get; set; }


        public string Debit_Non_Qual { get; set; }

        public string Credit_Qual { get; set; }

        public string Credit_MID_Qual { get; set; }

        public string Credit_MID_Qual_Per_Item { get; set; }

        public string Credit_Non_Qual { get; set; }

        public string Debit_Transaction_Fee { get; set; }

        public string Return_Transaction_Fee { get; set; }

        public string EBT_Transaction_Fee { get; set; }

        public string Electronic_AVS_Fee { get; set; }

        public string AMEX_Trans_Fee { get; set; }

        public string Statement_Fee { get; set; }

        public string MonthlyMinimum { get; set; }

        public string ChargeBack_Fee { get; set; }

        public string Batch_Fee { get; set; }

        public string ReserveAccount_Fee { get; set; }

        public string FDR_HelpDesk_Fee { get; set; }

        public string FDR_Asst_Service_Fee { get; set; }

        public string ACH_Change_Fee { get; set; }

        public string RetrivalRequest_Fee { get; set; }

        public string Voice_Auth_Fee { get; set; }

        public string Annual_Fee { get; set; }

        public string PCI_NonAction_Fee { get; set; }

        public string Regulatory_Fee { get; set; }

        public string Regulatory_NonComplience_Fee { get; set; }

        public string Early_Termination_Bef1 { get; set; }

        public string Early_Termination_Aft1 { get; set; }

        public string Early_Termination_Bef2 { get; set; }

        public string InterchangeClear_Fee { get; set; }

        public string WirelessSetup_Fee { get; set; }

        public string WirelessMonthly_Fee { get; set; }

        public string WirelessAuth_Fee { get; set; }

        public string GatewaySetup_Fee { get; set; }

        public string GatewayMonthly_Fee { get; set; }

        public string GatewayAuth_Fee { get; set; }
    }
}
