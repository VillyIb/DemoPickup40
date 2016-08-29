﻿using System;
using System.Collections.Generic;
using AppCode.Util;
using nu.gtx.POCO.Contract.Pickup;

namespace AppCode.Pages.Pickup2
{
    public class GuiCustomerPickup
    {
        /// <summary>
        /// Primary key, identical to CustomerPickupId.
        /// </summary>
        public int Id { get; set; }


        /// <summary>
        /// Address is visible on screen
        /// It only contain Street1 of the CustomerPickup Address
        /// </summary>
        public GuiAddress Address { get; set; }


        public string CarrierNameList { get; set; }


        public int GroupIndex { get; set; }


        /// <summary>
        /// Note is visible on screen
        /// </summary>
        public string Note { get; set; }


        public DateTime PickupDate { get; set; }


        public TimeSpan TimeReady { get; set; }


        public TimeSpan TimeClose { get; set; }

        /// <summary>
        /// Plain text representation of enum:PickupStatus
        /// Used to control GUI elements.
        /// </summary>
        public PickupStatusCustomer PickupStatusCustomer { get; set; }

        public PickupStatusForwarder PickupStatusForwarder { get; set; }


        public List<GuiShipment> Shipmentlist { get; set; }


        public string CssGlyphiconStatus
        {
            get { return PickupUtil.PickupStatusToGlyphiconStatus(PickupStatusCustomer); }
        }

        public string CssGlyphiconMove
        {
            get { return PickupUtil.PickupStatusToGlyphiconMove(PickupStatusCustomer); }
        }

        public bool CssVisibleCheckbox
        {
            get { return PickupUtil.PickupStatusToDisabled(PickupStatusForwarder); }
        }

        public bool IsExpanded { get; set; }

        public string CssGlyphiconExpand
        {
            get { return IsExpanded ? "glyphicon glyphicon-triangle-top big" : "glyphicon glyphicon-triangle-bottom big"; }
        }

        public string CssVisibleShipments
        {
            get { return IsExpanded ? "" : " hidden"; }
        }


        public int CountShipments
        {
            get { return Shipmentlist.Count; }
        }


        public GuiCustomerPickup(ICustomerPickup source)
        {
            source.Transfer(this);
            // ReSharper disable ArrangeThisQualifier
            this.TimeClose = source.TimeClose ?? new TimeSpan(23, 59, 59);
            this.TimeReady = source.TimeReady ?? new TimeSpan(0, 0, 0);
            this.Address = new GuiAddress(source.Address);
            this.Shipmentlist = new List<GuiShipment>();
            // ReSharper restore ArrangeThisQualifier

            foreach (var shipment in source.ShipmentList)
            {
                this.Shipmentlist.Add(new GuiShipment(shipment));
            }
        }

    }
}