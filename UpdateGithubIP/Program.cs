using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace UpdateGithubIP
{
    internal class Program
    {
        static void Main()
        {
            IPAddress[] ip = Dns.GetHostEntry("github.com").AddressList;
            try
            {
                UpdateHosts(ip[0].ToString(), "github.com");
                Console.WriteLine("github IP更新成功");
            }
            catch (Exception)
            {
                Console.WriteLine("github IP更新失败...");
            }
            Console.ReadLine();

        }

        static void UpdateHosts(string ip, string domain)
        {
            string path = @"C:\WINDOWS\system32\drivers\etc\hosts";

            //更改属性
            File.SetAttributes(path, FileAttributes.Normal);

            //避免重复写入
            string data = File.ReadAllText(path, Encoding.Default);
            if (data.Contains(ip) && data.Contains(domain))
            {
                File.SetAttributes(path, FileAttributes.ReadOnly);
                Console.WriteLine("初始化成功……");
                return;
            }

            try
            {
                //写入为追加模式
                StreamWriter sw = new StreamWriter(new FileStream(path, FileMode.Append), Encoding.Default);
                sw.WriteLine(ip + " " + domain);

                //关闭写入
                if (sw != null) {
                    sw.Close();
                }

                File.SetAttributes(path, FileAttributes.ReadOnly);
                Console.WriteLine("初始化成功……");
            }
            catch (Exception ex)
            {
                Console.WriteLine("初始化失败……" + ex.Message);
            }
        }
    }
}
