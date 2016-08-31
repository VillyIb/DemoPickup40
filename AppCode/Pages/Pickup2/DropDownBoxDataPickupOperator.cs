﻿namespace AppCode.Pages.Pickup2
{
    public class DropDownBoxDataPickupOperator
    {
        /// <summary>
        /// Key returned when selected.
        /// </summary>
        public string Value { get; set; }

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
