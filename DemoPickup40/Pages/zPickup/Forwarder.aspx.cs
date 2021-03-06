﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using AppCode.Pages.Pickup;
using nu.gtx.POCO.Contract.Pickup;
// ReSharper disable UseNullPropagation
// ReSharper disable ArgumentsStyleLiteral
using AppCode.Pages.zPickup;

namespace DemoPickup40.Pages.zPickup
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


        private void BindPage()
        {
            foreach (var forwarder in XpPrimaryRowList)
            {
                var t3 = new List<string>();

                foreach (var pickup in forwarder.CustomerPickupList)
                {
                    var t1 = pickup.Shipmentlist.Select(t => t.CarrierName).Distinct().OrderBy(t => t).ToList();
                    t3.AddRange(t1);

                    var t2 = t1.Count > 0 ? t1.Aggregate((current, next) => current + ", " + next) : "";

                    pickup.CarrierNameList = t2;

                    pickup.PickupStatusForwarder = forwarder.PickupStatusForwarder;

                    foreach (var shipment in pickup.Shipmentlist)
                    {
                        shipment.PickupStatusCustomer = pickup.PickupStatusCustomer;
                    }
                }

                var t4 = t3.Distinct().OrderBy(t => t).ToList();
                var t5 = t4.Count > 0 ? t4.Aggregate((current, next) => current + ", " + next) : "none";

                forwarder.CarrierNameList = t5;
            }

            XuForwarderPickup.DataSource = XpPrimaryRowList;
            XuForwarderPickup.DataBind();
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(IsPostBack))
            {
                if (XpGuiContainer == null)
                {
                    XpGuiContainer = PickupData.GetGuiContainer(0);
                }

                BindPage();
            }
        }


        protected void XuPickupStatus_Click(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// Cmd01: Operate on status of Forwarder Pickup
        /// </summary>
        private void XcCmd01(string commandArgument)
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

            BindPage();
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
        private void XcCmd02(string commandArgument, GridView source)
        {
            // Expected CommandArgument Syntax: {ForwarderPickupId}

            // scan the checkbox controls on each shipment row.
            var rowCheckboxList = FindSubControl(source, "XuShSelectItem");

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
                    foreach (var row in rowCheckboxList)
                    {
                        var checkbox = row as HtmlInputCheckBox;

                        if (checkbox == null)
                        {
                            continue;
                        }

                        if (!(checkbox.Checked))
                        {
                            continue;
                        }

                        int shipmentId;
                        if (int.TryParse(checkbox.Value, out shipmentId))
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

                            sourceCustomerPickup.Shipmentlist.Remove(currentShipment);
                            currentCustomerPickup.Shipmentlist.Add(currentShipment);
                        }
                    }
                }
            }

            BindPage();
        }

        protected string GetPickupText(string format, object picupDate, object timeReady, object timeClose)
        {
            return string.Format(format, picupDate, timeReady, timeClose);
        }

        protected string GetPickupText(object customerPickupId)
        {
            return "0" + customerPickupId;
        }



        protected void XuGridCustomerPickup_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "XcCmd01":
                    {
                        XcCmd01(e.CommandArgument as string);
                    }
                    break;

                case "XcCmd02":
                    {
                        XcCmd02(e.CommandArgument as string, sender as GridView);
                    }
                    break;
            }
        }


        private void XcCmd03(string commandArgument)
        {
            // Expected syntax: int: ForwarderPickup.Id.

            do
            {
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
                }
            } while (false);

            BindPage();
        }


        private void XcCmd04(string commandArgument)
        {
            // Expected syntax: int: ForwarderPickup.Id.

            do
            {
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

                }

            } while (false);

            BindPage();
        }

        private void XcCmd05(string commandArgument)
        {
            // Expected syntax: int: ForwarderPickup.Id.

            do
            {
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

            BindPage();
        }


        /// <summary>
        /// Business Code
        /// Move list of CustomerPickup to the specified ForwarderPickup.
        /// Uses identifies not types to bridge between GuiClass and coresponding BackendClas.
        /// </summary>
        /// <param name="targetForwarderPickupId"></param>
        /// <param name="customerPickupIdList"></param>
        private void XbMoveCustomerPickup(int targetForwarderPickupId, IEnumerable<int> customerPickupIdList)
        {
            var target = XpPrimaryRowList.FirstOrDefault(t => t.Id == targetForwarderPickupId);
            if (target == null) { return; }

            foreach (var customerPickupId in customerPickupIdList)
            {
                var source = XpPrimaryRowList.FirstOrDefault(t => t.CustomerPickupList.Any(r => r.Id == customerPickupId));
                if (source == null) { continue; }

                var subject = source.CustomerPickupList.FirstOrDefault(r => r.Id == customerPickupId);

                source.CustomerPickupList.Remove(subject);
                target.CustomerPickupList.Add(subject);
            }
        }


        /// <summary>
        /// Move CustomerPickups from one Forwarder Pickup to another.
        /// </summary>
        /// <param name="commandArgument"></param>
        /// <param name="source"></param>
        private void XcCmd06(string commandArgument, GridView source)
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

                    XbMoveCustomerPickup(forwarderPickupId, customerPickupIdList);
                }

            } while (false);

            BindPage();
        }


        // ReSharper disable once UnusedParameter.Local
        private void XcCmd07(string commandArgument, GridView source)
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

            BindPage();
        }


        /// <summary>
        /// Expand/Collapse Shipments below Customer below Forwarder
        /// </summary>
        /// <param name="commandArgument"></param>
        /// <param name="source"></param>
        // ReSharper disable once UnusedParameter.Local
        private void XcCmd08(string commandArgument, GridView source)
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

                    var isExpandedGroup = !currentForwarderPickup.CustomerPickupList.Any(t => t.IsExpanded);

                    if (isExpandedGroup)
                    {
                        if (bool.Parse("true")) // configuration on behavior
                        {
                            // Collapse all (other).
                            foreach (var forwarder in XpPrimaryRowList)
                            {
                                forwarder.IsExpandedCustomer = false;

                                foreach (var customer in forwarder.CustomerPickupList)
                                {
                                    customer.IsExpanded = false;
                                }
                            }
                        }

                        // Expand Customers when expanding Shipments
                        currentForwarderPickup.IsExpandedCustomer = true;
                    }

                    foreach (var currentCustomerPickup in currentForwarderPickup.CustomerPickupList)
                    {
                        currentCustomerPickup.IsExpanded = isExpandedGroup;
                    }
                }

            } while (false);

            BindPage();
        }


        /// <summary>
        /// Expand/Collapse Customers below Forwarder.
        /// </summary>
        /// <param name="commandArgument"></param>
        /// <param name="source"></param>
        // ReSharper disable once UnusedParameter.Local
        private void XcCmd09(string commandArgument, GridView source)
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

            BindPage();
        }


        protected
            void XuGridForwarderPicup_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "XcCmd03":
                    {
                        XcCmd03(e.CommandArgument as string);
                    }
                    break;

                case "XcCmd04":
                    {
                        // change .. status on forwarder pickup
                        XcCmd04(e.CommandArgument as string);
                    }
                    break;

                case "XcCmd05":
                    {
                        // change .. status on forwarder pickup
                        XcCmd05(e.CommandArgument as string);
                    }
                    break;

                case "XcCmd06":
                    {
                        // Move Customer Pickup to another Forwarder Pickup
                        XcCmd06(e.CommandArgument as string, sender as GridView);
                    }
                    break;

                case "XcCmd07":
                    {
                        XcCmd07(e.CommandArgument as string, sender as GridView);
                    }
                    break;

                case "XcCmd08":
                    {
                        XcCmd08(e.CommandArgument as string, sender as GridView);
                    }
                    break;

                case "XcCmd09":
                    {
                        XcCmd09(e.CommandArgument as string, sender as GridView);
                    }
                    break;

            }

            BindPage();
        }

        protected void XuContainerCol1_Click(object sender, EventArgs e)
        {
            var anyExpandedCustomer = XpPrimaryRowList.Any(t => t.IsExpandedCustomer);

            if (anyExpandedCustomer)
            {
                // Collapse all iterative
                foreach (var forwarder in XpPrimaryRowList)
                {
                    forwarder.IsExpandedCustomer = false;
                    foreach (var customer in forwarder.CustomerPickupList)
                    {
                        customer.IsExpanded = false;
                    }
                }
            }
            else
            {
                // Expand Customer Pickup on all Forwarder Pickup
                foreach (var forwarder in XpPrimaryRowList)
                {
                    forwarder.IsExpandedCustomer = true;
                }
                
            }

            BindPage();
        }
    }
}