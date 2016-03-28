using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSLNG.PEAR.Data.Entities.Mir
{
    public class MirConfiguration
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public IList<MirDataTable> MirDataTables { get; set; }
        public IList<MirHighlight> MirHighlights { get; set; }
        public IList<MirArtifact> MirArtifacts { get; set; }
        public bool IsActive { get; set; }
    }
}
