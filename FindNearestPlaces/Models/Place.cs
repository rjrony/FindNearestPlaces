﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Spatial;
using System.Globalization;
using System.Linq;
using System.Web;

namespace FindNearestPlaces.Models
{
    public class Place
    {
        public long Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    public class Location
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }


    [TypeConverter(typeof(GeoPointConverter))]
    public class GeoPoint
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public static bool TryParse(string s, out GeoPoint result)
        {
            result = null;

            var parts = s.Split(',');
            if (parts.Length != 2)
            {
                return false;
            }

            double latitude, longitude;
            if (double.TryParse(parts[0], out latitude) &&
                double.TryParse(parts[1], out longitude))
            {
                result = new GeoPoint() { Longitude = longitude, Latitude = latitude };
                return true;
            }
            return false;
        }
    }

    class GeoPointConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }
            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string)
            {
                GeoPoint point;
                if (GeoPoint.TryParse((string)value, out point))
                {
                    return point;
                }
            }
            return base.ConvertFrom(context, culture, value);
        }
    }
}