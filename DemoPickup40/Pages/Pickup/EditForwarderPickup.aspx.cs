using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using AppCode.ExceptionExtentions;
using Pages.Pickup.PageEditForwarderPickup;

using nu.gtx.POCO.Contract.Pickup;
using nu.gtx.POCO.Contract.Pickup.Constants;
using PickupStatusForwarder = nu.gtx.POCO.Contract.Pickup.PickupStatusForwarder;

// ReSharper disable UnusedMember.Local

// ReSharper disable ArgumentsStyleLiteral

namespace DemoPickup40.Pages.Pickup
{
    public partial class EditForwarderPickup : Page
    {

        private const string XpGuiContainerKey = "GuiContainer";

        private GuiContainer XpGuiContainer
        {
            get
            {
                var t1 = Session[XpGuiContainerKey];
                var t2 = t1 as GuiContainer;
                return t2;
            }
            set { Session[XpGuiContainerKey] = value; }
        }


        private List<GuiForwarderPickup> XpPrimaryRowList
        {
            get { return XpGuiContainer.ForwarderPickupList; }
        }


        private const string XpCurrentForwarderPickupIdKey = "CurrentForwarderPickupId";


        private int XpCurrentForwarderPickupId
        {
            get { return (int)(Session[XpCurrentForwarderPickupIdKey] ?? 0); }
            set { Session[XpCurrentForwarderPickupIdKey] = value; }
        }


        private List<DropDownBoxData> zPickupStatusList;

        public List<DropDownBoxData> XpPickupStatusList
        {
            get
            {
                if (zPickupStatusList == null)
                {

                    var t1 = Enum.GetValues(typeof(PickupStatusForwarder));
                    //t1.Remove(PickupStatuForwarder.Undefined);
                    //return t1;

                    zPickupStatusList = new List<DropDownBoxData>();

                    foreach (PickupStatusForwarder current in t1)
                    {
                        switch (current)
                        {
                            case PickupStatusForwarder.Undefined:
                                continue;

                            case PickupStatusForwarder.CustWait:
                                break;
                            case PickupStatusForwarder.CustHand:
                                break;
                            case PickupStatusForwarder.CustCan:
                                break;
                            case PickupStatusForwarder.ForwWait:
                                break;
                            case PickupStatusForwarder.ForwSched:
                                break;
                        }

                        var localizedText = (GetGlobalResourceObject("PickupStatusForwarder", current.ToString("G")) as string) ?? current.ToString("G");

                        zPickupStatusList.Add(new DropDownBoxData
                        {
                            Value = current,
                            Text = localizedText,
                            Sorting = localizedText.ToLower()
                        });
                    }

                    zPickupStatusList = zPickupStatusList.OrderBy(t => t.Sorting).ToList();
                }
                return zPickupStatusList;
            }
        }

        public List<DropDownBoxDataPickupOperator> XpPickupOperatorList
        {
            get
            {
                var result = new List<DropDownBoxDataPickupOperator>
                {
                    new DropDownBoxDataPickupOperator
                    {
                        Sorting = "00",
                        Text = "GLS",
                        Value = PickupOperator.GLS
                    },
                    new DropDownBoxDataPickupOperator
                    {
                        Sorting = "01",
                        Text = "DHL",
                        Value =PickupOperator.DHL
                    },
                    new DropDownBoxDataPickupOperator
                    {
                        Sorting = "02",
                        Text = "HS",
                        Value =PickupOperator.HS
                    },
                    new DropDownBoxDataPickupOperator
                    {
                        Sorting = "03",
                        Text = "UPS",
                        Value =PickupOperator.UPS
                    },
                    new DropDownBoxDataPickupOperator
                    {
                        Sorting = "98",
                        Text = "Anden Afhenter",
                        Value =PickupOperator.AA
                    },
                    new DropDownBoxDataPickupOperator
                    {
                        Sorting = "99",
                        Text = "Udefineret",
                        Value =PickupOperator.Undefined
                    },
                    new DropDownBoxDataPickupOperator
                    {
                        Sorting = "96",
                        Text = "Alternativ1",
                        Value =PickupOperator.Auto1
                    },
                    new DropDownBoxDataPickupOperator
                    {
                        Sorting = "97",
                        Text = "Alternativ2",
                        Value =PickupOperator.Auto2
                    },

                    new DropDownBoxDataPickupOperator
                    {
                        Sorting = "95",
                        Text = "Manual",
                        Value =PickupOperator.Manual
                    },

                };

                return result;
            }
        }

        private bool XmGetQueryParameter(out int value, string key)
        {
            var qs = HttpContext.Current.Request.QueryString;

            foreach (string current in qs.Keys)
            {
                if (key.Equals(current, StringComparison.OrdinalIgnoreCase))
                {
                    var t2 = qs[current];
                    var result = int.TryParse(t2, out value);
                    return result;
                }
            }

            value = 0;
            return false;
        }

