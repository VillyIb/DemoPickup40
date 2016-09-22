using System;
using System.Collections.Generic;
using System.Linq;
using nu.gtx.DatabaseAccess.DbMain;
//using nu.gtx.Common1.Extensions;
using nu.gtx.POCO.Contract.Pickup;

namespace AppCode.Pages.Pickup.EditForwarderPickup
{
    public class PickupData
    {
        private nu.gtx.DatabaseAccess.DbShared.DbSharedStandard DbSharedStandard { get; set; }

        private DbMainStandard DbMainStandard { get; set; }

        private nu.gtx.Business.Pickup.EFShared.RepositoryCustomerPickup RepositoryCustomerPickup { get; set; }

        private nu.gtx.Business.Pickup.EFShared.RepositoryForwarderPickup RepositoryForwarderPickup { get; set; }

        private nu.gtx.Business.Pickup.EFShared.RepositoryShipment RepositoryShipment { get; set; }

        private nu.gtx.Business.Pickup.EFShared.RepositoryParcelDetail RepositoryParcelDetail { get; set; }


        private nu.gtx.Business.Pickup.EFMain.RepositoryCustomer RepositoryCustomer { get; set; }

        private nu.gtx.Business.Pickup.Shared.ControllerForwarderPickup ControllerForwarder { get; set; }

        //private nu.gtx.Business.Pickup.Shared.ControllerCustomerPickup ControllerCustomer { get; set; }

        public GuiSettings GuiSettings { get; set; }

        public void Init()
        {
            if (DbSharedStandard == null)
            {
                DbSharedStandard = new nu.gtx.DatabaseAccess.DbShared.DbSharedStandard();

                DbMainStandard = new DbMainStandard();

                RepositoryShipment = new nu.gtx.Business.Pickup.EFShared.RepositoryShipment(DbSharedStandard);

                RepositoryCustomerPickup = new nu.gtx.Business.Pickup.EFShared.RepositoryCustomerPickup(DbSharedStandard);

                RepositoryForwarderPickup =
                    new nu.gtx.Business.Pickup.EFShared.RepositoryForwarderPickup(DbSharedStandard);

                RepositoryParcelDetail = new nu.gtx.Business.Pickup.EFShared.RepositoryParcelDetail(DbSharedStandard);

                RepositoryCustomer = new nu.gtx.Business.Pickup.EFMain.RepositoryCustomer(DbMainStandard);

                ControllerForwarder = new nu.gtx.Business.Pickup.Shared.ControllerForwarderPickup(
                    RepositoryCustomerPickup,
                    RepositoryForwarderPickup,
                    RepositoryShipment,
                    RepositoryParcelDetail
                    );

                //ControllerCustomer = new nu.gtx.Business.Pickup.Shared.ControllerCustomerPickup(
                //    RepositoryCustomerPickup,
                //    RepositoryForwarderPickup,
                //    RepositoryShipment,
                //    ControllerForwarder
                //    );
            }
        }


        public List<PickupStatusForwarder> FilterPickupStatus { get; set; }

        public int? LookForward { get; set; }

        public int? NumberOfShipments { get; set; }

        public DateTime? DatePickupBegin { get; set; }

        public DateTime? DatePickupEnd { get; set; }


        private bool IncludeGroup(PickupStatusForwarder pickupStatusForwarder)
        {
            return GuiSettings.FilterPickupStatusForwarder.Count == 0
                   || GuiSettings.FilterPickupStatusForwarder.Any(t => t == pickupStatusForwarder);
        }


