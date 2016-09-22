using System.Collections.Generic;
using System.Linq;

namespace AppCode.Pages.zPickup
{
    /// <summary>
    /// Data Container for WebForm.
    /// </summary>
    public class GuiContainer
    {
        public List<GuiForwarderPickup> ForwarderPickupList { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public string CssGlyphiconExpandCustomerHeader
        {
            get
            {
                var anyExpanded = ForwarderPickupList.Any(t => t.IsExpandedCustomer);
                return anyExpanded ? "glyphicon glyphicon-triangle-top big" : "glyphicon glyphicon-triangle-bottom big";
            }
        }
    }
}
