using ArcadiaFansub.Domain.Dtos;
using ArcadiaFansub.Domain.Interfaces;
using ArcadiaFansub.Domain.Models;
using ArcadiaFansub.Domain.RequestDtos;
using ArcadiaFansub.Domain.RequestDtos.EpisodeRequest;
using ArcadiaFansub.Services.Services.CommentServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ArcadiaFansub.Services.Services.EpisodeServices
{
    public class EpisodeHandler(ArcadiaFansubContext AF, CommentHandler CH) : IEpisodeInterface
    {
        public async Task<string> AddNewEpisode(AddNewEpisodeRequest newEpisode, CancellationToken cancellationToken)
        {
            try
            {
                var animeQuery = await AF.Animes.FirstOrDefaultAsync(x => x.AnimeId.Trim() == newEpisode.AnimeName.Trim(), cancellationToken);
                var doesEpisodeAlreadyExist = await AF.Episodes.AnyAsync(episode => episode.EpisodeNumber == newEpisode.EpisodeNumber && episode.AnimeName == newEpisode.AnimeName, cancellationToken);
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
                        EpisodeUploadDate = DateTime.Now,
                    };
                    AF.Episodes.Add(episode);
                    await AF.SaveChangesAsync(cancellationToken);
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
        public async Task<string> DeleteEpisode(DeleteEpisodeRequest deleteEpisode, CancellationToken cancellationToken)
        {
            var episodeToDelete = await AF.Episodes.FirstOrDefaultAsync(x => x.EpisodeId == deleteEpisode.EpisodeId, cancellationToken);
            if (episodeToDelete != null)
            {

                AF.Episodes.Remove(episodeToDelete);
                AF.SaveChanges();
                await CH.DeleteAllComments(deleteEpisode.EpisodeId, cancellationToken);
                return $"{deleteEpisode.EpisodeId} deleted";
            }
            else
            {
                return "Could Not Remove Episode";
            }
        }
        public async Task<IEnumerable<EpisodesDTO>> GetAllEpisodes(CancellationToken cancellationToken)
        {
            var allEpisodes = await AF.Episodes.Select(item => new EpisodesDTO
            {
                AnimeName = item.AnimeName,
                EpisodeNumber = item.EpisodeNumber,
                EpisodeId = item.EpisodeId,
                EpisodeLikes = item.EpisodeLikes,
                EpisodeUploadDate = item.EpisodeUploadDate,
                SortingDate = GetDate(item.EpisodeUploadDate),
                EpisodeLinks = item.EpisodeLinks,
                AnimeImage = item.Anime.AnimeImage,
                AnimeId = item.AnimeId.Trim(),
            }).ToListAsync(cancellationToken);

            return allEpisodes
                .OrderByDescending(e => e.EpisodeUploadDate);
        }
        public async Task<string> UpdateEpisode(UpdateEpisodeRequest updateEpisode, CancellationToken cancellationToken)
        {
            try
            {
                var episodeQuery = await AF.Episodes.FirstOrDefaultAsync(x => x.EpisodeId == updateEpisode.EpisodeId, cancellationToken);
                if (episodeQuery != null)
                {

                    episodeQuery.EpisodeLinks = updateEpisode.EpisodeLinks;
                    await AF.SaveChangesAsync(cancellationToken);
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
        public async Task<EpisodePageDTO> GetThatEpisode(string episodeId, CancellationToken cancellationToken)
        {
            try
            {
                var episodeQuery = await AF.Episodes
                    .Where(episode => episode.EpisodeId == episodeId)
                    .Select(newEpisode => new EpisodePageDTO
                    {
                        AnimeName = newEpisode.AnimeName.Trim(),
                        AnimeId = newEpisode.AnimeId.Trim(),
                        EpisodeNumber = newEpisode.EpisodeNumber,
                        EpisodeLinks = newEpisode.EpisodeLinks.Trim(),
                        EpisodeId = newEpisode.EpisodeId,
                    }).FirstOrDefaultAsync(cancellationToken);
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
        public async Task<IEnumerable<EpisodesDTO>> GetEpisodePanelAnimeEpisodes(string animeId, CancellationToken cancellationToken)
        {
            var episodepanelQuery = await AF.Episodes.Where(x => x.AnimeId == animeId.Trim()).Select(item => new EpisodesDTO
            {
                AnimeImage = item.Anime.AnimeImage,
                AnimeName = item.AnimeName,
                EpisodeId = item.EpisodeId,
                EpisodeLikes = item.EpisodeLikes,
                EpisodeLinks = item.EpisodeLinks,
                EpisodeNumber = item.EpisodeNumber,
                EpisodeUploadDate = item.EpisodeUploadDate,
            }).ToListAsync(cancellationToken);
            if (episodepanelQuery.Count > 0)
            {
                return episodepanelQuery;
            }
            return new List<EpisodesDTO>();
        }
        public async Task<IEnumerable<EpisodesDTO>> GetEpisodesByPageQuery(int offSet, CancellationToken cancellationToken)
        {

            var pageCount = Math.Ceiling(await AF.Episodes.CountAsync(cancellationToken) / 10f);
            var episodes = await AF.Episodes

                .OrderByDescending(e=>e.EpisodeUploadDate)
                .Skip((offSet - 1) * 12)
                .Take(12)
                .Select(x => new EpisodesDTO
                {
                    EpisodeUploadDate = x.EpisodeUploadDate,
                    SortingDate = GetDate(x.EpisodeUploadDate),
                    AnimeImage = x.Anime.AnimeImage,
                    EpisodeId = x.EpisodeId,
                    AnimeName = x.AnimeName,
                    EpisodeLikes = x.EpisodeLikes,
                    EpisodeLinks = x.EpisodeLinks,
                    EpisodeNumber = x.EpisodeNumber,
                })
                .ToListAsync(cancellationToken);
            return episodes;
        }
        public static string GetDate(DateTime episodeDate)
        {
            var timeDifference = episodeDate - DateTime.Now;
            var wantedEpisode = new TimeSpan(
                Math.Abs(timeDifference.Days),
                Math.Abs(timeDifference.Hours),
                Math.Abs(timeDifference.Minutes),
                Math.Abs(timeDifference.Seconds)
            );

            string seconds, minutes, hours, days;
            seconds = wantedEpisode.Seconds.ToString();
            minutes = wantedEpisode.Minutes.ToString();
            hours = wantedEpisode.Hours.ToString();
            days = wantedEpisode.Days.ToString();

            var fullDate = new StringBuilder();

            if (days != "0")
            {
                fullDate.Append(days + " gun");
            }
            if (hours != "0")
            {
                fullDate.Append(" " + hours + " saat");
            }
            if (minutes != "0")
            {
                fullDate.Append(" " + minutes + " dakika");
            }
            if (seconds != "0")
            {
                fullDate.Append(" " + seconds + " saniye");
            }
            fullDate.Append(" " + " önce eklendi");
            return fullDate.ToString();
        }

        public async Task DeleteAllEpisodes(string animeId, CancellationToken cancellationToken)
        {
            var episodes = await AF.Episodes.Where(x => x.AnimeId.Contains(animeId)).ToListAsync(cancellationToken);
            if (episodes != null)
            {
                AF.Episodes.RemoveRange(episodes);
            }
        }

        public async Task<int> GetAmountOfPages(CancellationToken cancellationToken)
        {
            var pageCount = Math.Ceiling(await AF.Episodes.CountAsync(cancellationToken) / 10f);
            return (int)pageCount;
        }
    }
}
