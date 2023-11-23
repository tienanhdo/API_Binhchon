using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Web.Http;
using System.Web.Http.Results;

namespace API_Binhchon.Controllers
{
    public class BinhchonController : ApiController
    {
        DataProvide getconn = new DataProvide();
        public HttpResponseMessage Get(string ma_dviqly)
        {
            //RedirectResult re ;
            var response = Request.CreateResponse(HttpStatusCode.Moved);
            response.Headers.Location = new Uri("http://www.abcmvc.com");
            String firstMacAddress = NetworkInterface
            .GetAllNetworkInterfaces()
            .Where(nic => nic.OperationalStatus == OperationalStatus.Up && nic.NetworkInterfaceType != NetworkInterfaceType.Loopback)
            .Select(nic => nic.GetPhysicalAddress().ToString())
            .FirstOrDefault();
            try
            {
                Int64 count = getconn.get_mac(firstMacAddress);
                Int64 coun_dvi = getconn.get_ma_dvi(ma_dviqly);
                if (count == 0)
                {
                    if (coun_dvi == 0)
                    {
                        Int64 value = getconn.ins_binhchon(ma_dviqly.Trim(), firstMacAddress.Trim());
                        if (value > 0)
                        {
                            response.Headers.Location = new Uri("https://kt_thanhtoantructuyen.evnhanoi.vn/QR/ok.html?ma_dvi='"+ ma_dviqly);

                        }
                    }
                    else
                    {
                        // re = "Tồn tại MAC";
                    }
                }
                else
                {
                    // re = "Tồn tại donvi";
                }
            }
            catch (Exception ex)
            {
                // re = ex.ToString();
            }
            return response;
        }
    }
}
