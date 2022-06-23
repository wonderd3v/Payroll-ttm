namespace Payroll.Core.BaseModels
{
    #region CreatebleEntity
    public interface ICreatebleEntity : IBaseEntity
    {
        DateTimeOffset CreatedDate { get; set; }
        string CreatedBy { get; set; }
    }
    public class CreatebleEntity : BaseEntity, ICreatebleEntity
    {
        public virtual DateTimeOffset CreatedDate { get; set; }
        public virtual string CreatedBy { get; set; }
    }
    #endregion

    #region CreatebleEntityDto
    public interface ICreatebleEntityDto : IBaseEntityDto
    {
        DateTimeOffset? CreatedDate { get; set; }
        string CreatedBy { get; set; }
    }
    public class CreatebleEntityDto : BaseEntityDto, ICreatebleEntityDto
    {
        public virtual DateTimeOffset? CreatedDate { get; set; }
        public virtual string CreatedBy { get; set; }
    }
    #endregion
}