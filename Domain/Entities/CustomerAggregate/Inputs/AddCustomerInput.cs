namespace Domain.CustomerAggregate.Inputs
{
    public class AddCustomerInput
    {
        public string ProfilePicture { get; set; }
        public string FirstName { get; set; }
        public string FamilyName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public long CustomerId { get; set; }
        public long UserId { get; set; }
    }
}