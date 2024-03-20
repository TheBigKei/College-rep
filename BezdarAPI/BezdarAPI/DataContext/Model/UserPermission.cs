﻿using System.ComponentModel.DataAnnotations;

namespace BezdarAPI.DataContext.Model
{
    public class UserPermission
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
