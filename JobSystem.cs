using System;
using System.Collections.Generic;
using System.Linq;

public class JobSystem
{
    private readonly List<JobOffer> _offers = new List<JobOffer>();
    private readonly List<JobAssignment> _assignments = new List<JobAssignment>();

    public IReadOnlyList<JobOffer> Offers => _offers;
    public IReadOnlyList<JobAssignment> Assignments => _assignments;

    public void GenerateTownJobs(string townName)
    {
        if (string.IsNullOrWhiteSpace(townName))
            return;

        AddOffer(CreateCustomJob("Escort Cargo", "Protect a wagon carrying supplies into town.", townName, 15m, 0m, 5, false));
        AddOffer(CreateCustomJob("Rob Bank Coach", "Ambush a coach hauling money and gold.", townName, 40m, 2m, -10, true));
        AddOffer(CreateCustomJob("Deliver Supplies", "Move food and ammunition to an outpost.", townName, 20m, 0m, 3, false));
    }

    public JobOffer CreateCustomJob(string title, string description, string locationName, decimal cash, decimal gold, int honorChange, bool criminal)
    {
        return new JobOffer
        {
            JobId = Guid.NewGuid().ToString("N"),
            Title = title,
            Description = description,
            LocationName = locationName,
            PaymentCash = cash,
            PaymentGold = gold,
            HonorChange = honorChange,
            IsCriminal = criminal,
            IsAvailable = true
        };
    }

    public void AddOffer(JobOffer offer)
    {
        if (offer == null)
            return;

        _offers.Add(offer);
    }

    public bool AcceptJob(string jobId, string gangMemberName = null)
    {
        JobOffer offer = _offers.FirstOrDefault(x => x.JobId == jobId);
        if (offer == null)
            return false;

        _offers.Remove(offer);
        _assignments.Add(new JobAssignment
        {
            AssignmentId = Guid.NewGuid().ToString("N"),
            JobId = offer.JobId,
            Title = offer.Title,
            GangMemberName = gangMemberName,
            Status = JobStatus.Active,
            PaymentCash = offer.PaymentCash,
            PaymentGold = offer.PaymentGold,
            HonorChange = offer.HonorChange,
            IsCriminal = offer.IsCriminal
        });

        return true;
    }

    public bool AcceptJobWithGangVote(string jobId, GangManager gangManager, string gangMemberName = null)
    {
        JobOffer offer = _offers.FirstOrDefault(x => x.JobId == jobId);
        if (offer == null || gangManager == null)
            return false;

        List<GangMember> activeMembers = gangManager.GetActiveMembers();
        if (activeMembers.Count == 0)
            return AcceptJob(jobId, gangMemberName);

        int yesVotes = 0;

        foreach (GangMember member in activeMembers)
        {
            if (member == null)
                continue;

            if (offer.IsCriminal)
            {
                if (member.Loyalty >= 40)
                    yesVotes++;
            }
            else
            {
                if (member.Loyalty >= 25)
                    yesVotes++;
            }
        }

        int noVotes = activeMembers.Count - yesVotes;
        if (yesVotes <= noVotes)
            return false;

        return AcceptJob(jobId, gangMemberName);
    }

    public bool CompleteJob(string assignmentId)
    {
        JobAssignment assignment = _assignments.FirstOrDefault(x => x.AssignmentId == assignmentId);
        if (assignment == null)
            return false;

        assignment.Status = JobStatus.Completed;
        return true;
    }

    public bool FailJob(string assignmentId)
    {
        JobAssignment assignment = _assignments.FirstOrDefault(x => x.AssignmentId == assignmentId);
        if (assignment == null)
            return false;

        assignment.Status = JobStatus.Failed;
        return true;
    }

    public void PayOutCompletedJobs(BankSystem bank, string playerAccountId, GangManager gangManager = null)
    {
        if (bank == null || string.IsNullOrWhiteSpace(playerAccountId))
            return;

        List<JobAssignment> completed = _assignments.Where(x => x.Status == JobStatus.Completed).ToList();

        foreach (JobAssignment assignment in completed)
        {
            if (assignment == null)
                continue;

            if (assignment.PaymentCash > 0m || assignment.PaymentGold > 0m)
            {
                List<GangMember> splitMembers = gangManager != null ? gangManager.GetActiveMembers() : new List<GangMember>();

                if (splitMembers.Count > 0)
                {
                    decimal memberShareCash = assignment.PaymentCash / (splitMembers.Count + 1);
                    decimal memberShareGold = assignment.PaymentGold / (splitMembers.Count + 1);

                    foreach (GangMember member in splitMembers)
                    {
                        if (member == null)
                            continue;

                        member.PersonalCash += memberShareCash;
                        member.PersonalGold += memberShareGold;
                    }

                    decimal playerCash = assignment.PaymentCash - (memberShareCash * splitMembers.Count);
                    decimal playerGold = assignment.PaymentGold - (memberShareGold * splitMembers.Count);

                    bank.DepositCash(playerAccountId, playerCash);
                    bank.DepositGold(playerAccountId, playerGold);
                }
                else
                {
                    bank.DepositCash(playerAccountId, assignment.PaymentCash);
                    bank.DepositGold(playerAccountId, assignment.PaymentGold);
                }
            }

            _assignments.Remove(assignment);
        }
    }

    public void LoadFromSave(List<JobOffer> offers, List<JobAssignment> assignments)
    {
        _offers.Clear();
        _assignments.Clear();

        if (offers != null)
            _offers.AddRange(offers);

        if (assignments != null)
            _assignments.AddRange(assignments);
    }

    public JobOffer GetOffer(string jobId)
    {
        return _offers.FirstOrDefault(x => x.JobId == jobId);
    }

    public JobAssignment GetAssignment(string assignmentId)
    {
        return _assignments.FirstOrDefault(x => x.AssignmentId == assignmentId);
    }
}