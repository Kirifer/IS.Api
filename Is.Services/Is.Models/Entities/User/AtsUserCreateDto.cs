﻿namespace Is.Models.Entities.User
{
    public class AtsUserCreateDto
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required string Email { get; set; }

    }
}