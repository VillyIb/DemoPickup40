using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using nu.gtx.Business.Pickup.Contract_V2B.Shared;
using nu.gtx.Business.Pickup.Contract_V2B.SiteSpecific;
using nu.gtx.Business.Pickup.Contract_V2B.SiteSpecificShipment;
using nu.gtx.Business.Pickup.EFMain;
using nu.gtx.Business.Pickup.EFShared;
using nu.gtx.Business.Pickup.Main.Import;
using nu.gtx.Business.Pickup.Shared;
using nu.gtx.Business.Pickup.SiteSpecific;
using nu.gtx.DatabaseAccess.DbMain;
using nu.gtx.DatabaseAccess.DbShared;

namespace AppCode.Services.Automation.V3.PickupLoadShipments
{
    public class Controller
    {
        private RepositoryCustomer RepositoryCustomer { get; set; }

        private IRepositoryCustomerPickup RepositoryCustomerPickup { get; set; }

        //private IRepositoryCustomerPickup RepositoryCustomerPickup { get; set; }

        //private IRepositoryLegacyPickup RepositoryLegacyPickup { get; set; }

        private IRepositoryDispositionSettings RepositoryDispositionSettings { get; set; }

        private IRepositoryLegacyPickup RepositoryLegacyPickup { get; set; }

        private IRepositorySourceParcelDetail RepositorySourceParcelDetail{ get; set; }

        private IRepositoryTransportProductMetadata RepositoryTransportProductMetadata{ get; set; }

        private IRepositoryForwarderPickup RepositoryForwarderPickup { get; set; }

        private IRepositoryShipment RepositoryShipment{ get; set; }

        private IRepositoryParcelDetail RepositoryParcelDetail{ get; set; }


        private IControllerCustomerPickup ControllerCustomerPickup { get; set; }

        private IControllerDispositionSetting ControllerDispositionSetting{ get; set; }

        private IControllerShipment ControllerShipment{ get; set; }

        private IControllerForwarderPickup ControllerForwarderPickup { get; set; }



        private ControllerSourceShipment ControllerSourceShipment { get; set; }

        private void Setup()
        {
            var dbMainStandard = new DbMainStandard();

            var dbSharedStandard  = new DbSharedStandard();

            RepositoryCustomer = new RepositoryCustomer(dbMainStandard);

            RepositoryCustomerPickup = new RepositoryCustomerPickup(dbSharedStandard);

            RepositoryDispositionSettings = new RepositoryDispositionSettings(dbMainStandard);

            RepositoryForwarderPickup = new RepositoryForwarderPickup(dbSharedStandard);

            RepositoryLegacyPickup = new RepositoryLegacyPickup(dbMainStandard);

            RepositoryParcelDetail = new RepositoryParcelDetail(dbSharedStandard);

            RepositoryShipment = new RepositoryShipment(dbSharedStandard);

            RepositorySourceParcelDetail = new RepositorySourceParcelDetail(dbMainStandard);

            RepositoryTransportProductMetadata = new RepositoryTransportProductMetadata(dbSharedStandard);


            ControllerDispositionSetting = new ControllerDispositionSetting(
                RepositoryDispositionSettings
            );
            
            ControllerForwarderPickup = new ControllerForwarderPickup(
                RepositoryCustomerPickup
                ,RepositoryForwarderPickup
                ,RepositoryShipment
                ,RepositoryParcelDetail
            );

            ControllerCustomerPickup = new ControllerCustomerPickup(
                RepositoryCustomerPickup
                , RepositoryForwarderPickup
                ,RepositoryShipment
                , ControllerForwarderPickup
            );

            ControllerShipment = new ControllerShipment(
                RepositoryDispositionSettings
                , RepositoryShipment
                , RepositoryParcelDetail
                , RepositoryLegacyPickup
                , RepositoryCustomerPickup
                , RepositoryCustomer
                , ControllerCustomerPickup
            );

            ControllerSourceShipment = new ControllerSourceShipment(
                ControllerDispositionSetting
                , ControllerShipment
                , RepositoryShipment
                , RepositoryTransportProductMetadata
                , RepositoryParcelDetail
                , RepositorySourceParcelDetail
            );
        }

        private void Teardown()
        {
            ControllerSourceShipment = null;
            ControllerShipment = null;
            ControllerCustomerPickup = null;
            ControllerForwarderPickup = null;
            ControllerDispositionSetting = null;

            RepositoryCustomer = null;
            RepositoryCustomerPickup = null;
            RepositoryDispositionSettings = null;
            RepositoryForwarderPickup = null;
            RepositoryLegacyPickup = null;
            RepositoryParcelDetail = null;
            RepositoryShipment = null;
            RepositorySourceParcelDetail = null;
            RepositoryTransportProductMetadata = null;
        }

        public void Execute()
        {
            try
            {
                Setup();

                var newShipmentList = ControllerSourceShipment.LoadShipment();

                // iteratet
                

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                Teardown();
            }
        }
    }
}
