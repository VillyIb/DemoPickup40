using System;
using System.Collections.Generic;
using System.Linq;
//using nu.gtx.Common1.Extensions;
using nu.gtx.Common1.Utils;
using AppCode.Util;
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

        private nu.gtx.Business.Pickup.EFShared.RepositoryParcelDetail RepositoryParcelDetail { get; set; }

        private nu.gtx.Business.Pickup.Shared.ControllerForwarderPickup ControllerForwarder { get; set; }

        private nu.gtx.Business.Pickup.Shared.ControllerCustomerPickup ControllerCustomer { get; set; }


        public void Init()
        {
            DbSharedStandard = new nu.gtx.DatabaseAccess.DbShared.DbSharedStandard();

            RepositoryShipment = new nu.gtx.Business.Pickup.EFShared.RepositoryShipment(DbSharedStandard);

            RepositoryCustomerPickup = new nu.gtx.Business.Pickup.EFShared.RepositoryCustomerPickup(DbSharedStandard);

            RepositoryForwarderPickup = new nu.gtx.Business.Pickup.EFShared.RepositoryForwarderPickup(DbSharedStandard);

            RepositoryParcelDetail = new nu.gtx.Business.Pickup.EFShared.RepositoryParcelDetail(DbSharedStandard);

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

                fw.Transfer(guiForwarder);
                guiForwarder.TimeClose = fw.TimeClose ?? new TimeSpan(23, 59, 59);
                guiForwarder.TimeReady = fw.TimeReady ?? new TimeSpan(0, 0, 0);
                guiForwarder.Address = new GuiAddress();
                fw.Address.Transfer(guiForwarder.Address);
                guiForwarder.CustomerPickupList = new List<GuiCustomerPickup>();

                List<ICustomerPickup> customerList;
                if (RepositoryCustomerPickup.Read(out customerList, new ForwarderPickup { Id = fw.Id }))
                {
                    foreach (var customerPickup in customerList)
                    {
                        var guiCustomer = new GuiCustomerPickup();
                        customerPickup.Transfer(guiCustomer);
                        guiCustomer.TimeClose = customerPickup.TimeClose ?? new TimeSpan(23, 59, 59);
                        guiCustomer.TimeReady = customerPickup.TimeReady ?? new TimeSpan(0, 0, 0);
                        guiCustomer.Address = new GuiAddress();
                        customerPickup.Address.Transfer(guiCustomer.Address);

                        guiCustomer.Shipmentlist = new List<GuiShipment>();

                        List<IShipment> shipmentList;
                        if (RepositoryShipment.Read(out shipmentList, customerPickup))
                        {
                            foreach (var shipment in shipmentList)
                            {
                                var guiShipment = new GuiShipment();
                                shipment.Transfer(guiShipment);
                                guiShipment.Id = shipment.Id;
                                guiShipment.Address = new GuiAddress();
                                shipment.Address.Transfer(guiShipment.Address);

                                List<IParcelDetail> parcelDetailList;
                                RepositoryParcelDetail.Read(out parcelDetailList, shipment);

                                if (parcelDetailList.Count == 0)
                                {
                                    guiShipment.Dimensions = "  No details".Replace(" ", "&nbsp;");
                                }
                                else if (parcelDetailList.Count == 1)
                                {
                                    if ("000".Equals(String.Format("{0}{1}{2}", parcelDetailList[0].Length, parcelDetailList[0].Width, parcelDetailList[0].Height)))
                                    {
                                        guiShipment.Dimensions = "  na".Replace(" ", "&nbsp;");
                                    }
                                    else
                                    {
                                        guiShipment.Dimensions = String.Format(
                                        "{3,4:0} l, {0,3:0} x {1,3:0} x {0,3:0}"   
                                        , parcelDetailList[0].Length
                                        , parcelDetailList[0].Width
                                        , parcelDetailList[0].Height
                                        , parcelDetailList[0].Length * parcelDetailList[0].Width * parcelDetailList[0].Height/1000m
                                        ).Replace(" ","&nbsp;");
                                    }
                                }
                                else
                                {
                                    // QQQ alternativ solution is to calculate sum of volume.

                                    var maxLength = parcelDetailList.Max(t => t.Length);
                                    var maxWidth = parcelDetailList.Max(t => t.Width);
                                    var maxHeight = parcelDetailList.Max(t => t.Height);

                                    var volume = parcelDetailList.Sum(t => t.Length*t.Width*t.Height);

                                    if ("000".Equals(String.Format("{0}{1}{2}", maxLength, maxWidth, maxHeight)))
                                    {
                                        guiShipment.Dimensions = String.Format(
                                            "  {0,2:0} parcels: no dimensions"
                                            , parcelDetailList.Count
                                            ).Replace(" ", "&nbsp;");
                                    }
                                    else
                                    {
                                        guiShipment.Dimensions = String.Format(
                                            "{4,4:0} l, {3,2:0} parcels:  {0,3:0}* - {1,3:0}* - {2,3:0}*, *: max values"
                                            , maxLength
                                            , maxWidth
                                            , maxHeight
                                            , parcelDetailList.Count
                                            , volume/1000m
                                            ).Replace(" ", "&nbsp;");
                                    }
                                }

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