using System;
using System.Collections.Generic;

using nu.gtx.POCO.Contract.Pickup;

namespace Pages.Pickup.PageEditForwarderPickup
{
    public class GuiSettings
    {
        /// <summary>
        /// Only show ForwardrPickups with the 
        /// </summary>
        public List<PickupStatusForwarder> FilterPickupStatusForwarder { get; set; }


        /// <summary>
        /// Filters on TimeClose, only show today up to now + lookAheadMinutes
        /// </summary>
        public int FilterLookAheadMinutes { get; set; }


        /// <summary>
        /// Enable LookAheadMinutes filter.
        /// </summary>
        public bool FilterLookAheadEabled { get; set; }


        /// <summary>
        /// Filter on PicupDate From, defalults to today
        /// </summary>
        public DateTime? FilterPickupDateFrom { get; set; }


        /// <summary>
        /// Filter on PickupDate Until, defaults to no limit
        /// </summary>
        public DateTime? FilterPickupDateUntil { get; set; }


        /// <summary>
        /// Show Customer-/Forwarder Pickup headers with 0 shipments.
        /// </summary>
        public bool FilterForShipmentCountZero { get; set; }


        /// <summary>
        /// Show Customer-/Forwarder Pickup headers with 1 or more shipments.
        /// </summary>
        public bool FilterForShipmentCountNonZero { get; set; }


        /// <summary>
        /// Blank: no filter
        /// Value: select information for this single customer (Only available for customers on own site).
        /// </summary>
        public string FilterSingleCustomer { get; set; }


        /// <summary>
        /// null: no filter
        /// Value: select information for this single website. 
        /// </summary>
        public Guid? FilterSingleWebsiteId { get; set; }


        public int SortOption { get; set; }

    }
}
