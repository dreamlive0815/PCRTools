using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Core.Common;
using Core.Extensions;
using SimpleHTTPClient;

namespace Core.PCR
{
    public class Unit
    {

        public static readonly int DEFAULT_ICON_SIZE = 128;


        private static Csv csvCache;

        public static Csv GetCsv()
        {
            if (csvCache != null)
                return csvCache;
            var path = ResourceManager.Default.GetResource("${G}/Csv/Unit.csv").AssertExists().Fullpath;
            var csv = Csv.FromFile(path);
            csvCache = csv;
            return csv;
        }

        public static List<string> GetAllIds()
        {
            var csv = GetCsv();
            var keys = csv.GetRowKeys();
            return keys;
        }

        public static string GetName(string id)
        {
            var csv = GetCsv();
            var row = csv[id];
            var cell = row["Nicknames"];
            var name = cell.SplitAndGet(';', 0);
            return name;
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

        private static ImageResource GetIconResource(string iconFileName)
        {
            return ResourceManager.Default.GetImageResource("${G}/Image/Unit/" + iconFileName);
        }

        public static ImageResource GetIconResource(string id, int star)
        {
            var fileName = GetIconFileName(id, star);
            return GetIconResource(fileName);
        }

        private static void DownloadIcon(string id, int star)
        {
            var fullPath = GetIconResource(id, star).Fullpath;
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
