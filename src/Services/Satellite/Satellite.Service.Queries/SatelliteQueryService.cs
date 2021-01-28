using Microsoft.EntityFrameworkCore;
using Satellite.Common.Functions;
using Satellite.Persistence.Database;
using Satellite.Service.Queries.DTOs;
using Service.Common.Collection;
using Service.Common.Mapping;
using Service.Common.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Satellite.Service.Queries
{
    public interface ISatelliteQueryService
    {
        Task<DataCollection<SatelliteDto>> GetAllAsync(int page, int take, IEnumerable<int> products = null);
        SatelliteDto GetSource();


    }

    public class SatelliteQueryService : ISatelliteQueryService
    {
        private readonly ApplicationDbContext _context;
        public SatelliteQueryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DataCollection<SatelliteDto>> GetAllAsync(int page, int take, IEnumerable<int> satellites = null)
        {
            var collection = await _context.Satellites
                                    .Where(x => satellites == null || satellites.Contains(x.SatelliteId))
                                    .OrderByDescending(x => x.SatelliteId)
                                    .GetPagedAsync(page, take);
            return collection.MapTo<DataCollection<SatelliteDto>>();
        }

        public SatelliteDto GetSource()
        {

            SatelliteDto dto = new SatelliteDto();

            double[] location =  GetLocation();
            var message =  GetMessage();
            if (location != null && message!="")
            {
                dto.Position.X = location[0];
                dto.Position.Y = location[1];
                dto.Message = message;
                return  dto;
            }
            else 
            {
                return null;
            }
            
            
           
        }

        private  double[] GetLocation()
        {
            var arrayPoints = new List<Point>();

            var collection =   _context.Satellites
                                    .Where(x => x.Distance > 0 && x.CoordinateX != 0 && x.CoordinateY != 0).Take(3).ToList();
            if (collection.Count == 3)
            {
                foreach (var item in collection)
                {
                    Point p = new Point(item.CoordinateX, item.CoordinateY, item.Distance);
                    arrayPoints.Add(p);
                }

                return CalculateThreeCircleIntersection.Calculate(
                    arrayPoints[0].PositionX(), arrayPoints[0].PositionY(), arrayPoints[0].Radio(),
                    arrayPoints[1].PositionX(), arrayPoints[1].PositionY(), arrayPoints[1].Radio(),
                    arrayPoints[2].PositionX(), arrayPoints[2].PositionY(), arrayPoints[2].Radio());
            }     
            else
            {
                return null;
            }
        }

        private string GetMessage()
        {
            string message = "";
            var phrases = new Dictionary<int, string>();
            var collection = _context.Satellites
                                    .Where(x => x.Distance > 0 && x.CoordinateX != 0 && x.CoordinateY != 0).Take(3).ToList();

            if (collection.Count > 0)
            {
                foreach (var item in collection)
                {
                    string[] splitMessage = item.Message.Split(',');

                    for (int i = 0; i < splitMessage.Length; i++)
                    {
                        if (splitMessage[i] != "" && !phrases.ContainsValue(splitMessage[i]))
                        {
                            phrases.Add(i, splitMessage[i]);

                        }
                    }

                }
                var orderPhrases = phrases.OrderBy(x => x.Key).ToList();
                foreach (var item in orderPhrases)
                {
                    message = message + item.Value.ToString() + " ";
                }

            }
            return message;
        }

    }
}
