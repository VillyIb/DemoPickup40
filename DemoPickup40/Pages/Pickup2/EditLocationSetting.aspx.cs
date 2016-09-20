using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AppCode.Pages.Pickup2.EditLocationSetting;
namespace DemoPickup40.Pages.Pickup2
{
    public partial class EditLocationSetting : System.Web.UI.Page
    {
        private const string ZGuiContainerKey = "GuiContainerKey";

        private GuiContainer GuiContainer
        {
            get { return (GuiContainer)(
                    Session[ZGuiContainerKey] 
                    ?? (Session[ZGuiContainerKey] = 
                        new GuiContainer {GuiLocationList = new List<GuiLocation>()}
                        ));
            }
            set { Session[ZGuiContainerKey] = value; }
        }


        private void XmInit()
        {
            GuiContainer.GuiLocationList.Clear();
            GuiContainer.GuiLocationList.Add(new GuiLocation { Displaytext = "Alfa" });
            GuiContainer.GuiLocationList.Add(new GuiLocation { Displaytext = "Bravo" });
            GuiContainer.GuiLocationList.Add(new GuiLocation { Displaytext = "Charlie" });
        }


        private void XmPopulatePage()
        {
            XuLocationList.DataSource = GuiContainer.GuiLocationList;
            XuLocationList.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(IsPostBack))
            {
                XmInit();
            }
            XmPopulatePage();
        }

        protected void XuLocationList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}