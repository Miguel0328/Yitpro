﻿using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Models;
using Repository.Interfaces;
using Resources.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class Phase : IPhase
    {
        private readonly DataContext _context;

        public Phase(DataContext context)
        {
            _context = context;
        }

        public async Task<List<PhaseModel>> Get(long phaseId = 0)
        {
            var phases =
                (from c in _context.Catalog
                 join leftP in _context.Phase on c.Id equals leftP.PhaseId into pcdf
                 from p in pcdf.DefaultIfEmpty()
                 where (c.CatalogId == Resources.Constants.Catalog.Phase && c.Active) &&
                 ((p != null && p.PhaseId == phaseId) || phaseId == 0)
                 select new { c, p = p ?? new PhaseModel() })
                 .AsEnumerable()
                 .GroupBy(x => new { x.c.Id, x.c.Description })
                 .Select(x => new PhaseModel
                 {
                     PhaseId = x.Key.Id,
                     Phase = new CatalogModel
                     {
                         Description = x.Key.Description
                     },
                     PSP = x.All(x => x.p?.PSP == true),
                     Active = x.Any(x => x.p?.Active == true)
                 })
                 .ToList();

            return phases;
        }

        public async Task<List<PhaseModel>> GetClasifications(long phaseId)
        {
            var clasifications = await
                (from c in _context.Catalog
                 join leftPc in _context.Phase.Where(x => x.PhaseId == phaseId) on c.Id equals leftPc.ClasificationId into pcdf
                 from pc in pcdf.DefaultIfEmpty()
                 where c.CatalogId == Resources.Constants.Catalog.Clasification && c.Active
                 select new { c, pc = pc ?? new PhaseModel() })
                 .Select(x => new PhaseModel
                 {
                     PhaseId = phaseId,
                     ClasificationId = x.c.Id,
                     Clasification = x.c,
                     PSP = x.pc.PSP,
                     Active = x.pc.Active,
                 }).ToListAsync();

            return clasifications;
        }

        public async Task<bool> Put(PhaseModel phase)
        {
            var exists = _context.Phase.Any(x => x.PhaseId == phase.PhaseId && x.ClasificationId == phase.ClasificationId);

            if (exists)
            {
                var entry = _context.Attach(phase);
                entry.Property(x => x.Active).IsModified = true;
                entry.Property(x => x.UpdatedById).IsModified = true;
                entry.Property(x => x.UpdatedAt).IsModified = true;
            }
            else
                _context.Phase.Add(phase);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> PutPSP(PhaseModel _phase)
        {
            var clasifications = await
                (from c in _context.Catalog
                 join pc in _context.Phase.Where(x => x.PhaseId == _phase.PhaseId) on c.Id equals pc.ClasificationId into pcdf
                 from pc in pcdf.DefaultIfEmpty()
                 where c.CatalogId == Resources.Constants.Catalog.Clasification && c.Active
                 select new { c, pc = pc ?? new PhaseModel() })
                 .Select(x => new PhaseModel
                 {
                     PhaseId = _phase.PhaseId,
                     ClasificationId = x.c.Id,
                     Clasification = x.c,
                     PSP = _phase.PSP,
                     Active = x.pc.Active,
                     UpdatedById = _phase.UpdatedById,
                     UpdatedAt = _phase.UpdatedAt
                 }).ToListAsync();

            await _context.BulkInsertOrUpdateAsync(clasifications);

            return true;
        }

        public async Task<bool> Put(long phaseId, long userId)
        {
            var clasifications = await
                (from c in _context.Catalog
                 join leftPc in _context.Phase.Where(x => x.PhaseId == phaseId) on c.Id equals leftPc.ClasificationId into pcdf
                 from pc in pcdf.DefaultIfEmpty()
                 where c.CatalogId == Resources.Constants.Catalog.Clasification && c.Active
                 select new { c, pc = pc ?? new PhaseModel() })
                 .Select(x => new PhaseModel
                 {
                     PhaseId = phaseId,
                     ClasificationId = x.c.Id,
                     Clasification = x.c,
                     PSP = x.pc.PSP,
                     Active = true,
                     UpdatedById = userId,
                     UpdatedAt = DateTime.Now
                 }).ToListAsync();

            await _context.BulkInsertOrUpdateAsync(clasifications);

            return true;
        }
    }
}
