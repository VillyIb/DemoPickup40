using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using AppCode.Pages.Pickup;
// ReSharper disable UseNullPropagation

namespace DemoPickup40.Pages.Pickup
{
    public partial class Forwarder : Page
    {
        private const string XpPrimaryRowListKey = "PrimaryRowList";

        private List<GuiCustomerPickup> XpPrimaryRowList
        {
            get { return Session[XpPrimaryRowListKey] as List<GuiCustomerPickup>; }
            set { Session[XpPrimaryRowListKey] = value; }
        }


        private void BindPage()
        {
            foreach (var pickup in XpPrimaryRowList)
            {
                var t1 = pickup.Shipmentlist.Select(t => t.CarrierName).Distinct().OrderBy(t => t);

                var t2 = t1.Aggregate((current, next) => current + ", " + next);

                pickup.CarrierNameList = t2;

                foreach (var shipment in pickup.Shipmentlist)
                {
                    shipment.PickupStatusText = pickup.PickupStatusText;
                }
            }

            XuForwarderPickup.DataSource = XpPrimaryRowList;
            XuForwarderPickup.DataBind();

        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(IsPostBack))
            {

                if (XpPrimaryRowList == null)
                {
                    XpPrimaryRowList = new List<GuiCustomerPickup>
                    {
                        PickupData.GetGuiCustomerPickup(1),
                        PickupData.GetGuiCustomerPickup(2),
                        PickupData.GetGuiCustomerPickup(3),
                        PickupData.GetGuiCustomerPickup(4),
                        PickupData.GetGuiCustomerPickup(5)
                    };
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
                    var targetForwarderPickup = XpPrimaryRowList.FirstOrDefault(t => t.Id == pickupId);
                    if (targetForwarderPickup != null)
                    {
                        switch (statusCode)
                        {
                            case "CustWait":
                            case "CustCan":
                            case "CustHand":
                                {
                                    targetForwarderPickup.PickupStatusText = "ForwWait";
                                }
                                break;

                            case "ForwWait":
                                {
                                    targetForwarderPickup.PickupStatusText = "CustCan";
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



        private void XcCmd02(string commandArgument, GridView source)
        {
            // Expected CommandArgument Syntax: {ForwarderPickupId}

            // scan the checkbox controls on each shipment row.
            var rowCheckboxList = FindSubControl(source, "XuSelectItem");

            int pickupId;
            if (int.TryParse(commandArgument, out pickupId))
            {
                var targetForwarderPickup = XpPrimaryRowList.FirstOrDefault(t => t.Id == pickupId);

                if (targetForwarderPickup != null)
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
                            var sourceForwarderPickup = XpPrimaryRowList.FirstOrDefault(
                                t =>
                                t.Shipmentlist.Any(r => r.Id == shipmentId)
                            );

                            if (sourceForwarderPickup == null) { continue; }

                            var shipment = sourceForwarderPickup.Shipmentlist.FirstOrDefault(
                                t =>
                                t.Id == shipmentId
                                );

                            if (shipment == null) { continue; }

                            sourceForwarderPickup.Shipmentlist.Remove(shipment);
                            targetForwarderPickup.Shipmentlist.Add(shipment);
                        }
                    }
                }
            }

            BindPage();
        }

        protected string GetPickupText(string format, object picupDate, object readyOpen, object readyClose)
        {
            return string.Format(format, picupDate, readyOpen, readyClose);
        }

        protected string GetPickupText(object customerPickupId)
        {
            return "0" + customerPickupId;
        }

        protected void XuGridForwarderPickup_RowCommand(object sender, GridViewCommandEventArgs e)
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
    }




}