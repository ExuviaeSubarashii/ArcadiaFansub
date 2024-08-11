using ArcadiaFansub.Domain.Models;

namespace ArcadiaFansub.Services.Services.CommentServices
{
	public class IsCommentOwner
	{
		private IsCommentOwner() { }
		public static bool IsOwner(string userToken, int userId)
		{
			using ArcadiaFansubContext AF = new ArcadiaFansubContext();
			var userQuery = AF.Users.FirstOrDefault(x => x.UserToken == userToken);
			if (userQuery != null)
			{
				bool isOwner = userQuery.UserId == userId;
				return isOwner;
			}
			else
			{
				return false;
			}
		}
	}
}
