
using DSLNG.PEAR.Data.Enums;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DSLNG.PEAR.Data.Entities.Mir
{
    public class MirDataTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public IList<Kpi> Kpis { get; set; }
        public MirTableType Type { get; set; }
    }
}
