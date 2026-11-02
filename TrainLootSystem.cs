using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public class TrainLootSystem
    {
        private readonly string _savePath;
        private readonly Random _rng = new Random();
        private readonly List<TrainLootSpawn> _activeSpawns = new List<TrainLootSpawn>();

        public bool Enabled { get; set; } = true;
        public int ScanIntervalMs { get; set; } = 2500;
        public float SpawnRadius { get; set; } = 55f;
        public float DespawnRadius { get; set; } = 140f;
        public int MaxActiveSpawns { get; set; } = 3;

        private long _nextScanAt;

        public IReadOnlyList<TrainLootSpawn> ActiveSpawns => _activeSpawns;

        public TrainLootSystem(string saveFileName = "TrainLootSystem.json")
        {
            _savePath = Path.Combine("scripts", saveFileName);
            Load();
        }

        public void Update(Vector3 playerPos, Func<Vector3, bool> isTrainNearby = null, Action<TrainLootSpawn> onSpawn = null, Action<TrainLootSpawn> onDespawn = null)
        {
            if (!Enabled)
                return;

            long now = Environment.TickCount64;
            if (now < _nextScanAt)
            {
                Cleanup(playerPos, onDespawn);
                return;
            }

            _nextScanAt = now + ScanIntervalMs;
            Cleanup(playerPos, onDespawn);

            if (_activeSpawns.Count >= MaxActiveSpawns)
                return;

            if (isTrainNearby == null)
                return;

            if (!isTrainNearby(playerPos))
                return;

            SpawnLoot(playerPos, onSpawn);
        }

        public bool TrySpawnLootAt(Vector3 position, out TrainLootSpawn spawn)
        {
            spawn = null;

            if (!Enabled || _activeSpawns.Count >= MaxActiveSpawns)
                return false;

            spawn = new TrainLootSpawn
            {
                Id = Guid.NewGuid().ToString("N"),
                Position = position,
                CreatedAtUtc = DateTime.UtcNow,
                LootTier = RollLootTier(),
                LootItems = GenerateLootItems()
            };

            _activeSpawns.Add(spawn);
            Save();
            return true;
        }

        public bool TryLootSpawn(string spawnId, out List<LootItem> items)
        {
            items = null;

            var spawn = _activeSpawns.FirstOrDefault(x => x.Id == spawnId);
            if (spawn == null || spawn.Collected)
                return false;

            spawn.Collected = true;
            items = new List<LootItem>(spawn.LootItems);
            _activeSpawns.Remove(spawn);
            Save();
            return true;
        }

        public bool RemoveSpawn(string spawnId)
        {
            var removed = _activeSpawns.RemoveAll(x => x.Id == spawnId) > 0;
            if (removed)
                Save();
            return removed;
        }

        public void ClearAll()
        {
            _activeSpawns.Clear();
            Save();
        }

        public void Save()
        {
            try
            {
                Directory.CreateDirectory("scripts");
                var options = new JsonSerializerOptions { WriteIndented = true };
                File.WriteAllText(_savePath, JsonSerializer.Serialize(_activeSpawns, options));
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
                var loaded = JsonSerializer.Deserialize<List<TrainLootSpawn>>(json);
                _activeSpawns.Clear();
                if (loaded != null)
                    _activeSpawns.AddRange(loaded.Where(x => !string.IsNullOrWhiteSpace(x.Id)));
            }
            catch
            {
                _activeSpawns.Clear();
            }
        }

        private void Cleanup(Vector3 playerPos, Action<TrainLootSpawn> onDespawn)
        {
            for (int i = _activeSpawns.Count - 1; i >= 0; i--)
            {
                var spawn = _activeSpawns[i];
                if (spawn.Collected)
                {
                    _activeSpawns.RemoveAt(i);
                    continue;
                }

                if (Vector3.Distance(playerPos, spawn.Position) > DespawnRadius)
                {
                    _activeSpawns.RemoveAt(i);
                    onDespawn?.Invoke(spawn);
                }
            }
        }

        private void SpawnLoot(Vector3 aroundPosition, Action<TrainLootSpawn> onSpawn)
        {
            var position = GetRandomOffset(aroundPosition, SpawnRadius);
            var spawn = new TrainLootSpawn
            {
                Id = Guid.NewGuid().ToString("N"),
                Position = position,
                CreatedAtUtc = DateTime.UtcNow,
                LootTier = RollLootTier(),
                LootItems = GenerateLootItems()
            };

            _activeSpawns.Add(spawn);
            Save();
            onSpawn?.Invoke(spawn);
        }

        private int RollLootTier()
        {
            int roll = _rng.Next(100);
            if (roll < 55) return 1;
            if (roll < 85) return 2;
            if (roll < 97) return 3;
            return 4;
        }

        private List<LootItem> GenerateLootItems()
        {
            var items = new List<LootItem>();
            int tier = RollLootTier();

            int count = tier switch
            {
                1 => _rng.Next(1, 3),
                2 => _rng.Next(2, 4),
                3 => _rng.Next(3, 5),
                _ => _rng.Next(4, 7)
            };

            for (int i = 0; i < count; i++)
            {
                items.Add(PickLootItem(tier));
            }

            if (_rng.NextDouble() < 0.35 + (tier * 0.08))
                items.Add(new LootItem { ItemId = "cash", Amount = _rng.Next(12, 60) * tier });

            return items;
        }

        private LootItem PickLootItem(int tier)
        {
            string[] pool =
            {
                "ammo_revolver",
                "ammo_rifle",
                "tonic_health",
                "tonic_stamina",
                "chewing_tobacco",
                "canned_food",
                "lock_breaker",
                "horse_stimulant",
                "jewelry",
                "watch",
                "gold_nugget",
                "medicine"
            };

            string item = pool[_rng.Next(pool.Length)];
            int amount = 1;

            if (item == "ammo_revolver" || item == "ammo_rifle")
                amount = _rng.Next(12, 36);
            else if (item == "canned_food" || item == "chewing_tobacco" || item == "medicine")
                amount = _rng.Next(1, 3);
            else if (item == "gold_nugget")
                amount = tier >= 3 ? _rng.Next(1, 3) : 1;
            else if (item == "jewelry" || item == "watch")
                amount = _rng.Next(1, 2);

            return new LootItem
            {
                ItemId = item,
                Amount = amount
            };
        }

        private Vector3 GetRandomOffset(Vector3 origin, float radius)
        {
            float angle = (float)(_rng.NextDouble() * Math.PI * 2.0);
            float dist = (float)(_rng.NextDouble() * radius);
            return new Vector3(
                origin.X + (float)Math.Cos(angle) * dist,
                origin.Y + (float)Math.Sin(angle) * dist,
                origin.Z
            );
        }
    }

public class TrainLootSpawn
{
    public string Id { get; set; }
    public Vector3 Position { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public int LootTier { get; set; }
    public bool Collected { get; set; }
    public List<LootItem> LootItems { get; set; } = new List<LootItem>();
}

public class LootItem
{
    public string ItemId { get; set; }
    public int Amount { get; set; }
}