using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppCode.Pages.Pickup2;
using nu.gtx.POCO.Contract.Pickup;

// ReSharper disable UnusedMember.Local

// ReSharper disable ArgumentsStyleLiteral

namespace DemoPickup40.Pages.Pickup2
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
            get { return (int) (Session[XpCurrentForwarderPickupIdKey] ?? 0); }
            set { Session[XpCurrentForwarderPickupIdKey] = value; }
        }


        private List<DropDownBoxData> zPickupStatusList;

        public List<DropDownBoxData> PickupStatusList
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


        private bool GetQueryParameter(out int value, string key)
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

        private IForwarderPickup XpForwarderPickup{get;set;}

        private PickupData BackendApi { get; set; }

        private bool XmLoadForwarderPickup(int forwarderPickupId)
        {
            IForwarderPickup t1;
            if (BackendApi.GetForwarderPickup(out t1, forwarderPickupId))
            {
                XpForwarderPickup = t1;
                return true;
            }

            XpForwarderPickup = null;
            return false;
        }


        private void PopulateXuPickupStatus(PickupStatusForwarder forwarderPickupStatus)
        {
            var control = XuPickupStatus;

            control.DataSource = PickupStatusList;
            control.DataTextField = "Text";
            control.DataValueField = "Value";
            control.DataBind();

            for (var index = 0; index < PickupStatusList.Count; index++)
            {
                if (forwarderPickupStatus.Equals(PickupStatusList[index].Value))
                {
                    control.SelectedIndex = index;
                    break;
                }
            }
        }


        private void PopuluateCalculatedValues()
        {
            // (from d in dataRows select d.Date).Min();
            var timeReadyMax = XpForwarderPickup.CustomerPickupList.Count > 0 
                ?  XpForwarderPickup.CustomerPickupList.Max(t => t.TimeReady) 
                : new TimeSpan(0,0,0);
            XuTimeReadyCalculated.Text = timeReadyMax.HasValue ? timeReadyMax.Value.ToString(@"hh\:mm") : "na";

            var timeCloseMin = XpForwarderPickup.CustomerPickupList.Count > 0
                ? XpForwarderPickup.CustomerPickupList.Min(t => t.TimeClose)
                : new TimeSpan(23, 59, 0);
            XuTimeCloseCalculated.Text = timeCloseMin.HasValue ? timeCloseMin.Value.ToString(@"hh\:mm") : "na";
        }


        private void Populate()
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
                PopulateXuPickupStatus(XpForwarderPickup.PickupStatus);
                XuTimeClose.Text = string.Format(@"{0:hh\:mm}", XpForwarderPickup.TimeClose);
                XuTimeReady.Text = string.Format(@"{0:hh\:mm}", XpForwarderPickup.TimeReady);
                XuState.Text = XpForwarderPickup.Address.State;
                XuStreet1.Text = XpForwarderPickup.Address.Street1;
                XuStreet2.Text = XpForwarderPickup.Address.Street2;
                XuZip.Text = XpForwarderPickup.Address.Zip;

                PopuluateCalculatedValues();
            }
        }


        private void ShowError(string key, List<string> argList)
        {
            var format = (GetLocalResourceObject(key) as string) ?? key + " {0}";
            XuError.Text = string.Format(format, argList);
        }



        protected void Page_Load(object sender, EventArgs e)
        {
            BackendApi = new PickupData();

            if (!(IsPostBack))
            {
                int forwarderPickupId;
                if (GetQueryParameter(out forwarderPickupId, "ForwarderPickuId"))
                {
                    XpCurrentForwarderPickupId = forwarderPickupId;
                    XmLoadForwarderPickup(forwarderPickupId);
                }

                Populate();
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


        protected void XuSubmit_Click(object sender, EventArgs e)
        {
            Hide(XuError);

            XpForwarderPickup.Address.Attention = XuAttention.Text;
            XpForwarderPickup.Address.CountryCode = XuCountryCode.Text;
            XpForwarderPickup.Address.CountryCode = XuEmail.Text;
            XpForwarderPickup.Address.Name = XuName.Text;
            XpForwarderPickup.Note = XuNote.Text;
            XpForwarderPickup.Address.Phone = XuPhone.Text;
            {
                DateTime t1;
                if (TryParse(XuPickupDate, out t1))
                {
                    XpForwarderPickup.PickupDate = t1;
                }
                else
                {
                    ShowError("Error_PickpDate", new List<string> { "" });
                    Unhide(XuError);
                    return;
                }
            }
            {
                PickupStatusForwarder t1;
                XpForwarderPickup.PickupStatus = Enum.TryParse(XuPickupStatus.Text, out t1) ? t1 : PickupStatusForwarder.CustWait;
            }
            {
                TimeSpan t1;
                if (TryParse(XuTimeReady.Text, out t1))
                {
                    XpForwarderPickup.TimeReady = t1;
                }
                else
                {
                    ShowError("Error_TimeReady", new List<string> { "" });
                    Unhide(XuError);
                    return;
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
                    ShowError("Error_TimeClose", new List<string> { "" });
                    Unhide(XuError);
                    return;
                }
            }
            XpForwarderPickup.Address.State = XuState.Text;
            XpForwarderPickup.Address.Street1 = XuStreet1.Text;
            XpForwarderPickup.Address.Street2 = XuStreet2.Text;
            XpForwarderPickup.Address.Zip = XuZip.Text;

            BackendApi.UpdateDatabase();

            var url = string.Format("Forwarder.aspx?ForwarderPickuId={0}", XpForwarderPickup.Id);
            Response.Redirect(url, endResponse: false);

        }

        protected void XuTimeReadyCalculated_Click(object sender, EventArgs e)
        {
            TimeSpan t1;
            if (TryParse(XuTimeReadyCalculated.Text, out t1))
            {
                XpForwarderPickup.TimeReady = t1;
            }
            Populate();
        }

        protected void XuTimeCloseCalculated_Click(object sender, EventArgs e)
        {
            TimeSpan t1;
            if (TryParse(XuTimeCloseCalculated.Text, out t1))
            {
                XpForwarderPickup.TimeClose = t1;
            }
            Populate();
        }
    }
}