        private IForwarderPickup XpForwarderPickup { get; set; }

        private PickupData XpBackendApi { get; set; }

        // ReSharper disable once UnusedMethodReturnValue.Local
        private bool XmLoadForwarderPickup(int forwarderPickupId)
        {
            IForwarderPickup t1;
            if (XpBackendApi.Read(out t1, forwarderPickupId))
            {
                XpForwarderPickup = t1;
                return true;
            }

            XpForwarderPickup = null;
            return false;
        }


        private void XmPopulateXuPickupStatus(PickupStatusForwarder forwarderPickupStatus)
        {
            var control = XuPickupStatus;

            control.DataSource = XpPickupStatusList;
            control.DataTextField = "Text";
            control.DataValueField = "Value";
            control.DataBind();

            for (var index = 0; index < XpPickupStatusList.Count; index++)
            {
                if (forwarderPickupStatus.Equals(XpPickupStatusList[index].Value))
                {
                    control.SelectedIndex = index;
                    break;
                }
            }
        }


        private void XmPopulateXuPickupOperator(PickupOperator currentPickupOperator)
        {
            var control = XuPickupOperator;

            control.DataSource = XpPickupOperatorList;
            control.DataTextField = "Text";
            control.DataValueField = "Value";
            control.DataBind();

            for (var index = 0; index < XpPickupOperatorList.Count; index++)
            {
                if (currentPickupOperator.Equals(XpPickupOperatorList[index].Value))
                {
                    control.SelectedIndex = index;
                    break;
                }
            }
        }


        private void XmPopuluateCalculatedValues()
        {
            // (from d in dataRows select d.Date).Min();
            var timeReadyMax = XpForwarderPickup.CustomerPickupList.Count > 0
                ? XpForwarderPickup.CustomerPickupList.Max(t => t.TimeReady)
                : new TimeSpan(0, 0, 0);
            XuTimeReadyCalculated.Text = timeReadyMax.ToString(@"hh\:mm");

            var timeCloseMin = XpForwarderPickup.CustomerPickupList.Count > 0
                ? XpForwarderPickup.CustomerPickupList.Min(t => t.TimeClose)
                : new TimeSpan(23, 59, 0);
            XuTimeCloseCalculated.Text = timeCloseMin.ToString(@"hh\:mm");
        }


        private void XmPopulate()
        {
            if (XpForwarderPickup != null)
            {
                XuAttention.Text = XpForwarderPickup.Address.Attention;
                XuCountryCode.Text = XpForwarderPickup.Address.CountryCode;
                XuEmail.Text = XpForwarderPickup.Address.CountryCode;
                XuName.Text = XpForwarderPickup.Address.Name;
                XuNote.Text = XpForwarderPickup.Note;
                XuPhone.Text = XpForwarderPickup.Address.Phone;
                XuPickupDate.Text = XpForwarderPickup.PickupDate.ToString("yyyy-MM-dd");
                XmPopulateXuPickupStatus(XpForwarderPickup.PickupStatus);
                XmPopulateXuPickupOperator(XpForwarderPickup.PickupOperator);
                XuTimeClose.Text = string.Format(@"{0:hh\:mm}", XpForwarderPickup.TimeClose);
                XuTimeReady.Text = string.Format(@"{0:hh\:mm}", XpForwarderPickup.TimeReady);
                XuState.Text = XpForwarderPickup.Address.State;
                XuStreet1.Text = XpForwarderPickup.Address.Street1;
                XuStreet2.Text = XpForwarderPickup.Address.Street2;
                XuZip.Text = XpForwarderPickup.Address.Zip;

                XmPopuluateCalculatedValues();
            }
        }


        private void XmShowError(string key, List<string> argList)
        {
            var format = (GetLocalResourceObject(key) as string) ?? key + " {0}";
            // ReSharper disable once CoVariantArrayConversion
            XuError.Text = string.Format(format, argList.ToArray());
        }



        protected void Page_Load(object sender, EventArgs e)
        {
            XpBackendApi = new PickupData();

            if (!(IsPostBack))
            {
                int forwarderPickupId;
                if (XmGetQueryParameter(out forwarderPickupId, "ForwarderPickuId"))
                {
                    XpCurrentForwarderPickupId = forwarderPickupId;
                    XmLoadForwarderPickup(forwarderPickupId);
                }

                XmPopulate();
            }
            else
            {
                XmLoadForwarderPickup(XpCurrentForwarderPickupId);
            }

            Page.ClientScript.RegisterOnSubmitStatement(typeof(Page), "closePage", "window.onunload = CloseWindow();");
        }


        private static bool TryParse(ITextControl source, out DateTime value)
        {
            var dateString = source.Text;

            var culture = CultureInfo.CreateSpecificCulture("fr-FR"); // fr: yyyy-MM-dd
            const DateTimeStyles styles = DateTimeStyles.AssumeLocal;
            return DateTime.TryParse(dateString, culture, styles, out value);
        }


