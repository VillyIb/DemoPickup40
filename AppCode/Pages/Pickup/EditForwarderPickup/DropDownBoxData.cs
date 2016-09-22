using nu.gtx.POCO.Contract.Pickup;

namespace AppCode.Pages.Pickup.EditForwarderPickup
{
    public class DropDownBoxData
    {
        public PickupStatusForwarder Value { get; set; }


        public string Text { get; set; }


        public string Sorting { get; set; }


        public override string ToString()
        {
            return Text;
        }
    }
}
