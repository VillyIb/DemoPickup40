using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using AppCode.Pages.Pickup2;
using nu.gtx.Common1.Utils;
// ReSharper disable UseNullPropagation
// ReSharper disable ArgumentsStyleLiteral

using nu.gtx.POCO.Contract.Pickup;


namespace DemoPickup40.Pages.Pickup2
{
    public partial class Forwarder : Page
    {
        private const string XpGuiContainerKey = "GuiContainer";

        private GuiContainer XpGuiContainer
        {
            get { return Session[XpGuiContainerKey] as GuiContainer; }
            set { Session[XpGuiContainerKey] = value; }
        }


        private List<GuiForwarderPickup> XpPrimaryRowList
        {
            get { return XpGuiContainer.ForwarderPickupList; }
        }


        private List<GuiCustomerPickup> XpAllCustomerPickupList
        {
            get
            {
                var result = new List<GuiCustomerPickup>();

                foreach (var forwarder in XpPrimaryRowList)
                {
                    result.AddRange(forwarder.CustomerPickupList);
                }

                return result;
            }
        }

        private PickupData zXpBackendApi;
        private PickupData XpBackendApi { get { return zXpBackendApi ?? (zXpBackendApi = new PickupData()); } }


        private IForwarderPickup XpSelectedForwarderPickup { get; set; }


        private bool XmLoadSelectedForwarderPickup(int forwarderPickupId)
        {
            IForwarderPickup t1;
            if (XpBackendApi.Read(out t1, forwarderPickupId))
            {
                XpSelectedForwarderPickup = t1;
                return true;
            }

            XpSelectedForwarderPickup = null;
            return false;
        }


        private ICustomerPickup XpSelectedCustomerPickup { get; set; }

        private bool XmLoadSelectedCustomerPickup(int customerPickupId)
        {
            ICustomerPickup t1;
            if (XpBackendApi.Read(out t1, customerPickupId))
            {
                XpSelectedCustomerPickup = t1;
                return true;
            }

            XpSelectedCustomerPickup = null;
            return false;
        }

        private void XmPopulatePage()
        {
            XuTimeOfDay.Text = SystemDateTime.Now.ToString("yyyy-MM-dd HH:mm");

            // save settings in container

            // calculate aggreagte values sav as container settings

            // set display settings accordingly to container settings

            XuForwarderPickup.DataSource = XpPrimaryRowList;
            XuForwarderPickup.DataBind();

            // -- Icon Expand/Collaps Settings
            XuSettingsIcon.Attributes["class"] = XpGuiContainer.CssGlyphiconExpandSetting;

            XuSettingsRow.Attributes["class"] = XpGuiContainer.IsSettingsVisible
                ? XuSettingsRow.Attributes["class"].Replace(" hidden", "")
                : XuSettingsRow.Attributes["class"] + " hidden";


            // -- Separate Icon Expand/Collapse All 
            XuExpandAllIcon.Attributes["class"] = XpGuiContainer.CssGlyphiconExpandCustomerHeader;
        }


        private PickupData zPickupdata;

        private PickupData PickupApi
        {
            get { return zPickupdata ?? (zPickupdata = new PickupData()); }
        }


        private bool GuiRead(out DateTime value, ITextControl textBox)
        {
            return DateTime.TryParseExact(
                textBox.Text,
                "yyyy-MM-dd",
                CultureInfo.CurrentCulture,
                DateTimeStyles.AllowWhiteSpaces | DateTimeStyles.AssumeLocal, out value
            );
        }

        private List<PickupStatusForwarder> GuiReadFilterPickupStatus()
        {
            var result = new List<PickupStatusForwarder>();

            foreach (ListItem t1 in XuStFilterPickupStatus.Items)
            {
                if (t1.Selected)
                {
                    PickupStatusForwarder t5;
                    if (Enum.TryParse(t1.Value, out t5))
                    {
                        result.Add(t5);
                    }
                }
            }

            return result;
        }


        /// <summary>
        /// Reads filter for number of shipments.
        /// null: filter not active (nothing selecte/both selected)
        /// 0: show only Pickups with 0 shipments
        /// 1: shop pickups with one or more shipments.
        /// </summary>
        /// <returns></returns>
        private int? GuiReadNumberOfShipments()
        {
            if (XuStNumberOfShipments0.Checked)
            {
                if (XuStNumberOfShipments1.Checked)
                {
                    return null;
                }
                return 0;
            }
            if (XuStNumberOfShipments1.Checked)
            {
                return 1;
            }

            return null;
        }


