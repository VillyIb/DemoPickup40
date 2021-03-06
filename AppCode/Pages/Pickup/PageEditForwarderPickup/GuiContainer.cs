﻿using System.Collections.Generic;
using System.Linq;

namespace Pages.Pickup.PageEditForwarderPickup
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

        /// <summary>
        /// Settings Box in top of page flipped from GUI.
        /// </summary>
        public bool IsSettingsVisible { get; set; }


        public string CssGlyphiconExpandSetting => IsSettingsVisible
            ? "glyphicon glyphicon-triangle-top big"
            : "glyphicon glyphicon-triangle-bottom big";



        public bool IsAnyCustomerExpanded
        {
            get { return ForwarderPickupList.Any(t1 => t1.CustomerPickupList.Any(t2 => t2.IsExpanded)); }
        }
    }
}
