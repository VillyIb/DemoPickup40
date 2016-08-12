using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoPickup40.App_Code.Pages.Pickup
{
    public class PickupData
    {
        public List<GuiData1> GetTestData()
        {
            return new List<GuiData1>
            {
                new GuiData1
                {
                    Address = "Stansholt Gydevej 70",
                    Pickup = "2016-08-12 fra 12:30 til 14:30",
                    Note = "Henvendelse den gule gård",
                    Status ="Afventer bestilling",
                    Shipmentlist = new List<GudData2>
                    {
                        new GudData2
                        {
                            WaybillNumber = "920124678912486521",
                            Weight = "0,5",
                            Dimension ="50 x 40 x 30"
                        },
                        new GudData2
                        {
                            WaybillNumber = "920124678912486535",
                            Weight = "0,5",
                            Dimension ="50 x 40 x 30"
                        },
                        new GudData2
                        {
                            WaybillNumber = "920124678912486547",
                            Weight = "0,5",
                            Dimension ="50 x 40 x 30"
                        },
                    }
                }
            };
        }
    }

    public class GuiData1
    {
        public string Address { get; set; }

        public string Pickup { get; set; }

        public string Note { get; set; }

        public string Status { get; set; }

        public List<GudData2> Shipmentlist { get; set; }

    }

    public class GudData2
    {
        public string WaybillNumber { get; set; }

        public string Weight { get; set; }

        public string Dimension { get; set; }

    }


}