using System.Collections.Generic;
using AppCode.Pages.Pickup2.PermanentPickup;
using nu.gtx.POCO.Contract.Pickup;

namespace AppCode.Pages.Pickup2.EditLocationSetting
{
    public class GuiContainer
    {
        public List<GuiRow> GuiRowList { get; set; }


        public List<PickupOperator> PickupOperatorList { get; set; }


        public List<GuiLocation> GuiLocationList { get; set; }


        public List<GuiCustomer> GuiCustomerList { get; set; }


        public int CurrentCustomerId { get; set; }


        public int CurrentLocationId { get; set; }


        public List<GuiWebsite> ForwarderList { get; set; }
    }
}
