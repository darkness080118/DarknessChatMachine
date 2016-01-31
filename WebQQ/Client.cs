using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WebQQ
{
    public class Client
    {
        //消息ID
        private static long MESSAGE_ID = 41000001;

        //客户端id，固定的
        private static long Client_ID = 42000001;

        HttpClient httpClient = new HttpClient();

        private IEnumerable<string> SetCookies = null;

        private CookieCollection cookies = new CookieCollection();

        private string CookieToString;

        private string ptwebqq;

        private string vfwebqq;

        private long uin;

        private string psessionid;

        private static String dir = @"C:\work\";

        /// <summary>
        /// Login
        /// </summary>
        public void login()
        {
            //获取验证码
            getQRCode();
            string url = verifyQRCode();
            getPtwebqq(url);
            //getVfwebqq();
            //getUinAndPsessionid();
        }

        public void getQRCode()
        {
            string url_login = "https://ssl.ptlogin2.qq.com/ptqrshow?appid=501004106&e=0&l=M&s=5&d=72&v=4&t=0.4634378447663039";
            httpClient.MaxResponseContentBufferSize = 256000;
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/47.0.2526.106 Safari/537.36");
            HttpResponseMessage response = httpClient.GetAsync(new Uri(url_login)).Result;
            HttpRequestMessage request = response.RequestMessage;
            if (response.StatusCode != HttpStatusCode.OK)
            {
                //异常，
            }
            SetCookies = response.Headers.GetValues("Set-Cookie");

            FileStream fs = new FileStream(dir + "code.png", FileMode.Create);       
            BufferedStream sw = new BufferedStream(fs,1024);
            byte[] imgdata = null;
            imgdata = response.Content.ReadAsByteArrayAsync().Result;
            //var data  =  response.Content.ReadAsByteArrayAsync().Result;
            sw.Write(imgdata,0, imgdata.Length);
            sw.Close();
            fs.Close();
        }

        public string verifyQRCode()
        {
            string url_verify = "https://ssl.ptlogin2.qq.com/ptqrlogin?webqq_type=10&remember_uin=1&login2qq=1&aid=501004106&u1=http%3A%2F%2Fw.qq.com%2Fproxy.html%3Flogin2qq%3D1%26webqq_type%3D10&ptredirect=0&ptlang=2052&daid=164&from_ui=1&pttype=1&dumy=&fp=loginerroralert&action=0-1-6240&mibao_css=m_webqq&t=undefined&g=1&js_type=0&js_ver=10148&login_sig=&pt_randsalt=0";
            string url_verify_referer ="https://ui.ptlogin2.qq.com/cgi-bin/login?daid=164&target=self&style=16&mibao_css=m_webqq&appid=501004106&enable_qlogin=0&no_verifyimg=1&s_url=http%3A%2F%2Fw.qq.com%2Fproxy.html&f_url=loginerroralert&strong_login=1&login_state=10&t=20131024001";
            httpClient.DefaultRequestHeaders.Referrer = new Uri(url_verify_referer);
            HttpResponseMessage response = httpClient.GetAsync(new Uri(url_verify)).Result;
            HttpContent responseText = response.Content;
            var data = response.Content.ReadAsStringAsync().Result;
            var datasplite = data.Split(',');
            foreach (var item in datasplite)
            {
                if (item.Contains("http"))
                {
                    return item.Replace("'","");
                }
            }
            return null;
        }

        public void getPtwebqq(string url)
        {
            string url_referer = "http://s.web2.qq.com/proxy.html?v=20130916001&callback=1&id=1";
            httpClient.DefaultRequestHeaders.Referrer = new Uri(url_referer);
            HttpResponseMessage response = httpClient.GetAsync(new Uri(url)).Result;
            HttpContent responseText = response.Content;
        }

    }
}
