﻿using System.ComponentModel.DataAnnotations;

namespace OnlineSchoolData.Entities
{
    public class StudentEntity : BaseEntity
    {
        [Required]
        public Guid ClassId { get; set; }
        public virtual ClassEntity Class { get; set; } = null!;

        [Required]
        public virtual UserEntity User { get; set; } = null!;
    }
}
