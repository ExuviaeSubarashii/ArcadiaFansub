using ArcadiaFansub.Domain.Dtos;
using ArcadiaFansub.Domain.Interfaces;
using ArcadiaFansub.Domain.Models;
using ArcadiaFansub.Domain.RequestDtos.AnimeRequest;
using ArcadiaFansub.Services.Services.EpisodeServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcadiaFansub.Services.Services.AnimeServices
{
    public class AnimeHandler(ArcadiaFansubContext AF) : IAnimeInterface
    {
        public async Task<IEnumerable<AnimesDTO>> GetAllAnimes(string userToken, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(userToken))
            {
                var getAllAnimesQuery = await AF.Animes.Select(item => new AnimesDTO
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
                var getAllAnimesQuery = await AF.Animes.Select(item => new AnimesDTO
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
        public async Task<IEnumerable<AnimesDTO>> GetAllAnimesByAlphabet(string letter, CancellationToken cancellationToken)
        {

            if (letter == null)
            {
                var queryWithOutAlphabet = await AF.Animes.Take(16).Select(item => new AnimesDTO
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
                    return new List<AnimesDTO>();
                }
                return queryWithOutAlphabet;
            }
            var queryWithAlphabet = await AF.Animes.Take(16).Where(x => x.AnimeName.StartsWith(letter)).Select(item => new AnimesDTO
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
                return new List<AnimesDTO>();
            }
            return queryWithAlphabet;

        }
        public async Task<IEnumerable<AnimesDTO>> GetAllAnimesBySearch(string searchInput, CancellationToken cancellationToken)
        {
            var queryBySearch = await AF.Animes.Where(x => x.AnimeName.Contains(searchInput)).Select(item => new AnimesDTO
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
                return new List<AnimesDTO>();
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
                AnimeImage = ar.ImageLink
            };
            AF.Animes.Add(newAnime);
            await AF.SaveChangesAsync(cancellationToken);
            return $"{ar.AnimeName} successfully created.";
        }
        public async Task<AnimePageDTO> GetThatAnime(string animeId, CancellationToken cancellationToken)
        {
            var animeQuery = await AF.Animes.Where(id => id.AnimeId == animeId.Trim()).Select(item => new AnimePageDTO
            {
                AnimeEpisodeAmount = item.AnimeEpisodeAmount,
                AnimeName = item.AnimeName.Trim(),
                Translator = item.Translator.Trim(),
                Editor = item.Editor.Trim(),
                AnimeImage = item.AnimeImage.Trim(),
                ReleaseDate = item.ReleaseDate.ToShortDateString().Trim(),

            }).FirstOrDefaultAsync(cancellationToken);
            if (animeQuery == null)
            {
                return new AnimePageDTO();
            }
            return animeQuery;

        }
        public async Task<IEnumerable<AnimePageEpisodesDTO>> GetThatAnimeEpisodeLinks(string animeId, CancellationToken cancellationToken)
        {
            var episodesQuery = await AF.Episodes.Where(id => id.AnimeId == animeId.Trim()).Select(item => new AnimePageEpisodesDTO
            {
                AnimeName = item.AnimeName.Trim(),
                EpisodeId = item.EpisodeId.Trim(),
                EpisodeNumber = item.EpisodeNumber,
            }).ToListAsync(cancellationToken);
            if (episodesQuery == null)
            {
                return new List<AnimePageEpisodesDTO>();
            }
            return episodesQuery;
        }
        public async Task<int> GetEpisodeNumber(string animeId, CancellationToken cancellationToken)
        {
            var propquery = await AF.Animes.Where(x => x.AnimeId == animeId).FirstOrDefaultAsync(cancellationToken);
            return propquery.AnimeEpisodeAmount;
        }

        public async Task<IEnumerable<AnimesDTO>> GetUserFavoritedAnimes(string[] animeId, string userToken, CancellationToken cancellationToken)
        {
            List<string> favoritedList = animeId.ToList();
            favoritedList.RemoveAll(x => x == "");
            List<AnimesDTO> animesDTOs = new();
            if (favoritedList.Count == 0)
            {
                return null;
            }
            foreach (var item in favoritedList)
            {
                var animeQuery = await AF.Animes.Where(x => x.AnimeId == item.Trim()).Select(x => new AnimesDTO()
                {
                    AnimeId = x.AnimeId,
                    AnimeEpisodeAmount = x.AnimeEpisodeAmount,
                    AnimeImage = x.AnimeImage,
                    AnimeName = x.AnimeName,
                    Editor = x.Editor,
                    ReleaseDate = x.ReleaseDate.ToShortDateString(),
                    Translator = x.Translator,
                    IsFavorited = IsFavorited.IsFavoritedByUser(userToken, x.AnimeId)
                }).FirstOrDefaultAsync(cancellationToken);
                if (animeQuery != null)
                {
                    animesDTOs.Add(animeQuery);
                }
            }


            if (animesDTOs != null)
            {
                return animesDTOs;
            }
            else
            {
                return null;
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

                animeQuery.AnimeName = string.IsNullOrEmpty(updateAnimeRequest.NewAnimeName)!=true ? updateAnimeRequest.NewAnimeName : animeQuery.AnimeName;

                animeQuery.Editor = string.IsNullOrEmpty(updateAnimeRequest.NewEditorName)!=true ? updateAnimeRequest.NewEditorName : animeQuery.Editor;

                animeQuery.Translator = string.IsNullOrEmpty(updateAnimeRequest.NewTranslatorName)!=true ? updateAnimeRequest.NewTranslatorName : animeQuery.Translator;

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
    }
}
