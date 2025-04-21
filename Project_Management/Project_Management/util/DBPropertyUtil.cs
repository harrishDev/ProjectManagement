using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Management.util
{
    public class DBPropertyUtil
    {
        public static string GetConnectionString(string filePath)
        {
            Dictionary<string, string> settings = new Dictionary<string, string>();

            try
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine($"Config file not found: {filePath}");
                    return null;
                }

                foreach (var line in File.ReadAllLines(filePath))
                {
                    if (!string.IsNullOrWhiteSpace(line) && line.Contains("=") && !line.Trim().StartsWith("#"))
                    {
                        var parts = line.Split('=', 2);
                        if (parts.Length == 2)
                        {
                            settings[parts[0].Trim()] = parts[1].Trim();
                        }
                    }
                }

                if (!settings.ContainsKey("server") || !settings.ContainsKey("database"))
                {
                    Console.WriteLine("Missing required keys in db.properties.");
                    return null;
                }

                return $"Server={settings["server"]};Database={settings["database"]};" +
                       $"Integrated Security={settings["integratedSecurity"]};" +
                       $"TrustServerCertificate={settings["trustServerCertificate"]};";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading connection string: " + ex.Message);
                return null;
            }
        }
    }
}
