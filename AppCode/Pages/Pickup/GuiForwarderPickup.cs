using System;
using System.Collections.Generic;

namespace AppCode.Pages.Pickup
{
    public class GuiForwarderPickup
    {
        public int Id { get; set; }


        public GuiAddress Address { get; set; }


        public string CarrierNameList { get; set; }


        public string Note { get; set; }


        public DateTime PickupDate { get; set; }


        public TimeSpan ReadyOpen { get; set; }


        public TimeSpan ReadyClose { get; set; }

        /// <summary>
        /// Plain text representation of enum:PickupStatus
        /// Used to control GUI elements.
        /// </summary>
        public PickupStatusForwarder PickupStatusForwarder { get; set; }


        public List <GuiCustomerPickup> CustomerPickupList { get; set; }


        public string CssGlyphiconStatus
        {
            get { return PickupUtil.PickupStatusToGlyphiconStatus(PickupStatusForwarder); }
        }

        public string CssGlyphiconMove
        {
            get { return PickupUtil.PickupStatusToGlyphiconMove(PickupStatusForwarder); }
        }

    }
}