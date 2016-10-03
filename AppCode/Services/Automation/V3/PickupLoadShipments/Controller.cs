using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

using nu.gtx.Business.Pickup.EFMain;
using nu.gtx.Business.Pickup.EFShared;
using nu.gtx.Business.Pickup.Main;
using nu.gtx.Business.Pickup.Main.Import;
using nu.gtx.Business.Pickup.Shared;
using nu.gtx.CodeFirst.DataAccess.Context;
using nu.gtx.Common1.Utils;
using nu.gtx.DbMain.Standard.PM;
using nu.gtx.DbShared.Standard.PM;

namespace AppCode.Services.Automation.V3.PickupLoadShipments
{
    public class Controller
    {
        private ContextSharedPickup ContextSharedPickup { get; set; }

        private RepositoryCustomer RepositoryCustomer { get; set; }

        private RepositoryCustomerPickup RepositoryCustomerPickup { get; set; }

        private RepositoryDispositionSettingsV2 RepositoryDispositionSettings { get; set; }

        private RepositoryForwarderPickup RepositoryForwarderPickup { get; set; }

        private RepositoryLegacyPickup RepositoryLegacyPickup { get; set; }

        private RepositoryParcelDetail RepositoryParcelDetail { get; set; }

        private RepositoryShipment RepositoryShipment { get; set; }

        private RepositorySourceParcelDetail RepositorySourceParcelDetail { get; set; }

        private RepositorySourceShipment RepositorySourceShipment { get; set; }

        private RepositoryTransportProductMetadata RepositoryTransportProductMetadata { get; set; }


        private ControllerCustomerPickup ControllerCustomerPickup { get; set; }

        private ControllerDispositionSetting ControllerDispositionSetting { get; set; }

        private ControllerShipment ControllerShipment { get; set; }

        private ControllerForwarderPickup ControllerForwarderPickup { get; set; }



        private ControllerSourceShipment ControllerSourceShipment { get; set; }

        private void Setup()
        {
            var dbMainStandard = new DbMainStandard();

            var dbSharedStandard = new DbSharedStandard();

            var connectionStringMain = ConfigurationManager.ConnectionStrings["EF_CodeFirst_Test"]; // TODO Change to live
            var connectionBuilderMain = new SqlConnectionStringBuilder(connectionStringMain.ConnectionString);
            var contextMainPickup = new ContextMainPickup(connectionBuilderMain);

            var connectionStringShared = ConfigurationManager.ConnectionStrings["EF_CodeFirst_Test"]; // TODO Change to live
            var connectionBuilderShared = new SqlConnectionStringBuilder(connectionStringShared.ConnectionString);
            var contextSharedPickup = new ContextSharedPickup(connectionBuilderShared);


            RepositoryCustomer = new RepositoryCustomer(dbMainStandard);

            RepositoryCustomerPickup = new RepositoryCustomerPickup(contextSharedPickup);

            RepositoryDispositionSettings = new RepositoryDispositionSettingsV2(contextMainPickup);

            RepositoryForwarderPickup = new RepositoryForwarderPickup(contextSharedPickup);

            RepositoryLegacyPickup = new RepositoryLegacyPickup(dbMainStandard);

            RepositoryParcelDetail = new RepositoryParcelDetail(contextSharedPickup);

            RepositoryShipment = new RepositoryShipment(contextSharedPickup);

            RepositorySourceParcelDetail = new RepositorySourceParcelDetail(dbMainStandard);

            RepositorySourceShipment = new RepositorySourceShipment(dbMainStandard);

            RepositoryTransportProductMetadata = new RepositoryTransportProductMetadata(dbSharedStandard);


            ControllerDispositionSetting = new ControllerDispositionSetting(
                RepositoryDispositionSettings
                );

            ControllerForwarderPickup = new ControllerForwarderPickup(
                RepositoryCustomerPickup
                , RepositoryForwarderPickup
                , RepositoryShipment
                , RepositoryParcelDetail
                );

            ControllerCustomerPickup = new ControllerCustomerPickup(
                ContextSharedPickup
                , RepositoryCustomerPickup
                , RepositoryForwarderPickup
                , RepositoryShipment
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
                , RepositorySourceShipment
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

        /// <summary>
        /// Loads new or changed shipments for pickup. 
        /// Called regulary from Scheduler through webservice.
        /// </summary>
        public string Execute(DateTime dateShipmentBegin, DateTime? dateShipmentEnd)
        {
            var stopwatch = EasyStopwatch.StartMs();
            var count = 0;
            string result;

            try
            {
                Setup();

                List<SourceShipmentHeader> list;
                if (RepositorySourceShipment.Read(out list, dateShipmentBegin, dateShipmentEnd))
                {
                    foreach (var shipment in list)
                    {
                        Teardown();
                        Setup();
                        if (ControllerSourceShipment.CascadeLoadShipment(shipment))
                        {
                            count++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            finally
            {
                Teardown();

                var duration = stopwatch.Stop();
                result = String.Format("Processed {0} shipments on {1} ms", count, duration);
            }

            return result;
        }


        public string Execute()
        {
            return Execute(SystemDateTime.Today, null);
        }
    }
}
