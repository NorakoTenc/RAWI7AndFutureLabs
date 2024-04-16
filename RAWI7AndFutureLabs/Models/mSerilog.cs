using Serilog.Events;
using Serilog;

namespace RAWI7AndFutureLabs.Models
{
    public class mSerilog
    {
        public static async Task SerilogTaskAsync()
        {
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .WriteTo.Console()
            .WriteTo.File("logs/myapp.txt", rollingInterval: RollingInterval.Day)
            .WriteTo.Seq("http://localhost:7102")
            .CreateLogger();

            Log.Information("Information");
            Log.Warning("Warning");
            Log.Fatal("Fatal error");
            var user = new AUser { FirstName = "FirstName", LastName = "LastName" };
            Log.Information("User {@User}. Time: {LoginTime}", user, DateTime.Now);

            float af = 1f;
            string s = "No No no no";
            try
            {
                Log.Debug("{af} / {s}", af, s);
                Console.WriteLine(af / float.Parse(s));
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error");
            }
            finally
            {
                await Log.CloseAndFlushAsync();
            }
        }
    }
}
