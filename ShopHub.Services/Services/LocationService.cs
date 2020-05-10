using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShopHub.Models.Context;
using ShopHub.Models.Dtos;
using ShopHub.Models.Models;
using ShopHub.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopHub.Services.Services
{
    public class LocationService : ILocation
    {
        private ShopHubContext _context;
        private IMapper _mapper;
        public LocationService(ShopHubContext shopHubContext, IMapper mapper)
        {
            _context = shopHubContext;
            _mapper = mapper;
        }

        public List<LocationDto> GetAllLocations()
        {
            var locations =  _context.Locations.ToList();
            if (!(locations is null) && locations.Count > 0)
            {
               return _mapper.Map<List<LocationDto>>(locations);
            }
            else
            {
                return null;
            }
        }

        public LocationDto CreateLocation(LocationDto location)
        {
            try
            {
                var mappedData = _mapper.Map<Location>(location);
                mappedData.Products = new List<Product>();

                _context.Locations.Add(mappedData);
                _context.SaveChanges();

                return _mapper.Map<LocationDto>(mappedData);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool RemoveLocation(int locationId)
        {
            var record = _context.Locations.FirstOrDefault(x => x.Id == locationId);
            var response = false;

            if (!(record is null))
            {
                _context.Locations.Remove(record);
                _context.SaveChanges();
                response = true;
                return response;
            }
            else
            {
                return response;
            }
        }

    }
}
