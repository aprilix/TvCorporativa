﻿using System.Collections.Generic;
using System.Linq;
using TvCorporativa.DAL;
using TvCorporativa.Models;

namespace TvCorporativa.DAO
{
    public class PontoDao : BaseDao<Ponto>
    {
        public PontoDao(TvContext context) : base(context)
        {
        }

        public override IList<Ponto> GetAll(Empresa empresa)
        {
            return (from p in Context.Pontos
                where p.Empresa.Id.Equals(empresa.Id)
                select p).ToList();
        }

        public IList<Ponto> GetAllNotInPlayList(Empresa empresa, ICollection<Ponto> pontos)
        {
            string query = " SELECT DISTINCT p.Id_Ponto as Id, p.Id_Empresa as IdEmpresa, p.Id_Template as IdTemplate, p.Nome, p.Status " +
                           " FROM ponto_tv p " +
                           " LEFT JOIN PONTO_PLAYLIST pp on pp.Id_Ponto = p.Id_Ponto " +
                           " WHERE p.Id_Empresa = " + empresa.Id +
                           " AND p.Id_Ponto NOT IN (" + pontos.Select(p => p.Id.ToString()).Aggregate((a, b) => a + ", " + b) + ") " +
                           " AND p.Status = 1 ";

            return Context.Pontos.SqlQuery(query).ToList();
        }
    }
}