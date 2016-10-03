using System.Collections.Generic;
using System.Linq;

namespace AppCode.Pages.Pickup.PageForwarder
{
    /// <summary>
    /// Data Container for WebForm.
    /// </summary>
    public class GuiContainer
    {
        public List<GuiForwarderPickup> ForwarderPickupList { get; set; }


        public List<GuiCustomer> CustomerList { get; set; }


        /// <summary>
        /// Returns CSS type defining icon for Expand/Collaps depending 
        /// </summary>
        public string CssGlyphiconExpandCustomerHeader
        {
            get
            {
                var anyExpanded = ForwarderPickupList.Any(t => t.IsExpandedCustomer);

                return anyExpanded
                    ? "glyphicon glyphicon-triangle-top big"
                    : "glyphicon glyphicon-triangle-bottom big";
            }
        }


        // -- Settings

        public bool IsSettingsVisible { get; set; }


        /// <summary>
        /// Returns CSS type defining icon for Expand/Collapse depending on "IsSettingsVisible".
        /// </summary>
        public string CssGlyphiconExpandSetting => IsSettingsVisible
            ? "glyphicon glyphicon-triangle-top big"
            : "glyphicon glyphicon-triangle-bottom big";


        /// <summary>
        /// Returns true if any child CustomerPickup is expanded (shipments folded out).
        /// </summary>
        public bool IsAnyCustomerExpanded
        {
            get { return ForwarderPickupList.Any(t1 => t1.CustomerPickupList.Any(t2 => t2.IsExpanded)); }
        }
    }
}
