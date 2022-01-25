using System;

namespace Entities.HelperModels
{
    public class EmployeeAvatar
    {
        public Guid UserId { get; set; }
        public byte[] File { get; set; }
    }
}