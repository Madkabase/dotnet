using IoDit.WebAPI.Persistence.Entities.Base;
using IoDit.WebAPI.Utilities.Types;

namespace IoDit.WebAPI.Persistence.Entities;

public class FarmUser : EntityBase, IEntity
{

    public User User { get; set; }
    public Farm Farm { get; set; }
    public FarmRoles FarmRole { get; set; }
}