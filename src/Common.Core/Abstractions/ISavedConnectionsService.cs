using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ServiceBusExplorer.Core.Models;

namespace ServiceBusExplorer.Core.Abstractions
{
    /// <summary>
    /// Abstraction for persisting and loading named connection profiles.
    /// Implementations include <see cref="Services.JsonSavedConnectionsService"/>
    /// (cross-platform JSON) and a future WinForms adapter that reads from App.config.
    /// </summary>
    public interface ISavedConnectionsService
    {
        /// <summary>Returns all saved connection profiles ordered by display name.</summary>
        Task<IReadOnlyList<ConnectionProfile>> GetAllAsync(CancellationToken ct = default);

        /// <summary>Saves or replaces a profile. The profile's <c>Name</c> is the unique key.</summary>
        Task SaveAsync(ConnectionProfile profile, CancellationToken ct = default);

        /// <summary>Removes a profile by its display name. No-op if the name is unknown.</summary>
        Task DeleteAsync(string name, CancellationToken ct = default);
    }
}

