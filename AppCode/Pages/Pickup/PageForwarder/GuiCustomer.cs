using System;

using nu.gtx.DbMain.Standard.PM;

namespace Pages.Pickup.PageForwarder
{
    public class GuiCustomer
    {
        public int Id { get; set; }


        public string Name { get; set; }



        public GuiCustomer()
        { }
        

        public GuiCustomer(aspnet_CompanyDB source)
        {
            Id = source.CompanyID;
            Name = String.Format("{0} ({1})", source.Company_Name, source.CompanyID);
        }


        //public override string ToString()
        //{
        //    return Name;
        //}
    }
}
