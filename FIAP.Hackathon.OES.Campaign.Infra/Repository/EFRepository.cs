using FIAP.Hackathon.OES.Campaign.Domain.Entity.Abstracts;
using FIAP.Hackathon.OES.Campaign.Infra.Repository.Interfaces;
using FIAP.Hackathon.OES.User.Infra.Repository;
using Microsoft.EntityFrameworkCore;

namespace FIAP.Hackathon.OES.Campaign.Infra.Repository
{
    public class EFRepository<T>(ApplicationDbContext context) : IRepository<T> where T : EntityBase
    {
        protected ApplicationDbContext _context = context;
        protected DbSet<T> _dbSet = context.Set<T>();

        public T Create(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public IList<T> GetAll() 
            => _dbSet.ToList();

        public T? GetById(long id)
            => _dbSet.FirstOrDefault(q => q.Id == id);

        public void Update(T entity)
        {
            _dbSet.Update(entity);
            _context.SaveChanges();
        }

        public void DeleteById(long id)
        {
            _dbSet.Remove(GetById(id)!);
            _context.SaveChanges();
        }
    }
}
