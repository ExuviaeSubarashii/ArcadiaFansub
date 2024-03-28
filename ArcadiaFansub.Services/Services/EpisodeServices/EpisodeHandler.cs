using ArcadiaFansub.Domain.Dtos;
using ArcadiaFansub.Domain.Interfaces;
using ArcadiaFansub.Domain.Models;
using ArcadiaFansub.Domain.RequestDtos.EpisodeRequest;
using ArcadiaFansub.Services.Services.CommentServices;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace ArcadiaFansub.Services.Services.EpisodeServices
{
    public class EpisodeHandler(ArcadiaFansubContext AF, CommentHandler CH) : IEpisodeInterface
    {
        public async Task<string> AddNewEpisode(AddNewEpisodeRequest newEpisode, CancellationToken cancellationToken)
        {
            try
            {
                var animeQuery = await AF.Animes.FirstOrDefaultAsync(x => x.AnimeId.Trim() == newEpisode.AnimeName.Trim(), cancellationToken);
                var doesEpisodeAlreadyExist = await AF.Episodes.AnyAsync(x => x.AnimeId == newEpisode.AnimeName.Trim() && x.EpisodeNumber == newEpisode.EpisodeNumber, cancellationToken);
                if (doesEpisodeAlreadyExist) { return "Bölüm Zaten Var."; }
                if (animeQuery != null && doesEpisodeAlreadyExist == false)
                {
                    string episodeLinks = "";
                    for (int i = 0; i < newEpisode.EpisodeLinks.Count(); i++)
                    {
                        episodeLinks = ConvertLinks.ConvertToEmbed(newEpisode.EpisodeLinks);
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
        public async Task<IEnumerable<EpisodesDto>> GetAllEpisodes(CancellationToken cancellationToken)
        {
            var allEpisodes = await AF.Episodes.Select(item => new EpisodesDto
            {
                AnimeName = item.AnimeName,
                EpisodeNumber = item.EpisodeNumber,
                EpisodeId = item.EpisodeId,
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
        public async Task<EpisodePageDto> GetThatEpisode(string episodeId, CancellationToken cancellationToken)
        {
            try
            {
                var episodeQuery = await AF.Episodes
                    .Where(episode => episode.EpisodeId == episodeId)
                    .Select(newEpisode => new EpisodePageDto
                    {
                        AnimeName = newEpisode.AnimeName.Trim(),
                        AnimeId = newEpisode.AnimeId.Trim(),
                        EpisodeNumber = newEpisode.EpisodeNumber,
                        EpisodeLinks = newEpisode.EpisodeLinks.Trim(),
                        EpisodeId = newEpisode.EpisodeId,
                    }).FirstOrDefaultAsync(cancellationToken);
                if (episodeQuery == null)
                {
                    return new EpisodePageDto();
                }
                return episodeQuery;

            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<IEnumerable<EpisodesDto>> GetEpisodePanelAnimeEpisodes(string animeId, CancellationToken cancellationToken)
        {
            var episodepanelQuery = await AF.Episodes.Where(x => x.AnimeId == animeId.Trim()).Select(item => new EpisodesDto
            {
                AnimeImage = item.Anime.AnimeImage,
                AnimeName = item.AnimeName,
                EpisodeId = item.EpisodeId,
                EpisodeLinks = item.EpisodeLinks,
                EpisodeNumber = item.EpisodeNumber,
                EpisodeUploadDate = item.EpisodeUploadDate,
            }).OrderBy(e => e.EpisodeNumber).ToListAsync(cancellationToken);
            if (episodepanelQuery.Count > 0)
            {
                return episodepanelQuery;
            }
            return new List<EpisodesDto>();
        }
        public async Task<IEnumerable<EpisodesDto>> GetEpisodesByPageQuery(int offSet, CancellationToken cancellationToken)
        {

            var pageCount = Math.Ceiling(await AF.Episodes.CountAsync(cancellationToken) / 10f);
            var episodes = await AF.Episodes

                .OrderByDescending(e => e.EpisodeUploadDate)
                .Skip((offSet - 1) * 12)
                .Take(36)
                .Select(x => new EpisodesDto
                {
                    EpisodeUploadDate = x.EpisodeUploadDate,
                    SortingDate = GetDate(x.EpisodeUploadDate),
                    AnimeImage = x.Anime.AnimeImage,
                    EpisodeId = x.EpisodeId,
                    AnimeName = x.AnimeName,
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
                fullDate.Append(days + " g");
            }
            if (hours != "0")
            {
                fullDate.Append(" " + hours + " sa");
            }
            if (minutes != "0")
            {
                fullDate.Append(" " + minutes + " dk");
            }
            //if (seconds != "0")
            //{
            //    fullDate.Append(" " + seconds + " sn");
            //}
            fullDate.Append(" " + " önce eklendi.");
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
            var pageCount = Math.Ceiling(await AF.Episodes.CountAsync(cancellationToken) / 12f);
            return (int)pageCount;
        }

        public async Task<string> BultDeleteImagesAsync(string[] episodeIds, string userToken, CancellationToken cancellationToken)
        {
            using (var transaction = await AF.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    var episodeQuery = await AF.Episodes.Where(x => episodeIds.Contains(x.EpisodeId.Trim())).ToListAsync(cancellationToken);
                    AF.RemoveRange(episodeQuery);
                    await AF.SaveChangesAsync(cancellationToken);
                    await transaction.CommitAsync(cancellationToken);

                    return $"{episodeQuery.Count} episodes deleted successfully.";
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    throw new Exception("An error occurred while deleting episodes.", ex);
                }
            }



        }
    }
}
