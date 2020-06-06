
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DomainModels.Entities
{
    public class Role
    {
        public Role()
        {
            UserRoles = new HashSet<UserRoles>();
        }

        [Key]
        public int RoleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<UserRoles> UserRoles { get; set; }
    }
}
