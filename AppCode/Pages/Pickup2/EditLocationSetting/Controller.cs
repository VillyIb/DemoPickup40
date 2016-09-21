using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
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


        public LocationSettings CurrentLocationSettings { get; set; }


        public GuiContainer GuiContainer { get; set; }


        #region Customers

        public void LoadGuiCustomerList()
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
                var locationCountInt = ContextMainPickup.LocationSettings.Count(t => t.FK_Customer_Id == customer.CompanyID);
                var locationCount = locationCountInt > 1 ? String.Format("({0})", locationCountInt) : "";
                var hasPermanentCollection =
                    ContextMainPickup.LocationSettings.Where(t => t.FK_Customer_Id == customer.CompanyID)
                        .SelectMany(t => t.PermanentCollectionList)
                        .Any(t2 => t2.HasPermanentPickup)
                        ? "PC"
                        : "";

                guiCustomerList.Add(new GuiCustomer
                {
                    Sorting = name,
                    Text = String.Format(
                        "{0,-55} {1,2} {2,4} {3,5}"
                        , name
                        , hasPermanentCollection
                        , locationCount
                        , id
                        ).Replace(' ', '\u00A0'),  // Appear as nice columns when used together with monospace font.
                    Value = customer.CompanyID
                });

            }

            guiCustomerList = guiCustomerList.OrderBy(t => t.Sorting).ToList();

            GuiContainer.GuiCustomerList = guiCustomerList;
        }


        /// <summary>
        /// Load Locations for the Current Customer.
        /// </summary>
        /// <param name="customerId"></param>
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

            GuiContainer.CurrentLocationSettingsId = GuiContainer.GuiLocationList.Count > 0
                ? GuiContainer.GuiLocationList[0].Id
                : -1;

            LoadLocation(GuiContainer.CurrentLocationSettingsId);
        }

        #endregion Customers


        #region Location Settings

        public void LoadLocation(int locationId)
        {
            CurrentLocationSettings = locationId >= 0 ? ContextMainPickup.LocationSettings.Include(t => t.PermanentCollectionList).FirstOrDefault(t => t.Id == locationId) : null;
        }


        public void GuiLoadPickupOperatorList()
        {
            GuiContainer.GuiPickupOperatorList.Clear();

            var t1 = Enum.GetValues(typeof(PickupOperator));

            foreach (PickupOperator t2 in t1)
            {
                GuiContainer.GuiPickupOperatorList.Add(t2);
            }
        }


        public void GuiLoadForwarderList()
        {
            GuiContainer.GuiForwarderList.Clear();

            GuiContainer.GuiForwarderList = DbSharedStandard.WebSites.Where(t => !t.IsDisabled ?? false).Select(t1 => new GuiWebsite
            {
                Sorting = t1.siteName,
                Text = t1.siteName,
                Value = t1.WebsiteID
            }
            ).OrderBy(t => t.Sorting).ToList();
        }

        #endregion


        #region Permanent Collection

        public void PermCollCreate()
        {
            if (CurrentLocationSettings.PermanentCollectionList.Count == 0)
            {
                CurrentLocationSettings.PermanentCollectionList.Add(new PermanentCollection
                {
                    HasFriday = true,
                    HasMonday = true,
                    HasSaturday = false,
                    HasSunday = false,
                    HasThursday = true,
                    HasPermanentPickup = true,
                    HasTuesday = true,
                    HasWedensday = true,
                    PickupOperator = CurrentLocationSettings.PickupOperator
                });
                ContextMainPickup.SaveChanges();

                LoadGuiCustomerList();
            }
        }


        public void PermCollUpdate()
        {
            ContextMainPickup.SaveChanges();

            LoadGuiCustomerList();
        }


        public void PermCollDelete()
        {
            if (CurrentLocationSettings.PermanentCollectionList.Count > 0)
            {
                var t1 = CurrentLocationSettings.PermanentCollectionList.ToList();

                foreach (var t2 in t1)
                {
                    ContextMainPickup.PermanentCollections.Remove(t2);
                }

                ContextMainPickup.SaveChanges();

                Debug.Assert(ContextMainPickup.PermanentCollections.Count(t => t.FK_LocationSetting_Id == CurrentLocationSettings.Id) == 0);
            }

            LoadGuiCustomerList();
        }


        #endregion


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
