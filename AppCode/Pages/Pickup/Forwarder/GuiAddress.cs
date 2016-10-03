using AppCode.Util;
using nu.gtx.CodeFirst.Model.Pickup;

namespace AppCode.Pages.Pickup.Forwarder
{
    public class GuiAddress
    {
        public int Id { get; set; }
        public string Attention { get; set; }
        public string CountryCode { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        //public string ParentType { get; set; }
        public string Phone { get; set; }
        public string State { get; set; }
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        //public int UniqueSequence { get; set; }
        public string Zip { get; set; }

        public string CompareInfo { get; set; }


        /// <summary>
        /// Full Address is visible as ToolTip on same field as Address1
        /// It Contains the natural combination of CustomerPickup Address Properties.
        /// </summary>
        public string FullAddress1 => string.Format(
            "{0}{1}{2}, {3} {4} {5}",
            Street1,
            string.IsNullOrWhiteSpace(Street2) ? "" : ", " + Street2,
            string.IsNullOrWhiteSpace(State) ? "" : ", " + State,
            CountryCode,
            Zip,
            City
            );


        public string Address1 => string.Format(
            "{0}{1}, {2}, {3}"
            , "DK".Equals(CountryCode) ? "" :  CountryCode + ", "
            , Zip
            , Street1
            , City
                     
            );


        public string Address2 => string.Format(
            "{0}"
            , Name
            );


        public GuiAddress(Address source)
        {
            source.Transfer(this);
        }
    }
}