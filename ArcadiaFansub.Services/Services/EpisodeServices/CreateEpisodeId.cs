namespace ArcadiaFansub.Services.Services.EpisodeServices
{
    public static class CreateId
    {
        public static string CreateEpisodeId(string episodeName, int episodeNumber)
        {
            string[] newtext = episodeName.Trim().Split(' ');
            string resulttext = "";
            for (int i = 0; i < newtext.Length; i++)
            {
                resulttext += newtext[i] + "-";
            }
            resulttext += episodeNumber;
            return resulttext;
        }
        public static string CreateAnimeId(string episodeName)
        {
            string[] newtext = episodeName.Trim().Split(' ');
            string resulttext = "";
            for (int i = 0; i < newtext.Length; i++)
            {
                resulttext += newtext[i];
                if (i < newtext.Length - 1)
                {
                    resulttext += "-";
                }
            }
            return resulttext.ToLower();
        }
    }
}
