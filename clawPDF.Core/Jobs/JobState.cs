namespace clawSoft.clawPDF.Core.Jobs
{
    public enum JobState
    {
        Pending,
        Cancelled,
        Failed,
        Succeeded,
        InvalidLicense,
        NoMatch,
        SingleMatch,
        MultipleMatches,
        Undefined
    }
}