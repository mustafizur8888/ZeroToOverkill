﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Business.Impl.Mappings;
using Business.Models;
using Business.Services;
using Data;
using Microsoft.EntityFrameworkCore;

namespace Business.Impl.Services
{
    public class GroupService : IGroupService
    {
        private readonly GroupMangmentDbContext _context;

        public GroupService(GroupMangmentDbContext context)
        {
            _context = context;
        }
        public async Task<IReadOnlyCollection<Group>> GetAllAsync(CancellationToken ct)
        {
            var groups = await _context.Groups.AsNoTracking().OrderBy(g => g.Id).ToListAsync(ct);
            return groups.ToService();
        }

        public async Task<Group> GetByIdAsync(long id, CancellationToken ct)
        {
            var group = await _context.Groups.AsNoTracking().SingleAsync(g => g.Id == id, ct);
            return group.ToService();
        }

        public async Task<Group> UpdateAsync(Group group, CancellationToken ct)
        {
            var updategriupEntity = _context.Groups.Update(group.ToEntity());
            await _context.SaveChangesAsync();
            return updategriupEntity.Entity.ToService();
        }

        public async Task<Group> AddAsync(Group group, CancellationToken ct)
        {
            var added = _context.Groups.Add(group.ToEntity());
            await _context.SaveChangesAsync();
            return added.Entity.ToService();
        }

        public async Task RemoveAsync(long id, CancellationToken ct)
        {
            var group = await _context.Groups.AsNoTracking().SingleAsync(g => g.Id == id, ct);
            _context.Groups.Remove(group);
            await _context.SaveChangesAsync(ct);
        }
    }
}
