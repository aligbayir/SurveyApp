﻿namespace SurveyApp.Models.Entities
{
    public class User
    {
        public Guid UserId { get; set; }
        public string? username { get; set; }
        public string? password { get; set; }
    }
}
