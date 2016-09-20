using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using nu.gtx.CodeFirst.DataAccess.Context;
using nu.gtx.CodeFirst.Model.Pickup;
using nu.gtx.DatabaseAccess.DbMain;
using nu.gtx.DatabaseAccess.DbShared;
using nu.gtx.POCO.Contract.Pickup;

namespace AppCode.Pages.Pickup2.EditLocationSetting
{
    public class Controller
    {
        private readonly DbMainStandard DbMainStandard;

        private readonly ContextMainPickup ContextMainPickup;

        private readonly DbSharedStandard DbSharedStandard;

        public LocationSettings LocationSetting { get; set; }


        public GuiContainer GuiContainer { get; set; }

        public void LoadCustomerList()
        {
            var guiCustomerList = new List<GuiCustomer>();

            var customersWithEnabledAccount =
                (from item1 in DbMainStandard.aspnet_CompanyDB
                 join item2 in DbMainStandard.aspnet_CompanyAccount
                 on item1.CompanyID equals item2.CompanyID
                 where item2.EnabledForUse && "DK".Equals(item2.ShipCountry_Iata)
                 select item1).Distinct().ToList();

            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (var customer in customersWithEnabledAccount)
            {
                var name = customer.Company_Name.Trim().Replace("  ", " ");
                var id = customer.CompanyID.ToString();
                var filler = "".PadLeft(60 - (name.Length + id.Length), '\u00A0');

                guiCustomerList.Add(new GuiCustomer
                {
                    Sorting = name,
                    Text = String.Format(
                        "{0} {1} {2}"
                        , name
                        , filler
                        , id
                        ),
                    Value = customer.CompanyID
                });

            }

            guiCustomerList = guiCustomerList.OrderBy(t => t.Sorting).ToList();

            GuiContainer.GuiCustomerList = guiCustomerList;
        }


        public void LoadLocationList(int customerId)
        {
            var locations = ContextMainPickup.LocationSettings.Where(t => t.FK_Customer_Id == customerId).ToList();

            GuiContainer.GuiLocationList.Clear();

            foreach (var location in locations)
            {
                var account = DbMainStandard.aspnet_CompanyAccount.AsNoTracking().First(t => t.AccountID == location.FK_Account_Id);

                GuiContainer.GuiLocationList.Add(
                    new GuiLocation
                    {
                        Displaytext = String.Format(
                            "{0}, {1}, {2}, {3} {4}, ({5})"
                            , account.ShipCompanyName
                            , account.ShipAddress1
                            , account.ShipAddress2
                            , account.ShipZip
                            , account.ShipCity
                            , account.AccountID
                            ),
                        Id = location.Id
                    }
                    );
            }

            GuiContainer.GuiLocationList = GuiContainer.GuiLocationList.OrderBy(t => t.Displaytext).ToList();
        }


        public void LoadLocation(int locationId)
        {
            LocationSetting = ContextMainPickup.LocationSettings.FirstOrDefault(t => t.Id == locationId);
        }


        public void LoadPickupOperatorList()
        {
            GuiContainer.PickupOperatorList.Clear();

            var t1 = Enum.GetValues(typeof(PickupOperator));

            foreach (PickupOperator t2 in t1)
            {
                GuiContainer.PickupOperatorList.Add(t2);
            }
        }

        public void LoadForwarderList()
        {
            GuiContainer.ForwarderList.Clear();

            GuiContainer.ForwarderList = DbSharedStandard.WebSites.Where(t => !t.IsDisabled ?? false).Select(t1 => new GuiWebsite
            {
                Sorting = t1.siteName,
                Text = t1.siteName,
                Value = t1.WebsiteID
            }
            ).OrderBy(t=>t.Sorting).ToList();
        }


        public void SaveChages()
        {
            ContextMainPickup.SaveChanges();
        }


        public Controller(
            DbMainStandard dbMainStandard
            , ContextMainPickup contextMainPickup
            , DbSharedStandard dbSharedStandard
            )
        {
            if (dbMainStandard == null) { throw new ArgumentNullException("dbMainStandard"); }
            if (contextMainPickup == null) { throw new ArgumentNullException("contextMainPickup"); }
            if (dbSharedStandard == null) { throw new ArgumentNullException("dbSharedStandard"); }

            DbMainStandard = dbMainStandard;
            ContextMainPickup = contextMainPickup;
            DbSharedStandard = dbSharedStandard;

        }

    }
}
