using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcadiaFansub.Services.Services.EpisodeServices
{
    public static class ConvertLinks
    {
        public static string ConvertToEmbed(string[] newEpisodeLinks)
        {
            string episodeLinks = "";
            for (int i = 0; i < newEpisodeLinks.Count(); i++)
            {
                if (episodeLinks == "")
                {
                    if (newEpisodeLinks[i].StartsWith("https://drive.google.com/file/d/"))
                    {
                        string[] gdriveStart = newEpisodeLinks[i].Split('/');
                        newEpisodeLinks[i] = $"https://drive.google.com/file/d/{gdriveStart[5]}/preview";
                        episodeLinks = string.Join("", newEpisodeLinks[i]);
                    }
                    else if (newEpisodeLinks[i].StartsWith("https://www.yourupload.com"))
                    {
                        string[] yourUploadStart = newEpisodeLinks[i].Split("/");
                        newEpisodeLinks[i] = $"https://www.yourupload.com/embed/{yourUploadStart[5]}";
                        episodeLinks = string.Join("", newEpisodeLinks[i]);
                    }
                    //add more options
                    else
                    {
                        episodeLinks = string.Join("", newEpisodeLinks[i]);
                    }
                }
                else
                {
                    episodeLinks = string.Join(",", episodeLinks, newEpisodeLinks[i]);
                }
            }
            return episodeLinks;
        }
    }
}