        private bool GuiRead(out int value, ITextControl textBox)
        {
            return int.TryParse(textBox.Text, out value);
        }


        private bool GuiRead(out bool value, ICheckBoxControl checkBox)
        {
            value = checkBox.Checked;
            return true;
        }

        private bool GuiRead(out Guid value, ITextControl textBox)
        {
            return Guid.TryParse(textBox.Text, out value);
        }


        private GuiSettings GuiSettings { get; set; }


        /// <summary>
        /// Load Settings from Gui.
        /// </summary>
        private void GuiReadSettings()
        {
            // Pickup Window
            DateTime t1;
            GuiSettings.FilterPickupDateFrom= GuiRead(out t1, XuStPickupDateFrom) ? t1 : (DateTime?)null;
            GuiSettings.FilterPickupDateUntil = GuiRead(out t1, XuStPickupDateUntil) ? t1 : (DateTime?)null;

            // ForwarderPickup.PickupStatus
            GuiSettings.FilterPickupStatusForwarder = GuiReadFilterPickupStatus();

            // Look ahead limit
            int t2;
            GuiSettings.FilterLookAheadMinutes = GuiRead(out t2, XuStLookForward) ? t2 : 3600;
            bool t3;
            GuiSettings.FilterLookAheadEabled = GuiRead(out t3, XuStLookForwardEnabled) && t3;

            // Number of shipments
            GuiSettings.FilterForShipmentCountZero = GuiRead(out t3, XuStNumberOfShipments0) && t3;
            GuiSettings.FilterForShipmentCountNonZero = GuiRead(out t3, XuStNumberOfShipments1) && t3;

            // Specific Customer
            GuiSettings.FilterSingleCustomer = XuStSpecificCustomer.Text;

            // Specific Website
            Guid t4;
            GuiSettings.FilterSingleWebsiteId = GuiRead(out t4, XuStWebsite) ? t4 : (Guid?)null;

        }


        private bool XmGetQueryParameter(out int value, string key)
        {
            var qs = HttpContext.Current.Request.QueryString;

            foreach (string current in qs.Keys)
            {
                if (key.Equals(current, StringComparison.OrdinalIgnoreCase))
                {
                    var t2 = qs[current];
                    var result = int.TryParse(t2, out value);
                    return result;
                }
            }

            value = 0;
            return false;
        }



        protected void Page_Load(object sender, EventArgs e)
        {
            SystemDateTime.SetTime(new DateTime(2016, 9, 2, 14, 0, 0), 0);

            int t1;

            if (!(IsPostBack))
            {
                GuiReadSettings();

                //if (XpGuiContainer == null)
                {
                    XpGuiContainer = PickupApi.GetGuiContainer(0);
                }

                XmPopulatePage();
            }
        }


