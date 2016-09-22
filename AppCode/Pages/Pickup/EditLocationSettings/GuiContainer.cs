using System.Collections.Generic;

using nu.gtx.POCO.Contract.Pickup;

namespace AppCode.Pages.Pickup.EditLocationSettings
{
    /// <summary>
    /// The GuiContainer is saved in the Session.
    /// </summary>
    public class GuiContainer
    {
        public List<PickupOperator> GuiPickupOperatorList { get; set; }


        public List<GuiLocation> GuiLocationList { get; set; }


        public List<GuiCustomer> GuiCustomerList { get; set; }


        public int CurrentCustomerId { get; set; }


        public int CurrentLocationSettingsId { get; set; }


        public List<GuiWebsite> GuiForwarderList { get; set; }
    }
}
