namespace Application.Interfaces
{
    public interface ICurrentUserService
    {
        string UserName { get; }

        string UserId { get; }

        bool IsAuthenticated { get; }
    }
}
