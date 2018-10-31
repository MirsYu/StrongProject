using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DY.Core.Configs;
using System.Windows.Forms;
using System.IO;


namespace StrongProject
{
    [Serializable]
    public class SolderConfig   :ConfigBase
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

        #endregion

        #region process
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
            return this.Save(GetSetPath("set.config"));
        }
        public new static SolderConfig Load()
        {
           SolderConfig sconf = Load(GetSetPath("set.config")) as SolderConfig;
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
