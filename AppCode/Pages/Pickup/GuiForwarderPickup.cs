using System;
using System.Linq;
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


        public List<GuiCustomerPickup> CustomerPickupList { get; set; }


        public string CssGlyphiconStatus
        {
            get { return PickupUtil.PickupStatusToGlyphiconStatus(PickupStatusForwarder); }
        }

        public string CssGlyphiconMove
        {
            get { return PickupUtil.PickupStatusToGlyphiconMove(PickupStatusForwarder); }
        }

        /// <summary>
        /// Shows Expand if all sub items are collapsed.
        /// Shows Collapse in any sub item is expanded.
        /// </summary>
        public string CssGlyphiconExpandGroup
        {
            get { return CustomerPickupList.Any(t => t.IsExpanded) ? "glyphicon glyphicon-triangle-top big" : "glyphicon glyphicon-triangle-bottom big"; }
        }

    }
}