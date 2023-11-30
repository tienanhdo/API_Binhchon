using API_Binhchon.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Drawing2D;
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
        [Route("api/Get")]
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
                            string ten_dvi = getconn.get_ten_dvi(ma_dviqly.Trim());
                            response.Headers.Location = new Uri("https://binhchon.evnhanoi.vn/QR/ok.html?ma_dvi='" + ten_dvi);

                        }
                    }
                    else
                    {
                        response.Headers.Location = new Uri("https://binhchon.evnhanoi.vn/QR/false.html");
                    }
                }
                else
                {
                    response.Headers.Location = new Uri("https://binhchon.evnhanoi.vn/QR/false.html");
                }
            }
            catch (Exception ex)
            {
                // re = ex.ToString();
            }
            return response;
        }
        [Route("api/Get_thongtin")]
        [HttpPost]
        public HttpResponseMessage Get_thongtin()
        {
            P_return res = new P_return();
            try
            {
                DataSet ds = new DataSet();
                ds = getconn.tong_ket();
                if (ds.Tables[0].Rows.Count == 0)
                {
                    res.STATUS = "ERROR";
                    res.MESSAGE = "Gửi thông tin thành công, khong co du lieu ";
                    res.DATA = null;

                }
                else
                {
                    var data = JsonConvert.SerializeObject(ds, Formatting.Indented);
                    P_data lst = JsonConvert.DeserializeObject<P_data>(data);
                    res.STATUS = "OK";
                    res.MESSAGE = "Gui thong tin thanh cong";
                    res.DATA = lst;
                }
            }
            catch (Exception ex)
            {
                res.STATUS = "ERR0R";
                res.MESSAGE = ex.ToString();
            }
            return Request.CreateResponse(HttpStatusCode.OK, res, Configuration.Formatters.JsonFormatter);
        } 
    }
}
