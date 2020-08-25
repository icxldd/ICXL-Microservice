using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;

public class DownLoadFile
{
    ///
    /// 下载文件方法
    /// 
    /// 文件保存路径和文件名
    /// 返回服务器文件名
    ///
    public bool DeownloadFile(string url, string strFileName)
    {
        bool flag = false;
        // 打开上次下载的文件
        long SPosition = 0;
        // 实例化流对象
        FileStream FStream;
        // 判断要下载的文件夹是否存在
        if (File.Exists(strFileName))
        {
            // 打开要下载的文件
            FStream = File.OpenWrite(strFileName);
            // 获取已经下载的长度
            SPosition = FStream.Length;
            FStream.Seek(SPosition, SeekOrigin.Current);
        }
        else
        {
            // 文件不保存创建一个文件
            FStream = new FileStream(strFileName, FileMode.Create);
            SPosition = 0;
        }
        try
        {
            // 打开网络连接
            HttpWebRequest myRequest = (HttpWebRequest)HttpWebRequest.Create(url);
            if (SPosition > 0)
                myRequest.AddRange((int)SPosition);             // 设置Range值
                                                                // 向服务器请求,获得服务器的回应数据流
            Stream myStream = myRequest.GetResponse().GetResponseStream();
            // 定义一个字节数据
            byte[] btContent = new byte[512];
            int intSize = 0;
            intSize = myStream.Read(btContent, 0, 512);
            while (intSize > 0)
            {
                FStream.Write(btContent, 0, intSize);
                intSize = myStream.Read(btContent, 0, 512);
            }
            // 关闭流
            FStream.Close();
            myStream.Close();
            flag = true;        // 返回true下载成功
        }
        catch (Exception)
        {
            FStream.Close();
            flag = false;       // 返回false下载失败
        }
        return flag;
    }
    class Program
    {

        static void Main(string[] args)
        {

            string url = "http://localhost:1997/";
            string filename = "BambooLauncher-MJ-norya.rar";
            string actionUrl = url + filename;
            DownLoadFile d = new DownLoadFile();
            //Console.WriteLine("请输入命令：1开始下载、2结束下载、3断点续传下载");
            d.DeownloadFile(actionUrl, "d://1.rar");








        }
    }
}