        protected void XuPickupStatus_Click(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// Cmd01: Operate on status of Forwarder Pickup
        /// </summary>
        private void XmChangePickupStatusCustomer(string commandArgument)
        {
            // Expected CommandArgument Syntax: {StatusCode}{.}{ForwarderPickupId}

            var t1 = commandArgument.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            if (t1.Length == 2)
            {
                var statusCode = t1[0];
                int pickupId;
                if (int.TryParse(t1[1], out pickupId))
                {
                    var targetForwarderPickup = XpAllCustomerPickupList.FirstOrDefault(t => t.Id == pickupId);
                    if (targetForwarderPickup != null)
                    {
                        switch (statusCode)
                        {
                            case "CustWait":
                            case "CustCan":
                            case "CustHand":
                                {
                                    targetForwarderPickup.PickupStatusCustomer = PickupStatusCustomer.ForwWait;
                                }
                                break;

                            case "ForwWait":
                                {
                                    targetForwarderPickup.PickupStatusCustomer = PickupStatusCustomer.CustCan;
                                }
                                break;
                        }
                    }
                }
            }

            XmPopulatePage();
        }


        private List<Control> FindSubControl(Control top, string id)
        {
            var result = new List<Control>();

            if (top.ID != null && top.ID.IndexOf(id, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                result.Add(top);
            }

            foreach (Control control in top.Controls)
            {
                result.AddRange(FindSubControl(control, id));
            }

            return result;

        }


        /// <summary>
        /// Move Shipments from one Customer Pickup to another.
        /// </summary>
        /// <param name="commandArgument"></param>
        /// <param name="source"></param>
        private void XmMoveShipment(string commandArgument, GridView source)
        {
            // Expected CommandArgument Syntax: {ForwarderPickupId}

            // scan the checkbox controls on each shipment row.
            var rowCheckboxList = FindSubControl(source, "XuShSelectItem");

            GuiForwarderPickup currentForwarderPickup = null;

            int customerPickupId;
            if (int.TryParse(commandArgument, out customerPickupId))
            {
                var currentCustomerPickup =
                        (from forwarderPickup in XpPrimaryRowList
                         from customerPickup in forwarderPickup.CustomerPickupList
                         where customerPickup.Id == customerPickupId
                         select customerPickup).First();

                if (currentCustomerPickup != null)
                {
                    currentForwarderPickup = XpPrimaryRowList.FirstOrDefault(
                        t1 =>
                        t1.CustomerPickupList.Any(
                            t2 =>
                            t2.Id == customerPickupId
                        )
                    );

                    foreach (var shipmentRow in rowCheckboxList)
                    {
                        var shipmentCheckbox = shipmentRow as HtmlInputCheckBox;

                        if (shipmentCheckbox == null)
                        {
                            continue;
                        }

                        if (!(shipmentCheckbox.Checked))
                        {
                            continue;
                        }

                        int shipmentId;
                        if (int.TryParse(shipmentCheckbox.Value, out shipmentId))
                        {
                            var sourceCustomerPickup =
                                (from forwarderPickup in XpPrimaryRowList
                                 from customerPickup in forwarderPickup.CustomerPickupList
                                 from shipment in customerPickup.Shipmentlist
                                 where shipment.Id == shipmentId
                                 select customerPickup).First();

                            if (sourceCustomerPickup == null)
                            {
                                continue;
                            }

                            var currentShipment = sourceCustomerPickup.Shipmentlist.FirstOrDefault(
                                t =>
                                    t.Id == shipmentId
                                );

                            //sourceCustomerPickup.Shipmentlist.Remove(currentShipment);
                            //currentCustomerPickup.Shipmentlist.Add(currentShipment);

                            // Update Database
                            IShipment t1;
                            if (XpBackendApi.Read(out t1, shipmentId))
                            {
                                t1.CustomerPickupId = customerPickupId;
                            }
                        }
                    }
                }
                XpBackendApi.UpdateDatabase();

                XpBackendApi.Refresh(XpGuiContainer, currentForwarderPickup);
            }

            XmPopulatePage();
        }

        protected string XmGetPickupText(string format, object picupDate, object timeReady, object timeClose)
        {
            return string.Format(format, picupDate, timeReady, timeClose);
        }

        protected string GetPickupText(object customerPickupId)
        {
            return "0" + customerPickupId;
        }



        protected void XuGridCustomerPickup_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var Argument = e.CommandArgument;
            var name = e.CommandName;
            var source = e.CommandSource;


            switch (e.CommandName)
            {
                case "XcCmd01":
                    {
                        XmChangePickupStatusCustomer(e.CommandArgument as string);
                    }
                    break;

                case "XcCmd02":
                    {
                        XmMoveShipment(e.CommandArgument as string, sender as GridView);
                    }
                    break;

                default:
                    break;
            }

        }


        /// <summary>
        /// Calculate Smallest pickup window.
        /// </summary>
        /// <param name="commandArgument"></param>
        private void XmCalculatePickupWindow(string commandArgument)
        {
            // Expected syntax: int: ForwarderPickup.Id.

            do
            {
                // TODO MOVE BI to backend...
                int forwarderPickupId;
                if (int.TryParse(commandArgument, out forwarderPickupId))
                {
                    var currentForwarderPickup = XpPrimaryRowList.FirstOrDefault(t => t.Id == forwarderPickupId);
                    if (currentForwarderPickup == null)
                    {
                        continue;
                    }

                    var open = new TimeSpan(0, 0, 0);
                    var close = new TimeSpan(23, 59, 59);

                    foreach (var customerPickup in currentForwarderPickup.CustomerPickupList)
                    {
                        if (open < customerPickup.TimeReady)
                        {
                            open = customerPickup.TimeReady;
                        }

                        if (customerPickup.TimeClose < close)
                        {
                            close = customerPickup.TimeClose;
                        }
                    }

                    currentForwarderPickup.TimeClose = close;
                    currentForwarderPickup.TimeReady = open;

                    // Update repository
                    XmLoadSelectedForwarderPickup(forwarderPickupId);
                    XpSelectedForwarderPickup.TimeClose = close;
                    XpSelectedForwarderPickup.TimeReady = open;
                    XpBackendApi.UpdateDatabase();
                }
            } while (false);

            XmPopulatePage();
        }


        /// <summary>
        /// Update ForwarderPickup PickupStatus.
        /// </summary>
        /// <param name="commandArgument"></param>
        private void XmChangePickupStausForwarder(string commandArgument)
        {
            // Expected syntax: int: ForwarderPickup.Id.

            do
            {
                // TODO MOVE BI to backend.
                int forwarderPickupId;
                if (int.TryParse(commandArgument, out forwarderPickupId))
                {
                    var currentForwarderPickup = XpPrimaryRowList.FirstOrDefault(t => t.Id == forwarderPickupId);
                    if (currentForwarderPickup == null) { continue; }

                    switch (currentForwarderPickup.PickupStatusForwarder)
                    {
                        case PickupStatusForwarder.CustWait:
                            {
                                currentForwarderPickup.PickupStatusForwarder = PickupStatusForwarder.ForwWait;
                            }
                            break;

                        case PickupStatusForwarder.ForwWait:
                            {
                                currentForwarderPickup.PickupStatusForwarder = PickupStatusForwarder.CustWait;
                            }
                            break;

                    }

                    // update database
                    XmLoadSelectedForwarderPickup(forwarderPickupId);
                    XpSelectedForwarderPickup.PickupStatus = currentForwarderPickup.PickupStatusForwarder;
                    XpBackendApi.UpdateDatabase();
                }

            } while (false);

            XmPopulatePage();
        }


        /// <summary>
        /// Edit ForwarderPickup.
        /// </summary>
        /// <param name="commandArgument"></param>
        private void XmEditForwarderPickup(string commandArgument)
        {
            // Expected syntax: int: ForwarderPickup.Id.

            do
            {
                // TODO Move BI to backend .. CanEndit() ?
                int forwarderPickupId;
                if (int.TryParse(commandArgument, out forwarderPickupId))
                {
                    var currentForwarderPickup = XpPrimaryRowList.FirstOrDefault(t => t.Id == forwarderPickupId);
                    if (currentForwarderPickup == null) { continue; }

                    switch (currentForwarderPickup.PickupStatusForwarder)
                    {
                        case PickupStatusForwarder.CustWait:
                            {
                            }
                            break;

                        case PickupStatusForwarder.ForwWait:
                            {
                            }
                            break;


                    }

                    var url = string.Format("EditForwarderPickup.aspx?ForwarderPickuId={0}", currentForwarderPickup.Id);
                    Response.Redirect(url, endResponse: false);
                }

            } while (false);

            XmPopulatePage();
        }


        /// <summary>
        /// Business Code
        /// Move list of CustomerPickup to the specified ForwarderPickup.
        /// Uses identifies not types to bridge between GuiClass and coresponding BackendClas.
        /// </summary>
        /// <param name="targetForwarderPickupId"></param>
        /// <param name="customerPickupIdList"></param>
        private void XmMoveCustomerPickup(int targetForwarderPickupId, IEnumerable<int> customerPickupIdList)
        {
            var target = XpPrimaryRowList.FirstOrDefault(t => t.Id == targetForwarderPickupId);
            if (target == null) { return; }

            var t1 = XpPrimaryRowList.FirstOrDefault(t => t.Id == targetForwarderPickupId);
            if (t1 == null) { return; }

            var toUpdateLIst = new List<GuiForwarderPickup> { t1 };

            foreach (var customerPickupId in customerPickupIdList)
            {
                var source = XpPrimaryRowList.FirstOrDefault(t => t.CustomerPickupList.Any(r => r.Id == customerPickupId));
                if (source == null) { continue; }

                toUpdateLIst.Add(source);

                //var subject = source.CustomerPickupList.FirstOrDefault(r => r.Id == customerPickupId);

                //source.CustomerPickupList.Remove(subject);
                //target.CustomerPickupList.Add(subject);

                if (XmLoadSelectedCustomerPickup(customerPickupId))
                {
                    XpSelectedCustomerPickup.ForwarderPickpId = targetForwarderPickupId;
                }
            }
            XpBackendApi.UpdateDatabase();

            foreach (var currentForwarderPickup in toUpdateLIst)
            {
                XpBackendApi.Refresh(XpGuiContainer, currentForwarderPickup);
            }
        }


        /// <summary>
        /// Move CustomerPickups from one Forwarder Pickup to another.
        /// </summary>
        /// <param name="commandArgument"></param>
        /// <param name="source"></param>
        private void XmCmd06(string commandArgument, GridView source)
        {
            // Expected syntax: int: ForwarderPickup.Id.

            do
            {
                int forwarderPickupId;
                if (int.TryParse(commandArgument, out forwarderPickupId))
                {
                    var rowFwCheckboxList = FindSubControl(source, "XuCuSelectItem");

                    var customerPickupIdList = new List<int>();

                    foreach (var row in rowFwCheckboxList)
                    {
                        var checkbox = row as HtmlInputCheckBox;

                        if (checkbox == null) { continue; }

                        if (!(checkbox.Checked)) { continue; }

                        int customerPickupId;


                        if (int.TryParse(checkbox.Value, out customerPickupId))
                        {
                            customerPickupIdList.Add(customerPickupId);
                        }
                    }

                    XmMoveCustomerPickup(forwarderPickupId, customerPickupIdList);
                }

            } while (false);

            XmPopulatePage();

            // Expand.
            XmCmd09(commandArgument, source);
        }


        /// <summary>
        /// Expand/Collaps Shipment below CustomerPickup.
        /// </summary>
        /// <param name="commandArgument"></param>
        /// <param name="source"></param>
        private void XmCmd07(string commandArgument, GridView source)
        {
            // Expected syntax: int: CustomerPickup.Id.

            do
            {
                int customerPickupId;
                if (int.TryParse(commandArgument, out customerPickupId))
                {
                    var currentCustomerPickup =
                            (from forwarderPickup in XpPrimaryRowList
                             from customerPickup in forwarderPickup.CustomerPickupList
                             where customerPickup.Id == customerPickupId
                             select customerPickup).First();

                    currentCustomerPickup.IsExpanded = !currentCustomerPickup.IsExpanded;
                }

            } while (false);

            XmPopulatePage();
        }


        /// <summary>
        /// Expand/Collapse 2 levels.
        /// </summary>
        /// <param name="commandArgument"></param>
        /// <param name="source"></param>
        private void XmCmd08(string commandArgument, GridView source)
        {
            // Expected syntax: int: ForwarderPickup.Id.

            do
            {
                int forwarderPickupId;
                if (int.TryParse(commandArgument, out forwarderPickupId))
                {
                    var currentForwarderPickup =
                        (from forwarderPickup in XpPrimaryRowList
                         where forwarderPickup.Id == forwarderPickupId
                         select forwarderPickup).First();

                    if (currentForwarderPickup == null)
                    {
                        continue;
                    }

                    var isExpandedGroup = !currentForwarderPickup.CustomerPickupList.Any(t => t.IsExpanded);

                    currentForwarderPickup.IsExpandedCustomer = isExpandedGroup;
                    currentForwarderPickup.CustomerPickupList.ForEach(t => { t.IsExpanded = isExpandedGroup; });
                }

            } while (false);

            XmPopulatePage();
        }


        /// <summary>
        /// Expand 1 level/Collapse all levels on Customers below Forwarder
        /// </summary>
        /// <param name="commandArgument"></param>
        /// <param name="source"></param>
        private void XmCmd09(string commandArgument, GridView source)
        {
            // Expected syntax: int: ForwarderPickup.Id.

            do
            {
                int forwarderPickupId;
                if (int.TryParse(commandArgument, out forwarderPickupId))
                {
                    var currentForwarderPickup =
                            (from forwarderPickup in XpPrimaryRowList
                             where forwarderPickup.Id == forwarderPickupId
                             select forwarderPickup).First();

                    if (currentForwarderPickup == null) { continue; }

                    currentForwarderPickup.IsExpandedCustomer = !currentForwarderPickup.IsExpandedCustomer;

                    if (!(currentForwarderPickup.IsExpandedCustomer))
                    {
                        // Collapse Shipments when collapsing Customer
                        foreach (var customer in currentForwarderPickup.CustomerPickupList)
                        {
                            customer.IsExpanded = false;
                        }
                    }
                }

            } while (false);

            XmPopulatePage();
        }


        protected
            void XuGridForwarderPicup_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "XcCmd03":
                    {
                        XmCalculatePickupWindow(e.CommandArgument as string);
                    }
                    break;

                case "XcCmd04":
                    {
                        // change .. status on forwarder pickup
                        XmChangePickupStausForwarder(e.CommandArgument as string);
                    }
                    break;

                case "XcCmd05":
                    {
                        // change .. status on forwarder pickup
                        XmEditForwarderPickup(e.CommandArgument as string);
                    }
                    break;

                case "XcCmd06":
                    {
                        // Move Customer Pickup to another Forwarder Pickup
                        XmCmd06(e.CommandArgument as string, sender as GridView);
                    }
                    break;

                case "XcCmd07":
                    {
                        XmCmd07(e.CommandArgument as string, sender as GridView);
                    }
                    break;

                case "XcCmd08":
                    {
                        XmCmd08(e.CommandArgument as string, sender as GridView);
                    }
                    break;

                case "XcCmd09":
                    {
                        XmCmd09(e.CommandArgument as string, sender as GridView);
                    }
                    break;

            }

