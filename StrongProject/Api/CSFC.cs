using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace StrongProject
{
	public class CSFC
	{
		public CSFC()
		{
		}
		public const int SFC_OK = 1;
		public const int SFC_NG = 2;

		//WebClient wc = new WebClient();
		//如用WebClient类，用下面两行代码访问
		//byte[] bResponse = wc.DownloadData(data);
		//(string)strResponse = Encoding.ASCII.GetString(bResponse);
		//通过HttpPost方式访问SFC, posDataStr一般变量为空即可

		/// <summary>
		/// 富士康SFC调用函数
		/// </summary>
		/// <param name="Url"></param>
		/// <param name="postDataStr"></param>
		/// <returns></returns>
		public static string GetFoxconnSFC(string url, string postDataStr = "")
		{
			try
			{
				HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
				request.Method = "POST";
				request.ContentType = "application/x-www-form-urlencoded";
				request.ContentLength = postDataStr.Length;
				request.Proxy = null;
				StreamWriter writer = new StreamWriter(request.GetRequestStream(), Encoding.ASCII);
				writer.Write(postDataStr);
				writer.Flush();
				HttpWebResponse response = (HttpWebResponse)request.GetResponse();
				string encoding = response.ContentEncoding;
				if (encoding == null || encoding.Length < 1)
				{
					encoding = "UTF-8"; //默认编码  
				}
				StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding));
				string retString = reader.ReadToEnd();
				return retString;
			}
			catch (Exception ex)
			{
				Console.Write(ex.ToString());
				return "";
			}


			//try
			//{
			//    WebClient wc = new WebClient();
			//    byte[] bResponse = wc.DownloadData(url);
			//    return Encoding.ASCII.GetString(bResponse);
			//}
			//catch (Exception ex)
			//{
			//    return "";
			//}
		}

		/// <summary>
		/// 昌硕SFC调用函数
		/// </summary>
		/// <param name="Url"></param>
		/// <returns></returns>
		public static string GetPegatronSFC(string url)
		{
			string responseText;
			System.Net.HttpWebRequest request;
			// 创建一个HTTP请求
			try
			{
				request = (System.Net.HttpWebRequest)WebRequest.Create(url);
				//request.Method="get";
				System.Net.HttpWebResponse response;
				response = (System.Net.HttpWebResponse)request.GetResponse();
				System.IO.StreamReader myreader = new System.IO.StreamReader(response.GetResponseStream(), Encoding.UTF8);
				responseText = myreader.ReadToEnd();
				myreader.Close();
			}
			catch (System.Exception ex)
			{
				Console.Write(ex.ToString());
				return "";
			}
			return responseText;
		}

		#region http 屏蔽代码 备用

		/*
        public static string HttpPost(string Url, string postDataStr="")
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = postDataStr.Length;
            StreamWriter writer = new StreamWriter(request.GetRequestStream(), Encoding.ASCII);
            writer.Write(postDataStr);
            writer.Flush();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string encoding = response.ContentEncoding;
            if (encoding == null || encoding.Length < 1)
            {
                encoding = "UTF-8"; //默认编码  
            }
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding));
            string retString = reader.ReadToEnd();
            return retString;
        } 
		//using System.web.dll
		private static void httpwedRequestPost1()
		{
			//string strURL = "http://localhost/WinformSubmit.php";
			System.Net.HttpWebRequest request;
			request = (System.Net.HttpWebRequest)WebRequest.Create(testUrl);
			//Post请求方式
			request.Method = "POST";
			// 内容类型
			request.ContentType = "application/x-www-form-urlencoded";
			// 参数经过URL编码
			//string paraUrlCoded = System.Web.HttpUtility.UrlEncode("keyword");
			//paraUrlCoded += "=" + System.Web.HttpUtility.UrlEncode("多月");
			string paraUrlCoded="";
			byte[] payload;
			//将URL编码后的字符串转化为字节
			payload = System.Text.Encoding.UTF8.GetBytes(paraUrlCoded);
			//设置请求的 ContentLength 
			request.ContentLength = payload.Length;
			//获得请 求流
			System.IO.Stream writer = request.GetRequestStream();
			//将请求参数写入流
			writer.Write(payload, 0, payload.Length);
			// 关闭请求流
			writer.Close();
			System.Net.HttpWebResponse response;
			// 获得响应流
			response = (System.Net.HttpWebResponse)request.GetResponse();
			System.IO.StreamReader myreader = new System.IO.StreamReader(response.GetResponseStream(), Encoding.UTF8);
			string responseText = myreader.ReadToEnd();
			myreader.Close();
			MessageBox.Show(responseText);
		}
		//using System.IO;
		public static string HttpGet(string Url, string postDataStr)
		{
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url + (postDataStr == "" ? "" : "?") + postDataStr);
			request.Method = "GET";
			request.ContentType = "text/html;charset=UTF-8";

			HttpWebResponse response = (HttpWebResponse)request.GetResponse();
			Stream myResponseStream = response.GetResponseStream();
			StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
			string retString = myStreamReader.ReadToEnd();
			myStreamReader.Close();
			myResponseStream.Close();
			return retString;
		}
        public static string HttpPost(string Url, string postDataStr)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = postDataStr.Length;
            StreamWriter writer = new StreamWriter(request.GetRequestStream(), Encoding.ASCII);
            writer.Write(postDataStr);
            writer.Flush();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string encoding = response.ContentEncoding;
            if (encoding == null || encoding.Length < 1)
            {
                encoding = "UTF-8"; //默认编码  
            }
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding));
            string retString = reader.ReadToEnd();
            return retString;
        } 

		//postWebRequest1
		 private string PostWebRequest(string postUrl, string paramData, Encoding dataEncode)
        {
           string ret = string.Empty;
            try
            {
                byte[] byteArray = dataEncode.GetBytes(paramData); //转化
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(postUrl));
                webReq.Method = "POST";
                webReq.ContentType = "application/x-www-form-urlencoded";

                webReq.ContentLength = byteArray.Length;
                Stream newStream = webReq.GetRequestStream();
                newStream.Write(byteArray, 0, byteArray.Length);//写入参数
                newStream.Close();
                HttpWebResponse response = (HttpWebResponse)webReq.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.Default);
                ret = sr.ReadToEnd();
                sr.Close();
                response.Close();
                newStream.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return ret;
        }


		 public static string PostData1(string str="")
		 {
			 try
			 {
				 byte[] data = System.Text.Encoding.GetEncoding("GB2312").GetBytes(str);
				 // 准备请求...
				 HttpWebRequest req = (HttpWebRequest)WebRequest.Create(testUrl );
				 req.Method = "Post";
				 req.ContentType = "application/x-www-form-urlencoded";
				 req.ContentLength = data.Length;
				 Stream stream = req.GetRequestStream();
				 // 发送数据
				 stream.Write(data, 0, data.Length);
				 stream.Close();

				 HttpWebResponse rep = (HttpWebResponse)req.GetResponse();
				 Stream receiveStream = rep.GetResponseStream();
				 Encoding encode = System.Text.Encoding.GetEncoding("GB2312");
				 // Pipes the stream to a higher level stream reader with the required encoding format. 
				 StreamReader readStream = new StreamReader(receiveStream, encode);

				 MessageBox.Show( readStream.ReadToEnd());
				 Char[] read = new Char[256];
				 int count = readStream.Read(read, 0, 256);
				 StringBuilder sb = new StringBuilder("");
				 while (count > 0)
				 {
					 String readstr = new String(read, 0, count);
					 sb.Append(readstr);
					 count = readStream.Read(read, 0, 256);
				 }

				 rep.Close();
				 readStream.Close();
				 return sb.ToString();
			 }
			 catch (Exception ex)
			 {
				 MessageBox.Show(ex.Message);
				 return "";
			 }
		 }

		 public string GetPostString(string url, string data)
		 {
			 try
			 {
				 byte[] postBytes = Encoding.GetEncoding("utf-8").GetBytes(data);
				 HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
				 myRequest.Method = "POST";
				 myRequest.ContentType = "text/html";
				 myRequest.ContentLength = postBytes.Length;
				 myRequest.Proxy = null;//搜索 
				 Stream newStream = myRequest.GetRequestStream();
				 newStream.Write(postBytes, 0, postBytes.Length);
				 newStream.Close();
				 // Get response 
				 HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
				 using (StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.GetEncoding("utf-8")))
				 {
					 string content = reader.ReadToEnd();
					 return content;
				 }
			 }
			 catch (System.Exception ex)
			 {				 return ex.Message;
			 }
		 }
         * 
         * 
         * 


        //post webclient
        public static string Request_WebClient(string uri, string paramStr, Encoding encoding, string username, string password)
        {
            if (encoding == null)
                encoding = Encoding.UTF8;
            string result = string.Empty;
            WebClient wc = new WebClient();
            // 采取POST方式必须加的Header
            wc.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            byte[] postData = encoding.GetBytes(paramStr);
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                wc.Credentials = GetCredentialCache(uri, username, password);
                wc.Headers.Add("Authorization", GetAuthorization(username, password));
            }
            byte[] responseData = wc.UploadData(uri, "POST", postData); // 得到返回字符流
            return encoding.GetString(responseData);// 解码                  
        }

        //post webRequist
        //body是要传递的参数,格式"roleId=1&uid=2"
        //post的cotentType填写:
        //"application/x-www-form-urlencoded"
        //soap填写:"text/xml; charset=utf-8"
        public static string PostHttp(string url, string body, string contentType)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = contentType;
            httpWebRequest.Method = "POST";
            httpWebRequest.Timeout = 20000;
            byte[] btBodys = Encoding.UTF8.GetBytes(body);
            httpWebRequest.ContentLength = btBodys.Length;
            httpWebRequest.GetRequestStream().Write(btBodys, 0, btBodys.Length);
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream());
            string responseContent = streamReader.ReadToEnd();
            httpWebResponse.Close();
            streamReader.Close();
            httpWebRequest.Abort();
            httpWebResponse.Close();
            return responseContent;
        }

        */

		#endregion

	}
	public class PassStationATRet
	{
		public string tag_ret;
		public string tag_str;
		public string tag_Apn;
	}
	/// <summary>
	/// 过站查询
	/// </summary>
	public class PassStation
	{
		private List<string> tag_nameList;
		private List<string> tag_varList;
		public string tag_c;
		public string tag_sn;
		public string tag_station_id;
		public string tag_product;
		public string tag_test_station_name;
		public string tag_result;
		public string tag_start_time;
		public string tag_stop_time;
		public string tag_mac_address;
		public string tag_link_cl;
		public PassStation()
		{
			tag_nameList = new List<string>();
			tag_varList = new List<string>();
		}
		/// <summary>
		/// 解析数据
		/// </summary>
		/// <param name="data"></param>
		public void SeprateData(string data)
		{
			string[] str = System.Text.RegularExpressions.Regex.Split(data, "\r\n");
			int lenth = str.Length;
			tag_nameList.Clear();
			tag_varList.Clear();
			for (int i = 0; i < lenth; i++)
			{
				string[] info = System.Text.RegularExpressions.Regex.Split(str[i], "=");
				if (info.Length > 1)
				{
					tag_nameList.Add(info[0]);
					tag_nameList.Add(info[1]);
				}
			}
		}
		/// <summary>
		/// 获取变量
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public string GetSfcfVar(string name)
		{
			for (int i = 0; i < tag_varList.Count; i++)
			{
				if (tag_nameList[i] == name)
				{
					return tag_varList[i];
				}
			}
			return "";
		}
		/// <summary>
		/// 过站信息
		/// </summary>
		/// <returns></returns>
		public string PassStationAdd()
		{
			string url = "http://10.16.17.39/JediBobcat/JediSFC?c=ADD_RECORD" + "&sn=" + tag_sn + "&station_id=" + tag_station_id +
			"&product=" + tag_product + "&test_station_name=" + tag_test_station_name + "&result=" + tag_result + "&start_time=" + tag_start_time + "&stop_time=" + tag_stop_time + "&mac_address=" + tag_mac_address + "&link_cl=" + tag_link_cl;
			string ret = CSFC.GetPegatronSFC(url);
			return ret;
		}
		/*
         * http://10.16.17.39/JediBobcat/JediSFC?c=QUERY_RECORD&sn=C7CT800BHG6W&p=ht_pn
         * 查询
         */
		public string PassStationQueryRecord(string iphoneSn, string pn)
		{
			string url = "http://10.16.17.39/JediBobcat/JediSFC?" + "QUERY_RECORD" + "c=" + "&sn=" + iphoneSn + "&p=" + pn;
			string ret = CSFC.GetPegatronSFC(url);
			SeprateData(ret);
			return GetSfcfVar(pn);
		}

	}
}
