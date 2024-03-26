using ArcadiaFansub.Domain.Dtos;
using ArcadiaFansub.Domain.Interfaces;
using ArcadiaFansub.Domain.Models;
using ArcadiaFansub.Domain.RequestDtos.AnimeRequest;
using ArcadiaFansub.Services.Services.CommentServices;
using ArcadiaFansub.Services.Services.EpisodeServices;
using Microsoft.EntityFrameworkCore;

namespace ArcadiaFansub.Services.Services.AnimeServices
{
    public class AnimeHandler(ArcadiaFansubContext AF, CommentHandler CH, EpisodeHandler EH) : IAnimeInterface
    {
        public async Task<IEnumerable<AnimesDto>> GetAllAnimes(string userToken, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(userToken))
            {
                var getAllAnimesQuery = await AF.Animes.Select(item => new AnimesDto
                {
                    AnimeEpisodeAmount = item.AnimeEpisodeAmount,
                    AnimeId = item.AnimeId,
                    AnimeName = item.AnimeName.Trim(),
                    ReleaseDate = item.ReleaseDate.ToShortDateString(),
                    Editor = item.Editor.Trim(),
                    Translator = item.Translator.Trim(),
                    AnimeImage = item.AnimeImage.Trim(),
                    IsFavorited = IsFavorited.IsFavoritedByUser(userToken, item.AnimeId)
                }).OrderBy(s => s.AnimeName).ToListAsync(cancellationToken);
                return getAllAnimesQuery;
            }
            else
            {
                var getAllAnimesQuery = await AF.Animes.Select(item => new AnimesDto
                {
                    AnimeEpisodeAmount = item.AnimeEpisodeAmount,
                    AnimeId = item.AnimeId,
                    AnimeName = item.AnimeName.Trim(),
                    ReleaseDate = item.ReleaseDate.ToShortDateString(),
                    Editor = item.Editor.Trim(),
                    Translator = item.Translator.Trim(),
                    AnimeImage = item.AnimeImage.Trim(),
                    IsFavorited = false
                }).OrderBy(s => s.AnimeName).ToListAsync(cancellationToken);
                return getAllAnimesQuery;
            }


        }
        public async Task<IEnumerable<AnimesDto>> GetAllAnimesByAlphabet(string letter, CancellationToken cancellationToken)
        {

            if (letter == null)
            {
                var queryWithOutAlphabet = await AF.Animes.Take(16).Select(item => new AnimesDto
                {
                    AnimeEpisodeAmount = item.AnimeEpisodeAmount,
                    AnimeId = item.AnimeId,
                    AnimeName = item.AnimeName.Trim(),
                    Editor = item.Editor.Trim(),
                    ReleaseDate = item.ReleaseDate.ToShortDateString(),
                    Translator = item.Translator.Trim(),
                    AnimeImage = item.AnimeImage.Trim(),
                }).ToListAsync(cancellationToken);
                if (!queryWithOutAlphabet.Any())
                {
                    return new List<AnimesDto>();
                }
                return queryWithOutAlphabet;
            }
            var queryWithAlphabet = await AF.Animes.Take(16).Where(x => x.AnimeName.StartsWith(letter)).Select(item => new AnimesDto
            {
                AnimeEpisodeAmount = item.AnimeEpisodeAmount,
                AnimeId = item.AnimeId,
                AnimeName = item.AnimeName.Trim(),
                Editor = item.Editor.Trim(),
                ReleaseDate = item.ReleaseDate.ToShortDateString(),
                Translator = item.Translator.Trim(),
                AnimeImage = item.AnimeImage.Trim(),
            }).ToListAsync(cancellationToken);
            if (!queryWithAlphabet.Any())
            {
                return new List<AnimesDto>();
            }
            return queryWithAlphabet;

        }
        public async Task<IEnumerable<AnimesDto>> GetAllAnimesBySearch(string searchInput, CancellationToken cancellationToken)
        {
            var queryBySearch = await AF.Animes.Where(x => x.AnimeName.StartsWith(searchInput)).Select(item => new AnimesDto
            {
                AnimeEpisodeAmount = item.AnimeEpisodeAmount,
                AnimeId = item.AnimeId,
                AnimeName = item.AnimeName,
                Editor = item.Editor,
                ReleaseDate = item.ReleaseDate.ToShortDateString(),
                Translator = item.Translator,
            }).ToListAsync(cancellationToken);
            if (!queryBySearch.Any())
            {
                return new List<AnimesDto>();
            }
            return queryBySearch;
        }
        public async Task<string> DeleteAnime(string animeId, CancellationToken cancellationToken)
        {
            var doesAnimeExist = await AF.Animes.FirstOrDefaultAsync(x => x.AnimeId == animeId);
            if (doesAnimeExist == null)
            {
                return "Id does not exist.";
            }
            AF.Remove(doesAnimeExist);
            await CH.DeleteAllComments(animeId, cancellationToken);
            await EH.DeleteAllEpisodes(animeId, cancellationToken);
            await DeleteAnimeFromEverybody(animeId, cancellationToken);
            await AF.SaveChangesAsync(cancellationToken);
            return $"Succesfully Deleted {doesAnimeExist.AnimeName}";
        }
        public async Task<string> CreateAnime(AddNewAnimeRequest ar, CancellationToken cancellationToken)
        {
            var doesAnimeAlreadyExist = await AF.Animes.Select(x => x.AnimeName == ar.AnimeName).FirstOrDefaultAsync();
            if (doesAnimeAlreadyExist)
            {
                return "Anime already exists";
            }
            Anime newAnime = new()
            {
                AnimeId = CreateId.CreateAnimeId(ar.AnimeName),
                AnimeEpisodeAmount = ar.AnimeEpisodeAmount,
                AnimeName = ar.AnimeName,
                Editor = ar.Editor,
                ReleaseDate = DateTime.Now,
                Translator = ar.Translator,
                AnimeImage = CreateId.CreateAnimeId(ar.AnimeName) + ".webp"
            };
            AF.Animes.Add(newAnime);
            await AF.SaveChangesAsync(cancellationToken);
            return $"{ar.AnimeName} successfully created.";
        }
        public async Task<AnimePageDto> GetThatAnime(string animeId, CancellationToken cancellationToken)
        {
            var animeQuery = await AF.Animes.Where(id => id.AnimeId == animeId.Trim()).Select(item => new AnimePageDto
            {
                AnimeEpisodeAmount = item.AnimeEpisodeAmount,
                AnimeName = item.AnimeName.Trim(),
                Translator = item.Translator.Trim(),
                Editor = item.Editor.Trim(),
                AnimeImage = item.AnimeImage.Trim(),
                ReleaseDate = item.ReleaseDate.ToShortDateString().Trim(),
                AnimeId = item.AnimeId.Trim()


            }).FirstOrDefaultAsync(cancellationToken);
            if (animeQuery == null)
            {
                return new AnimePageDto();
            }
            return animeQuery;

        }
        public async Task<IEnumerable<AnimePageEpisodesDto>> GetThatAnimeEpisodeLinks(string animeId, CancellationToken cancellationToken)
        {
            var episodesQuery = await AF.Episodes.Where(id => id.AnimeId == animeId.Trim()).Select(item => new AnimePageEpisodesDto
            {
                AnimeName = item.AnimeName.Trim(),
                EpisodeId = item.EpisodeId.Trim(),
                EpisodeNumber = item.EpisodeNumber,
            }).ToListAsync(cancellationToken);
            if (episodesQuery == null)
            {
                return new List<AnimePageEpisodesDto>();
            }
            return episodesQuery.OrderBy(x => x.EpisodeNumber);
        }
        public async Task<int> GetEpisodeNumber(string animeId, CancellationToken cancellationToken)
        {
            var propquery = await AF.Animes.Where(x => x.AnimeId == animeId).FirstOrDefaultAsync(cancellationToken);
            return propquery.AnimeEpisodeAmount;
        }
        public async Task<IEnumerable<AnimesDto>> GetUserFavoritedAnimes(string[] animeId, string userToken, CancellationToken cancellationToken)
        {
            List<string> favoritedList = animeId.ToList();
            favoritedList.RemoveAll(x => x == "");
            if (favoritedList.Count == 0)
            {
                return new List<AnimesDto>();
            }
            var animeQuery = await AF.Animes.Where(x => favoritedList.Contains(x.AnimeId)).Select(x => new AnimesDto()
            {
                AnimeId = x.AnimeId,
                AnimeEpisodeAmount = x.AnimeEpisodeAmount,
                AnimeImage = x.AnimeImage,
                AnimeName = x.AnimeName,
                Editor = x.Editor,
                ReleaseDate = x.ReleaseDate.ToShortDateString(),
                Translator = x.Translator,
                IsFavorited = IsFavorited.IsFavoritedByUser(userToken, x.AnimeId)
            }).ToListAsync(cancellationToken);

            if (animeQuery != null)
            {
                return animeQuery;
            }
            else
            {
                return new List<AnimesDto>();
            }
        }
        public async Task<string> AddOrRemoveAnimeToFavorites(string animeId, string userToken, CancellationToken cancellationToken)
        {
            var userQuery = await AF.Users.FirstOrDefaultAsync(x => x.UserToken == userToken);
            if (userQuery != null)
            {
                List<string> favoritedList = userQuery.FavoritedAnimes.Split(',').ToList();
                favoritedList.RemoveAll(x => x == "");
                if (favoritedList.Contains(animeId.Trim()))
                {
                    favoritedList.Remove(animeId.Trim());
                    userQuery.FavoritedAnimes = string.Join(",", favoritedList);
                    await AF.SaveChangesAsync(cancellationToken);
                    return "Anime removed from favorites";
                }
                favoritedList.Add(animeId.Trim());
                userQuery.FavoritedAnimes = string.Join(",", favoritedList);
                await AF.SaveChangesAsync(cancellationToken);
                return "Anime favorited";
            }
            else
            {
                return "Could not add anime to favorites";
            }
        }
        public async Task<string> UpdateAnimeProperties(UpdateAnimeRequest updateAnimeRequest, CancellationToken cancellationToken)
        {
            try
            {
                var animeQuery = await AF.Animes.FirstOrDefaultAsync(x => x.AnimeId == updateAnimeRequest.AnimeId, cancellationToken);
                if (animeQuery == null)
                {
                    return "Anime does not exist";
                }
                animeQuery.AnimeEpisodeAmount = updateAnimeRequest.NewEpisodeAmount != 0 || updateAnimeRequest.NewEpisodeAmount != null ? animeQuery.AnimeEpisodeAmount : (int)updateAnimeRequest.NewEpisodeAmount;

                animeQuery.AnimeName = string.IsNullOrEmpty(updateAnimeRequest.NewAnimeName) != true ? updateAnimeRequest.NewAnimeName : animeQuery.AnimeName;

                animeQuery.Editor = string.IsNullOrEmpty(updateAnimeRequest.NewEditorName) != true ? updateAnimeRequest.NewEditorName : animeQuery.Editor;

                animeQuery.Translator = string.IsNullOrEmpty(updateAnimeRequest.NewTranslatorName) != true ? updateAnimeRequest.NewTranslatorName : animeQuery.Translator;

                animeQuery.ReleaseDate = updateAnimeRequest.NewReleaseDate != null ? (DateTime)updateAnimeRequest.NewReleaseDate : animeQuery.ReleaseDate;


                //if (updateAnimeRequest.NewEpisodeAmount != 0 && updateAnimeRequest.NewEpisodeAmount != null)
                //{
                //    animeQuery.AnimeEpisodeAmount = (int)updateAnimeRequest.NewEpisodeAmount;
                //}
                //if (string.IsNullOrEmpty(updateAnimeRequest.NewAnimeName) != true)
                //{
                //    animeQuery.AnimeName = updateAnimeRequest.NewAnimeName;
                //}
                //if (string.IsNullOrEmpty(updateAnimeRequest.NewEditorName) != true)
                //{
                //    animeQuery.Editor = updateAnimeRequest.NewEditorName;
                //}
                //if (string.IsNullOrEmpty(updateAnimeRequest.NewTranslatorName) != true)
                //{
                //    animeQuery.Translator = updateAnimeRequest.NewTranslatorName;
                //}
                //if (updateAnimeRequest.NewReleaseDate != null)
                //{
                //    animeQuery.ReleaseDate = (DateTime)updateAnimeRequest.NewReleaseDate;
                //}


                //change every episode's anime name to the new anime name
                if (updateAnimeRequest.NewAnimeName != null)
                {

                    var episodesQuery = await AF.Episodes.Where(x => x.AnimeId == updateAnimeRequest.AnimeId).ToListAsync(cancellationToken);
                    foreach (var item in episodesQuery)
                    {
                        item.AnimeName = updateAnimeRequest.NewAnimeName;

                    }
                }
                var result = await AF.SaveChangesAsync(cancellationToken);
                if (result > 0)
                {
                    return "Anime properties updated";
                }
                return "Could not update anime properties";
            }
            catch (Exception)
            {

                return "Could not update anime properties";
                throw;
            }
        }
        public async Task<string> DeleteAnimeFromEverybody(string animeId, CancellationToken cancellationToken)
        {
            var usersQuery = await AF.Users.Where(x => x.FavoritedAnimes.Contains(animeId)).ToListAsync();
            foreach (var user in usersQuery)
            {
                user.FavoritedAnimes = user.FavoritedAnimes.Replace(animeId, "");
            }
            await AF.SaveChangesAsync(cancellationToken);
            return "Succesfully Deleted";
        }
    }
}
