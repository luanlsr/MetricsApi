using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsApi.CrossCutting.Dtos.Users
{
    public record UserDto(
       Guid Id,
       string Name,
       string Email,
       bool Active,
       DateTime CreatedAt,
       DateTime UpdatedAt
   );
}
