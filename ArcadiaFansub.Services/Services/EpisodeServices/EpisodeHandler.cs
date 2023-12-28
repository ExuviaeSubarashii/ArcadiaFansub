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
                var doesEpisodeAlreadyExist = AF.Episodes.Where(episode => episode.EpisodeNumber == newEpisode.EpisodeNumber && episode.AnimeName == newEpisode.AnimeName);
                if (!doesEpisodeAlreadyExist.Any())
                {
                    return $"Episode {newEpisode.EpisodeNumber} already exists";
                }
                Episode episode = new Episode()
                {
                    EpisodeNumber = newEpisode.EpisodeNumber,
                    AnimeName = newEpisode.AnimeName,
                    EpisodeLikes = 0,
                    EpisodeLinks = newEpisode.EpisodeLinks,
                    EpisodeUploadDate = DateTime.Today
                };
                AF.Episodes.Add(episode);
                AF.SaveChanges();
                return $"Episode {newEpisode.EpisodeNumber} succesfully added.";
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<string> DeleteEpisode(DeleteEpisodeRequest deleteEpisode)
        {
            var episodeToDelete = await AF.Episodes.FirstOrDefaultAsync(x => x.EpisodeId == deleteEpisode.EpisodeId);
            AF.Episodes.Remove(episodeToDelete);
            AF.SaveChanges();
            return $"{deleteEpisode.EpisodeId} deleted";
        }

        public async Task<IEnumerable<EpisodesDTO>> GetAllEpisodes()
        {
            var allEpisodes = await AF.Episodes.Select(item => new EpisodesDTO
            {
                AnimeName = item.AnimeName,
                EpisodeNumber = item.EpisodeNumber,
                EpisodeId = item.EpisodeId,
                EpisodeLikes = item.EpisodeLikes,
                EpisodeUploadDate = item.EpisodeUploadDate
            }).ToListAsync();

            return allEpisodes.OrderBy(e => e.EpisodeUploadDate);
        }

        public async Task<string> UpdateEpisode(UpdateEpisodeRequest updateEpisode)
        {
            try
            {
                var episodeQuery = await AF.Episodes.FirstOrDefaultAsync(x => x.EpisodeId == updateEpisode.EpisodeId);
                episodeQuery.EpisodeLinks = updateEpisode.EpisodeLinks;
                AF.SaveChanges();
                return $"Succesfully changed links for {episodeQuery.AnimeName} episode {episodeQuery.EpisodeNumber}";
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
