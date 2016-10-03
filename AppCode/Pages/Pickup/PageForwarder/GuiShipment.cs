using System;
using System.Linq;

using AppCode.Util;

using nu.gtx.CodeFirst.Model.Pickup;
using nu.gtx.POCO.Contract.Pickup;

namespace AppCode.Pages.Pickup.PageForwarder
{
    public class GuiShipment
    {
        /// <summary>
        /// PK, identical to Shipment.Id
        /// </summary>
        public int Id { get; set; }


        public GuiAddress Address { get; set; }


        public string CarrierName { get; set; }


        /// <summary>
        /// FK pointer to [{website}].[dbo].[aspnet_CompanyAccount]
        /// </summary>
        public int CustomerAccountId { get; set; }


        /// <summary>
        /// FK on table [{website}].[dbo].[aspnet_CompanyDB]
        /// </summary>
        public int CustomerId { get; set; }


        /// <summary>
        /// Displayed on screen as a concatenation of Length x Width x Height
        /// </summary>
        public string Dimensions { get; set; }


        /// <summary>
        /// Plain text representation of enum:PickupStatus.
        /// Used to control GUI elements.
        /// Replicated from principal GuiCustomerPickup.
        /// </summary>
        public PickupStatusCustomer PickupStatusCustomer { get; set; }


        public string Waybillnumber { get; set; }


        public decimal Weight { get; set; }


        /// <summary>
        /// Enable move of shipment between CustomerPickups.
        /// </summary>
        public bool IsMoveEnabled { get; set; }


        public bool CssDisabledCheckbox => !IsMoveEnabled;


        public GuiShipment(Shipment source)
        {
            source.Transfer(this);
            Address = new GuiAddress(source.Address);

            Address.CompareInfo = String.Format(
                "{0}, {1}, {2}, {3}, {4}, {10}, CustomerId/AccountId: {5}/{9},  Branch: {6}, State: {7},  {8:yyyy-MM-dd}"
                , Address.Name
                , Address.Street1
                , Address.Street2
                , Address.CountryCode
                , Address.Zip
                , source.CustomerId
                , source.BranchId
                , Address.State
                , source.PickupDate
                , source.CustomerAccountId
                , Address.City
                );

            var parcelDetailList = source.ParcelDetailsList;

            if (parcelDetailList.Count == 0)
            {
                Dimensions = "No details".Replace(" ", " & nbsp; ");
            }
            else if (parcelDetailList.Count == 1)
            {
                if (
                    "000".Equals(String.Format("{0}{1}{2}", parcelDetailList[0].Length, parcelDetailList[0].Width,
                        parcelDetailList[0].Height)))
                {
                    Dimensions = "  na".Replace(" ", "&nbsp;");
                }
                else
                {
                    Dimensions = String.Format(
                        "{3,4:0} l, {0,3:0} x {1,3:0} x {0,3:0}"
                        , parcelDetailList[0].Length
                        , parcelDetailList[0].Width
                        // ReSharper disable once FormatStringProblem
                        , parcelDetailList[0].Height
                        , parcelDetailList[0].Length * parcelDetailList[0].Width * parcelDetailList[0].Height / 1000m
                        ).Replace(" ", "&nbsp;");
                }
            }
            else
            {
                var maxLength = parcelDetailList.Max(t => t.Length);
                var maxWidth = parcelDetailList.Max(t => t.Width);
                var maxHeight = parcelDetailList.Max(t => t.Height);

                var volume = parcelDetailList.Sum(t => t.Length * t.Width * t.Height);

                if ("000".Equals(String.Format("{0}{1}{2}", maxLength, maxWidth, maxHeight)))
                {
                    Dimensions = String.Format(
                        "  {0,2:0} parcels: no dimensions"
                        , parcelDetailList.Count
                        ).Replace(" ", "&nbsp;");
                }
                else
                {
                    Dimensions = String.Format(
                        "{4,4:0} l, {3,2:0} parcels:  {0,3:0}* - {1,3:0}* - {2,3:0}*, *: max values"
                        , maxLength
                        , maxWidth
                        , maxHeight
                        , parcelDetailList.Count
                        , volume / 1000m
                        ).Replace(" ", "&nbsp;");
                }

            }

        }

    }

}