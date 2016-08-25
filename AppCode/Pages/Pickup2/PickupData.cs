using System;
using System.Collections.Generic;
using nu.gtx.Business.Pickup.Shared;
using nu.gtx.Common1.Extensions;
using nu.gtx.Common1.Utils;
using nu.gtx.DatabaseAccess.DbShared;
using nu.gtx.POCO.Contract.Pickup;

namespace AppCode.Pages.Pickup2
{
    public class PickupData
    {
        private nu.gtx.DatabaseAccess.DbShared.DbSharedStandard DbSharedStandard { get; set; }

        private nu.gtx.Business.Pickup.EFShared.RepositoryCustomerPickup RepositoryCustomerPickup { get; set; }

        private nu.gtx.Business.Pickup.EFShared.RepositoryForwarderPickup RepositoryForwarderPickup { get; set; }

        private nu.gtx.Business.Pickup.EFShared.RepositoryShipment RepositoryShipment { get; set; }

        private nu.gtx.Business.Pickup.Shared.ControllerForwarderPickup ControllerForwarder { get; set; }

        private nu.gtx.Business.Pickup.Shared.ControllerCustomerPickup ControllerCustomer { get; set; }


        public void Init()
        {
            DbSharedStandard = new nu.gtx.DatabaseAccess.DbShared.DbSharedStandard();

            RepositoryShipment = new nu.gtx.Business.Pickup.EFShared.RepositoryShipment(DbSharedStandard);

            RepositoryCustomerPickup = new nu.gtx.Business.Pickup.EFShared.RepositoryCustomerPickup(DbSharedStandard);

            RepositoryForwarderPickup = new nu.gtx.Business.Pickup.EFShared.RepositoryForwarderPickup(DbSharedStandard);

            ControllerForwarder = new nu.gtx.Business.Pickup.Shared.ControllerForwarderPickup(
                RepositoryCustomerPickup,
                RepositoryForwarderPickup,
                RepositoryShipment
                );

            ControllerCustomer = new nu.gtx.Business.Pickup.Shared.ControllerCustomerPickup(
                RepositoryCustomerPickup,
                RepositoryForwarderPickup,
                RepositoryShipment,
                ControllerForwarder
                );
        }


        private List<GuiForwarderPickup> GetGuiForwarderPickupList()
        {
            var result = new List<GuiForwarderPickup>();

            var currentWebsiteId = new Guid("46F2BD47-4F64-4BE6-8A8A-ABF280DD780B");

            var forwarderPickupList = ControllerForwarder.GetForwarderPickupList(currentWebsiteId, SystemDateTime.Yesterday);

            var forwarderPickupListSorted = ControllerForwarder.Sort(forwarderPickupList, nu.gtx.Business.Pickup.Contract_V2B.SortFields.Location);


            foreach (var fw in forwarderPickupListSorted)
            {
                var guiForwarder = new GuiForwarderPickup();

                ((IForwarderPickup)fw).Transfer( guiForwarder);
                guiForwarder.Id = fw.Id;
                guiForwarder.Address = new GuiAddress();
                fw.Address.Transfer(guiForwarder.Address);
                guiForwarder.CustomerPickupList = new List<GuiCustomerPickup>();

                List<ICustomerPickup> customerList;
                if (RepositoryCustomerPickup.Read(out customerList, new ForwarderPickup {Id = fw.Id}))
                {
                    foreach (var customerPickup in customerList)
                    {
                        var guiCustomer = new GuiCustomerPickup();
                        customerPickup.Transfer( guiCustomer);
                        guiCustomer.Id = customerPickup.Id;
                        guiCustomer.Address = new GuiAddress();
                        customerPickup.Address.Transfer(guiCustomer.Address);

                        guiCustomer.Shipmentlist = new List<GuiShipment>();

                        List<IShipment> shipmentList;
                        if (RepositoryShipment.Read(out shipmentList, customerPickup))
                        {
                            foreach (var shipment in shipmentList)
                            {
                                var guiShipment = new GuiShipment();
                                shipment.Transfer( guiShipment);
                                guiShipment.Id = shipment.Id;
                                guiShipment.Address = new GuiAddress();
                                shipment.Address.Transfer(guiShipment.Address);

                                guiCustomer.Shipmentlist.Add(guiShipment);
                            }
                        }

                        guiForwarder.CustomerPickupList.Add(guiCustomer);
                    }
                }

                result.Add(guiForwarder);
            }

            return result;
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