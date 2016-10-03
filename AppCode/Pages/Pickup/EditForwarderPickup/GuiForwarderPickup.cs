using System;
using System.Linq;
using System.Collections.Generic;

using AppCode.Util;
using nu.gtx.Business.Pickup.Shared;
using nu.gtx.Common1.Utils;
using nu.gtx.POCO.Contract.Pickup;

namespace AppCode.Pages.Pickup.EditForwarderPickup
{
    public class GuiForwarderPickup
    {
        public int Id { get; set; }


        public GuiAddress Address { get; set; }


        public string CarrierNameList { get; set; }


        public List<GuiCustomerPickup> CustomerPickupList { get; set; }


        public int GroupIndex { get; set; }


        /// <summary>
        /// Contols display of Customer Pickup rows below Forwarder Pickup
        /// </summary>
        public bool IsExpandedCustomer { get; set; }


        public string Note { get; set; }


        public DateTime PickupDate { get; set; }


        public TimeSpan TimeReady { get; set; }


        public TimeSpan TimeClose { get; set; }


        /// <summary>
        /// ???CarrierId to collect parcels at customer site.
        /// </summary>
        public string PickupOperator { get; set; }


        /// <summary>
        /// Plain text representation of enum:PickupStatus
        /// Used to control GUI elements.
        /// </summary>
        public PickupStatusForwarder PickupStatusForwarder { get; set; }


        public string CssGlyphiconStatus => PickupUtil.PickupStatusToGlyphiconStatus(PickupStatusForwarder);


        public string CssGlyphiconMove => PickupUtil.PickupStatusToGlyphiconMove(PickupStatusForwarder);


        /// <summary>
        /// Shows Expand if all sub items are collapsed.
        /// Shows Collapse in any sub item is expanded.
        /// </summary>
        public string CssGlyphiconExpandGroup
        {
            get { return CustomerPickupList.Any(t => t.IsExpanded) ? "glyphicon glyphicon-triangle-top big" : "glyphicon glyphicon-triangle-bottom big"; }
        }


        public string CssGlyphiconExpandCustomer => IsExpandedCustomer ? "glyphicon glyphicon-triangle-top big" : "glyphicon glyphicon-triangle-bottom big";


        public string CssVisibleCustomer => IsExpandedCustomer ? "" : " hidden";


        public int CountCustomers => CustomerPickupList.Count;


        public int CountShipments
        {
            get { return CustomerPickupList.Sum(t => t.Shipmentlist.Count); }
        }


        private void LoadCarrierNameList(ForwarderPickupSortable forwarderPickup)
        {
            var t3 = forwarderPickup.CustomerPickupList.SelectMany(t1 => t1.ShipmentList.Select(t2 => t2.CarrierName));

            var t4 = t3.Distinct().ToList();

            var t5 = t4.Count > 0 ? t4.Aggregate((current, next) => current + ", " + next) : "none";
            CarrierNameList = t5;
        }


        public GuiForwarderPickup(ForwarderPickupSortable forwarderPickup)
        {
            forwarderPickup.Transfer(this);
            TimeClose = forwarderPickup.TimeClose;
            TimeReady = forwarderPickup.TimeReady;
            Address = new GuiAddress(forwarderPickup.Address);

            Address.CompareInfo = String.Format(
                "{0}, {1}, {2}, {3}, {4}, {9}, Pickup: {5}, State: {7},  {8:yyyy-MM-dd}"
                , Address.Name
                , Address.Street1
                , Address.Street2
                , Address.CountryCode
                , Address.Zip
                , forwarderPickup.PickupOperator
                // ReSharper disable once FormatStringProblem
                , ""
                , Address.State
                , forwarderPickup.PickupDate
                , Address.City
                );

            if (forwarderPickup.CustomerPickupList.Any(t => t.Address.Name.Equals(Address.Name, StringComparison.OrdinalIgnoreCase)))
            {
                // Same name found on at least one child, remove parent name
                Address.Name = "";
            }

            PickupStatusForwarder = forwarderPickup.PickupStatus;

            LoadCarrierNameList(forwarderPickup);

            CustomerPickupList = new List<GuiCustomerPickup>();
            foreach (var customer in forwarderPickup.CustomerPickupList)
            {
                Address.Name = Address.Name.Length > 0
                    ? String.Format("{0}, <br /> {1}", Address.Name, customer.Address.Name)
                    : customer.Address.Name;

                var t1 = new GuiCustomerPickup(customer)
                {
                    PickupStatusForwarder = PickupStatusForwarder,
                    IsMoveEnabled = customer.PickupDate >= SystemDateTime.Today, // It is not possible to move old pickups. // TODO overrule by setting.
                };
                CustomerPickupList.Add(t1);
            }
        }


    }
}