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
            //String url = verifyQRCode();
            //getPtwebqq(url);
            //getVfwebqq();
            //getUinAndPsessionid();
        }

        public  void getQRCode()
        {
            string url_login = "https://ssl.ptlogin2.qq.com/ptqrshow?appid=501004106&e=0&l=M&s=5&d=72&v=4&t=0.1";
            httpClient.MaxResponseContentBufferSize = 256000;
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/47.0.2526.106 Safari/537.36");
            HttpResponseMessage response = httpClient.GetAsync(new Uri(url_login)).Result;
            if (response.StatusCode != HttpStatusCode.OK)
            {
                //异常，
            }
            FileStream fs = new FileStream(dir + "code.png", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs, Encoding.Default);
            var data  =  response.Content.ReadAsByteArrayAsync().Result;
            sw.Write(data);
            sw.Close();
            fs.Close();
        }

        /// <summary>
        /// 写文件到本地
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="html"></param>
        public static void WriteFile(string fileName, string html)
        {
            try
            {
                FileStream fs = new FileStream(dir + fileName, FileMode.Create);
                StreamWriter sw = new StreamWriter(fs, Encoding.Default);
                sw.Write(html);
                sw.Close();
                fs.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

        }
    }
}
