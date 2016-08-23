﻿using System;
using System.Collections.Generic;

namespace AppCode.Pages.Pickup
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


        /// <summary>
        /// Note is visible on screen
        /// </summary>
        public string Note { get; set; }


        public DateTime PickupDate { get; set; }


        public TimeSpan ReadyOpen { get; set; }


        public TimeSpan ReadyClose { get; set; }

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


    }
}