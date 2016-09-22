using nu.gtx.POCO.Contract.Pickup;

namespace AppCode.Pages.Pickup.EditForwarderPickup
{
    public class DropDownBoxDataPickupOperator
    {
        /// <summary>
        /// Key returned when selected.
        /// </summary>
        public PickupOperator Value { get; set; }

        /// <summary>
        /// Visual text.
        /// </summary>
        public string Text { get; set; }


        public string Sorting { get; set; }


        public override string ToString()
        {
            return Text;
        }
    }
}
