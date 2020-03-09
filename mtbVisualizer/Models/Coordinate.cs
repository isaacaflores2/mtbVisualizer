﻿
namespace MtbVisualizer.Models
{
    public struct Coordinate
    {
        public float? Latitude;
        public float? Longitude;

        public Coordinate(float? latitude, float? longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }        
    }
}
