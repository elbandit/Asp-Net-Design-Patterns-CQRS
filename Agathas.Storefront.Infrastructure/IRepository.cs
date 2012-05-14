using System;

namespace Agathas.Storefront.Infrastructure
{
    public interface IRepository<Entity, EntityKey> 
    {
        Entity find_by(EntityKey id);
        void add(Entity entity);
        void save(Entity entity);
        Entity query_for_single(Func<Entity, bool> func);        
    }
}
