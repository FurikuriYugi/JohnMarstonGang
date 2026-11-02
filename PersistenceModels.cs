// PersistenceModels.cs
using System;
using System.Collections.Generic;

public class GangSaveData
{
    public List<SavedCompanion> Companions { get; set; } = new List<SavedCompanion>();
    public string CurrentOrder { get; set; }
    public string CurrentCombatStyle { get; set; }
}

public class SavedCompanion
{
    public int PedHandle { get; set; }
    public string Name { get; set; }
    public float Loyalty { get; set; }
    public float Skill { get; set; }
    public string Style { get; set; }
    public bool Alive { get; set; }
}