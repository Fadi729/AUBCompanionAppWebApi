using System;
using System.Collections.Generic;

namespace CompanionApp.Models
{
    public partial class Like
    {
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }

        public virtual Post Post { get; set; } = null!;
    }
}
