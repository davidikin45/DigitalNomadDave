using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;
using Solution.Base.Interfaces.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using System.ComponentModel.DataAnnotations;

namespace Solution.Base.Implementation.Model
{
    public abstract class BaseApplicationUser : IdentityUser, IBaseEntity<string>, IBaseEntityAuditable
    {
        public string Name { get; set; }
        object IBaseEntity.Id
        {
            get { return this.Id; }
            set { this.Id = (string)value; }
        }

        private DateTime? _dateCreated;
        public DateTime DateCreated
        {
            get { return _dateCreated ?? DateTime.UtcNow; }
            set { _dateCreated = value; }
        }

        public string UserCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public string UserModified { get; set; }

        public BaseApplicationUser()
        {

        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<BaseApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public Boolean IsValid()
        {
            return Validate().Count() == 0;
        }

        public IEnumerable<ValidationResult> Validate()
        {
            var context = new ValidationContext(this);

            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(
                this,
                context,
               validationResults,
               true);

            return validationResults;
        }

        public abstract IEnumerable<ValidationResult> Validate(ValidationContext validationContext);
    }
}
