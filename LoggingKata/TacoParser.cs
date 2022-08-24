namespace LoggingKata
{
    /// <summary>
    /// Parses a POI file to locate all the Taco Bells
    /// </summary>
    public class TacoParser
    {
        readonly ILog logger = new TacoLogger();
        
        public ITrackable Parse(string line)
        {
            logger.LogInfo("Parsing Distance. . .");

            string[] cells = line.Split(',');

            if (cells.Length < 3 || cells.Length > 3)
            {
                logger.LogWarning("more or less than 3 items, data does not meet criteria");
                return null; 
            }

            var latitude = double.Parse(cells[0]);
            var longitude = double.Parse(cells[1]);
            string name = cells[2];

            var point = new Point();
            point.Latitude = latitude;
            point.Longitude = longitude;

            var tacoBell = new TacoBell();
            tacoBell.Name = name;
            tacoBell.Location = point;

            return tacoBell;
        }
    }
}