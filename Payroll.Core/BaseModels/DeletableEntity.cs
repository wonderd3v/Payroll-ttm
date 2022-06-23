namespace Payroll.Core.BaseModels
{
    #region DeletableEntity
    public interface IDeletableEntityFilter { }
    public interface IDeletableEntity : IBaseEntity
    {
        bool Deleted { get; set; }
        DateTimeOffset? DeletedDate { get; set; }
        string DeletedBy { get; set; }
    }
    public class DeletableEntity : BaseEntity, IDeletableEntity
    {
        public virtual bool Deleted { get; set; }
        public virtual DateTimeOffset? DeletedDate { get; set; }
        public virtual string DeletedBy { get; set; }
    }
    #endregion

    #region DeletableEntityDto
    public interface IDeletableEntityDto : IBaseEntityDto
    {
        bool Deleted { get; set; }
        DateTimeOffset? DeletedDate { get; set; }
        string DeletedBy { get; set; }
    }
    public class DeletableEntityDto : BaseEntityDto, IDeletableEntityDto
    {
        public virtual bool Deleted { get; set; }
        public virtual DateTimeOffset? DeletedDate { get; set; }
        public virtual string DeletedBy { get; set; }
    }
    #endregion
}