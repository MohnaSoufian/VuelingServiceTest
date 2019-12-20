using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;

namespace VuelingService
{
    public class ExeptionLog
    {

        public static void Save(object obj, Exception ex)
        {
            try
            {
                string fecha = System.DateTime.Now.ToString("yyyyMMdd");
                string hora = System.DateTime.Now.ToString("HH:mm:ss");
                string path = HttpContext.Current.Request.MapPath("~/log/" + fecha + ".txt");

                StreamWriter sw = new StreamWriter(path, true);

                StackTrace stacktrace = new StackTrace();
                sw.WriteLine(obj.GetType().FullName + " " + hora);
                sw.WriteLine(stacktrace.GetFrame(1).GetMethod().Name + " - " + ex.Message);
                sw.WriteLine("");

                sw.Flush();
                sw.Close();

            }
            catch (Exception e)
            {

            }

        }
        public static void Save(string msg)
        {
            try
            {
                string fecha = System.DateTime.Now.ToString("yyyyMMdd");
                string hora = System.DateTime.Now.ToString("HH:mm:ss");
                string path = HttpContext.Current.Request.MapPath("~/log/" + fecha + ".txt");

                StreamWriter sw = new StreamWriter(path, true);

                StackTrace stacktrace = new StackTrace();
                sw.WriteLine(msg + " " + hora);
                sw.WriteLine("");

                sw.Flush();
                sw.Close();
            }

            catch (Exception e)
            {

            }
        }
    }
}