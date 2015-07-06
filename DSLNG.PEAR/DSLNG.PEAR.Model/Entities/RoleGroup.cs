﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DSLNG.PEAR.Data.Entities
{
    public class RoleGroup
    {
        public RoleGroup()
        {
            Menus = new HashSet<Menu>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public Level Level { get; set; }
        public int? LevelId { get; set; }
        public string Icon { get; set; }
        public string Remark { get; set; }
        public bool IsActive { get; set; }
        public string Code { get; set; }
        public ICollection<Menu> Menus { get; set; }
    }
}
