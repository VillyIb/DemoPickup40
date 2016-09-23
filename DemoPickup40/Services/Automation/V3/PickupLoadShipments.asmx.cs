using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;


using nu.gtx.Common1.Utils;
using AppCode.Services.Automation.V3.PickupLoadShipments;

namespace DemoPickup40.Services.Automation.V3
{
    /// <summary>
    /// Summary description for PickupLoadShipments
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class PickupLoadShipments : System.Web.Services.WebService
    {
        //private static readonly ILog Logger = LogManager.GetLogger("nu.gtx.Services.Automation.V3.PickupLoadShipments");

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }


        [WebMethod(Description = "Load Shipments for Pickup processing")]
        //[TraceExtension]
        public String Execute()
        {
            //Logger.Debug();
            var stopwatch = EasyStopwatch.StartMs();

            try
            {
                return new Controller().Execute();
            }

            catch (Exception ex)
            {
                //ExceptionLogging.Log(Logger, ex);
                return ex.Message;
            }

            finally
            {
                //Logger.DebugFormat("-Exit, duration: {0} ms ", stopwatch.Stop());
            }
        }


        [WebMethod(Description = "Load Shipments for Pickup processing")]
        //[TraceExtension]
        public String ExecuteWindow(DateTime dateShipmentBeginInclusive, DateTime dateShipmentEndInclusive)
        {
            //Logger.Debug();
            var stopwatch = EasyStopwatch.StartMs();

            try
            {
                return new Controller().Execute(dateShipmentBeginInclusive, dateShipmentEndInclusive);
            }

            catch (Exception ex)
            {
                //ExceptionLogging.Log(Logger, ex);
                return ex.Message;
            }

            finally
            {
                //Logger.DebugFormat("-Exit, duration: {0} ms ", stopwatch.Stop());
            }
        }


    }
}
