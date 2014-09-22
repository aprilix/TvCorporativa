﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using TvCorporativa.DAL;
using TvCorporativa.Models;

namespace TvCorporativa.DAO
{
    public abstract class BaseDao<T> : IDao<T> where T : class
    {
        protected readonly TvContext Context;

        protected BaseDao(TvContext context)
        {
            Context = context;
        } 

        public T Save(T entity)
        {
            if (Context.Entry(entity).State == EntityState.Detached)
            {
                Context.Set<T>().Attach(entity);
                Context.Entry(entity).State = EntityState.Modified;
            }
            else
                Context.Set<T>().Add(entity);

            Context.SaveChanges();
            
            return entity;
        }

        public IList<T> SaveColection(IList<T> colection)
        {
            var newEntitys = Context.Set<T>().AddRange(colection).ToList();
            Context.SaveChanges();
            return newEntitys;
        }

        public IList<T> GetAll()
        {
            return Context.Set<T>().ToList();
        }

        public abstract IList<T> GetAll(Empresa empresa);

        public T Get(int id)
        {
            return Context.Set<T>().Find(id);
        }

        public void Delete(T entity)
        {
            Context.Set<T>().Remove(entity);
            Context.SaveChanges();
        }

        public void DeleteColection(IList<T> colection)
        {
            Context.Set<T>().RemoveRange(colection);
            Context.SaveChanges();
        }
    }
}