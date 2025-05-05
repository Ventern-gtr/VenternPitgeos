using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Linq;
using System.Numerics;

namespace VenternPitgeos
{
    class Config
    {
        private static string MainFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Ventern");
        private static string SaveFilePath = Path.Combine(MainFolder, "Pitgeo-config.txt");

        public static void SaveConfig()
        {
            if (File.Exists(SaveFilePath))
            {
                using StreamWriter streamWriter = new StreamWriter(SaveFilePath);
                streamWriter.WriteLine($"pitgroundValue: {Plugin.pitgroundValue.ToString(CultureInfo.InvariantCulture)}");
                streamWriter.WriteLine($"pitregularwallV2Value: {Plugin.pitregularwallV2Value.ToString(CultureInfo.InvariantCulture)}");
                streamWriter.WriteLine($"pitlowerslipperywallValue: {Plugin.pitlowerslipperywallValue.ToString(CultureInfo.InvariantCulture)}");
                streamWriter.WriteLine($"verticalwall1Value: {Plugin.verticalwall1Value.ToString(CultureInfo.InvariantCulture)}");
                streamWriter.WriteLine($"verticalwall2Value: {Plugin.verticalwall2Value.ToString(CultureInfo.InvariantCulture)}");
            }
            else
            {
                File.Create(SaveFilePath);
                SaveConfig();
            }
        }

        public static void LoadConfig()
        {
            if (File.Exists(SaveFilePath))
            {
                var data = File.ReadAllLines(SaveFilePath).Select(line => line.Split(':')).Where(parts => parts.Length == 2).ToDictionary(parts => parts[0], parts => parts[1]);
                float.TryParse(data.GetValueOrDefault("pitgroundValue"), out Plugin.pitgroundValue);
                float.TryParse(data.GetValueOrDefault("pitregularwallV2Value"), out Plugin.pitregularwallV2Value);
                float.TryParse(data.GetValueOrDefault("pitlowerslipperywallValue"), out Plugin.pitlowerslipperywallValue);
                float.TryParse(data.GetValueOrDefault("verticalwall1Value"), out Plugin.verticalwall1Value);
                float.TryParse(data.GetValueOrDefault("verticalwall2Value"), out Plugin.verticalwall2Value);
            }
            else
            {
                File.Create(SaveFilePath);
                LoadConfig();
            }
        }

        private static void ParseVector(string value, out Vector3 result)
        {
            result = Vector3.Zero;
            if (string.IsNullOrEmpty(value)) return;
            var parts = value.Split(',');
            if (parts.Length == 3 && float.TryParse(parts[0], out float x) && float.TryParse(parts[1], out float y) && float.TryParse(parts[2], out float z))
                result = new Vector3(x, y, z);
        }
    }
}