        private static bool TryParse(string source, out TimeSpan value)
        {
            var dateString = source;

            var culture = CultureInfo.InvariantCulture;
            const DateTimeStyles styles = DateTimeStyles.AssumeLocal;
            DateTime t1;
            var result = DateTime.TryParseExact(dateString, "HH:mm", culture, styles, out t1);
            if (result)
            {
                value = t1.Subtract(t1.Date);
                return true;
            }

            value = default(TimeSpan);
            return false;
        }


        private void Hide(WebControl control)
        {
            var t1 = control.CssClass.Replace("hidden", "") + " hidden";
            t1 = t1.Replace("  ", " ");
            control.CssClass = t1;
        }

        private void Unhide(WebControl control)
        {
            control.CssClass = control.CssClass.Replace("hidden", "");
        }


        /// <summary>
        /// Reads fields from the GUI
        /// A Return value of true indicate an error.
        /// </summary>
        /// <returns></returns>
        // ReSharper disable once InconsistentNaming
        private bool XmReadFieldsFromGUI()
        {
            Hide(XuError);

            XpForwarderPickup.Address.Attention = XuAttention.Text;
            XpForwarderPickup.Address.Email = XuEmail.Text;
            XpForwarderPickup.Address.Name = XuName.Text;
            XpForwarderPickup.Address.Phone = XuPhone.Text;
            XpForwarderPickup.Address.Street1 = XuStreet1.Text;
            XpForwarderPickup.Address.Street2 = XuStreet2.Text;
            XpForwarderPickup.Address.Zip = XuZip.Text;

            XpForwarderPickup.Note = XuNote.Text;

            {
                PickupOperator t1;
                XpForwarderPickup.PickupOperator = Enum.TryParse(XuPickupOperator.SelectedValue, out t1) ? t1 : PickupOperator.Undefined;
            }
            {
                PickupStatusForwarder t1;
                XpForwarderPickup.PickupStatus = Enum.TryParse(XuPickupStatus.Text, out t1) ? t1 : PickupStatusForwarder.CustWait;
            }

            // Possible validation errors.
            {
                if (XuCountryCode.Text.Trim().Length > 2)
                {
                    XmShowError("Error_Country", new List<string> { "Max length 2" });
                    Unhide(XuError);
                    return true;
                }
                XpForwarderPickup.Address.CountryCode = XuCountryCode.Text.Trim();
            }
            {
                if (XuState.Text.Trim().Length > 2)
                {
                    XmShowError("Error_State", new List<string> { "Max length 2" });
                    Unhide(XuError);
                    return true;
                }
                XpForwarderPickup.Address.State = XuState.Text.Trim();
            }
            {
                DateTime t1;
                if (TryParse(XuPickupDate, out t1))
                {
                    XpForwarderPickup.PickupDate = t1;
                }
                else
                {
                    XmShowError("Error_PickpDate", new List<string> { "" });
                    Unhide(XuError);
                    return true;
                }
            }
            {
                TimeSpan t1;
                if (TryParse(XuTimeReady.Text, out t1))
                {
                    XpForwarderPickup.TimeReady = t1;
                }
                else
                {
                    XmShowError("Error_TimeReady", new List<string> { "" });
                    Unhide(XuError);
                    return true;
                }
            }
            {
                TimeSpan t1;
                if (TryParse(XuTimeClose.Text, out t1))
                {
                    XpForwarderPickup.TimeClose = t1;
                }
                else
                {
                    XmShowError("Error_TimeClose", new List<string> { "" });
                    Unhide(XuError);
                    return true;
                }
            }

            return false; // no error
        }

        protected void XuSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (XmReadFieldsFromGUI())
                {
                    return;
                }

                XpBackendApi.UpdateDatabase();

                var url = string.Format("Forwarder.aspx?ForwarderPickuId={0}", XpForwarderPickup.Id);
                Response.Redirect(url, endResponse: false);
            }
            catch (Exception ex)
            {
                var msg = ExceptionExtention.Analyze(ex);
                XmShowError("Exception", new List<string> { msg });
                Unhide(XuError);
            }
        }

        protected void XuTimeReadyCalculated_Click(object sender, EventArgs e)
        {
            TimeSpan t1;
            if (TryParse(XuTimeReadyCalculated.Text, out t1))
            {
                XuTimeReady.Text = string.Format(@"{0:hh\:mm}", t1);
            }
            XmReadFieldsFromGUI();
            XmPopulate();
        }

        protected void XuTimeCloseCalculated_Click(object sender, EventArgs e)
        {
            TimeSpan t1;
            if (TryParse(XuTimeCloseCalculated.Text, out t1))
            {
                XuTimeClose.Text = string.Format(@"{0:hh\:mm}", t1);
            }
            XmReadFieldsFromGUI();
            XmPopulate();
        }
    }
}