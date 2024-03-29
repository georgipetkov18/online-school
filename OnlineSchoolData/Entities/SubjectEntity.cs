﻿using System.ComponentModel.DataAnnotations;

namespace OnlineSchoolData.Entities
{
    public class SubjectEntity : BaseEntity
    {
        [Required]
        [MaxLength(128)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(30)]
        public string Code { get; set; } = string.Empty;

        public virtual ICollection<TimetableEntity> TimetableEntities { get; set; } = new HashSet<TimetableEntity>();
        public virtual ICollection<TeacherEntity> Teachers { get; set; } = new HashSet<TeacherEntity>();
    }
}
