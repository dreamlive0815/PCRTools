using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

using Core.Common;
using Core.Emulators;
using Core.Extensions;
using Core.Model;

using SimpleHTTPClient;

using CvPoint = OpenCvSharp.Point;
using CvSize = OpenCvSharp.Size;
using OpenCvSharp;


namespace Core.PCR
{
    public class Arena
    {
#if DEBUG
        //private static string SERVER_IP = "127.0.0.1";
        private static string SERVER_IP = "8.210.27.173";
#else
        private static string SERVER_IP = "8.210.27.173";
#endif
        private static int SERVER_PORT = 80;

        private static string GetFullUrl(string uri)
        {
            uri = uri.TrimStart('/');
            return $"http://{SERVER_IP}:{SERVER_PORT}/{uri}";
        }

        public static List<ArenaAttackTeam> AttackTeamQuery(ArenaAttackTeamQueryParams args)
        {
            var url = GetFullUrl("/PCRArena");
            var jsonStr = JsonUtils.SerializeObject(args);
            var encrtpted = SimpleEncryptor.Default.Encrypt(jsonStr);
            var r = Client.Default.Post(url, encrtpted, ContentTypes.PlainText);
            var apiResult = APIResult.Parse<string>(r);
            apiResult.AssertSuccessCode();
            var rawData = apiResult.Data;
            var arenaApiResult = ArenaAPIResult.Parse<ArenaAttackTeamQueryResult>(rawData);
            arenaApiResult.AssertSuccessCode();
            var teams = arenaApiResult.Data.Teams;
            return teams;
        }

        public static Mat Render(List<ArenaAttackTeam> teams)
        {
            //var iconSize = 64;
            //var width = iconSize * 7;
            //var height = iconSize * (teams.Count + 3);
            //var mat = new Mat(new CvSize(width, height), MatType.CV_8UC3, Scalar.White);

            //var getIcon = new Func<ArenaUnit, Mat>((unit) =>
            //{
            //    var icon = new Mat(GetUnitIconFilePath(unit.ID.ToString()));
            //    icon = icon.Resize(new CvSize(64, 64));
            //    return icon;
            //});
            //var drawIcon = new Action<Mat, int, int>((icon, rowIndex, colIndex) =>
            //{
            //    var xRange = new Range(colIndex * iconSize, (colIndex + 1) * iconSize);
            //    var yRange = new Range(rowIndex * iconSize, (rowIndex + 1) * iconSize);
            //    icon.CopyTo(mat[yRange, xRange]);
            //});
            //var drawIcons = new Action<List<ArenaUnit>, int>((arenaUnits, rowIndex) =>
            //{
            //    for (var i = 0; i < arenaUnits.Count; i++)
            //    {
            //        var defUnit = arenaUnits[i];
            //        var icon = getIcon(defUnit);
            //        drawIcon(icon, rowIndex, i);
            //    }
            //});

            //var first = teams[0];
            //drawIcons(first.DefenceUnits, 1);

            //for (var i = 0; i < teams.Count; i++)
            //{
            //    var team = teams[i];
            //    drawIcons(team.AttackUnits, i + 3);
            //}

            //Cv2.ImShow("QueryResult", mat);

            return null;
        }

        private static int GetSquareSize(CvPoint p1, CvPoint p0)
        {
            var dx = p1.X - p0.X;
            var dy = p1.Y - p0.Y;
            return dx * dx + dy * dy;
        }

        private static bool IsSquare(CvPoint[] points)
        {
            for (var j = 2; j < 5; j++)
            {
                var p1 = points[j % 4];
                var p2 = points[j - 2];
                var p0 = points[j - 1];
                var ratio = 1.0 * GetSquareSize(p1, p0) / GetSquareSize(p2, p0);
                if (ratio < 0.8 || ratio > 1.2)
                    return false;
            }
            return true;
        }

        private static CvPoint GetCenterPoint(CvPoint[] points)
        {
            var x = points.Average(v => v.X);
            var y = points.Average(v => v.Y);
            return new CvPoint(x, y);
        }

        class FindUnitIconMatTempInfo
        {
            public int Index;
            public double BorderLength;
            public CvPoint[] Points;
            public CvPoint CenterPoint;
        }

