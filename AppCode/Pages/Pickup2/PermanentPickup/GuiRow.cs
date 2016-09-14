using System;
using System.Collections.Generic;
using nu.gtx.POCO.Contract.Pickup;

namespace AppCode.Pages.Pickup2.PermanentPickup
{
    public class GuiRow
    {
        public string AccountName { get; set; }

        public string AccountNameLong { get; set; }

        public string CustomerName { get; set; }

        public string CustomerNameLong { get; set; }

        /// <summary>
        /// Enabled on mondays.
        /// </summary>
        //public bool Enabled1 { get; set; }


        //public bool Enabled2 { get; set; }


        //public bool Enabled3 { get; set; }


        //public bool Enabled4 { get; set; }


        //public bool Enabled5 { get; set; }


        //public bool Enabled6 { get; set; }


        //public bool Enabled7 { get; set; }

        public string EnabledWeek { get; set; }

        /// <summary>
        /// Master enable.
        /// </summary>
        public bool EnabledMaster { get; set; }


        Guid ForwarderWebsiteId { get; set; }


        // ReSharper disable InconsistentNaming

        public int FK_Customer_Id { get; set; }


        public int FK_Account_Id { get; set; }


        // ReSharper restore InconsistentNaming


        /// <summary>
        /// True if a row exists in table [WEBSITE].[Pickup].[PermanentPickup]
        /// </summary>
        public bool HasPermanentPickupRow { get; set; }


        public int Id { get; set; }


        public PickupOperator PickupOperator { get; set; }


        public string Text { get; set; }


        public TimeSpan TimeClose { get; set; }


        public TimeSpan TimeReady { get; set; }


        public GuiRow()
        {
            //Enabled1 = true;
            //Enabled2 = true;
            //Enabled3 = true;
            //Enabled4 = true;
            //Enabled5 = true;
        }
    }
}
