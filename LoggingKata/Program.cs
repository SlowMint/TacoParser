using System;
using System.Linq;
using System.IO;
using GeoCoordinatePortable;
using System.Collections.Generic;

namespace LoggingKata
{
    class Program
    {
        static readonly ILog logger = new TacoLogger();
        const string csvPath = "TacoBell-US-AL.csv";

        static void Main(string[] args)
        {
            logger.LogInfo("Log initialized");

            string[] lines = File.ReadAllLines(csvPath);
            if (lines.Length == 0)
            {
                logger.LogError("file has no input");
            }

            if (lines.Length == 1)
            {
                logger.LogError("file only contains one line of data");
            }

            logger.LogInfo($"The first line of data is as follows: {lines[0]}");

            var parser = new TacoParser();

            var locations = lines.Select(lines => parser.Parse(lines)).ToArray();

            ITrackable tbell1 = null;
            ITrackable tbell2 = null;
            double distance = 0;

            for (int i = 0; i < locations.Length; i++)
            {
                var locationA = locations[i];
                var coordinateA = new GeoCoordinate();
                coordinateA.Latitude = locationA.Location.Latitude;
                coordinateA.Longitude = locationA.Location.Longitude;

                for (var j = 0; j < locations.Length; j++)
                {
                    var locationB = locations[j];
                    var coordinateB = new GeoCoordinate();
                    coordinateB.Latitude = locationB.Location.Latitude;
                    coordinateB.Longitude = locationB.Location.Longitude;

                    if (coordinateA.GetDistanceTo(coordinateB) > distance)
                    {
                        distance = coordinateA.GetDistanceTo(coordinateB);
                        tbell1 = locationA;
                        tbell2 = locationB;
                    }
                }
            }

            logger.LogInfo($"{tbell1.Name} and {tbell2.Name} are the furthest from each other");
        }
    }
}
