using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Domain.Interfaces;

namespace UserService.Infrastructure.UnitOfWork.Interfaces {
    public interface IUnitOfWork {
        IUserRepository Users { get; }
        Task<int> SaveChangesAsync();
    }
}
