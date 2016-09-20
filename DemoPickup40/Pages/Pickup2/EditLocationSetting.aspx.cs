using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppCode.Pages.Pickup2.EditLocationSetting;
using nu.gtx.DatabaseAccess.DbMain;
using nu.gtx.CodeFirst.DataAccess.Context;
using nu.gtx.DatabaseAccess.DbShared;

namespace DemoPickup40.Pages.Pickup2
{
    public partial class EditLocationSetting : System.Web.UI.Page
    {
        private const string ZGuiContainerKey = "GuiContainerKey";

        private Controller Controller { get; set; }



        private GuiContainer GuiContainer
        {
            get
            {
                var result = Session[ZGuiContainerKey] as GuiContainer;
                if (result == null)
                {
                    result = new GuiContainer
                    {
                        GuiCustomerList = new List<GuiCustomer>(),
                        GuiLocationList = new List<GuiLocation>(),
                        PickupOperatorList = new List<nu.gtx.POCO.Contract.Pickup.PickupOperator>(),
                        GuiRowList = new List<AppCode.Pages.Pickup2.PermanentPickup.GuiRow>(),
                        ForwarderList = new List<GuiWebsite>(),                       
                    };
                    Session[ZGuiContainerKey] = result;
                }

                return result;
            }
        }

        private void XmInit()
        {
            var dbMainStandard = new DbMainStandard();
            var dbSharedStandard = new DbSharedStandard();

            var connectionStringMain = ConfigurationManager.ConnectionStrings["EF_CodeFirst_Test"];
            var connectionBuilderMain = new SqlConnectionStringBuilder(connectionStringMain.ConnectionString);
            var contextMainPickup = new ContextMainPickup(connectionBuilderMain);

            Controller = new Controller(
                dbMainStandard
                , contextMainPickup
                , dbSharedStandard
                )
            {
                GuiContainer = GuiContainer
            };
        }


        private bool XmNotPostback()
        {
            if (IsPostBack) { return false; }

            GuiContainer.CurrentLocationId = -1;

            foreach (string key in Request.QueryString.Keys)
            {
                if ("CustomerId".Equals(key))
                {
                    int t1;
                    GuiContainer.CurrentCustomerId = int.TryParse(Request.QueryString[key], out t1) ? t1 : -1;
                }
            }

            Controller.LoadCustomerList();
            if (GuiContainer.CurrentCustomerId >= 0)
            {
                Controller.LoadLocationList(GuiContainer.CurrentCustomerId);
            }

            Controller.LoadPickupOperatorList();

            Controller.LoadForwarderList();

            return true;
        }


        private void XmPopulatePage()
        {
            XuLocationList.DataSource = GuiContainer.GuiLocationList;
            XuLocationList.DataBind();
            XuLocationList.SelectedIndex = GuiContainer.GuiLocationList.FindIndex(t => t.Id == GuiContainer.CurrentLocationId);

            XuCustomerList.DataSource = GuiContainer.GuiCustomerList;
            XuCustomerList.DataBind();
            XuCustomerList.SelectedIndex = GuiContainer.GuiCustomerList.FindIndex(t => t.Value == GuiContainer.CurrentCustomerId);

            if (GuiContainer.CurrentLocationId >= 0)
            {
                XuLocationDetails.Attributes["class"] = XuLocationDetails.Attributes["class"].Replace("hidden", "");
                Controller.LoadLocation(GuiContainer.CurrentLocationId);
                if (Controller.LocationSetting != null)
                {
                    var location = Controller.LocationSetting;

                    XuForwarderList.DataSource = GuiContainer.ForwarderList;
                    XuForwarderList.DataBind();
                    XuForwarderList.SelectedIndex = GuiContainer.ForwarderList.FindIndex(t => t.Value.Equals(location.ForwarderWebsiteId));

                    XuHasLoadingGear.Checked = location.HasLoadingGear;
                    XuLoadingGearDetails.Text = location.LoadingGearDetails;
                    XuCustomerFeedback.Checked = location.IsFeedbackVisible;
                    XuNote.Text = location.Text;

                    XuPickupOperatorList.DataSource = GuiContainer.PickupOperatorList;
                    XuPickupOperatorList.DataBind();
                    XuPickupOperatorList.SelectedIndex = GuiContainer.PickupOperatorList.FindIndex(t => t == location.PickupOperator);


                }
            }
            else
            {
                XuLocationDetails.Attributes["class"] = XuLocationDetails.Attributes["class"].Replace("hidden", "") + " hidden";
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            XmInit();

            if (XmNotPostback())
            {
                XmPopulatePage();
            }
        }

        protected void XuLocationList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var gw = sender as GridView;

            var index = gw.SelectedIndex;

            GuiContainer.CurrentLocationId = GuiContainer.GuiLocationList[index].Id;

            XmPopulatePage();
        }

        protected void XuCustomerList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int t1;
            GuiContainer.CurrentCustomerId = int.TryParse(((DropDownList)sender).SelectedValue, out t1) ? t1 : 0;

            Controller.LoadLocationList(GuiContainer.CurrentCustomerId);
            GuiContainer.CurrentLocationId = GuiContainer.GuiLocationList.Count > 0
                ? GuiContainer.GuiLocationList[0].Id
                : -1;
            XmPopulatePage();
        }

        protected void XuLocationList_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        private void ReadGuiLocation()
        {
            Controller.LoadLocation(GuiContainer.CurrentLocationId);
            var location = Controller.LocationSetting;

            //location.ForwarderWebsiteId = 
            location.HasLoadingGear = XuHasLoadingGear.Checked;
            location.IsFeedbackVisible = XuCustomerFeedback.Checked;
            location.LoadingGearDetails = XuLoadingGearDetails.Text.Trim();
            location.PickupOperator = GuiContainer.PickupOperatorList[XuPickupOperatorList.SelectedIndex];
            location.Text = XuNote.Text;
            location.ForwarderWebsiteId = GuiContainer.ForwarderList[XuForwarderList.SelectedIndex].Value;
        }


        protected void XuSaveLocation_Click(object sender, EventArgs e)
        {
            ReadGuiLocation();
            Controller.SaveChages();
            XmPopulatePage();
        }
    }
}