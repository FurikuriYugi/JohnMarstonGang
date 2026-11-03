using System;

public class JobOffer
{
    public string JobId { get; set; } = Guid.NewGuid().ToString("N");
    public JobType JobType { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal PayoutCash { get; set; }
    public decimal PayoutGold { get; set; }
    public int HonorChange { get; set; }
    public bool Completed { get; set; }
}