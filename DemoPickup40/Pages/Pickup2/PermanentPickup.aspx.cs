using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using AppCode.Pages.Pickup2.PermanentPickup;
using nu.gtx.Common1.Utils;
// ReSharper disable UseNullPropagation
// ReSharper disable ArgumentsStyleLiteral

using nu.gtx.POCO.Contract.Pickup;


namespace DemoPickup40.Pages.Pickup2
{
    public partial class PermanentPickup : Page
    {
        private const string XpGuiContainerKey = "GuiContainer";

        private GuiContainer XpGuiContainer
        {
            get { return Session[XpGuiContainerKey] as GuiContainer; }
            set { Session[XpGuiContainerKey] = value; }
        }


        private Controller zController;

        private Controller Controller
        {
            get { return zController ?? (zController = new Controller()); }
        }



        private void XmPopulatePage()
        {
            XuPermanentPickup.DataSource = XpGuiContainer.GuiRowList;
            XuPermanentPickup.DataBind();

            //XuPickupOperatorItem.

            //XuPickupOperatorItem.DataSource = XpGuiContainer.PickupOperatorList;

        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(IsPostBack))
            {
                if (XpGuiContainer == null)
                {
                    Controller.LoadData();
                    XpGuiContainer = Controller.GuiContainer;
                }

                XmPopulatePage();
            }
        }

        protected void XuPermanentPickup_SelectedIndexChanged(object sender, EventArgs e)
        {
            var t1 = sender;
            var t2 = e;
        }

        protected void XuPermanentPickup_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var t1 = sender;
            var t2 = e;

        }

        protected void XuPermanentPickup_RowEditing(object sender, GridViewEditEventArgs e)
        {
            var t1 = sender;
            var t2 = e;
        }
    }
}