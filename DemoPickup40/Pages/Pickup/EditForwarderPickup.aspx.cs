﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppCode.Pages.Pickup;
// ReSharper disable ArgumentsStyleLiteral

namespace DemoPickup40.Pages.Pickup
{
    public partial class EditForwarderPickup : System.Web.UI.Page
    {
        #region P: XpForwarderPickup { get; set; } in Session
        private const string XpForwarderPickupKey = "ForwarderPickup";

        private GuiForwarderPickup XpForwarderPickup
        {
            get { return Session[XpForwarderPickupKey] as GuiForwarderPickup; }
            set { Session[XpForwarderPickupKey] = value; }
        }
        #endregion
        

        #region P: XpPrimaryRowList { get; set; } in Session
        private const string XpPrimaryRowListKey = "PrimaryRowList";

        private List<GuiForwarderPickup> XpPrimaryRowList
        {
            get { return Session[XpPrimaryRowListKey] as List<GuiForwarderPickup>; }
            set { Session[XpPrimaryRowListKey] = value; }
        }
        #endregion


        public NameValueCollection PickupStatusList
        {
            get
            {
                var t1 = Enum.GetNames(typeof(PickupStatuForwarder));
                //t1.Remove(PickupStatuForwarder.Undefined);
                //return t1;

                var t2 = new NameValueCollection();

                foreach (var current in t1)
                {
                    var localizedText = (GetLocalResourceObject(current) as string) ?? current;
                    
                    t2.Add( current, "Xt" + localizedText); // dur ikke kommer ud som 2 kolonner med current....
                }


                return t2;
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


        private void LoadForwarderPickup()
        {
            int forwarderPickupId;
            if (GetQueryParameter(out forwarderPickupId, "ForwarderPickuId"))
            {
                XpForwarderPickup = XpPrimaryRowList.FirstOrDefault(t => t.Id == forwarderPickupId);
            }
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
                XuPickupStatus.Text = XpForwarderPickup.PickupStatusText;
                XuReadyClose.Text = string.Format(@"{0:hh\:mm}", XpForwarderPickup.ReadyClose);
                XuReadyOpen.Text = string.Format(@"{0:hh\:mm}", XpForwarderPickup.ReadyOpen);
                XuState.Text = XpForwarderPickup.Address.State;
                XuStreet1.Text = XpForwarderPickup.Address.Street1;
                XuStreet2.Text = XpForwarderPickup.Address.Street2;
                XuZip.Text = XpForwarderPickup.Address.Zip;
            }
        }

        private void ShowError(string key, List<string> argList)
        {
            var format = (GetLocalResourceObject(key) as string) ?? key + " {0}";
            XuError.Text = string.Format(format, argList);
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(IsPostBack))
            {
                LoadForwarderPickup();
                Populate();
            }

            DropDownList1.DataSource = PickupStatusList;
            DropDownList1.DataBind();

            Page.ClientScript.RegisterOnSubmitStatement(typeof(Page), "closePage", "window.onunload = CloseWindow();");
        }


        private static bool TryParse(ITextControl source, out DateTime value)
        {
            var dateString = source.Text;

            var culture = CultureInfo.CreateSpecificCulture("fr-FR"); // fr: yyyy-MM-dd
            const DateTimeStyles styles = DateTimeStyles.AssumeLocal;
            return DateTime.TryParse(dateString, culture, styles, out value);
        }


        private static bool TryParse(ITextControl source, out TimeSpan value)
        {
            var dateString = source.Text;

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
            XpForwarderPickup.PickupStatusText = XuPickupStatus.Text;
            {
                TimeSpan t1;
                if (TryParse(XuReadyOpen, out t1))
                {
                    XpForwarderPickup.ReadyOpen = t1;
                }
                else
                {
                    ShowError("Error_ReadyOpen", new List<string> { "" });
                    Unhide(XuError);
                    return;
                }
            }
            {
                TimeSpan t1;
                if (TryParse(XuReadyClose, out t1))
                {
                    XpForwarderPickup.ReadyClose = t1;
                }
                else
                {
                    ShowError("Error_ReadyClose", new List<string> { "" });
                    Unhide(XuError);
                    return;
                }
            }
            XpForwarderPickup.Address.State = XuState.Text;
            XpForwarderPickup.Address.Street1 = XuStreet1.Text;
            XpForwarderPickup.Address.Street2 = XuStreet2.Text;
            XpForwarderPickup.Address.Zip = XuZip.Text;

            var url = string.Format("Forwarder.aspx?ForwarderPickuId={0}", XpForwarderPickup.Id);
            Response.Redirect(url, endResponse: false);

        }


    }
}