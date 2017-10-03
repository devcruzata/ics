using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Web.ApplicationHelper.structure
{
    public class AppRequest
    {
        public List<AppDetail> FieldValues { get; set; }

        public List<Principal> Principals { get; set; }

        public List<DocDetail> Documents { get; set; }

        public Equipment Equipments { get; set; }

        public string SalesrepID { get; set; }

        public string PackageID { get; set; }

        public AppRequest()
        {
            FieldValues = new List<AppDetail>();
            Principals = new List<Principal>();
            Documents = new List<DocDetail>();
            Equipments = new Equipment();
        }

    }

    public class AppDetail
    {
        public string FieldId { get; set; }

        public string Value { get; set; }
    }

    public class DocDetail
    {


    }

    public class Principal
    {
        public List<AppDetail> FieldValues { get; set; }

        public Principal()
        {
            FieldValues = new List<AppDetail>();
        }
    }

    public class Equipment
    {
        public string EquipmentTypeID { get; set; }

        public string EquipmentID { get; set; }

        public string ProcessorID { get; set; }

        public List<AppDetail> FieldValues { get; set; }

        public List<Peripherial> Peripherials { get; set; }

        public Equipment()
        {
            FieldValues = new List<AppDetail>();
            Peripherials = new List<Peripherial>();
        }
    }

    public class Peripherial
    {
        public string PeripherialsID { get; set; }

        public List<AppDetail> FieldValues { get; set; }

        public Peripherial()
        {
            FieldValues = new List<AppDetail>();
        }
    }

}