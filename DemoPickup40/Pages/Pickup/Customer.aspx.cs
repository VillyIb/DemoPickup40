using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace DemoPickup40.Pages.Pickup
{
    public partial class Customer : System.Web.UI.Page
    {


        private const string XpPrimaryRowListKey = "PrimaryRowList";
        private List<GuiData1> XpPrimaryRowList
        {
            get { return Session[XpPrimaryRowListKey] as List<GuiData1>; }
            set { Session[XpPrimaryRowListKey] = value; }
        }

        int RowCount { get; set; }


        private Control FindControl(ControlCollection list, string name)
        {
            if (list == null) return null;

            foreach (Control control in list)
            {
                if (control.ID != null && control.ID.Contains(name))
                {
                    return control;
                }
                return FindControl(control.Controls, name);
            }

            return null;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(IsPostBack))
            {
                if (ClientQueryString.Contains("PickupId"))
                {
                    // parse sub-grid rows.

                    var control = FindControl(Controls, "XuSelectItem");

                }


                if (XpPrimaryRowList == null)
                {
                    XpPrimaryRowList = new List<GuiData1>
                    {
                        new GuiData1
                        {
                            ActionCode = "Req",
                            ActionCodeGlyphIcon = "download-alt",
                            Address = "Stavnsholt Gydevej 71",
                            FullAddress = "Global Transport Xpress, Stavnsholt Gydevej 71, DK 3460 Birkerød",
                            Pickup = "2016-08-12 from 12:10 to 14:50",
                            Note = "Henvendelse på den 'Gule gård'",
                            PickupId = 012,
                            StatusCodeText = "CustHand",
                            RowNumber = 1,
                            Shipmentlist = new List<GudData2>
                            {
                                new GudData2
                                {
                                    Dimensions = "50 x 37 x 22",
                                    ShipmentId = 34576,
                                    Waybillnumber = "901015378166987332598",
                                    Weight = 0.5m,
                                    StatusCodeText = "CustHand"
                                },
                                new GudData2
                                {
                                    Dimensions = "50 x 37 x 22",
                                    ShipmentId = 34577,
                                    Waybillnumber = "902015378166987332605",
                                    Weight = 0.4m,
                                    StatusCodeText = "CustHand"
                                },
                                new GudData2
                                {
                                    Dimensions = "50 x 37 x 22",
                                    ShipmentId = 34578,
                                    Waybillnumber = "903015378166987332618",
                                    Weight = 0.3m,
                                    StatusCodeText = "CustHand"
                                }
                            }
                        },

                        new GuiData1
                        {
                            ActionCode = "Req",
                            ActionCodeGlyphIcon = "download-alt",
                            Address = "Stavnsholt Gydevej 72",
                            FullAddress = "Global Transport Xpress, Stavnsholt Gydevej 72, DK 3460 Birkerød",
                            Pickup = "2016-08-12 from 12:10 to 14:50",
                            Note = "Henvendelse på den 'Gule gård'",
                            PickupId = 123,
                            StatusCodeText = "CustWait",
                            RowNumber = 1,
                            Shipmentlist = new List<GudData2>
                            {
                                new GudData2
                                {
                                    Dimensions = "50 x 37 x 22",
                                    ShipmentId = 34567,
                                    Waybillnumber = "911015378166987332598",
                                    Weight = 0.5m,
                                    StatusCodeText = "CustWait"
                                },
                                new GudData2
                                {
                                    Dimensions = "50 x 37 x 22",
                                    ShipmentId = 34568,
                                    Waybillnumber = "912015378166987332605",
                                    Weight = 0.4m,
                                    StatusCodeText = "CustWait"
                                },
                                new GudData2
                                {
                                    Dimensions = "50 x 37 x 22",
                                    ShipmentId = 34569,
                                    Waybillnumber = "913015378166987332618",
                                    Weight = 0.3m,
                                    StatusCodeText = "CustWait"
                                }
                            }
                        },

                        new GuiData1
                        {
                            ActionCode = "Can",
                            ActionCodeGlyphIcon = "none",
                            Address = "Stavnsholt Gydevej 73",
                            FullAddress = "Global Transport Xpress, Stavnsholt Gydevej 73, DK 3460 Birkerød",
                            Pickup = "2016-08-12 from 12:10 to 14:50",
                            Note = "Henvendelse på den 'Gule gård'",
                            PickupId = 234,
                            StatusCodeText = "ForwWait",
                            RowNumber = 1,
                            Shipmentlist = new List<GudData2>
                            {
                                new GudData2
                                {
                                    Dimensions = "50 x 37 x 22",
                                    ShipmentId = 34570,
                                    Waybillnumber = "921015378166987332598",
                                    Weight = 0.5m,
                                    StatusCodeText = "ForwWait"
                                },
                                new GudData2
                                {
                                    Dimensions = "50 x 37 x 22",
                                    ShipmentId = 34571,
                                    Waybillnumber = "922015378166987332605",
                                    Weight = 0.4m,
                                    StatusCodeText = "ForwWait"
                                },
                                new GudData2
                                {
                                    Dimensions = "50 x 37 x 22",
                                    ShipmentId = 34572,
                                    Waybillnumber = "923015378166987332618",
                                    Weight = 0.3m,
                                    StatusCodeText = "ForwWait"
                                }
                            }
                        },

                        new GuiData1
                        {
                            ActionCode = "Can",
                            ActionCodeGlyphIcon = "none",
                            Address = "Stavnsholt Gydevej 74",
                            FullAddress = "Global Transport Xpress, Stavnsholt Gydevej 74, DK 3460 Birkerød",
                            Pickup = "2016-08-12 from 12:10 to 14:50",
                            Note = "Henvendelse på den 'Gule gård'",
                            PickupId = 235,
                            StatusCodeText = "ForwSched",
                            RowNumber = 1,
                            Shipmentlist = new List<GudData2>
                            {
                                new GudData2
                                {
                                    Dimensions = "50 x 37 x 22",
                                    ShipmentId = 34573,
                                    Waybillnumber = "931015378166987332598",
                                    Weight = 0.5m,
                                    StatusCodeText = "ForwSched"
                                },
                                new GudData2
                                {
                                    Dimensions = "50 x 37 x 22",
                                    ShipmentId = 34574,
                                    Waybillnumber = "932015378166987332605",
                                    Weight = 0.4m,
                                    StatusCodeText = "ForwSched"
                                },
                                new GudData2
                                {
                                    Dimensions = "50 x 37 x 22",
                                    ShipmentId = 34575,
                                    Waybillnumber = "933015378166987332618",
                                    Weight = 0.3m,
                                    StatusCodeText = "ForwSched"
                                }
                            }
                        }

                    };

                    foreach (var pickup in XpPrimaryRowList)
                    {
                        foreach (var shipment in pickup.Shipmentlist)
                        {
                            shipment.FullAddress = pickup.FullAddress;
                            shipment.StatusCodeText = pickup.StatusCodeText;
                        }
                    }

                }
                XuCustomerPickup.DataSource = XpPrimaryRowList;
                XuCustomerPickup.DataBind();
            }



        }

        protected void XuPickupStatus_Click(object sender, EventArgs e)
        {

        }



        /// <summary>
        /// Cmd01: Operate on status of Customer Pickup
        /// </summary>
        private void XcCmd01(string commandArgument)
        {
            // Expected CommandArgument Syntax: {StatusCode}{.}{CustomerPickupId}

            var t1 = commandArgument.Split(new [] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            if (t1.Length == 2)
            {
                var statusCode = t1[0];
                int pickupId;
                if (int.TryParse(t1[1], out pickupId))
                {
                    var targetCustomerPickup = XpPrimaryRowList.FirstOrDefault(t => t.PickupId == pickupId);
                    if (targetCustomerPickup != null)
                    {
                        switch (statusCode)
                        {
                            case "CustWait":
                            case "CustCan":
                            case "CustHand":
                                {
                                    targetCustomerPickup.StatusCodeText = "ForwWait";
                                }
                                break;

                            case "ForwWait":
                                {
                                    targetCustomerPickup.StatusCodeText = "CustCan";
                                }
                                break;
                        }
                    }
                }
            }

            foreach (var pickup in XpPrimaryRowList)
            {
                foreach (var shipment in pickup.Shipmentlist)
                {
                    shipment.StatusCodeText = pickup.StatusCodeText;
                }
            }

            XuCustomerPickup.DataSource = XpPrimaryRowList;
            XuCustomerPickup.DataBind();

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
            // Expected CommandArgument Syntax: {CustomerPickupId}

            // scan for rows wher checkbox are selected.

            var rowCheckboxList = FindSubControl(source, "XuSelectItem");

            var t1 = rowCheckboxList;

            int pickupId;
            if (int.TryParse(commandArgument, out pickupId))
            {
                var targetCustomerPickup = XpPrimaryRowList.FirstOrDefault(t => t.PickupId == pickupId);

                if (targetCustomerPickup != null)
                {
                    foreach (var row in rowCheckboxList)
                    {
                        var checkbox = row as HtmlInputCheckBox;

                        if (checkbox == null) { continue; }

                        if (!(checkbox.Checked)) { continue; }

                        int shipmentId;
                        if (int.TryParse(checkbox.Value, out shipmentId))
                        {
                            var sourceCustomerPickup =
                                XpPrimaryRowList.FirstOrDefault(t => t.Shipmentlist.Any(r => r.ShipmentId == shipmentId));

                            if (sourceCustomerPickup != null)
                            {
                                var shipment =
                                    sourceCustomerPickup.Shipmentlist.FirstOrDefault(t => t.ShipmentId == shipmentId);
                                if (shipment != null)
                                {
                                    sourceCustomerPickup.Shipmentlist.Remove(shipment);
                                    targetCustomerPickup.Shipmentlist.Add(shipment);
                                }
                            }
                        }
                    }
                }
            }

            foreach (var pickup in XpPrimaryRowList)
            {
                foreach (var shipment in pickup.Shipmentlist)
                {
                    shipment.StatusCodeText = pickup.StatusCodeText;
                }
            }

            XuCustomerPickup.DataSource = XpPrimaryRowList;
            XuCustomerPickup.DataBind();

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




            //int index = Convert.ToInt32(e.CommandArgument);

            //GridViewRow row = Xu001.Rows[index];

            //var dd = row.Cells[0];

            //long BacthId = Int32.Parse(dd.Text);

            //XuGridError.Visible = false;
            if (e.CommandName.Equals("ErrorLink"))
            {
                //BatchErrorId.Value = BacthId.ToString();
            }
            else
            {
                //if (ActionList.SelectedValue.Trim().Equals("markdone"))
                //{
                //    SetAllDone(BacthId); // conect to SqlDataSource2
                //    PageRefresh();
                //}

                //if (ActionList.SelectedValue.Equals("excelok"))
                //{
                //    MakeExcelFile(BacthId);
                //}
                //if (ActionList.SelectedValue.Trim().Equals("axok"))
                //{

                //    MakeAxFile(BacthId);
                //}
                //if (ActionList.SelectedValue.Trim().Equals("transfere"))
                //{
                //    SetStatusTransfere(BacthId); // 
                //    PageRefresh();
                //}
                //if (ActionList.SelectedValue.Trim().Equals("rollback"))
                //{

                //    SetStatusRoll(BacthId);
                //    PageRefresh();
                //}
                //if (ActionList.SelectedValue.Trim().Equals("rerunerror"))
                //{

                //    SetReRun(BacthId);
                //    // PageRefresh();
                //}


            }
            //ActionList.SelectedIndex = 0;

        }
    }




    public class GuiData1
    {
        /// <summary>
        /// Action Code is Mapped to Icon/Button text in GUI.
        /// </summary>
        public string ActionCode { get; set; }

        public string ActionCodeGlyphIcon { get; set; }

        public string Address { get; set; }

        public string Pickup { get; set; }

        public string Note { get; set; }

        /// <summary>
        /// StatusCode as plain text  
        /// </summary>
        public string StatusCodeText { get; set; }

        public int PickupId { get; set; }

        ///// <summary>
        ///// Key to lookup into Localization table.
        ///// </summary>
        //public string StatusCodeLocalizeKey { get; set; }


        public string FullAddress { get; set; }

        public int RowNumber { get; set; }

        public List<GudData2> Shipmentlist { get; set; }


    }

    public class GudData2
    {
        public string Dimensions { get; set; }

        public string Waybillnumber { get; set; }

        public decimal Weight { get; set; }

        public int ShipmentId { get; set; }

        public string StatusCodeText { get; set; }

        public string FullAddress { get; set; }


    }

}