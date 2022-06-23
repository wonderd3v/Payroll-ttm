namespace Payroll.Core.BaseModels
{
    #region BaseEntity
    public interface IBaseEntity
    {
        int Id { get; set; }
    }
    public class BaseEntity : IBaseEntity
    {
        public virtual int Id { get; set; }
    }
    #endregion

    #region BaseEntityDto
    public interface IBaseEntityDto
    {
        int? Id { get; set; }
    }
    public class BaseEntityDto : IBaseEntityDto
    {
        public virtual int? Id { get; set; }
    }
    #endregion
}