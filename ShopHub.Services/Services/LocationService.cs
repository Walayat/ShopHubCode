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
        /*Dependency Injection Code Start
         As we are not creating instance like
         ProductService product = new ProductService()
         we are creating private types of class type variable (obj)
         which is populating from constructor. So there is no need
         to dispose the object it will automatically dispose from memroy
             */
        private ShopHubContext _context;
        private IMapper _mapper;
        public LocationService(ShopHubContext shopHubContext, IMapper mapper)
        {
            _context = shopHubContext;
            _mapper = mapper;
        }
        /*Dependency Injection Code End*/

        /*This mehod I am using to get all locations from location table*/
        /*Using _context which is of type DbContext we are using this
          to access our database tables and then apply queries on it. 
         */
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

        /*This mehod I am using to create location.
         _context.Locations.Add(mappedData) using this I am adding object to our context location table
          but record will not save to database untill we will call _context.SaveChanges().
          SaveChanges() initiate the changes to database.
          ** Automapper **
          One more concept here I am using i.e AutoMapper
          Automapper help us to map one class of fields to other one's, it mapps only the similar fields.
             */
        public LocationDto CreateLocation(LocationDto location)
        {
            var mappedData = _mapper.Map<Location>(location);
            mappedData.Products = new List<Product>();

            _context.Locations.Add(mappedData);
            _context.SaveChanges();

            return _mapper.Map<LocationDto>(mappedData);
        }

        /*This mehod I am using to remove location from database */

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
