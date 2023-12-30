using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcadiaFansub.Services.Services.EpisodeServices
{
    public static class CreateEpisodeId
    {
        public static string CreateId(string episodeName,int episodeNumber)
        {
            string[] newtext = episodeName.Split(' ');
            string resulttext = "";
            for (int i = 0; i < newtext.Length; i++)
            {
                resulttext += newtext[i] + "-";
            }
            resulttext += episodeNumber;
            return resulttext;
        }
    }
}
