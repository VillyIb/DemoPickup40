using System;
using System.Collections.Generic;
using AppCode.Util;
using nu.gtx.POCO.Contract.Pickup;

namespace AppCode.Pages.zPickup
{
    public static class PickupData
    {
        private static int CurrentId { get; set; }

        public static GuiAddress GetAddress(int option, GuiAddress seed = null)
        {
            GuiAddress result;

            if (seed != null)
            {
                seed.Convert(out result);
                result.Id = CurrentId++;
            }
            else
            {
                result = new GuiAddress
                {
                    Attention = "",
                    City = "Birkerød",
                    CountryCode = "DK",
                    Email = "",
                    Id = CurrentId++,
                    Name = "Global Transport Xpress",
                    Phone = "",
                    State = "",
                    Street1 = "Stavnsholt Gydevej 70",
                    Street2 = "",
                    Zip = "3390"
                };
            }

            switch (option)
            {
                case 1:
                    {
                        result.Street1 = "Stavnsholt Gydevej 71";
                    }
                    break;

                case 2:
                    {
                        result.Street1 = "Stavnsholt Gydevej 72";
                    }
                    break;

                case 3:
                    {
                        result.Street1 = "Stavnsholt Gydevej 73";
                    }
                    break;

                case 4:
                    {
                        result.Street1 = "Stavnsholt Gydevej 74";
                    }
                    break;

                case 5:
                    {
                        result.Street1 = "Stavnsholt Gydevej 75";
                    }
                    break;


            }

            return result;
        }


        public static GuiShipment GetGuiShipment(int option, GuiShipment seed = null)
        {
            GuiShipment result;

            if (seed != null)
            {
                seed.Convert(out result);
                result.Address = GetAddress(option, seed.Address);
            }
            else
            {
                result = new GuiShipment
                {
                    CarrierName = "DHL",
                    Dimensions = "50 x 40 x 20",
                    Id = CurrentId++,
                    Address = GetAddress(option),
                    PickupStatusCustomer = PickupStatusCustomer.CustWait,
                    Waybillnumber = "920001012141600",
                    Weight = 0.5m
                };
            }

            switch (option)
#pragma warning disable 1522
            {
            }
#pragma warning restore 1522

            var t1 = result.Id % 5 + 1;

            switch (t1)
            {
                case 1:
                    {
                        result.CarrierName = "Post Nord";
                    }
                    break;

                case 2:
                    {
                        result.CarrierName = "DHL Express";
                    }
                    break;

                case 3:
                    {
                        result.CarrierName = "GLS";
                    }
                    break;

                case 4:
                    {
                        result.CarrierName = "FedEx";
                    }
                    break;

                case 5:
                    {
                        result.CarrierName = "DHL Freight";
                    }
                    break;

            }

            result.Waybillnumber = result.Waybillnumber.Substring(0, result.Waybillnumber.Length - 2) + result.Id;

            return result;
        }


        public static GuiCustomerPickup GetGuiCustomerPickup(int option, GuiCustomerPickup seed = null)
        {
            GuiCustomerPickup result;

            if (seed != null)
            {
                seed.Convert(out result);
                result.Id = CurrentId++;
                result.Address = GetAddress(option, result.Address);
                var t1 = result.Shipmentlist;
                result.Shipmentlist.Clear();
                foreach (var current in t1)
                {
                    result.Shipmentlist.Add(GetGuiShipment(option, current));
                }
            }
            else
            {
                result = new GuiCustomerPickup
                {
                    Address = GetAddress(option),
                    Id = CurrentId++,
                    Note = "Henvendelse i den gule gård",
                    PickupDate = new DateTime(2016, 8, 19),
                    TimeClose = new TimeSpan(15, 30, 0),
                    TimeReady = new TimeSpan(12, 30, 0),
                    PickupStatusCustomer = PickupStatusCustomer.CustWait,
                    Shipmentlist = new List<GuiShipment>
                    {
                        GetGuiShipment(option),
                        GetGuiShipment(option),
                        GetGuiShipment(option)
                    }
                };
            }

            switch (option)
            {
                case 1:
                    {
                        result.PickupStatusCustomer = PickupStatusCustomer.CustHand;
                    }
                    break;

                case 2:
                    {
                        result.PickupStatusCustomer = PickupStatusCustomer.CustWait;
                    }
                    break;

                case 3:
                    {
                        result.PickupStatusCustomer = PickupStatusCustomer.CustCan;
                    }
                    break;

                case 4:
                    {
                        result.PickupStatusCustomer = PickupStatusCustomer.ForwWait;
                    }
                    break;

                case 5:
                    {
                        result.PickupStatusCustomer = PickupStatusCustomer.ForwSched;
                    }
                    break;


            }

            return result;
        }


        public static GuiForwarderPickup GetGuiForwarderPickup(int option, GuiForwarderPickup seed = null)
        {
            GuiForwarderPickup result;

            if (seed != null)
            {
                seed.Convert(out result);
                result.Id = CurrentId++;
            }
            else
            {
                result = new GuiForwarderPickup
                {
                    Address = GetAddress(option),
                    CustomerPickupList = new List<GuiCustomerPickup>(),
                    Id = CurrentId++,
                    Note = "Flere afhentninger på samme adresse",
                    PickupDate = new DateTime(2016, 08, 19),
                    PickupStatusForwarder = PickupStatusForwarder.ForwWait,
                    TimeClose = new TimeSpan(15, 15, 0),
                    TimeReady = new TimeSpan(12, 12, 13)
                };

                result.CustomerPickupList.Add(GetGuiCustomerPickup(option));
                result.CustomerPickupList.Add(GetGuiCustomerPickup(option));
                result.CustomerPickupList.Add(GetGuiCustomerPickup(option));
            }

            return result;
        }


        public static GuiContainer GetGuiContainer(int option)
        {
            var result = new GuiContainer
            {
                ForwarderPickupList = new List<GuiForwarderPickup>
                {
                    GetGuiForwarderPickup(1),
                    GetGuiForwarderPickup(2),
                    GetGuiForwarderPickup(3),
                    GetGuiForwarderPickup(4),
                    GetGuiForwarderPickup(5)
                }
            };

            return result;
        }

        // -- Class

        static PickupData()
        {
            CurrentId = 37;
        }
    }

}