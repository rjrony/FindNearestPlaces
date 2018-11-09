//using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
//using System.Data.Objects.SqlClient;
using System.Data.Entity.SqlServer;
using System.Data.Spatial;
using System.Device.Location;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FindNearestPlaces.Models;
using DbGeography = System.Data.Entity.Spatial.DbGeography;

//using Microsoft.SqlServer.Types;

namespace FindNearestPlaces.Controllers
{
    public class PlaceController : ApiController
    {
        
        private readonly IMapsRepository mapsRepository;

        //public PlaceController(IMapsRepository mapsRepository)
        //{
        //    this.mapsRepository = mapsRepository;
        //}

        [HttpGet]
        [Route("api/Place/GetNearestPlacesUsingGeography")]
        public List<GeographyPlace> GetNearestPlacesUsingGeography(double latitude, double longitude, double radius = 10, int limit = 10)
        {
            var point = DbGeography.FromText("POINT(" + longitude + " " + latitude + ")", 4326);
            var topLeftLatitude = 23.783726;
            var topLeftLongitude = 90.344245;
            var bottomRightLatitude = 23.685148;
            var bottomRightLongitude = 90.492713;
            MapsContext context = new MapsContext();
            var datas =
                context.GeographyPlaces.Where(x => x.Location.Distance(point) <= radius * 1000)
                    .Take(limit)
                    .OrderBy(x => x.Location.Distance(point));
            System.Data.Spatial.DbGeography polygon;
            try
            {

                polygon =
                    System.Data.Spatial.DbGeography.PolygonFromText(
                        "POLYGON((127.652 -26.244,127.652 -26.194,90.344245 23.783726,127.652 -26.244))",
                        4326);
            }
            catch (System.Exception ex)
            {

                throw;
            }
            //polygon.
            var result = datas.ToList();
            return result;
        }

        [HttpGet]
        [Route("api/Place/GetPlacesFromRectangle")]
        public List<Place> GetPlacesFromRectangle( GeoPoint top_left, double latitude1, double longitude1, double latitude2, double longitude2)
        {
            MapsContext context = new MapsContext();

            //SELECT* FROM Places WHERE (Lat => latmin AND Lat <= latmax) AND(Lon >= -lonmin AND Lon <= lonmax)

            //var datas = context.Places.Where(x => SqlFunctions.Acos(SqlFunctions.Sin(latitude * SqlFunctions.Pi() / 180.0)
            //                        * SqlFunctions.Sin(x.Latitude * SqlFunctions.Pi() / 180.0)
            //                        + SqlFunctions.Cos(latitude * SqlFunctions.Pi() / 180.0)
            //                        * SqlFunctions.Cos(x.Latitude * SqlFunctions.Pi() / 180.0)
            //                        * SqlFunctions.Cos((x.Longitude * SqlFunctions.Pi() / 180.0) - (longitude * SqlFunctions.Pi() / 180.0))) * 6371 <= radius)
            //                        .Take(limit)
            //                        .OrderBy(x => SqlFunctions.Acos(SqlFunctions.Sin(latitude * SqlFunctions.Pi() / 180.0)
            //                        * SqlFunctions.Sin(x.Latitude * SqlFunctions.Pi() / 180.0)
            //                        + SqlFunctions.Cos(latitude * SqlFunctions.Pi() / 180.0)
            //                        * SqlFunctions.Cos(x.Latitude * SqlFunctions.Pi() / 180.0)
            //                        * SqlFunctions.Cos((x.Longitude * SqlFunctions.Pi() / 180.0) - (longitude * SqlFunctions.Pi() / 180.0))) * 6371);
            var datas = context.Places.Where(x => (x.Latitude >= latitude1 && x.Latitude <= latitude2)
                                                  && (x.Longitude >= longitude1 && x.Longitude <= longitude2));

            var result = datas.ToList();
            return result;
        }

