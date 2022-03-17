//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Http;
//using System.ServiceModel.Channels;
//using System.Text;
//using System.Threading.Tasks;
//using System.Web;

//namespace Tente.Utilities
//{
//    public static class ClientTools
//    {

//        public static string GetClientIp(HttpRequestMessage request)
//        {
//            if (request.Properties.ContainsKey("MS_HttpContext"))
//            {
//                return ((HttpContext)request.Properties["MS_HttpContext"]).Request.UserHostAddress;
//            }
//            else if (request.Properties.ContainsKey(RemoteEndpointMessageProperty.Name))
//            {
//                RemoteEndpointMessageProperty prop;
//                prop = (RemoteEndpointMessageProperty)request.Properties[RemoteEndpointMessageProperty.Name];
//                return prop.Address;
//            }
//            else
//            {
//                return null;
//            }
//        }

//    }
//}
