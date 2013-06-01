using System.Collections.Generic;

namespace Service.DTO
{
    public class DebugDTO
    {
        public IList<UserDTO> Users { get; set; }
        public IList<PendingUserDTO> PendingUsers { get; set; }
        public IList<PendingPasswordResetDTO> PendingPasswordResets { get; set; }
    }
}