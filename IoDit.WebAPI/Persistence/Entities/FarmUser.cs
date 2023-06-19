using IoDit.WebAPI.Persistence.Entities.Base;
using IoDit.WebAPI.Utilities.Types;

namespace IoDit.WebAPI.Persistence.Entities;

public class FarmUser : EntityBase, IEntity
{

    public virtual User User { get; set; }
    public virtual Farm Farm { get; set; }
    public FarmRoles FarmRole { get; set; }
}