namespace Component.Utility.Enums
{
    public enum UserTypes
    {
        User=1,
        Driver=2
    }

    public enum Gender
    {
        Male=1,
        Female=2
    }

    public enum UserStatus
    {
        Inactive=0,
        Active =1
    }
    public enum RequestItemStatus
    {
        Requested,
        Pending,
        Delivered,
        Completed,
        Cancelled,
        InProgress

    }
    public enum RequestPaymentStatus
    {
        Cash,
        CreditCard
    }
    public enum CultureType
    {
        Both = 0,
        English = 1,
        Arabic = 2
    }
    public enum RatingTypes
    {
        ReportProblem=0,
        RateDriver=1
    }
}
