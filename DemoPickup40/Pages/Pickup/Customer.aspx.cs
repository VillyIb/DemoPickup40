using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
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
            if (!(IsCallback))
            {
                if (ClientQueryString.Contains("PickupId"))
                {
                    // parse sub-grid rows.

                    var control = FindControl(Controls, "XuSelected");

                }


                if (XpPrimaryRowList == null)
                {
                    XpPrimaryRowList = new List<GuiData1>
                    {
                        new GuiData1
                        {
                            ActionCode = "Req",
                            ActionCodeGlyphIcon = "download-alt",
                            Address = "Stavnsholt Gydevej 70",
                            FullAddress = "Global Transport Xpress, Stavnsholt Gydevej 70, DK 3460 Birkerød",
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
                                    Waybillnumber = "920015378166987332598",
                                    Weight = 0.5m
                                },
                                new GudData2
                                {
                                    Dimensions = "50 x 37 x 22",
                                    ShipmentId = 34568,
                                    Waybillnumber = "920015378166987332605",
                                    Weight = 0.4m
                                },
                                new GudData2
                                {
                                    Dimensions = "50 x 37 x 22",
                                    ShipmentId = 34569,
                                    Waybillnumber = "920015378166987332618",
                                    Weight = 0.3m
                                }
                            }
                        },

                        new GuiData1
                        {
                            ActionCode = "Can",
                            ActionCodeGlyphIcon = "none",
                            Address = "Stavnsholt Gydevej 70",
                            FullAddress = "Global Transport Xpress, Stavnsholt Gydevej 70, DK 3460 Birkerød",
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
                                    Waybillnumber = "920015378166987332598",
                                    Weight = 0.5m
                                },
                                new GudData2
                                {
                                    Dimensions = "50 x 37 x 22",
                                    ShipmentId = 34571,
                                    Waybillnumber = "920015378166987332605",
                                    Weight = 0.4m
                                },
                                new GudData2
                                {
                                    Dimensions = "50 x 37 x 22",
                                    ShipmentId = 34572,
                                    Waybillnumber = "920015378166987332618",
                                    Weight = 0.3m
                                }
                            }
                        },

                        new GuiData1
                        {
                            ActionCode = "Can",
                            ActionCodeGlyphIcon = "none",
                            Address = "Stavnsholt Gydevej 70",
                            FullAddress = "Global Transport Xpress, Stavnsholt Gydevej 70, DK 3460 Birkerød",
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
                                    Waybillnumber = "920015378166987332598",
                                    Weight = 0.5m
                                },
                                new GudData2
                                {
                                    Dimensions = "50 x 37 x 22",
                                    ShipmentId = 34574,
                                    Waybillnumber = "920015378166987332605",
                                    Weight = 0.4m
                                },
                                new GudData2
                                {
                                    Dimensions = "50 x 37 x 22",
                                    ShipmentId = 34575,
                                    Waybillnumber = "920015378166987332618",
                                    Weight = 0.3m
                                }
                            }
                        }

                    };

                    foreach (var row in XpPrimaryRowList)
                    {
                        row.StatusCodeLocalizeKey = string.Format("XtStatusCode_{0}", row.StatusCodeText);
                    }

                }
                Xu001.DataSource = XpPrimaryRowList;
                Xu001.DataBind();
            }



        }

        protected void XuPickupStatus_Click(object sender, EventArgs e)
        {

        }

        protected void Xu001_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var Argument = e.CommandArgument;
            var name = e.CommandName;
            var source = e.CommandSource;


            int index = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = Xu001.Rows[index];

            var dd = row.Cells[0];

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

        /// <summary>
        /// Key to lookup into Localization table.
        /// </summary>
        public string StatusCodeLocalizeKey { get; set; }


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


    }

}