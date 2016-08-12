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

        List<GuiData1> XpPrimaryRowList { get; set; }

        int RowCount { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(IsCallback))
            {
                XpPrimaryRowList = new List<GuiData1>();


                    var t1 =new List<GuiData1>
                {
                    new GuiData1 { Name="Alfa", RowNumber = 1},
                    new GuiData1 { Name="Bravo", RowNumber = 2 },
                    new GuiData1 { Name="Charlie", RowNumber = 3 },
                    new GuiData1 { Name="Echo", RowNumber = 4 },
                };

                foreach (var row1 in t1)
                {
                    var subrownumber = 1;
                    row1.Shipmentlist = new List<GudData2>
                    {
                        new GudData2 {Name= row1.Name.ToUpper(), RowNumber = subrownumber++ },
                        new GudData2 {Name= row1.Name.ToLower(), RowNumber = subrownumber++ },
                        new GudData2 {Name= row1.Name, RowNumber = subrownumber++ }
                    };

                    XpPrimaryRowList.Add(row1);
                    XpPrimaryRowList.Add(row1);
                }


                Xu001.DataSource = XpPrimaryRowList;
                Xu001.DataBind();
            }
        }


        List<GudData2> CurrentShipmentlist { get; set; }


        protected void Xu001_DataBinding(object sender, EventArgs e)
        {
            var t1 = sender;
            var t2 = t1.GetType().ToString();

            var gw = sender as GridView;

            //CurrentShipmentlist = XpPrimaryRowList[RowCount++].Shipmentlist;

            //gw.ro

            //gw.DataSource = CurrentShipmentlist;
            //gw.DataBind();


        }


        /// <summary>
        /// Locate first GridWiew in GridViwRow.
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private static GridView FindGridView(GridViewRow row)
        {
            foreach(TableCell control in row.Cells)
            {
                foreach(Control cell in control.Controls)
                {
                    var result = cell as GridView;
                    if(result != null) { return result; }
                }
            }

            return null;
        }


        /// <summary>
        /// First Level Grid View Create Row.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Xu001_RowCreated(object sender, GridViewRowEventArgs e)
        {
            var t1 = sender;
            var t2 = t1.GetType().ToString();

            var t3 = e;
            var t4 = e.GetType().ToString();

            if(e.Row.RowType == DataControlRowType.DataRow)
            {
                var control1 = e.Row.Controls[0];
                var t5 = control1.GetType().ToString();
                var t6 = t5;

                var cell = e.Row.Cells[0].Controls[3];


                var secondLevelGridView = FindGridView(e.Row);

                if(secondLevelGridView!=null)
                {
                    CurrentShipmentlist = XpPrimaryRowList[e.Row.RowIndex].Shipmentlist;
                    secondLevelGridView.DataSource = CurrentShipmentlist;
                }

            }


        }
    }


    public class GuiData1
    {
        public string Name { get; set; }

        public int RowNumber { get; set; }

        public List<GudData2> Shipmentlist { get; set; }

    }

    public class GudData2
    {
        public string Name { get; set; }

        public int RowNumber { get; set; }


    }

}