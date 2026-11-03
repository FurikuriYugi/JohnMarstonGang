using RDR2;
using System;
using System.IO;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

public class Main : Script
{
    private const string SaveFileName = "JohnMarstonGang_Save.json";
    private PlayerGangSaveData _saveData = new PlayerGangSaveData();

    public Main()
    {
        Tick += OnTick;
        Interval = 250;
        LoadState();
    }

    private void OnTick(object sender, EventArgs e)
    {
        // hook systems here next: companions, bank, jobs, wagons, combat
    }

    private void LoadState()
    {
        var path = GetSavePath();
        _saveData = JsonSaveSystem.Load<PlayerGangSaveData>(path);
    }

    private void SaveState()
    {
        var path = GetSavePath();
        JsonSaveSystem.Save(path, _saveData);
    }

    private string GetSavePath()
    {
        return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, SaveFileName);
    }
}