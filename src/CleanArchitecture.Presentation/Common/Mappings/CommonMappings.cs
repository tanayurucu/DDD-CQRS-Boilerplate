using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Contracts.Common;

using Mapster;

namespace CleanArchitecture.Presentation.Common.Mappings;

/// <summary>
/// Mappings for Common cases
/// </summary>
public sealed class CommonMappings : IRegister
{
    /// <inheritdoc />
    public void Register(TypeAdapterConfig config)
    {
        config.ForType(typeof(Paged<>), typeof(PagedResponse<>));
    }
}