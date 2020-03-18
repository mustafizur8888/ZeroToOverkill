using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Models;

namespace Business.Services
{
    public interface IGroupService
    {
        Task<IReadOnlyCollection<Group>> GetAllAsync();
        Task<Group> GetByIdAsync(long id);
        Task<Group> UpdateAsync(Group group);
        Task<Group> AddAsync(Group group);
    }
}
