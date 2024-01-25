//using Microsoft.Extensions.Hosting.Internal;
using System;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;

namespace Imtihani.Helpers
{
    public static class LogHelper
    {
        public static void Log(Exception ex)
        {

            StringBuilder builder = new StringBuilder();
            builder
                .AppendLine("----------")
                .AppendLine(DateTime.Now.ToString())
                .AppendFormat("Type:\t{0}", ex.GetType().Name)
                .AppendLine()
                .AppendFormat("Message:\t{0}", ex.Message)
                .AppendLine()
                .AppendFormat("Source:\t{0}", ex.Source)
                .AppendLine()
                .AppendFormat("Target:\t{0}", ex.TargetSite)
                .AppendLine()
                .AppendFormat("Stack:\t{0}", ex.StackTrace)
                .AppendLine();


            DateTime LogFileDate = DateTime.Now;
            string filePath = string.Format("./wwwroot/log/Error_{0}.log", LogFileDate.ToString("yyyy.MM.dd"));
            if (!Directory.Exists(Path.GetDirectoryName(filePath)))
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            if (!System.IO.File.Exists(filePath))
            {
                using (FileStream fs = System.IO.File.Create(filePath))
                {
                    fs.Close();
                }
            }

            using (StreamWriter writer = System.IO.File.AppendText(filePath))
            {
                writer.Write(builder.ToString());
                writer.Flush();
            }

            //if (SendEmail)
            //{
                //TODO plug exception sending code 
            //}
        }

        //public static bool SendEmail
        //{
        //    get
        //    {
        //        if (ConfigurationManager.AppSettings["SendEmailOnError"] != null)
        //            return Convert.ToBoolean(ConfigurationManager.AppSettings["SendEmailOnError"]);
        //        return false;
        //    }
        //}
    }
}
