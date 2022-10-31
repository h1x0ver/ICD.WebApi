namespace ICD.Business.Base;

public interface IBaseService<TGet, TCreate, TUpdate>
{
    Task<TGet> Get(int id);
    Task<List<TGet>> GetAll();
    Task Create(TCreate entity);
    Task Update(int id, TUpdate entity);
    Task Delete(int id);

}
