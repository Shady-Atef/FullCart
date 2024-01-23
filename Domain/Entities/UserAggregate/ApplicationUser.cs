using Domain.Entities.UserAggregate.Inputs;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.UserAggregate
{
    public class ApplicationUser : IdentityUser<long>
    {
        public string Uuid { get; set; } = Guid.NewGuid().ToString();
        public int Version { get; private set; }
        public long? CreatedBy { get; private set; }
        public DateTime? CreationTime { get; private set; }
        public long? UpdatedBy { get; private set; }
        public DateTime? UpdateTime { get; private set; }
        public long? DeletedBy { get; private set; }
        public DateTime? DeletedTime { get; private set; }
        public bool IsDeleted { get; private set; }
        public int FK_DefaultLanguageID { get; private set; }
        public DateTime LockoutStart { get; private set; }
        public long? RoleID { get; private set; }
        public string? FirstName { get; private set; }

        public event EventHandler OnEvent;

        public static string RandomPassword(PasswordOptions opts = null)
        {
            if (opts == null) opts = new PasswordOptions()
            {
                RequiredLength = 8,
                RequiredUniqueChars = 5,
                RequireDigit = true,
                RequireLowercase = true,
                RequireNonAlphanumeric = false,
                RequireUppercase = true
            };

            string[] randomChars = new[] {
            "ABCDEFGHJKLMNOPQRSTUVWXYZ",    // uppercase 
            "abcdefghijkmnopqrstuvwxyz",    // lowercase
            "0123456789",                   // digits
            "_@$-"                        // non-alphanumeric
        };

            Random rand = new Random(Environment.TickCount);
            List<char> chars = new List<char>();

            if (opts.RequireUppercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[0][rand.Next(0, randomChars[0].Length)]);

            if (opts.RequireLowercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[1][rand.Next(0, randomChars[1].Length)]);

            if (opts.RequireNonAlphanumeric)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[3][rand.Next(0, randomChars[3].Length)]);

            if (opts.RequireDigit)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[2][rand.Next(0, randomChars[2].Length)]);



            for (int i = chars.Count; i < opts.RequiredLength
                || chars.Distinct().Count() < opts.RequiredUniqueChars; i++)
            {
                //string rcs = randomChars[rand.Next(0, randomChars.Length)];
                //chars.Insert(rand.Next(0, chars.Count),
                //    rcs[rand.Next(0, rcs.Length)]);
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[2][rand.Next(0, randomChars[2].Length)]);
            }

            return new string(chars.ToArray());
        }

        public void ChangePassword(long id)
        {
            throw new NotImplementedException();
        }

        public void Login(long id)
        {
            //throw new NotImplementedException();
        }

        public void Logout(long id)
        {
            // throw new NotImplementedException();
        }

        public void ResetPassword(long id, string newPass, bool sendViaEmail)
        {
            throw new NotImplementedException();
        }

        public void WrongCredintials(int maxfailed, long updateById)
        {
            if (!LockoutEnabled)
            {
                //throw new RbException(ValidationKey.InvalidCredentialsTryAgain);
            }
            else
            {
                Version++;
                AccessFailedCount++;
                UpdatedBy = updateById;
                UpdateTime = DateTime.Now;

                if (AccessFailedCount == maxfailed)
                {
                    LockoutEnd = DateTimeOffset.MaxValue;
                    LockoutStart = DateTime.Now;
                }
            }
        }

        public void Create(AccountCreateInput input)
        {
            Version++;
            FirstName = input.FullName;
            UserName = input.UserName;
            NormalizedUserName = input.UserName.ToLower();
            PhoneNumber = input.PhoneNumber;
            Email = input.Email;
            NormalizedEmail = input.Email.ToLower();
            Id = input.Id;
            CreatedBy = input.CreatedBy;
            LockoutEnabled = true;
        }
    }
}
