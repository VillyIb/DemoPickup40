namespace AppCode.Pages.Pickup2
{
public enum PickupStatusCustomer
    {
        // NB max 20 character names due to database coloumn size.

        Undefined = 0,

        /// <summary>
        /// Waiting for customer action
        /// </summary>
        CustWait,


        /// <summary>
        /// Selv pickup delivery.
        /// </summary>
        CustHand,


        /// <summary>
        /// Cancelled by customer.
        /// </summary>
        CustCan,


        /// <summary>
        /// Customer retuested pickup, waiting for forwarder acton.
        /// Customer can cancel pickup.
        /// 
        /// </summary>
        ForwWait,


        /// <summary>
        /// Forwarder has scheduled a 
        /// </summary>
        ForwSched,
    }
}
