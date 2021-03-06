﻿using nu.gtx.POCO.Contract.Pickup;

namespace AppCode.Pages.Pickup2
{
    public class DropDownBoxData
    {
        /// <summary>
        /// Key returned when selected.
        /// </summary>
        public PickupStatusForwarder Value { get; set; }


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
