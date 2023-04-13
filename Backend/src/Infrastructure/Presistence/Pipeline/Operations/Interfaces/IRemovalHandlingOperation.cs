using Infrastructure.Presistence.Pipeline.Common;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infrastructure.Presistence.Pipeline.Operations.Interfaces;

public interface IRemovalHandlingOperation : IOperation<EntityEntry>
{
    
}