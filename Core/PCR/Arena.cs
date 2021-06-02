using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

using Core.Common;
using Core.Extensions;

using SimpleHTTPClient;

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

        public static void AttackTeamQuery(ArenaAttackTeamQueryParams args)
        {
            var url = GetFullUrl("/PCRArena");
            var jsonStr = JsonUtils.SerializeObject(args);
            var encrtpted = SimpleEncryptor.Default.Encrypt(jsonStr);
            var r = Client.Default.Post(url, encrtpted, ContentTypes.PlainText);
            var apiResult = APIResult.Parse<string>(r);
            apiResult.AssertSuccessCode();
        }
    }


    public class ArenaTeam
    {

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