        public static List<Unit> FindUnits(Img img)
        {
            var src = img.ToMat().Clone();
            var blurred = src.GaussianBlur(new OpenCvSharp.Size(3, 3), 0);
            var gray = blurred.ToGray();
            var canny = gray.Canny(30, 100);
            
            CvPoint[][] contours;
            HierarchyIndex[] hierarchy;
            canny.FindContours(out contours, out hierarchy, RetrievalModes.Tree, ContourApproximationModes.ApproxSimple);

            var list = new List<FindUnitIconMatTempInfo>();
            for (var i = 0; i < contours.Length; i++)
            {
                var contour = contours[i];
                var borderLen = Cv2.ArcLength(contour, true);
                var approx = Cv2.ApproxPolyDP(contour, 0.02 * borderLen, true);
                if (approx.Length == 4 && Cv2.IsContourConvex(approx))
                {
                    if (borderLen > 100 && IsSquare(approx))
                    {
                        list.Add(new FindUnitIconMatTempInfo()
                        {
                            Index = i,
                            BorderLength = borderLen,
                            Points = approx,
                            CenterPoint = GetCenterPoint(approx),
                        });
                    }
                } 
            }
            if (list.Count == 0)
                return new List<Unit>();

            list.Sort((a, b) =>
            {
                return a.BorderLength.CompareTo(b.BorderLength);
            });
            var listList = new List<List<FindUnitIconMatTempInfo>>();
            var tempList = new List<FindUnitIconMatTempInfo>(){ list[0] };
            for (var i = 1; i < list.Count; i++)
            {
                var ratio = list[i].BorderLength / list[i - 1].BorderLength;
                if (ratio > 1.1)
                {
                    listList.Add(tempList);
                    tempList = new List<FindUnitIconMatTempInfo>();
                }
                tempList.Add(list[i]);
            }
            listList.Add(tempList);
            var maxCount = listList.Max(x => x.Count);
            var maxCountList = listList.Where(x => x.Count == maxCount).FirstOrDefault();
            //根据FindContours的索引重新排序
            maxCountList.Sort((a, b) =>
            {
                return a.Index.CompareTo(b.Index);
            });

            //过滤掉重复的
            var centerPointList = new List<CvPoint>();
            tempList = new List<FindUnitIconMatTempInfo>();
            var hasSame = new Func<CvPoint, bool>((point) =>
            {
                foreach (var pt in centerPointList)
                {
                    if (Math.Abs(point.X - pt.X) < 10 && Math.Abs(point.Y - pt.Y) < 10)
                    {
                        return true;
                    }
                }
                return false;
            });
            foreach (var item in maxCountList)
            {
                var center = item.CenterPoint;
                if (!hasSame(center))
                {
                    tempList.Add(item);
                    centerPointList.Add(center);
                }
            }
            maxCountList = tempList;

            var iconSources = new List<Img>();
            for (var i = 0; i < maxCountList.Count; i++)
            {
                var item = maxCountList[i];
                var wid = item.BorderLength * 0.25 * 1.1;
                var center = item.CenterPoint;
                var rect = new Rect(center - new CvPoint(wid / 2, wid / 2), new CvSize(wid, wid));
                var mat = new Mat(src, rect);
                iconSources.Add(new Img(mat));
                //Cv2.ImShow(i.ToString(), mat);
            }

            var averBorderLen = maxCountList.Average(x => x.BorderLength);
            var r = FindUnits(iconSources, averBorderLen * 0.25);
            r.Reverse();
            return r;
        }

        private static double templateFixScale = 0.98;

        private static double GetFindUnitThreshold()
        {
            var res = ResourceManager.Default.GetResource("${G}/Json/" + ImageSamplingData.DefaultFileName);
            var data = res.ParseObjWithCache(int.MaxValue, (filePath) =>
            {
                return ImageSamplingData.FromFile(filePath);
            });
            var r = data.GetThreshold("ArenaFindUnit");
            return r;
        }

