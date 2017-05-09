using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MyLog
{
    class Log
    {
        private static string path = System.IO.Directory.GetCurrentDirectory().Replace('\\', '/'); //获取当前运行目录
        private static bool debug = true;

        public static void i(string strLog)
        {
            if (debug)
                log(DateTime.Now.ToString("[yyyyMMdd HHmmss]") + "[info]: " + strLog);
        }
        
        public static void e(string strLog, string err = "")
        {
            if (debug)
                log(DateTime.Now.ToString("[yyyyMMdd HHmmss]") + "[error]: " + strLog);
        }

        public static void e(string strLog, Exception e)
        {
            if (debug)
                log(DateTime.Now.ToString("[yyyyMMdd HHmmss]") + "[error]: " + strLog + ", err:" + e.Message);
        }

        private static void log(string strLog)
        {
            string sFilePath = path + DateTime.Now.ToString("yyyyMM");
            string sFileName = "VSLogs" + DateTime.Now.ToString("dd") + ".log";
            sFileName = sFilePath + "\\" + sFileName; //文件的绝对路径
            if (!Directory.Exists(sFilePath))//验证路径是否存在
            {
                Directory.CreateDirectory(sFilePath);
                //不存在则创建
            }
            FileStream fs;
            StreamWriter sw;
            if (File.Exists(sFileName))
            //验证文件是否存在，有则追加，无则创建
            {
                fs = new FileStream(sFileName, FileMode.Append, FileAccess.Write);
            }
            else
            {
                fs = new FileStream(sFileName, FileMode.Create, FileAccess.Write);
            }
            sw = new StreamWriter(fs);
            sw.WriteLine(strLog);
            sw.Close();
            fs.Close();
        }
    }
}
