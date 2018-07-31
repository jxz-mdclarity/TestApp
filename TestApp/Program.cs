using System;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Net;

namespace TestApp
{
    static class Utilities
    {
        public static string RemoveComment(string s)
        {
            if (s == null)
            {
                return null;
            }
            else
            {
                var r = "";
                int start = s.IndexOf("/*");
                int end = s.IndexOf("*/") + 2;

                if (start == 0 && end > 0)
                {
                    r = s.Substring(0, start);
                    r = s.Substring(end);
                }
                else
                {
                    r = s;
                }

                if (string.IsNullOrWhiteSpace(r))
                {
                    r = "";
                }

                return RemoveNewLine(r);
            }
        }

        public static string RemoveNewLine(string s)
        {
            return Regex.Replace(s, @"\t|\n|\r", " ");
        }

        public static void CheckAndEnableTLS21()
        {
            SecurityProtocolType type = System.Net.ServicePointManager.SecurityProtocol;
            Console.WriteLine(type.ToString());

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls; // comparable to modern browsers
            var response = WebRequest.Create("https://www.howsmyssl.com/").GetResponse();
            var body = new System.IO.StreamReader(response.GetResponseStream()).ReadToEnd();

            string folderPath = @"C:\Users\Public\TestFolder";
            if (!System.IO.File.Exists(folderPath))
            {
                System.IO.Directory.CreateDirectory(folderPath);
            }

            string htmlFileName = "EnableTLS2.1.html";
            string htmlFilePath = System.IO.Path.Combine(folderPath, htmlFileName);

            System.IO.File.WriteAllText(htmlFilePath, body);

            ProcessStartInfo startInfo = new ProcessStartInfo("IExplore.exe");
            startInfo.Verb = "RunAs";
            startInfo.Arguments = htmlFilePath;
            Process.Start(startInfo);
        }
    }

    class Program
    { 
        static void Main(string[] args)
        {
            //            string test = @"/* Query must be given when IndexAll set to False */
            //SELECT AccountID 
            //FROM AccountDeltaAllscripts";
            //            test = Utilities.RemoveComment(test);
            //            Console.WriteLine(test);

            //            string s = "this is a guid " + Guid.NewGuid();
            //            Console.WriteLine(s);

            //            //string query = null;
            //            string query = "SELECT PatientID from [AccountDelta]";
            //            string FilterSql = " Where PatientID =  '22455'";
            //            string str = @"SELECT COUNT(*)
            //                        FROM [PaymentTxn] P 
            //                        INNER JOIN Patient PA on PA.PatientID = P.PatientID
            //                    "
            //                    + (query != null ? $"INNER JOIN ({query}) AS Q ON P.[Key] = Q.[Key]" : "")
            //                    + FilterSql;
            //            Console.WriteLine(str);

            //Utilities.CheckAndEnableTLS21();

            string s1 = "Hello World!";
            string s2 = "Hello World! ";

            if (s1.Equals(s2))
            {
                Console.WriteLine("s1 equals s2");
            }
            else
            {
                Console.WriteLine("s1 differs from s2");
            }

            if (s1.GetHashCode() == s2.GetHashCode())
            {
                Console.WriteLine("s1 and s2 have the same hash code");
            }
            else
            {
                Console.WriteLine("s1 and s2 have different hash codes");
            }

#if DEBUG
            Console.WriteLine("Press enter to close...");
            Console.ReadLine();
#endif
        }
    }
}
