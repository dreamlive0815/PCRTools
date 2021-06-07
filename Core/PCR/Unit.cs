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

        static Unit()
        {
            GetAllUnits();
        }

        public static readonly int DEFAULT_ICON_SIZE = 128;

        private static Dictionary<string, Unit> nameToUnitDic;

        private static List<Unit> ParseUnits(string filePath)
        {
            var csv = Csv.FromFile(filePath);
            var r = new List<Unit>();
            foreach (var row in csv)
            {
                var id = int.Parse(row["ID"]);
                var unit = new Unit()
                {
                    Id = id,
                    Star = HasStar6(id) ? 6 : 3,
                    Nicknames = new List<string>(row["Nicknames"].Split(new char[]{ ';' }, StringSplitOptions.RemoveEmptyEntries)),
                };
                r.Add(unit);
            }
            r.Sort((a, b) =>
            {
                return a.Id.CompareTo(b.Id);
            });
            return r;
        }

        public static List<Unit> GetAllUnits()
        {
            var res = ResourceManager.Default.GetResource("${G}/Csv/Unit.csv");
            var units = res.ParseObjWithCache(int.MaxValue, (filePath) =>
            {
                var r = ParseUnits(filePath);
                InitNameToUnitDic(r);
                return r;
            });
            return units;
        }

        public static Unit GetUnitById(int unitId)
        {
            var units = GetAllUnits();
            var r = Utils.BinSearch(units, new Unit() { Id = unitId }, (a, b) =>
            {
                return a.Id.CompareTo(b.Id);
            });
            return r;
        }

        public static List<int> GetAllIds()
        {
            var units = GetAllUnits();
            var ids = units.Select(x => x.Id).ToList();
            return ids;
        }

        public static bool HasStar6(int unitId)
        {
            var iconStar6 = GetIconResource(unitId, 6);
            return iconStar6.Exists;
        }

        public static string GetName(int unitId)
        {
            var unit = GetUnitById(unitId);
            return unit?.Name ?? "不存在的角色";
        }

        private static void InitNameToUnitDic(List<Unit> units)
        {
            nameToUnitDic = new Dictionary<string, Unit>();
            foreach (var unit in units)
            {
                foreach (var nickname in unit.Nicknames)
                {
                    if (!string.IsNullOrWhiteSpace(nickname))
                        nameToUnitDic[nickname] = unit;
                }
            }
        }

        public static Unit GetUnitByName(string name)
        {
            return nameToUnitDic.ContainsKey(name) ? nameToUnitDic[name] : null;
        }

        public static string GetIconFileName(int id, int star)
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

        public static ImageResource GetIconResource(int id, int star)
        {
            var fileName = GetIconFileName(id, star);
            return GetIconResource(fileName);
        }

        private static void DownloadIcon(int id, int star)
        {
            var fullPath = GetIconResource(id, star).Fullpath;
            DownloadIcon(id, star, fullPath);
        }

        private static void DownloadIcon(int id, int star, string savePath)
        {
            throw new Exception("暂不支持");
            var url = $"https://redive.estertion.win/icon/unit/{id}{star}1.webp";
            Logger.GetInstance().Debug("PCRUnit", $"Downloading PCR Unit Icon From {url}");
            //Logger.GetInstance().Debug("PCRUnit", $"Succeeded To Save PCR Unit Icon id = {id} star = {star} To {savePath}");
        }

        public int Id { get; set; }

        public int Star { get; set; }

        public List<string> Nicknames { get; set; } = new List<string>();

        public string Name
        {
            get { return Nicknames[0]; }
        }

        public ImageResource GetIconResource()
        {
            return GetIconResource(Id, Star);
        }
    }
}
