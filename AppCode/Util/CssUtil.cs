using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace AppCode.Util
{
    public static class CssUtil
    {
        public static void CssAdd(this WebControl control, string css)
        {
            if (control.CssClass.Contains(css)) { return; }

            control.CssClass = String.Format("{0} {1}", control.CssClass.Trim(), css.Trim()).Replace("  ", " ");
        }

        public static void CssAdd(this HtmlGenericControl control, string css)
        {
            var t1 = css.Trim();
            control.Attributes["class"] = control.Attributes["class"].Replace(t1, "") + " " + t1;
        }

        public static void CssRemove(this WebControl control, string css)
        {
            if (!(control.CssClass.Contains(css))) { return; }

            control.CssClass = control.CssClass.Replace(css, "").Replace("  ", " ");
        }


        public static void CssRemove(this HtmlGenericControl control, string css)
        {
            var t1 = css.Trim();
            control.Attributes["class"] = control.Attributes["class"].Replace(t1, "").Replace("  "," ");
        }

    }
}
