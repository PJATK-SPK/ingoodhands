﻿namespace Orders.Actions.RequestHelpActions.RequestHelpGetMap
{
    public class RequestHelpGetMapItemBaseResponse
    {
        public string Name { get; set; } = default!;
        public double GpsLatitude { get; set; }
        public double GpsLongitude { get; set; }
    }
}