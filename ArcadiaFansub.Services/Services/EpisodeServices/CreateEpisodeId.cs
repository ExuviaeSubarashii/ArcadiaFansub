using System.Text;

namespace ArcadiaFansub.Services.Services.EpisodeServices
{
	public static class CreateId
	{
		public static string CreateEpisodeId(string episodeName, int episodeNumber)
		{
			string[] newtext = episodeName.Trim().Split(' ');
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < newtext.Length; i++)
			{
				stringBuilder.Append($"{newtext[i]}-");
			}
			stringBuilder.Append(episodeNumber);
			return stringBuilder.ToString();
		}
		public static string CreateAnimeId(string episodeName)
		{
			string[] newtext = episodeName.Trim().Split(' ');

			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < newtext.Length; i++)
			{
				stringBuilder.Append(newtext[i]);
			}

			return stringBuilder.ToString().ToLower();
		}
	}
}
