using System;
using System.Collections.Generic;

namespace DemoPickup40.Pages.Pickup
{
    public partial class Customer03 : System.Web.UI.Page
    {

        List<GuiData1> XpPrimaryRowList { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(IsCallback))
            {
                XpPrimaryRowList = new List<GuiData1>
                {
                    new GuiData1 {
                        ActionCode="Req",
                        ActionCodeGlyphIcon = "download-alt",
                        Address ="Stavnsholt Gydevej 70",
                        FullAddress ="Global Transport Xpress, Stavnsholt Gydevej 70, DK 3460 Birkerød",
                        Pickup ="2016-08-12 from 12:10 to 14:50",
                        Note ="Henvendelse på den 'Gule gård'",
                        StatusCodeText ="CustWait",
                        RowNumber = 1,
                        Shipmentlist = new List<GudData2>
                        {
                            new GudData2
                            {
                                Dimensions = "50 x 37 x 22",
                                Waybillnumber = "920015378166987332598",
                                Weight = 0.5m
                            },
                            new GudData2
                            {
                                Dimensions = "50 x 37 x 22",
                                Waybillnumber = "920015378166987332605",
                                Weight = 0.4m
                            },
                            new GudData2
                            {
                                Dimensions = "50 x 37 x 22",
                                Waybillnumber = "920015378166987332618",
                                Weight = 0.3m
                            }
                        }
                    },

                    new GuiData1 {
                        ActionCode="Can",
                        ActionCodeGlyphIcon = "remove red",
                        Address ="Stavnsholt Gydevej 70",
                        FullAddress ="Global Transport Xpress, Stavnsholt Gydevej 70, DK 3460 Birkerød",
                        Pickup ="2016-08-12 from 12:10 to 14:50",
                        Note ="Henvendelse på den 'Gule gård'",
                        StatusCodeText ="ForwWait",
                        RowNumber = 1,
                        Shipmentlist = new List<GudData2>
                        {
                            new GudData2
                            {
                                Dimensions = "50 x 37 x 22",
                                Waybillnumber = "920015378166987332598",
                                Weight = 0.5m
                            },
                            new GudData2
                            {
                                Dimensions = "50 x 37 x 22",
                                Waybillnumber = "920015378166987332605",
                                Weight = 0.4m
                            },
                            new GudData2
                            {
                                Dimensions = "50 x 37 x 22",
                                Waybillnumber = "920015378166987332618",
                                Weight = 0.3m
                            }
                        }
                    }

                };
            }


            //foreach (var row in XpPrimaryRowList)
            //{
            //    row.StatusCodeLocalizeKey = string.Format("{0:G}", row.StatusCodeText);
            //}

            //Xu001.DataSource = XpPrimaryRowList;
            Xu001.DataBind();

        }
    }


   

}