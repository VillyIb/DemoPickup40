﻿using System;
using System.Linq;
using System.Collections.Generic;
using AppCode.Util;
using nu.gtx.POCO.Contract.Pickup;

namespace AppCode.Pages.Pickup2
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


        public string CssGlyphiconStatus
        {
            get { return PickupUtil.PickupStatusToGlyphiconStatus(PickupStatusForwarder); }
        }


        public string CssGlyphiconMove
        {
            get { return PickupUtil.PickupStatusToGlyphiconMove(PickupStatusForwarder); }
        }


        /// <summary>
        /// Shows Expand if all sub items are collapsed.
        /// Shows Collapse in any sub item is expanded.
        /// </summary>
        public string CssGlyphiconExpandGroup
        {
            get { return CustomerPickupList.Any(t => t.IsExpanded) ? "glyphicon glyphicon-triangle-top big" : "glyphicon glyphicon-triangle-bottom big"; }
        }


        public string CssGlyphiconExpandCustomer
        {
            get { return IsExpandedCustomer ? "glyphicon glyphicon-triangle-top big" : "glyphicon glyphicon-triangle-bottom big"; }
        }


        public string CssVisibleCustomer
        {
            get { return IsExpandedCustomer ? "" : " hidden"; }
        }


        public int CountCustomers
        {
            get { return CustomerPickupList.Count; }
        }


        public int CountShipments
        {
            get { return CustomerPickupList.Sum(t => t.Shipmentlist.Count); }
        }


        private void LoadCarrierNameList(IForwarderPickupSortable forwarderPickup)
        {
            var t3 = forwarderPickup.CustomerPickupList.SelectMany(t1 => t1.ShipmentList.Select(t2 => t2.CarrierName));

            var t4 = t3.Distinct().ToList();

            var t5 = t4.Count > 0 ? t4.Aggregate((current, next) => current + ", " + next) : "none";
            CarrierNameList = t5;
        }


        public GuiForwarderPickup(IForwarderPickupSortable forwarderPickup)
        {
            forwarderPickup.Transfer(this);
            TimeClose = forwarderPickup.TimeClose ?? new TimeSpan(23, 59, 59);
            TimeReady = forwarderPickup.TimeReady ?? new TimeSpan(0, 0, 0);
            Address = new GuiAddress(forwarderPickup.Address);
            PickupStatusForwarder = forwarderPickup.PickupStatus;

            LoadCarrierNameList(forwarderPickup);

            CustomerPickupList = new List<GuiCustomerPickup>();
            foreach (var customer in forwarderPickup.CustomerPickupList)
            {
                var t1 = new GuiCustomerPickup(customer) {PickupStatusForwarder = PickupStatusForwarder};
                CustomerPickupList.Add(t1);
            }
        }


    }
}