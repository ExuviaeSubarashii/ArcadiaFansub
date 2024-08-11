using ArcadiaFansub.Domain.Models;

namespace ArcadiaFansub.Services.Services.AnimeServices
{
	public class IsFavorited
	{
		private IsFavorited() { }
		public static bool IsFavoritedByUser(string userToken, string animeId)
		{
			using ArcadiaFansubContext AF = new();
			var userQuery = AF.Users.FirstOrDefault(x => x.UserToken == userToken);
			if (userQuery != null)
			{
				List<string> favoritedSeries = userQuery.FavoritedAnimes != null ? userQuery.FavoritedAnimes.Split(',').ToList() : new List<string>();
				bool isFavorited = favoritedSeries.Contains(animeId.Trim());
				return isFavorited;
			}
			else { return false; }
		}
	}
}
