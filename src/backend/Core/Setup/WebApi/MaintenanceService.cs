using System.Reflection;

namespace Core.Setup.WebApi
{
    public static class MaintenanceService
    {
        public static string LoadHelloHtml()
        {
            var location = Assembly.GetExecutingAssembly().Location;
            var result = new Uri(location).LocalPath;
            location = result.ToString();

            var path =
               Path.Combine(
                   Path.GetDirectoryName(location)!,
                   Path.Join("start.html")
               );

            return File.ReadAllText(path);
        }
    }
}
