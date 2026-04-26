using FIAP.Hackathon.OES.Campaign.Domain.Entity.Abstracts;

namespace FIAP.Hackathon.OES.Campaign.Infra.Repository.Interfaces;

public interface IRepository<T> where T : EntityBase
{
    IList<T> GetAll();
    T? GetById(long id);
    T Create(T entity);
    void Update(T entity);
    void DeleteById(long id);

}
