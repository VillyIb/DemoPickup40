using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using AppCode.Pages.Pickup2;
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


        private void BindPage( )
        {
            foreach (var forwarderPickup in XpPrimaryRowList)
            {
                var t3 = new List<string>();

                foreach (var customerPickup in forwarderPickup.CustomerPickupList)
                {
                    var t1 = customerPickup.Shipmentlist.Select(t => t.CarrierName).Distinct().OrderBy(t => t).ToList();
                    t3.AddRange(t1);


                    customerPickup.PickupStatusForwarder = forwarderPickup.PickupStatusForwarder;

                    foreach (var shipment in customerPickup.Shipmentlist)
                    {
                        shipment.PickupStatusCustomer = customerPickup.PickupStatusCustomer;
                    }
                }

                var t4 = t3.Distinct().OrderBy(t => t).ToList();
                var t5 = t4.Count > 0 ? t4.Aggregate((current, next) => current + ", " + next) : "none";

                forwarderPickup.CarrierNameList = t5;
            }

            XuForwarderPickup.DataSource = XpPrimaryRowList;
            XuForwarderPickup.DataBind();
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(IsPostBack))
            {
                if (XpGuiContainer == null || true)
                {
                    var pickupApi = new PickupData();

                    //pickupApi.Init();

                    XpGuiContainer = pickupApi.GetGuiContainer(0);
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
            var Argument = e.CommandArgument;
            var name = e.CommandName;
            var source = e.CommandSource;


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

                default:
                    break;
            }

        }


        /// <summary>
        /// Calculate Smallest pickup window.
        /// </summary>
        /// <param name="commandArgument"></param>
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

                    // Update repository
                    XmLoadSelectedForwarderPickup(forwarderPickupId);
                    XpSelectedForwarderPickup.TimeClose = close;
                    XpSelectedForwarderPickup.TimeReady = open;
                    XpBackendApi.UpdateDatabase();
                }
            } while (false);

            BindPage();
        }


        /// <summary>
        /// Update ForwarderPickup PickupStatus.
        /// </summary>
        /// <param name="commandArgument"></param>
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

                    // update database
                    XmLoadSelectedForwarderPickup(forwarderPickupId);
                    XpSelectedForwarderPickup.PickupStatus = currentForwarderPickup.PickupStatusForwarder;
                    XpBackendApi.UpdateDatabase();
                }

            } while (false);

            BindPage();
        }


        /// <summary>
        /// Edit ForwarderPickup.
        /// </summary>
        /// <param name="commandArgument"></param>
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

            var t1 = XpPrimaryRowList.FirstOrDefault(t => t.Id == targetForwarderPickupId);
            if(t1==null) { return; }
            
            var toUpdateLIst = new List<GuiForwarderPickup> {t1};

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

            // Expand.
            XcCmd09(commandArgument, source);
        }


        /// <summary>
        /// Expand/Collaps Shipment below CustomerPickup.
        /// </summary>
        /// <param name="commandArgument"></param>
        /// <param name="source"></param>
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
        /// Expand all levels/Collapse 1 levle of Shipments below Customer below Forwarder
        /// </summary>
        /// <param name="commandArgument"></param>
        /// <param name="source"></param>
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
        /// Expand 1 level/Collapse all levels on Customers below Forwarder
        /// </summary>
        /// <param name="commandArgument"></param>
        /// <param name="source"></param>
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

            XuContainerCol1Icon.Attributes["class"] = anyExpandedCustomer
                ? "glyphicon glyphicon-triangle-top big"
                : "glyphicon glyphicon-triangle-bottom big";

            BindPage();
        }
    }
}