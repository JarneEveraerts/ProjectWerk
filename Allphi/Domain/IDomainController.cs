namespace Domain
{
    public interface IDomainController
    {
        bool IsBtwValid(string btwNumber);
        bool IsEmailValid(string email);
        bool IsLicensePlateValid(string licensePlate);
    }
}