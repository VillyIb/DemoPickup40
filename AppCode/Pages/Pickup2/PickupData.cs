using System;
using System.Collections.Generic;
//using nu.gtx.Common1.Extensions;
using nu.gtx.Common1.Utils;
using nu.gtx.POCO.Contract.Pickup;

namespace AppCode.Pages.Pickup2
{
    public class PickupData
    {
        private nu.gtx.DatabaseAccess.DbShared.DbSharedStandard DbSharedStandard { get; set; }

        private nu.gtx.Business.Pickup.EFShared.RepositoryCustomerPickup RepositoryCustomerPickup { get; set; }

        private nu.gtx.Business.Pickup.EFShared.RepositoryForwarderPickup RepositoryForwarderPickup { get; set; }

        private nu.gtx.Business.Pickup.EFShared.RepositoryShipment RepositoryShipment { get; set; }

        private nu.gtx.Business.Pickup.EFShared.RepositoryParcelDetail RepositoryParcelDetail { get; set; }

        private nu.gtx.Business.Pickup.Shared.ControllerForwarderPickup ControllerForwarder { get; set; }

        private nu.gtx.Business.Pickup.Shared.ControllerCustomerPickup ControllerCustomer { get; set; }


        public void Init()
        {
            if (DbSharedStandard == null)
            {
                DbSharedStandard = new nu.gtx.DatabaseAccess.DbShared.DbSharedStandard();

                RepositoryShipment = new nu.gtx.Business.Pickup.EFShared.RepositoryShipment(DbSharedStandard);

                RepositoryCustomerPickup = new nu.gtx.Business.Pickup.EFShared.RepositoryCustomerPickup(DbSharedStandard);

                RepositoryForwarderPickup =
                    new nu.gtx.Business.Pickup.EFShared.RepositoryForwarderPickup(DbSharedStandard);

                RepositoryParcelDetail = new nu.gtx.Business.Pickup.EFShared.RepositoryParcelDetail(DbSharedStandard);

                ControllerForwarder = new nu.gtx.Business.Pickup.Shared.ControllerForwarderPickup(
                    RepositoryCustomerPickup,
                    RepositoryForwarderPickup,
                    RepositoryShipment,
                    RepositoryParcelDetail
                    );

                ControllerCustomer = new nu.gtx.Business.Pickup.Shared.ControllerCustomerPickup(
                    RepositoryCustomerPickup,
                    RepositoryForwarderPickup,
                    RepositoryShipment,
                    ControllerForwarder
                    );
            }
        }


        private List<GuiForwarderPickup> GetGuiForwarderPickupList()
        {
            Init();

            var result = new List<GuiForwarderPickup>();

            var currentWebsiteId = new Guid("46F2BD47-4F64-4BE6-8A8A-ABF280DD780B"); // TODO get from local database.

            var forwarderPickupList = ControllerForwarder.GetForwarderPickupList(currentWebsiteId, SystemDateTime.Yesterday);

            var forwarderPickupListSorted = ControllerForwarder.Sort(forwarderPickupList, nu.gtx.Business.Pickup.Contract_V2B.SortFields.Location);


            foreach (var fw in forwarderPickupListSorted)
            {
                var guiForwarder = new GuiForwarderPickup(fw);

                result.Add(guiForwarder);
            }

            return result;
        }


        public bool GetForwarderPickup(out IForwarderPickup  forwarderPickup, int forwarderPickupId)
        {
            Init();
            return RepositoryForwarderPickup.Read(out forwarderPickup, forwarderPickupId);
        }


        public void UpdateDatabase()
        {
            RepositoryForwarderPickup.UpdateDatabase();
        }


        public GuiContainer GetGuiContainer(int option)
        {
            var result = new GuiContainer
            {
                ForwarderPickupList = GetGuiForwarderPickupList()
            };

            return result;
        }

        // -- Class

        static PickupData()
        {
        }
    }

}