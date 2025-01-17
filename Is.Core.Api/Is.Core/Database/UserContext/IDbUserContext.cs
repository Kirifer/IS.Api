﻿using Is.Core.Authentication;

namespace Is.Core.Database
{
    public interface IDbUserContext
    {
        public Guid UserId { get; set; }

        public Guid AuthId { get; set; }

        public string UserEmail { get; set; }

        public bool IsAdmin { get; set; }

        public void AssignUserContext(IUserContext userContext);
    }
}
