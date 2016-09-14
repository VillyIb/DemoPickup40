using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using nu.gtx.Business.Pickup.PermanentPickup;
using nu.gtx.CodeFirst.DataAccess.Context;
using nu.gtx.DatabaseAccess.DbMain;
using nu.gtx.POCO.Contract.Pickup;

namespace AppCode.Pages.Pickup2.PermanentPickup
{
    public class Controller
    {
        private ControllerPermanentPickup BackendApi { get; set; }

        public GuiContainer GuiContainer { get; set; }

        public ControllerPermanentPickup Init()
        {
            var connectionStringMain = ConfigurationManager.ConnectionStrings["EF_CodeFirst_Test"];
            var connectionBuilderMain = new SqlConnectionStringBuilder(connectionStringMain.ConnectionString);

            var contextMainPickup = new ContextMainPickup(connectionBuilderMain);
            var contextMainStandard = new DbMainStandard();

            var result = new ControllerPermanentPickup(
                contextMainPickup
                , contextMainStandard
                );

            return result;
        }


        public void LoadData()
        {
            (BackendApi ?? (BackendApi = Init())).Init();

            GuiContainer = new GuiContainer { GuiRowList = new List<GuiRow>() };

            var t3 = Enum.GetValues(typeof(PickupOperator));
            GuiContainer.PickupOperatorList = new List<PickupOperator>();
            foreach (PickupOperator t2 in t3)
            {
                GuiContainer.PickupOperatorList.Add(t2);
            }


            #region GuiRowList

            foreach (var pp in BackendApi.PermanentPickupRowList)
            {
                var account = pp.Account;
                var customer = pp.Customer;
                var permanentPickup = pp.PermanentPickup;

                var guirow = new GuiRow
                {
                    AccountName = pp.Account.SearchName,
                    AccountNameLong = String.Format(
                            "{2}, {3}, {4}, {5},  {0}, {1}  "
                            , account.Account_Desciption
                            , account.Accountnumber
                            , account.ShipCompanyName
                            , account.ShipAddress1
                            , account.ShipZip
                            , account.ShipCity
                            ),
                    CustomerName = customer.Company_Name,
                    CustomerNameLong = String.Format(
                            "{0}, {1}, {2} {3}"
                            , customer.Company_Name
                            , customer.Company_Address1
                            , customer.Company_Zip
                            , customer.Company_City
                        ),
                    FK_Account_Id = account.AccountID,
                    FK_Customer_Id = customer.CompanyID,

                    //Enabled1 = false,
                    //Enabled2 = false,
                    //Enabled3 = false,
                    //Enabled4 = false,
                    //Enabled5 = false,
                    //Enabled6 = false,
                    //Enabled7 = false,
                    EnabledMaster = false,
                    PickupOperator = nu.gtx.POCO.Contract.Pickup.PickupOperator.Undefined,
                    Text = "no PermanentPickup information",
                    TimeClose = new TimeSpan(0, 0, 0),
                    TimeReady = new TimeSpan(23, 59, 0),
                    HasPermanentPickupRow = false,
                    //PickupOperatorList = new List<PickupOperator>()
                };

                if (permanentPickup != null)
                {
                    var t1 = String.IsNullOrWhiteSpace(permanentPickup.EnabledOnWeekday) &&
                             permanentPickup.EnabledOnWeekday.Length == 7
                        ? permanentPickup.EnabledOnWeekday
                        : "1111100";

                    guirow.EnabledWeek = t1;

                    //var t2 = t1.ToCharArray();

                    //guirow.Enabled1 = t2[0] != '0';
                    //guirow.Enabled2 = t2[1] != '0';
                    //guirow.Enabled3 = t2[2] != '0';
                    //guirow.Enabled4 = t2[3] != '0';
                    //guirow.Enabled5 = t2[4] != '0';
                    //guirow.Enabled6 = t2[5] != '0';
                    //guirow.Enabled7 = t2[6] != '0';

                    guirow.EnabledMaster = permanentPickup.Enabled;

                    guirow.HasPermanentPickupRow = true;
                    guirow.Id = permanentPickup.Id;
                    guirow.PickupOperator = permanentPickup.PickupOperator;

                    guirow.Text = permanentPickup.Text;
                    guirow.TimeClose = permanentPickup.TimeClose;
                    guirow.TimeReady = permanentPickup.TimeReady;
                    //guirow.PickupOperatorList = GuiContainer.PickupOperatorList;
                }

                GuiContainer.GuiRowList.Add(guirow);

            };


            #endregion



        }
    }

}
