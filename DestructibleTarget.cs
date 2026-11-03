public class DestructibleTarget
{
    public string TargetId { get; set; } = string.Empty;
    public float Health { get; set; } = 100f;
    public bool CanBeBlasted { get; set; }
}