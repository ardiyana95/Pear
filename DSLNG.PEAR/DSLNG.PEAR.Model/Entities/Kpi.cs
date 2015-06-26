﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DSLNG.PEAR.Data.Enums;

namespace DSLNG.PEAR.Data.Entities
{
    public class Kpi : BaseEntity
    {
        public Kpi()
        {
            KpiTargets = new Collection<KpiTarget>();
            KpiAchievements = new Collection<KpiAchievement>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Code { get; set; }
        public string Name { get; set; }
        public int? PillarId { get; set; } //to make this nullable we need to include it
        public Pillar Pillar { get; set; }
        public Level Level { get; set; }
        public RoleGroup RoleGroup { get; set; }
        public Type Type { get; set; }
        public Group Group { get; set; }
        public bool IsEconomic { get; set; }
        public int Order { get; set; }
        public YtdFormula YtdFormula { get; set; }
        public Measurement Measurement { get; set; }
        public Method Method { get; set; }
        public int? ConversionId { get; set; }
        public Conversion Conversion { get; set; }
        public FormatInput FormatInput { get; set; }
        public Periode Periode { get; set; }
        public string Remark { get; set; }
        public ICollection<KpiRelationModel> RelationModels { get; set; }
        public DateTime? Value { get; set; }
        public ICollection<KpiTarget> KpiTargets { get; set; }
        public ICollection<KpiAchievement> KpiAchievements { get; set; }

        public bool IsActive { get; set; }
    }
}
