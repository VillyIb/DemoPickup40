using System;
using System.Collections.Generic;
using System.Linq;
using AppCode.Util;
using nu.gtx.Common1.Utils;
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


        /// <summary>
        /// Enable move of shipment between CustomerPickups.
        /// </summary>
        public bool IsMoveEnabled { get; set; }


        public bool CssDisabledCheckbox
        {
            get { return !IsMoveEnabled; }
        }


        private void LoadCarrierNameList(ICustomerPickup customerPickup)
        {
            var t1 = customerPickup.ShipmentList.Select(t => t.CarrierName).Distinct().OrderBy(t => t).ToList();
            var t2 = t1.Count > 0 ? t1.Aggregate((current, next) => current + ", " + next) : "";
            CarrierNameList = t2;
        }


        public GuiCustomerPickup(ICustomerPickup customerPickup)
        {
            customerPickup.Transfer(this);
            TimeClose = customerPickup.TimeClose ?? new TimeSpan(23, 59, 59);
            TimeReady = customerPickup.TimeReady ?? new TimeSpan(0, 0, 0);
            Address = new GuiAddress(customerPickup.Address);
            PickupStatusCustomer = customerPickup.PickupStatus;
            
            LoadCarrierNameList(customerPickup);

            var timeClose = customerPickup.PickupDate.Add(customerPickup.TimeClose ?? new TimeSpan(23,59,00));

            Shipmentlist = new List<GuiShipment>();
            foreach (var shipment in customerPickup.ShipmentList)
            {
                var t1 = new GuiShipment(shipment)
                {
                    PickupStatusCustomer = customerPickup.PickupStatus,
                    IsMoveEnabled = SystemDateTime.Now < timeClose,  // Move is only enabled until TimeClose is passed. // TODO overrule by setting.
                };

                Shipmentlist.Add(t1);
            }
        }

    }
}