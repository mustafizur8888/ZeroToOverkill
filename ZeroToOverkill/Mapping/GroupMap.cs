using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Business.Models;
using ZeroToOverkill.Models;

namespace ZeroToOverkill.Mapping
{
    public static class GroupMap
    {
        public static GroupViewModel ToViewModel(this Group model)
        {
            return model != null ? new GroupViewModel { Id = model.Id, Name = model.Name, RowVersion = model.RowVersion } : null;
        }
        public static Group ToServiceModel(this GroupViewModel model)
        {
            return model != null ? new Group { Id = model.Id, Name = model.Name, RowVersion = model.RowVersion } : null;
        }
        public static IReadOnlyCollection<GroupViewModel> ToViewModel(this IReadOnlyCollection<Group> models)
        {
            if (models.Count == 0)
            {
                return Array.Empty<GroupViewModel>();
            }
            var groups = new GroupViewModel[models.Count];
            var i = 0;
            foreach (var model in models)
            {
                groups[i] = model.ToViewModel();
                i++;
            }
            return new ReadOnlyCollection<GroupViewModel>(groups);
        }
    }
}
