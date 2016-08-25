namespace AppCode.Pages.Pickup2
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
