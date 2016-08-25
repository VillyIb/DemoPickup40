namespace AppCode.Pages.Pickup2
{
    public static class PickupUtil
    {
        #region PickupStatusCustomer

        /// <summary>
        /// Map from PickupStatus to glyphicon used as operations on CustomerPickup
        /// </summary>
        /// <param name="pickupStatuses"></param>
        /// <returns></returns>
        public static string PickupStatusToGlyphiconStatus(PickupStatusCustomer pickupStatuses)
        {
            string result = "glyphicon ";

            switch (pickupStatuses)
            {
                case PickupStatusCustomer.Undefined:
                    result += "glyphicon-question-sign";
                    break;

                case PickupStatusCustomer.CustWait:
                    result += "glyphicon-send gray";
                    break;

                case PickupStatusCustomer.CustHand:
                    result += "glyphicon-send gray";
                    break;

                case PickupStatusCustomer.CustCan:
                    result += "glyphicon-send";
                    break;

                case PickupStatusCustomer.ForwWait:
                    result += "glyphicon-remove red";
                    break;

                case PickupStatusCustomer.ForwSched:
                    result += "glyphicon-none"; // invisible
                    break;
            }

            return result;
        }


        /// <summary>
        /// Map from PickupStatus to glyphicon used when moving simpents to CustomerPickup
        /// </summary>
        /// <param name="pickupStatuses"></param>
        /// <returns></returns>
        public static string PickupStatusToGlyphiconMove(PickupStatusCustomer pickupStatuses)
        {
            string result = "glyphicon ";

            switch (pickupStatuses)
            {
                case PickupStatusCustomer.Undefined:
                    result += "glyphicon-question-sign";
                    break;

                case PickupStatusCustomer.CustWait:
                    result += "glyphicon-download-alt";
                    break;

                case PickupStatusCustomer.CustHand:
                    result += "glyphicon-download-alt";
                    break;

                case PickupStatusCustomer.CustCan:
                    result += "glyphicon-download-alt";
                    break;

                case PickupStatusCustomer.ForwWait:
                    result += "glyphicon-download-alt";
                    break;

                case PickupStatusCustomer.ForwSched:
                    result += "glyphicon-none"; // invisible
                    break;
            }

            return result;
        }


        /// <summary>
        /// Map from PickupStatus to css option "disabled" / nothing.
        /// </summary>
        /// <param name="pickupStatuses"></param>
        /// <returns></returns>
        public static bool PickupStatusToDisabled(PickupStatusCustomer pickupStatuses)
        {
            bool result = false;

            switch (pickupStatuses)
            {
                case PickupStatusCustomer.Undefined:
                    result =true;
                    break;

                case PickupStatusCustomer.CustWait:
                    break;

                case PickupStatusCustomer.CustHand:
                    break;

                case PickupStatusCustomer.CustCan:
                    break;

                case PickupStatusCustomer.ForwWait:
                    break;

                case PickupStatusCustomer.ForwSched:
                    result = true; 
                    break;
            }

            return result;
        }

        #endregion


        #region PickupStatusForwarder

        /// <summary>
        /// Map from PickupStatus to glyphicon used as operations on ForwarderPickup.
        /// </summary>
        /// <param name="pickupStatuses"></param>
        /// <returns></returns>
        public static string PickupStatusToGlyphiconStatus(PickupStatusForwarder pickupStatuses)
        {
            string result = "glyphicon ";

            switch (pickupStatuses)
            {
                case PickupStatusForwarder.Undefined:
                    result += "glyphicon-question-sign";
                    break;

                case PickupStatusForwarder.CustWait:
                    result += "glyphicon-send gray";
                    break;

                case PickupStatusForwarder.CustHand:
                    result += "glyphicon-send gray";
                    break;

                case PickupStatusForwarder.CustCan:
                    result += "glyphicon-send";
                    break;

                case PickupStatusForwarder.ForwWait:
                    result += "glyphicon-remove red";
                    break;

                case PickupStatusForwarder.ForwSched:
                    result += "glyphicon-none"; // invisible
                    break;
            }

            return result;
        }


        /// <summary>
        /// Map from PickupStatus to glyphicon used when moving simpents to CustomerPickup
        /// </summary>
        /// <param name="pickupStatuses"></param>
        /// <returns></returns>
        public static string PickupStatusToGlyphiconMove(PickupStatusForwarder pickupStatuses)
        {
            string result = "glyphicon ";

            switch (pickupStatuses)
            {
                case PickupStatusForwarder.Undefined:
                    result += "glyphicon-question-sign";
                    break;

                case PickupStatusForwarder.CustWait:
                    result += "glyphicon-download-alt";
                    break;

                case PickupStatusForwarder.CustHand:
                    result += "glyphicon-download-alt";
                    break;

                case PickupStatusForwarder.CustCan:
                    result += "glyphicon-download-alt";
                    break;

                case PickupStatusForwarder.ForwWait:
                    result += "glyphicon-download-alt";
                    break;

                case PickupStatusForwarder.ForwSched:
                    result += "glyphicon-none"; // invisible
                    break;
            }

            return result;
        }


        /// <summary>
        /// Map from PickupStatus to css option "disabled" / nothing.
        /// </summary>
        /// <param name="pickupStatuses"></param>
        /// <returns></returns>
        public static bool PickupStatusToDisabled(PickupStatusForwarder pickupStatuses)
        {
            bool result = false;

            switch (pickupStatuses)
            {
                case PickupStatusForwarder.Undefined:
                    result = true;
                    break;

                case PickupStatusForwarder.CustWait:
                    break;

                case PickupStatusForwarder.CustHand:
                    break;

                case PickupStatusForwarder.CustCan:
                    break;

                case PickupStatusForwarder.ForwWait:
                    break;

                case PickupStatusForwarder.ForwSched:
                    result = true;
                    break;
            }

            return result;
        }



        #endregion
    }
}