        public static List<Unit> FindUnits(List<Img> iconSources, double iconSize)
        {
            var unitIds = Unit.GetAllIds();

            var iconCache = new Dictionary<Tuple<int, int>,Img>();
            var getIcon = new Func<int, int, Img>((id, star) =>
            {
                var key = new Tuple<int, int>(id, star);
                if (iconCache.ContainsKey(key))
                    return iconCache[key];
                var res = Unit.GetIconResource(id, star);
                if (!res.Exists)
                    return null;
                var icon = res.ToImg();
                icon = icon.GetScaled(iconSize / Unit.DEFAULT_ICON_SIZE * templateFixScale);
                icon = icon.GetPartial(new RVec4f(0, 0.2, 1, 0.6));
                iconCache[key] = icon;
                return icon;
            });

            var threshold = GetFindUnitThreshold();
            var stars = new int[] { 6, 3, 1 };
            var findUnit = new Func<Img, Unit>((iconSource) =>
            {
                foreach (var star in stars)
                {
                    foreach (var id in unitIds)
                    {
                        var icon = getIcon(id, star);
                        if (icon == null)
                            continue;
                        var matchRes = iconSource.Match(icon, threshold);
                        if (matchRes.Success)
                            return new Unit() { Id = id, Star = star };
                    }
                }
                return null;
            });

            var r = new List<Unit>();
            foreach (var iconSource in iconSources)
            {
                var unit = findUnit(iconSource);
                if (unit == null)
                    continue;
                r.Add(unit);
            }

            return r;
        }
    }


    public class ArenaAPIResult
    {
        public static ArenaAPIResult<T> Parse<T>(string s)
        {
            var r = JsonUtils.DeserializeObject<ArenaAPIResult<T>>(s);
            return r;
        }
    }

    public class ArenaAPIResult<T>
    {
        public static readonly int SUCCESS_CODE = 0;

        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("data")]
        public T Data { get; set; }

        public void AssertSuccessCode()
        {
            if (Code != SUCCESS_CODE)
            {
                throw new Exception(Message);
            }
        }
    }

    public class ArenaAttackTeamQueryResult
    {
        [JsonProperty("result")]
        public List<ArenaAttackTeam> Teams { get; set; }
    }

    public class ArenaAttackTeam
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("atk")]
        public List<ArenaUnit> AttackUnits { get; set; }

        [JsonProperty("def")]
        public List<ArenaUnit> DefenceUnits { get; set; }

        [JsonProperty("updated")]
        public string Time { get; set; }

        [JsonProperty("up")]
        public int Like { get; set; }

        [JsonProperty("down")]
        public int Dislike { get; set; }
    }

    public class ArenaUnit
    {
        [JsonProperty("id")]
        public int LongID { get; set; }

        [JsonIgnore]
        public int ID
        {
            get { return LongID / 100; }
        }

        [JsonProperty("star")]
        public int Star { get; set; }

        [JsonProperty("equip")]
        public bool HasSpecialEquip { get; set; }
    }

    public class ArenaAttackTeamQueryParams
    {
        public static readonly int REGION_ALL = 1;
        public static readonly int REGION_MAINLAND = 2;
        public static readonly int REGION_TAIWAN = 3;
        public static readonly int REGION_JAPAN = 4;
        public static readonly int REGION_INTERNATIONAL = 5;

        public static readonly int SORT_ALL = 1;
        public static readonly int SORT_TIME = 2;
        public static readonly int SORT_COMMENT = 3;

        [JsonProperty("_sign")]
        public string Sign { get; set; } = "a";

        [JsonProperty("nonce")]
        public string Nonce { get; set; } = "a";

        [JsonProperty("def")]
        public List<int> DefenceTeamIds { get; set; } = new List<int>();

        [JsonProperty("page")]
        public int Page { get; set; } = 1;

        [JsonProperty("sort")]
        public int Sort { get; set; } = SORT_ALL;

        [JsonProperty("region")]
        public int Region { get; set; } = REGION_ALL;

        [JsonProperty("ts")]
        public long Timestamp { get; set; } = DateTime.Now.ToUnixTimestamp();

        public ArenaAttackTeamQueryParams SetDefenceTeamIds(IEnumerable<int> defenceUnitIds)
        {
            DefenceTeamIds = defenceUnitIds.Select(id => id * 100 + 1).ToList();
            return this;
        }
    }
}
