using System;
using System.Collections.Generic;
using System.Text;
using Business.Models;
using Data.Entities;

namespace Business.Impl.Mappings
{
    internal static class GroupMappings
    {
        public static Group ToService(this GroupEntity entity)
        {
            return entity != null ? new Group { Id = entity.Id, Name = entity.Name } : null;
        }
        public static GroupEntity ToEntity(this Group model)
        {
            return model != null ? new GroupEntity { Id = model.Id, Name = model.Name } : null;
        }

        public static IReadOnlyCollection<Group> ToService(this IReadOnlyCollection<GroupEntity> entities) => entities.MapCollection(ToService);


    }
}