        [HttpGet]
        [Route("api/Place/GetNearestPlaces")]
        public List<Place> GetNearestPlaces(double latitude, double longitude, double radius = 10, int limit = 10)
        {
            MapsContext context = new MapsContext();
            //var datas = context.Places.Select(x => Math.Acos(Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) 
            //                + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta))));
            //var datas = context.Places.Select(x => Math.Acos(Math.Sin(deg2rad(latitude)) * Math.Sin(deg2rad(x.Latitude))
            //               + Math.Cos(deg2rad(latitude)) * Math.Cos(deg2rad(x.Latitude)) * Math.Cos(deg2rad(x.Longitude-longitude))));

            //var datas = context.Places.Where(x => Math.Acos(Math.Sin(latitude * Math.PI / 180.0) * Math.Sin(x.Latitude * Math.PI / 180.0)
            //   + Math.Cos(latitude * Math.PI / 180.0) * Math.Cos(x.Latitude * Math.PI / 180.0) * Math.Cos(x.Longitude - longitude * Math.PI / 180.0)) * 6371 <= radius);

            //var datas = context.Places.Where(x => 3959 * Math.Acos(Math.Cos(radians(x.Latitude)) * Math.Cos(radians(latitude))
            //                                * Math.Cos(radians(x.Longitude) - radians(longitude)) + Math.Sin(radians(latitude)) *
            //                                Math.Sin(radians(x.Latitude))) * 1.60934 <= radius).ToList();
            //var datas = context.Places.Where(x => Math.Sin(x.Latitude / 57.2957795130823) * Math.Sin(latitude / 57.2957795130823) + Math.Cos(x.Latitude / 57.2957795130823) * Math.Cos(latitude / 57.2957795130823) * Math.Cos(x.Latitude / 57.2957795130823 - latitude / 57.2957795130823) * 6371 <= radius);



            //var tt = (3959*acos(
            //    cos(radians(@myLat))*cos(radians(Latitude))*cos(radians(Longitude) - radians(@myLong))
            //    + sin(radians(@myLat))*sin(radians(Latitude))))*1.60934 <= 10;

            //var tt2 = select
            //ACOS(
            //    SIN(PI()*@lat1/180.0)*SIN(PI()*@lat2/180.0) +
            //    COS(PI()*@lat1/180.0)*COS(PI()*@lat2/180.0)*COS(PI()*@lon2/180.0 - PI()*@lon1/180.0)
            //)*6371;


            //var center = new GeoCoordinate(latitude, longitude);
            //var result = context.Places.Select(x => new GeoCoordinate(x.Latitude, x.Longitude))
            //                      .Where(x => x.GetDistanceTo(center) < radius);
            //var datas = result.ToList();
            //var datas =  SELECT* FROM Places WHERE
            // (Lat >= 1.2393 AND Lat <= 1.5532) AND(Lon >= -1.8184 AND Lon <= 0.4221)
            // HAVING
            // acos(sin(1.3963) * sin(Lat) + cos(1.3963) * cos(Lat) * cos(Lon - (-0.6981))) <= 0.1570;

            var datas = context.Places.Where(x => SqlFunctions.Acos(SqlFunctions.Sin(latitude * SqlFunctions.Pi() / 180.0)
                                    * SqlFunctions.Sin(x.Latitude * SqlFunctions.Pi() / 180.0)
                                    + SqlFunctions.Cos(latitude * SqlFunctions.Pi() / 180.0)
                                    * SqlFunctions.Cos(x.Latitude * SqlFunctions.Pi() / 180.0)
                                    * SqlFunctions.Cos((x.Longitude * SqlFunctions.Pi() / 180.0) - (longitude * SqlFunctions.Pi() / 180.0))) * 6371 <= radius)
                                    .Take(limit)
                                    .OrderBy(x => SqlFunctions.Acos(SqlFunctions.Sin(latitude * SqlFunctions.Pi() / 180.0)
                                    * SqlFunctions.Sin(x.Latitude * SqlFunctions.Pi() / 180.0)
                                    + SqlFunctions.Cos(latitude * SqlFunctions.Pi() / 180.0)
                                    * SqlFunctions.Cos(x.Latitude * SqlFunctions.Pi() / 180.0)
                                    * SqlFunctions.Cos((x.Longitude * SqlFunctions.Pi() / 180.0) - (longitude * SqlFunctions.Pi() / 180.0))) * 6371);


            var result = datas.ToList();
            return result;
        }


        //private double deg2rad(double deg)
        //{
        //    return (deg * Math.PI / 180.0);
        //}

        //private double rad2deg(double rad)
        //{
        //    return (rad * 180.0 / Math.PI);
        //}

    }
}
