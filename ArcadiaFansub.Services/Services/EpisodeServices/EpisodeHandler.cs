using ArcadiaFansub.Domain.Dtos;
using ArcadiaFansub.Domain.Interfaces;
using ArcadiaFansub.Domain.Models;
using ArcadiaFansub.Domain.RequestDtos;
using ArcadiaFansub.Domain.RequestDtos.EpisodeRequest;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcadiaFansub.Services.Services.EpisodeServices
{
    public class EpisodeHandler(ArcadiaFansubContext AF) : IEpisodeInterface
    {
        public async Task<string> AddNewEpisode(AddNewEpisodeRequest newEpisode)
        {
            try
            {
                var animeQuery = await AF.Animes.FirstOrDefaultAsync(x => x.AnimeId.Trim() == newEpisode.AnimeName.Trim());
                var doesEpisodeAlreadyExist = AF.Episodes.Any(episode => episode.EpisodeNumber == newEpisode.EpisodeNumber && episode.AnimeName == newEpisode.AnimeName);
                if (animeQuery != null && doesEpisodeAlreadyExist == false)
                {
                    //if ()
                    //{
                    //    return $"Episode {newEpisode.EpisodeNumber} already exists";
                    //}
                    string episodeLinks = "";
                    for (int i = 0; i < newEpisode.EpisodeLinks.Count(); i++)
                    {
                        if (episodeLinks == "")
                        {
                            episodeLinks = string.Join("", newEpisode.EpisodeLinks[i]);
                        }
                        else
                        {
                            episodeLinks = string.Join(",", episodeLinks, newEpisode.EpisodeLinks[i]);
                        }
                    }
                    Episode episode = new()
                    {
                        EpisodeId = CreateId.CreateEpisodeId(newEpisode.AnimeName, newEpisode.EpisodeNumber),
                        EpisodeNumber = newEpisode.EpisodeNumber,
                        AnimeId = animeQuery.AnimeId,
                        AnimeName = animeQuery.AnimeName,
                        EpisodeLikes = 0,
                        EpisodeLinks = episodeLinks,
                        EpisodeUploadDate = DateTime.Today,
                    };
                    AF.Episodes.Add(episode);
                    AF.SaveChanges();
                    return $"Episode {newEpisode.EpisodeNumber} succesfully added.";
                }
                else
                {
                    return "Error";
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        public async Task<string> DeleteEpisode(DeleteEpisodeRequest deleteEpisode)
        {
            var episodeToDelete = await AF.Episodes.FirstOrDefaultAsync(x => x.EpisodeId == deleteEpisode.EpisodeId);
            if (episodeToDelete != null)
            {

                AF.Episodes.Remove(episodeToDelete);
                AF.SaveChanges();
                return $"{deleteEpisode.EpisodeId} deleted";
            }
            else
            {
                return "Could Not Remove Episode";
            }
        }
        public async Task<IEnumerable<EpisodesDTO>> GetAllEpisodes()
        {
            var allEpisodes = await AF.Episodes.Select(item => new EpisodesDTO
            {
                AnimeName = item.AnimeName,
                EpisodeNumber = item.EpisodeNumber,
                EpisodeId = item.EpisodeId,
                EpisodeLikes = item.EpisodeLikes,
                EpisodeUploadDate = item.EpisodeUploadDate.ToShortDateString(),
                EpisodeLinks = item.EpisodeLinks,
                AnimeImage = item.Anime.AnimeImage
            }).ToListAsync();

            return allEpisodes.OrderBy(e => e.EpisodeUploadDate).ToList();
        }
        public async Task<string> UpdateEpisode(UpdateEpisodeRequest updateEpisode)
        {
            try
            {
                var episodeQuery = await AF.Episodes.FirstOrDefaultAsync(x => x.EpisodeId == updateEpisode.EpisodeId);
                if (episodeQuery != null)
                {

                    episodeQuery.EpisodeLinks = updateEpisode.EpisodeLinks;
                    AF.SaveChanges();
                    return $"Succesfully changed links for {episodeQuery.AnimeName} episode {episodeQuery.EpisodeNumber}";
                }
                else
                {
                    return "Episode does not exist";
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        public async Task<EpisodePageDTO> GetThatEpisode(string episodeId)
        {
            try
            {
                var episodeQuery = await AF.Episodes
                    .Where(episode => episode.EpisodeId == episodeId)
                    .Select(newEpisode => new EpisodePageDTO
                    {
                        AnimeName = newEpisode.AnimeName.Trim(),
                        EpisodeNumber = newEpisode.EpisodeNumber,
                        EpisodeLinks = newEpisode.EpisodeLinks.Trim(),
                        EpisodeId=newEpisode.EpisodeId,
                    }).FirstOrDefaultAsync();
                if (episodeQuery == null)
                {
                    return new EpisodePageDTO();
                }
                return episodeQuery;

            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<IEnumerable<EpisodesDTO>> GetEpisodePanelAnimeEpisodes(string animeId)
        {
            var episodepanelQuery = await AF.Episodes.Where(x => x.AnimeId == animeId.Trim()).Select(item => new EpisodesDTO
            {
                AnimeImage = item.Anime.AnimeImage,
                AnimeName = item.AnimeName,
                EpisodeId = item.EpisodeId,
                EpisodeLikes = item.EpisodeLikes,
                EpisodeLinks = item.EpisodeLinks,
                EpisodeNumber = item.EpisodeNumber,
                EpisodeUploadDate = item.EpisodeUploadDate.ToShortDateString(),
            }).ToListAsync();
            if (episodepanelQuery.Count > 0)
            {
                return episodepanelQuery;
            }
            return new List<EpisodesDTO>();
        }
        public async Task<IEnumerable<EpisodesDTO>> GetEpisodesByPageQuery(int offSet)
        {
            var episodes = await AF.Episodes.OrderBy(e => e.EpisodeUploadDate)
                .Skip(offSet)
                .Take(6)
                .Select(x => new EpisodesDTO
                {
                    EpisodeUploadDate = x.EpisodeUploadDate.ToShortDateString(),
                    AnimeImage = x.Anime.AnimeImage,
                    EpisodeId = x.EpisodeId,
                    AnimeName = x.AnimeName,
                    EpisodeLikes = x.EpisodeLikes,
                    EpisodeLinks = x.EpisodeLinks,
                    EpisodeNumber = x.EpisodeNumber,
                }).ToListAsync();
            return episodes;
        }
    }
}
