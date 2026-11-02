using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public class CampSupplyForecastSystem
{
    private readonly string _savePath;
    private readonly List<CampSupplyForecastRecord> _forecasts = new List<CampSupplyForecastRecord>();

    public int MaxForecasts { get; set; } = 128;
    public IReadOnlyList<CampSupplyForecastRecord> Forecasts => _forecasts;

    public CampSupplyForecastSystem(string saveFileName = "CampSupplyForecastSystem.json")
    {
        _savePath = Path.Combine("scripts", saveFileName);
        Load();
    }

    public bool AddForecast(string title, string category, string prediction, string source = null, int confidence = 50)
    {
        if (string.IsNullOrWhiteSpace(title))
            return false;

        if (confidence < 0)
            confidence = 0;
        if (confidence > 100)
            confidence = 100;

        if (_forecasts.Count >= MaxForecasts)
            _forecasts.RemoveAt(0);

        _forecasts.Add(new CampSupplyForecastRecord
        {
            ForecastId = Guid.NewGuid().ToString("N"),
            Title = title,
            Category = string.IsNullOrWhiteSpace(category) ? "General" : category,
            Prediction = string.IsNullOrWhiteSpace(prediction) ? title : prediction,
            Source = source,
            Confidence = confidence,
            CreatedUtc = DateTime.UtcNow,
            UpdatedUtc = DateTime.UtcNow
        });

        Save();
        return true;
    }

    public bool RemoveForecast(string forecastId)
    {
        if (string.IsNullOrWhiteSpace(forecastId))
            return false;

        bool removed = _forecasts.RemoveAll(x => x.ForecastId == forecastId) > 0;
        if (removed)
            Save();

        return removed;
    }

    public CampSupplyForecastRecord GetForecast(string forecastId)
    {
        if (string.IsNullOrWhiteSpace(forecastId))
            return null;

        return _forecasts.FirstOrDefault(x => x.ForecastId == forecastId);
    }

    public IEnumerable<CampSupplyForecastRecord> GetForecastsByCategory(string category)
    {
        if (string.IsNullOrWhiteSpace(category))
            return _forecasts.OrderByDescending(x => x.CreatedUtc);

        return _forecasts.Where(x => string.Equals(x.Category, category, StringComparison.OrdinalIgnoreCase))
            .OrderByDescending(x => x.CreatedUtc);
    }

    public IEnumerable<CampSupplyForecastRecord> GetHighConfidenceForecasts(int minimumConfidence = 75)
    {
        if (minimumConfidence < 0)
            minimumConfidence = 0;
        if (minimumConfidence > 100)
            minimumConfidence = 100;

        return _forecasts.Where(x => x.Confidence >= minimumConfidence)
            .OrderByDescending(x => x.Confidence)
            .ThenByDescending(x => x.CreatedUtc);
    }

    public int GetAverageConfidence()
    {
        if (_forecasts.Count == 0)
            return 0;

        return (int)Math.Round(_forecasts.Average(x => x.Confidence));
    }

    public string GetForecastStatus()
    {
        int count = _forecasts.Count;

        if (count >= 30)
            return "Overloaded";
        if (count >= 15)
            return "Active";
        if (count >= 5)
            return "Monitoring";
        if (count >= 1)
            return "Quietly Active";

        return "Idle";
    }

    public void Clear()
    {
        _forecasts.Clear();
        Save();
    }

    public void Save()
    {
        try
        {
            Directory.CreateDirectory("scripts");
            var options = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(_savePath, JsonSerializer.Serialize(_forecasts, options));
        }
        catch
        {
        }
    }

    public void Load()
    {
        try
        {
            if (!File.Exists(_savePath))
                return;

            var json = File.ReadAllText(_savePath);
            var loaded = JsonSerializer.Deserialize<List<CampSupplyForecastRecord>>(json);

            _forecasts.Clear();
            if (loaded != null)
                _forecasts.AddRange(loaded.Where(x => !string.IsNullOrWhiteSpace(x.ForecastId)));
        }
        catch
        {
            _forecasts.Clear();
        }
    }
}

public class CampSupplyForecastRecord
{
    public string ForecastId { get; set; }
    public string Title { get; set; }
    public string Category { get; set; }
    public string Prediction { get; set; }
    public string Source { get; set; }
    public int Confidence { get; set; }
    public DateTime CreatedUtc { get; set; }
    public DateTime UpdatedUtc { get; set; }
}