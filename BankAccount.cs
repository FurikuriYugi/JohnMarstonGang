public class BankAccount
{
    public string OwnerId { get; set; } = string.Empty;
    public string OwnerName { get; set; } = string.Empty;
    public decimal Cash { get; set; }
    public decimal Gold { get; set; }
    public int Suspicion { get; set; }
    public int Reputation { get; set; }

    public BankAccountSaveData ToSaveData()
    {
        return new BankAccountSaveData
        {
            OwnerId = OwnerId,
            OwnerName = OwnerName,
            Cash = Cash,
            Gold = Gold,
            Suspicion = Suspicion,
            Reputation = Reputation
        };
    }

    public static BankAccount FromSaveData(BankAccountSaveData data)
    {
        return new BankAccount
        {
            OwnerId = data.OwnerId,
            OwnerName = data.OwnerName,
            Cash = data.Cash,
            Gold = data.Gold,
            Suspicion = data.Suspicion,
            Reputation = data.Reputation
        };
    }
}