using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using FindNearestPlaces.Models;

namespace FindNearestPlaces
{
    public interface IMapsRepository
    {
        IQueryable<Place> All { get; }

        IQueryable<Place> AllIncluding(params Expression<Func<Place, object>>[] includeProperties);

        Place Find(int id);

        void InsertOrUpdate(Place customer);

        void Delete(int id);

        void Save();
    }

    public class MapsRepository
    {
        private readonly MapsContext context = new MapsContext();

        public IQueryable<Place> All
        {
            get { return this.context.Places; }
        }

        public IQueryable<Place> AllIncluding(params Expression<Func<Place, object>>[] includeProperties)
        {
            IQueryable<Place> query = this.context.Places;

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return query;
        }

        public Place Find(int id)
        {
            return this.context.Places.Find(id);
        }

        public void InsertOrUpdate(Place customer)
        {
            if (customer.Id == default(int))
            {
                this.context.Places.Add(customer);
            }
            else
            {
                this.context.Entry(customer).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var customer = this.context.Places.Find(id);
            this.context.Places.Remove(customer);
        }

        public void Save()
        {
            this.context.SaveChanges();
        }
    }
}