            XmPopulatePage();
        }

        protected void XuExpandAllRefresh_Click(object sender, EventArgs e)
        {
            GuiReadSettings();

            var saved = XpGuiContainer;
            var start = DateTime.UtcNow;
            XpGuiContainer = PickupApi.GetGuiContainer(0); // TODO pass on filters.
            var duration = DateTime.UtcNow.Subtract(start);
            XpGuiContainer.IsSettingsVisible = false; // saved.IsSettingsVisible;

            XmPopulatePage();

            //XuUpdatePanel1.Update();
        }


        /// <summary>
        /// Expand/Collapse Settings.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void XuSettingsExpand_Click(object sender, EventArgs e)
        {
            XpGuiContainer.IsSettingsVisible = !XpGuiContainer.IsSettingsVisible;

            XmPopulatePage();
        }

        /// <summary>
        /// Expand/Collaps all rows ForvarderPickup -> CustomerPickup
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void XuExpandAll_Click(object sender, EventArgs e)
        {
            var anyExpandedCustomer = XpPrimaryRowList.Any(t => t.IsExpandedCustomer);

            if (anyExpandedCustomer)
            {
                // Collapse two level down: CustomerPickup on all ForwarderPickup, Shipment on all CustomerPickup
                XpGuiContainer.ForwarderPickupList.ForEach(
                    t1 =>
                    {
                        t1.IsExpandedCustomer = false;
                        t1.CustomerPickupList.ForEach(
                            t2 =>
                            {
                                t2.IsExpanded = false;
                            }
                        );
                    }
                );

                //// Collapse all iterative
                //foreach (var forwarder in XpPrimaryRowList)
                //{
                //    forwarder.IsExpandedCustomer = false;
                //    foreach (var customer in forwarder.CustomerPickupList)
                //    {
                //        customer.IsExpanded = false;
                //    }

                //    forwarder.CustomerPickupList.ForEach(t => { t.IsExpanded = false; });

                //}
            }
            else
            {
                // Expand one level down: Customer Pickup on all Forwarder Pickup

                XpGuiContainer.ForwarderPickupList.ForEach(
                    t1 =>
                    {
                        t1.IsExpandedCustomer = true;
                    }
                );

                //foreach (var forwarder in XpPrimaryRowList)
                //{
                //    forwarder.IsExpandedCustomer = true;
                //}

            }


            XmPopulatePage();
        }
    }
}