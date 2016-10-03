using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

using AppCode.Pages.Pickup.EditLocationSettings;

using nu.gtx.CodeFirst.DataAccess.Context;
using nu.gtx.Common1.Extensions;
using nu.gtx.DbMain.Standard.PM;
using nu.gtx.DbShared.Standard.PM;
using nu.gtx.POCO.Contract.Pickup.Constants;

namespace DemoPickup40.Pages.Pickup
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
                        GuiPickupOperatorList = new List<PickupOperator>(),
                        GuiForwarderList = new List<GuiWebsite>(),                       
                    };
                    Session[ZGuiContainerKey] = result;
                }

                return result;
            }
        }


        /// <summary>
        /// Setup Controller with GuiContainer
        /// </summary>
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

            GuiContainer.CurrentLocationSettingsId = -1;

            foreach (string key in Request.QueryString.Keys)
            {
                if ("CustomerId".Equals(key))
                {
                    int t1;
                    GuiContainer.CurrentCustomerId = int.TryParse(Request.QueryString[key], out t1) ? t1 : -1;
                }
            }

            Controller.LoadGuiCustomerList();
            if (GuiContainer.CurrentCustomerId >= 0)
            {
                Controller.LoadLocationList(GuiContainer.CurrentCustomerId);
            }

            Controller.GuiLoadPickupOperatorList();

            Controller.GuiLoadForwarderList();

            return true;
        }

        private void XmPopulateLeftColumn()
        {
            XuLocationList.DataSource = GuiContainer.GuiLocationList;
            XuLocationList.DataBind();
            XuLocationList.SelectedIndex = GuiContainer.GuiLocationList.FindIndex(t => t.Id == GuiContainer.CurrentLocationSettingsId);

            XuCustomerList.DataSource = GuiContainer.GuiCustomerList;
            XuCustomerList.DataBind();
            XuCustomerList.SelectedIndex = GuiContainer.GuiCustomerList.FindIndex(t => t.Value == GuiContainer.CurrentCustomerId);
        }

        private void XmPopulateLocation()
        {
            if (GuiContainer.CurrentLocationSettingsId >= 0)
            {
                XuLocationDetails.CssRemove("hidden");
                Controller.LoadLocation(GuiContainer.CurrentLocationSettingsId);
                if (Controller.CurrentLocationSettings != null)
                {
                    var location = Controller.CurrentLocationSettings;

                    XuForwarderList.DataSource = GuiContainer.GuiForwarderList;
                    XuForwarderList.DataBind();
                    XuForwarderList.SelectedIndex = GuiContainer.GuiForwarderList.FindIndex(t => t.Value.Equals(location.ForwarderWebsiteId));

                    XuHasLoadingGear.Checked = location.HasLoadingGear;
                    XuLoadingGearDetails.Text = location.LoadingGearDetails;
                    XuCustomerFeedback.Checked = location.IsFeedbackVisible;
                    XuNote.Text = location.Text;

                    XuPickupOperatorList.DataSource = GuiContainer.GuiPickupOperatorList;
                    XuPickupOperatorList.DataBind();
                    XuPickupOperatorList.SelectedIndex = GuiContainer.GuiPickupOperatorList.FindIndex(t => t == location.PickupOperator);


                }
            }
            else
            {
                XuLocationDetails.CssAdd("hidden");
            }
        }


        private void XmPopulatePermanentCollection()
        {
            if (GuiContainer.CurrentLocationSettingsId >= 0)
            {
                XuPermanentCollectionOuterTop.CssRemove("hidden");
                if (Controller.CurrentLocationSettings.PermanentCollectionList.Count > 0)
                {
                    XuPermanentCollectionOuterBottom.CssAdd("hidden");
                }
                else
                {
                    XuPermanentCollectionOuterBottom.CssRemove("hidden");
                }
            }
            else
            {
                XuPermanentCollectionOuterTop.CssAdd("hidden");
                XuPermanentCollectionOuterBottom.CssAdd("hidden");
            }


            if (GuiContainer.CurrentLocationSettingsId >= 0 && Controller.CurrentLocationSettings.PermanentCollectionList.Count > 0)
            {
                XuPermanentCollection.CssRemove("hidden");

                var pc = Controller.CurrentLocationSettings.PermanentCollectionList[0];

                XuPermCollEnabled.Checked = pc.HasPermanentPickup;
                XuScheduleFruiday.Checked = pc.HasFriday;
                XuScheduleMonday.Checked = pc.HasMonday;
                XuScheduleSaturday.Checked = pc.HasSaturday;
                XuScheduleSunday.Checked = pc.HasSunday;
                XuScheduleThursday.Checked = pc.HasThursday;
                XuScheduleTuesday.Checked = pc.HasTuesday;
                XuScheduleWedensday.Checked = pc.HasWedensday;
            }
            else
            {
                XuPermanentCollection.CssAdd("hidden");
            }

        }


        private void XmPopulatePage()
        {
            XmPopulateLeftColumn();
            XmPopulateLocation();
            XmPopulatePermanentCollection();
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
            if(gw==null) throw new ArgumentNullException("sender");

            var index = gw.SelectedIndex;

            GuiContainer.CurrentLocationSettingsId = GuiContainer.GuiLocationList[index].Id;

            XmPopulatePage();
        }


        protected void XuCustomerList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int t1;
            GuiContainer.CurrentCustomerId = int.TryParse(((DropDownList)sender).SelectedValue, out t1) ? t1 : 0;

            Controller.LoadLocationList(GuiContainer.CurrentCustomerId);

            XmPopulatePage();
        }


        protected void XuLocationList_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }


        private void XmReadGuiLocation()
        {
            Controller.LoadLocation(GuiContainer.CurrentLocationSettingsId);
            var location = Controller.CurrentLocationSettings;

            location.HasLoadingGear = XuHasLoadingGear.Checked;
            location.IsFeedbackVisible = XuCustomerFeedback.Checked;
            location.LoadingGearDetails = XuLoadingGearDetails.Text.Trim();
            location.PickupOperator = GuiContainer.GuiPickupOperatorList[XuPickupOperatorList.SelectedIndex];
            location.Text = XuNote.Text;
            location.ForwarderWebsiteId = GuiContainer.GuiForwarderList[XuForwarderList.SelectedIndex].Value;
        }


        protected void XuSaveLocation_Click(object sender, EventArgs e)
        {
            XmReadGuiLocation();
            Controller.SaveChages();
            XmPopulatePage();
        }


        protected void XuPermCollDelete_Click(object sender, EventArgs e)
        {
            Controller.LoadLocation(GuiContainer.CurrentLocationSettingsId);
            Controller.PermCollDelete();
            XmPopulatePage();
        }


        protected void XuPermCollUpdate_Click(object sender, EventArgs e)
        {
            Controller.LoadLocation(GuiContainer.CurrentLocationSettingsId);

            if (Controller.CurrentLocationSettings.PermanentCollectionList.Count > 0)
            {
                var pc = Controller.CurrentLocationSettings.PermanentCollectionList[0];

                pc.HasFriday = XuScheduleFruiday.Checked;
                pc.HasMonday = XuScheduleMonday.Checked;
                pc.HasPermanentPickup = XuPermCollEnabled.Checked;
                pc.HasSaturday = XuScheduleSaturday.Checked;
                pc.HasSunday = XuScheduleSunday.Checked;
                pc.HasThursday = XuScheduleThursday.Checked;
                pc.HasTuesday = XuScheduleTuesday.Checked;
                pc.HasWedensday = XuScheduleWedensday.Checked;

                // Note from Location Setting section.
                PickupOperator t1;
                pc.PickupOperator = Enum.TryParse(XuPickupOperatorList.SelectedValue, out t1)
                    ? t1
                    : PickupOperator.Undefined;
            }

            Controller.PermCollUpdate();
            XmPopulatePage();

        }


        protected void XuPermCollCreate_Click(object sender, EventArgs e)
        {
            Controller.LoadLocation(GuiContainer.CurrentLocationSettingsId);
            Controller.PermCollCreate();
            XmPopulatePage();
        }
    }
}