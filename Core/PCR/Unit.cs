﻿using System;
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
            InitNameToIdDic();
        }

        public static readonly int DEFAULT_ICON_SIZE = 128;

        public static Csv GetCsv()
        {
            var res = ResourceManager.Default.GetResource("${G}/Csv/Unit.csv");
            var csv = res.ParseObjWithCache(int.MaxValue, (filePath) =>
            {
                res.AssertExists();
                return Csv.FromFile(filePath);
            });
            //Console.WriteLine(csv.GetHashCode());
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
            var nicknames = row["Nicknames"];
            var name = nicknames.SplitAndGet(';', 0);
            return name;
        }

        private static Dictionary<string, string> nameToIdDic;

        private static void InitNameToIdDic()
        {
            nameToIdDic = new Dictionary<string, string>();
            var csv = GetCsv();
            foreach (var row in csv)
            {
                var id = row["ID"];
                var nicknameStr = row["Nicknames"];
                var nicknames = nicknameStr.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var nickname in nicknames)
                {
                    if (!string.IsNullOrWhiteSpace(nickname))
                        nameToIdDic[nickname] = id;
                }
            }
        }

        public static string GetIdByName(string name)
        {
            if (nameToIdDic.ContainsKey(name))
                return nameToIdDic[name];
            else
                return null;
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

        public string Id { get; set; }

        public int Star { get; set; }

        public string Name
        {
            get { return GetName(Id); }
        }
    }
}