        public List<IForwarderPickupSortable> SortAndFilter(List<IForwarderPickupSortable> source)
        {
            var result = new List<IForwarderPickupSortable>();
            var empty = new List<IForwarderPickupSortable>(0);

            // 1 Split into groups
            var custCan1 = source.Where(t => t.PickupStatus == PickupStatusForwarder.CustCan);
            var custHand1 = source.Where(t => t.PickupStatus == PickupStatusForwarder.CustHand);
            var custWait1 = source.Where(t => t.PickupStatus == PickupStatusForwarder.CustWait);
            var forwSched1 = source.Where(t => t.PickupStatus == PickupStatusForwarder.ForwSched);
            var forwWait1 = source.Where(t => t.PickupStatus == PickupStatusForwarder.ForwWait);
            var permColl1 = source.Where(t => t.PickupStatus == PickupStatusForwarder.PermColl);

            // 2 sort each group
            var custCan2 = custCan1.OrderBy(t => t.Address.CountryCode).ThenBy(t => t.Address.Zip).ThenBy(t => t.Address.Street1).ToList();
            var custHand2 = custHand1.OrderBy(t => t.Address.CountryCode).ThenBy(t => t.Address.Zip).ThenBy(t => t.Address.Street1).ToList();
            var custWait2 = custWait1.OrderBy(t => t.TimeClose).ThenBy(t => t.Address.CountryCode).ThenBy(t => t.Address.Zip).ThenBy(t => t.Address.Street1).ToList();
            var forwSched2 = forwSched1.OrderBy(t => t.Address.CountryCode).ThenBy(t => t.Address.Zip).ThenBy(t => t.Address.Street1).ToList();
            var forwWait2 = forwWait1.OrderBy(t => t.TimeClose).ThenBy(t => t.Address.CountryCode).ThenBy(t => t.Address.Zip).ThenBy(t => t.Address.Street1).ToList();
            var permColl2 = permColl1.OrderBy(t => t.Address.CountryCode).ThenBy(t => t.Address.Zip).ThenBy(t => t.Address.Street1).ToList();

            // 3 filter on group
            var custCan3 = IncludeGroup(PickupStatusForwarder.CustCan) ? custCan2 : empty;
            var custHand3 = IncludeGroup(PickupStatusForwarder.CustHand) ? custHand2 : empty;
            var custWait3 = IncludeGroup(PickupStatusForwarder.CustWait) ? custWait2 : empty;
            var forwSched3 = IncludeGroup(PickupStatusForwarder.ForwSched) ? forwSched2 : empty;
            var forwWait3 = IncludeGroup(PickupStatusForwarder.ForwWait) ? forwWait2 : empty;
            var permColl3 = IncludeGroup(PickupStatusForwarder.PermColl) ? permColl2 : empty;

            // 4 filter on number of shipments - only valid for Permanent Collection 
            var permColl4 = new List<IForwarderPickupSortable>();
            if (permColl3.Count > 0)
            {
                // ReSharper disable once LoopCanBeConvertedToQuery
                foreach (var forwarderPickup in permColl3)
                {
                    var count = forwarderPickup.CustomerPickupList.Sum(t => t.ShipmentList.Count);

                    if (GuiSettings.FilterForShipmentCountNonZero && count > 0 ||
                        GuiSettings.FilterForShipmentCountZero && count == 0)
                    {
                        permColl4.Add(forwarderPickup);
                    }
                }
            }

            // 5 filter on timeClose an number of minutes to look forward - only valid for CustWait and ForwWait
            var custWait4 = new List<IForwarderPickupSortable>();
            if (custWait3.Count > 0)
            {
                if (GuiSettings.FilterLookAheadEabled)
                {
                    var threshold = DateTime.Now.AddMinutes(GuiSettings.FilterLookAheadMinutes).Subtract(DateTime.Now.Date);

                    // ReSharper disable once LoopCanBeConvertedToQuery
                    foreach (var forwarderPickup in custWait3)
                    {
                        if (forwarderPickup.TimeClose < threshold)
                        {
                            custWait4.Add(forwarderPickup);
                        }
                    }
                }
                else
                {
                    custWait4 = custWait3;
                }
            }

            switch (GuiSettings.SortOption)
            {
                case 1:
                    {
                        // merge together in specific group order

                        result.AddRange(forwWait3); // Pending on Forwarder
                        result.AddRange(custWait4); // Pending on Customer
                        result.AddRange(custCan3); // Cancelled by customer
                        result.AddRange(custHand3); // No Collection (handled by customer)
                        result.AddRange(permColl4); // Permanent Collection
                        result.AddRange(forwSched3); // Schecduled by Forwarder
                    }
                    break;

                case 2:
                    {
                        result.AddRange(forwWait3); // Pending on Forwarder
                        result.AddRange(custWait3); // Pending on Customer
                        result.AddRange(custCan3); // Cancelled by customer
                        result.AddRange(custHand3); // No Collection (handled by customer)
                        result.AddRange(permColl4); // Permanent Collection
                        result.AddRange(forwSched3); // Schecduled by Forwarder

                        result = result.OrderBy(t => t.Address.CountryCode).ThenBy(t => t.Address.Zip).ThenBy(t => t.Address.Street1).ThenBy(t => t.Address.Street2).ThenBy(t => t.Address.Name).ToList();

                    }
                    break;

                default:
                    result.AddRange(source);
                    break;
            }

            return result;
        }


        private List<GuiForwarderPickup> GetGuiForwarderPickupList()
        {
            //SystemDateTime.SetTime(new DateTime(2016, 9, 12), 1);

            Init();

            var result = new List<GuiForwarderPickup>();

            var currentWebsiteId = new Guid("46F2BD47-4F64-4BE6-8A8A-ABF280DD780B"); // TODO get from local database.

            //var forwarderPickupList = ControllerForwarder.GetForwarderPickupList(currentWebsiteId, SystemDateTime.Yesterday.AddDays(-1));
            var forwarderPickupList = ControllerForwarder.GetForwarderPickupList(currentWebsiteId, DatePickupBegin, DatePickupEnd, FilterPickupStatus, LookForward, NumberOfShipments);

            var useFilterAndSort = true;

            if (!(String.IsNullOrWhiteSpace(GuiSettings.FilterSingleCustomer)) && !("-1".Equals(GuiSettings.FilterSingleCustomer)))
            {
                int selectedCustomer;
                if (int.TryParse(GuiSettings.FilterSingleCustomer, out selectedCustomer))
                {
                    // filter is active
                    forwarderPickupList = forwarderPickupList.Where(t => t.CustomerPickupList.Any(t2 => t2.FK_Customer_Id == selectedCustomer)).ToList();
                    useFilterAndSort = false;
                }
            }

            var forwarderPickupListSorted = useFilterAndSort ? SortAndFilter(forwarderPickupList) : forwarderPickupList;

            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (var fw in forwarderPickupListSorted)
            {
                var guiForwarder = new GuiForwarderPickup(fw);

                result.Add(guiForwarder);
            }

            return result;
        }


        private List<GuiCustomer> GetGuiCustomer()
        {
            var result = new List<GuiCustomer>();

            List<aspnet_CompanyDB> t1;
            if (RepositoryCustomer.Read(out t1))
            {
                // ReSharper disable once LoopCanBeConvertedToQuery
                foreach (var customer in t1)
                {
                    if (customer.DisabledCompany.HasValue && 1 == customer.DisabledCompany.Value) { continue; }

                    result.Add(
                        new GuiCustomer(customer)
                        );
                }
            }

            result = result.OrderBy(t => t.Name).ToList();
            result.Insert(
                0
                , new GuiCustomer
                {
                    Id = -1
                    , Name = "Show All Customers"
                }
            );

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
                , CustomerList = GetGuiCustomer()
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

    }

}