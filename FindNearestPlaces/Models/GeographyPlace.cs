using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Web;

namespace FindNearestPlaces.Models
{
    public class GeographyPlace
    {
        public long Id { get; set; }
        public DbGeography Location { get; set; }
    }
}