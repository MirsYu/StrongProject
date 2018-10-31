using DY.Core.Configs;
using System;
using System.IO;
using System.Windows.Forms;


namespace StrongProject
{
	[Serializable]
	public class SolderConfig : ConfigBase
	{

		#region 密码&参数
		public string SuperPassword;//超级密码
		public string AdminPassword;//AdminiPassword
		public string UserPassword;//用户密码

		public bool bPDCAtype;//pdca上传类型, true: pegatron，false: foxconn


		public string TCP_PDCA_IP;//PDCA IP
		public string TCP_PDCA_Port;//



		//public double BoxBigWeightHigh;//大盒最大重量
		//public double BoxBigWeightLow;//大盒最小重量


		public string SnReadWay;//读SN方式，
		public string WeightReadWay;//读SN方式，

		public int tag_leftShenChanOKTotal;
		public int tag_leftShenChanNGTotal;
		public int tag_RightShenChanOKTotal;
		public int tag_RightShenChanNGTotal;
		public int tag_RightShenChanCT;
		public int tag_leftShenChanCT;

		#endregion

		#region process
		public SolderConfig()
		{

		}
		protected static string GetSetPath(string filename)
		{
			string dir = Path.Combine(Application.StartupPath, "set");
			if (Directory.Exists(dir) == false)
			{
				Directory.CreateDirectory(dir);
			}
			return Path.Combine(dir, filename);
		}

		public override bool Save()
		{
			return this.Save(GetSetPath("password.config"));
		}
		public new static SolderConfig Load()
		{
			SolderConfig sconf = Load(GetSetPath("password.config")) as SolderConfig;
			if (sconf == null)
			{
				sconf = new SolderConfig();
			}

			if (sconf.TCP_PDCA_IP == null)
			{
				sconf.TCP_PDCA_IP = "127.0.0.1";
				sconf.TCP_PDCA_Port = "5000";
			}


			return sconf;
		}
		#endregion
	}


}
