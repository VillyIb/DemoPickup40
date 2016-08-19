using System;
using System.Collections.Generic;
using AppCode.Util;

namespace AppCode.Pages.Pickup
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
                    Dimensions = "50 x 40 x 20",
                    Id = CurrentId++,
                    Address = GetAddress(option),
                    PickupStatusText = "CustWait",
                    Waybillnumber = "920001012141600",
                    Weight = 0.5m
                };
            }

            switch (option)
            {
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
                    ReadyClose = new TimeSpan(15, 30, 0),
                    ReadyOpen = new TimeSpan(12, 30, 0),
                    PickupStatusText = "CustWait",
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
                        result.PickupStatusText = "CustHand";
                    }
                    break;

                case 2:
                    {
                        result.PickupStatusText = "CustWait";
                    }
                    break;

                case 3:
                    { 
                        result.PickupStatusText = "CustCan";
                    }
                    break;

                case 4:
                    {
                        result.PickupStatusText = "ForwWait";
                    }
                    break;

                case 5:
                    {
                        result.PickupStatusText = "ForwSched";
                    }
                    break;


            }

            return result;
        }


        static PickupData()
        {
            CurrentId = 37;
        }
    }

}