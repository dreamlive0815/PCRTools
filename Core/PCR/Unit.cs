using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Core.Common;
using SimpleHTTPClient;

namespace Core.PCR
{
    public class Unit
    {

        public static Csv GetCsv()
        {
            var path = ResourceMgr.Default.Csv("Unit.csv").AssertExists().Fullpath;
            var csv = Csv.FromFile(path);
            return csv;
        }

        public static List<string> GetAllIds()
        {
            var csv = GetCsv();
            var keys = csv.GetRowKeys();
            return keys;
        }

        public static string GetIconFileName(string id, int star)
        {
            if (star >= 6)
                star = 6;
            else if (star >= 3)
                star = 3;
            else
                star = 1;
            return $"icon_unit_{id}{star}1.png";
        }

        private static string GetIconFullPath(string iconFileName)
        {
            return ResourceMgr.Default.GetFullPath(ResourceMgr.IMAGE_PIECE, "Unit", iconFileName);
        }

        public static string GetIconFullPath(string id, int star)
        {
            var fileName = GetIconFileName(id, star);
            return GetIconFullPath(fileName);
        }

        private static void DownloadIcon(string id, int star)
        {
            var fullPath = GetIconFullPath(id, star);
            DownloadIcon(id, star, fullPath);
        }

        private static void DownloadIcon(string id, int star, string savePath)
        {
            throw new Exception("暂不支持");
            var url = $"https://redive.estertion.win/icon/unit/{id}{star}1.webp";
            Logger.GetInstance().Debug("PCRUnit", $"Downloading PCR Unit Icon From {url}");
            //Logger.GetInstance().Debug("PCRUnit", $"Succeeded To Save PCR Unit Icon id = {id} star = {star} To {savePath}");
        }
    }
}
