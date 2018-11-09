using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using FindNearestPlaces.Models;

namespace FindNearestPlaces
{
    public class MapsContext : DbContext
    {
        public DbSet<Place> Places { get; set; }
        public DbSet<GeographyPlace> GeographyPlaces { get; set; } 
    }
}