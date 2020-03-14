using System.Collections.Generic;
using Business.Models;

namespace Business.Services
{
    public interface IGroupService
    {
        IReadOnlyCollection<Group> GetAll();
        Group GetById(long id);
        Group Update(Group group);
        Group Add(Group group);
    }
}
