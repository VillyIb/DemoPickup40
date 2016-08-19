namespace AppCode.Pages.Pickup
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
        /// Displayed on screen as a concatenation of Length x Width x Height
        /// </summary>
        public string Dimensions { get; set; }


        /// <summary>
        /// Plain text representation of enum:PickupStatus.
        /// Used to control GUI elements.
        /// Replicated from principal GuiCustomerPickup.
        /// </summary>
        public string PickupStatusText { get; set; }


        public string Waybillnumber { get; set; }


        public decimal Weight { get; set; }


    }
}