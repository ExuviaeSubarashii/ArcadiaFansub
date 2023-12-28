using ArcadiaFansub.Domain.Dtos;
using ArcadiaFansub.Domain.Interfaces;
using ArcadiaFansub.Domain.Models;
using ArcadiaFansub.Domain.RequestDtos.AnimeRequest;
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
        public async Task<IEnumerable<AnimesDTO>> GetAllAnimes()
        {
            var getAllAnimesQuery = await AF.Animes.Select(item => new AnimesDTO
            {
                AnimeEpisodeAmount = item.AnimeEpisodeAmount,
                AnimeId = item.AnimeId,
                AnimeName = item.AnimeName.Trim(),
                Links = item.Links.Trim(),
                ReleaseDate = item.ReleaseDate,
                Editor = item.Editor.Trim(),
                Translator = item.Translator.Trim(),
            }).ToListAsync();
            return getAllAnimesQuery;
        }
        public async Task<IEnumerable<AnimesDTO>> GetAllAnimesByAlphabet(string letter)
        {
            var queryWithAlphabet = await AF.Animes.Take(16).Where(x => x.AnimeName.StartsWith(letter)).Select(item => new AnimesDTO
            {
                AnimeEpisodeAmount = item.AnimeEpisodeAmount,
                AnimeId = item.AnimeId,
                AnimeName = item.AnimeName,
                Editor = item.Editor,
                Links = item.Links,
                ReleaseDate = item.ReleaseDate,
                Translator = item.Translator,
            }).ToListAsync();
            if (!queryWithAlphabet.Any())
            {
                return new List<AnimesDTO>();
            }
            return queryWithAlphabet;

        }
        public async Task<IEnumerable<AnimesDTO>> GetAllAnimesBySearch(string searchInput)
        {
            var queryBySearch = await AF.Animes.Where(x => x.AnimeName.Contains(searchInput)).Select(item => new AnimesDTO
            {
                AnimeEpisodeAmount = item.AnimeEpisodeAmount,
                AnimeId = item.AnimeId,
                AnimeName = item.AnimeName,
                Editor = item.Editor,
                Links = item.Links,
                ReleaseDate = item.ReleaseDate,
                Translator = item.Translator,
            }).ToListAsync();
            if (!queryBySearch.Any())
            {
                return new List<AnimesDTO>();
            }
            return queryBySearch;
        }
        public async Task<string> DeleteAnime(int animeId)
        {
            var doesAnimeExist=await AF.Animes.FirstOrDefaultAsync(x=>x.AnimeId == animeId);
            if (doesAnimeExist == null)
            {
                return "Id does not exist.";
            }
            AF.Remove(doesAnimeExist);
            AF.SaveChanges();
            return $"Succesfully Deleted {doesAnimeExist.AnimeName}";
        }
        public async Task<string> CreateAnime(AddNewAnimeRequest ar)
        {
            var doesAnimeAlreadyExist=await AF.Animes.Select(x=>x.AnimeName == ar.AnimeName).FirstOrDefaultAsync();
            if (doesAnimeAlreadyExist)
            {
                return "Anime already exists";
            }
            Anime newAnime = new()
            {
                AnimeId = ar.AnimeId,
                AnimeEpisodeAmount = ar.AnimeEpisodeAmount,
                AnimeName = ar.AnimeName,
                Editor = ar.Editor,
                Links = ar.Links,
                ReleaseDate = ar.ReleaseDate,
                Translator = ar.Translator,
            };
            AF.Animes.Add(newAnime);
            AF.SaveChanges();
            return $"{ar.AnimeName} added";
        }
    }
}
