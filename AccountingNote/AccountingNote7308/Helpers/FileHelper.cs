using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace AccountingNote7308.Helpers
{
    public class FileHelper
    {
        private static string[] allowFileExt = { ".bmp", ".jpg", ".png" };              //規範的副檔名集合
        private const int _mbs = 10;
        private const int _maxlenght = _mbs * 1024 * 1024;

        public static string GetNewFileName(FileUpload fileUpload)
        {
            //重名解1
            System.Threading.Thread.Sleep(4);

            //重名解2
            string seqText = new Random((int)DateTime.Now.Ticks).Next(0, 1000).ToString().PadLeft(3, '0');

            string orgFileName = fileUpload.FileName;
            string ext = System.IO.Path.GetFileName(orgFileName);
            string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssFFFFFF") + seqText + ext;
            return newFileName;

        }

        public bool VaildFileUpload(FileUpload fileUpload, out List<string> msglist)
        {
            msglist = new List<string>();

            if (!ValidFileExt(fileUpload.FileName))
            {
                
                msglist.Add("only allow .bmp, .jpg, .png");
            }

            if (!ValidFileLength(fileUpload.FileBytes))
            {
                
                msglist.Add("only alloe lenght:" + _mbs + "mb");
            }

            if (msglist.Any())
                return false;
            else
                return true;

        }

        public static bool ValidFileUpload(FileUpload fileUpload, out List<string> msgList)
        {
            msgList = new List<string>();

            if (!ValidFileExt(fileUpload.FileName))
                msgList.Add("Only allow .bmp, .jpg, .png");

            if (!ValidFileLength(fileUpload.FileBytes))
                msgList.Add("Only allow Length: " + _mbs + "MB");

            if (msgList.Any())
                return false;
            else
                return true;
        }

        public static bool ValidFileLength(byte[] fileContent)                             //限制寫入檔案大小
        {
            if (fileContent.Length > _maxlenght)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool ValidFileExt(string orgFileName)
        {
            string ext = System.IO.Path.GetExtension(orgFileName);                  //取得原副檔名
            if (!allowFileExt.Contains(ext.ToLower()))                              //比較寫入的副檔名做限制
            {
                return false;
            }
            return true;
        }



    }

}
