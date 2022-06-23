namespace Payroll.Core.BaseModels
{
    #region UpdatableEntity
    public interface IUpdatebleEntity : IBaseEntity
    {
        bool Updated { get; set; }
        DateTimeOffset? UpdatedDate { get; set; }
        string UpdatedBy { get; set; }
    }
    public class UpdatableEntity : BaseEntity, IUpdatebleEntity
    {
        public virtual bool Updated { get; set; }
        public virtual DateTimeOffset? UpdatedDate { get; set; }
        public virtual string UpdatedBy { get; set; }
    }
    #endregion

    #region UpdatableEntityDto
    public interface IUpdatebleEntityDto : IBaseEntityDto
    {
        bool Updated { get; set; }
        DateTimeOffset? UpdatedDate { get; set; }
        string UpdatedBy { get; set; }
    }
    public class UpdatableEntityDto : BaseEntityDto, IUpdatebleEntityDto
    {
        public virtual bool Updated { get; set; }
        public virtual DateTimeOffset? UpdatedDate { get; set; }
        public virtual string UpdatedBy { get; set; }
    }
    #endregion
}