using System;
using System.Collections.Generic;
using System.Linq;
//using nu.gtx.Common1.Extensions;
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


        public List<PickupStatusForwarder> FilterPickupStatus { get; set; }

        public int? LookForward { get; set; }

        public int? NumberOfShipments { get; set; }

        public DateTime? DatePickupBegin { get; set; }

        public DateTime? DatePickupEnd { get; set; }


        private List<GuiForwarderPickup> GetGuiForwarderPickupList()
        {
            Init();

            var result = new List<GuiForwarderPickup>();

            var currentWebsiteId = new Guid("46F2BD47-4F64-4BE6-8A8A-ABF280DD780B"); // TODO get from local database.

            //var forwarderPickupList = ControllerForwarder.GetForwarderPickupList(currentWebsiteId, SystemDateTime.Yesterday.AddDays(-1));
            var forwarderPickupList  = ControllerForwarder.GetForwarderPickupList(currentWebsiteId, DatePickupBegin, DatePickupEnd,FilterPickupStatus, LookForward, NumberOfShipments);

            var forwarderPickupListSorted = ControllerForwarder.Sort(forwarderPickupList, nu.gtx.Business.Pickup.Contract_V2B.SortFields.Location);


            foreach (var fw in forwarderPickupListSorted)
            {
                var guiForwarder = new GuiForwarderPickup(fw);

                result.Add(guiForwarder);
            }

            return result;
        }


        public bool Refresh(GuiContainer guiContainer, GuiForwarderPickup forwarderPickup)
        {
            if (forwarderPickup == null) { return false; }

            Init();
            IForwarderPickupSortable t1;
            if (ControllerForwarder.Refresh(out t1, forwarderPickup.Id))
            {
                guiContainer.ForwarderPickupList.Remove(forwarderPickup);
                var t2 = new GuiForwarderPickup(t1);
                guiContainer.ForwarderPickupList.Add(t2);

                guiContainer.ForwarderPickupList = guiContainer.ForwarderPickupList
                    .OrderBy(t => t.PickupDate)
                    .ThenBy(t => t.Address.CountryCode)
                    .ThenBy(t => t.Address.State)
                    //.ThenBy(t => t.City)
                    .ThenBy(t => t.Address.Zip)
                    .ThenBy(t => t.Address.Street1)
                    .ThenBy(t => t.Address.Street2)
                    .ThenBy(t => t.Address.Name)
                    .ThenBy(t => t.GroupIndex)
                    .ToList();

                return true;
            }

            return false;
        }


        public bool Read(out IForwarderPickup forwarderPickup, int forwarderPickupId)
        {
            Init();
            return RepositoryForwarderPickup.Read(out forwarderPickup, forwarderPickupId);
        }


        public bool Read(out IShipment shipment, int shipmentId)
        {
            Init();
            return RepositoryShipment.Read(out shipment, shipmentId);
        }


        public bool Read(out ICustomerPickup customerPickup, int customerPickupId)
        {
            Init();
            return RepositoryCustomerPickup.Read(out customerPickup, customerPickupId);
        }


        public void UpdateDatabase()
        {
            RepositoryForwarderPickup?.UpdateDatabase();
        }


        public GuiContainer GetGuiContainer(int option)
        {
            var result = new GuiContainer
            {
                ForwarderPickupList = GetGuiForwarderPickupList()
            };

            return result;
        }

        public GuiContainer Update(GuiContainer guiContainer, GuiForwarderPickup forwarderPickup)
        {
            var current = guiContainer.ForwarderPickupList.FirstOrDefault(t => t.Id == forwarderPickup.Id);
            if (current != null)
            {

            }


            return guiContainer;
        }

        // -- Class

        static PickupData()
        {
        }
    }

}