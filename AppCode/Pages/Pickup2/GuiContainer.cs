using System.Collections.Generic;
using System.Linq;

namespace AppCode.Pages.Pickup2
{
    /// <summary>
    /// Data Container for WebForm.
    /// </summary>
    public class GuiContainer
    {
        public List<GuiForwarderPickup> ForwarderPickupList { get; set; }


        public List<GuiCustomer> CustomerList { get; set; }

        /// <summary>
        /// Icon, Expand/Collaps all 1 level down.
        /// </summary>
        public string CssGlyphiconExpandCustomerHeader
        {
            get
            {
                var anyExpanded =
                    ForwarderPickupList.Any(t => t.IsExpandedCustomer);

                return anyExpanded
                    ? "glyphicon glyphicon-triangle-top big"
                    : "glyphicon glyphicon-triangle-bottom big";
            }
        }

        // -- Settings

        public bool IsSettingsVisible { get; set; }

        public string CssGlyphiconExpandSetting
        {
            get
            {
                return IsSettingsVisible
                    ? "glyphicon glyphicon-triangle-top big"
                    : "glyphicon glyphicon-triangle-bottom big";
            }
        }

        public bool IsAnyCustomerExpanded
        {
            get { return ForwarderPickupList.Any(t1 => t1.CustomerPickupList.Any(t2 => t2.IsExpanded)); }
        }
    }
}
