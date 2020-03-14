using System;
using System.Collections.Generic;
using System.Linq;
using Business.Models;
using Business.Services;

namespace Business.Impl.Services
{
    public class InMemoryGroupService : IGroupService
    {
        private readonly List<Group> _groups = new List<Group>();
        private long _currentId = 0;

        public IReadOnlyCollection<Group> GetAll()
        {
            return _groups.AsReadOnly();
        }

        public Group GetById(long id)
        {
            return _groups.SingleOrDefault(g => g.Id == id);
        }

        public Group Update(Group group)
        {
            Group toUpdate = GetById(group.Id);
            if (toUpdate == null)
            {
                return null;
            }
            toUpdate.Name = group.Name;
            return toUpdate;
        }

        public Group Add(Group group)
        {
            group.Id = ++_currentId;
            _groups.Add(group);
            return group;
        }
    }
}
