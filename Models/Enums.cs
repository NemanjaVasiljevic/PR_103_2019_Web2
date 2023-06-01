namespace PR_103_2019.Models
{
    public enum Role
    {
        ADMIN,
        USER,
        SELLER
    }
    public enum OrderState
    {
        AVAILABLE,
        RESERVED,
        SHIPPING,
        ARRIVED,
        RETURNED
    }

    public enum VerificationState
    {
        ACCEPTED,
        REJECTED,
        PENDING
    }
}
