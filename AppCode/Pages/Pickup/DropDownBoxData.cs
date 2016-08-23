using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppCode.Pages.Pickup
{
    public class DropDownBoxData
    {
        public string Key { get; set; }


        public string Value { get; set; }


        public string Sorting { get; set; }


        public override string ToString()
        {
            return Value;
        }
    }
}
