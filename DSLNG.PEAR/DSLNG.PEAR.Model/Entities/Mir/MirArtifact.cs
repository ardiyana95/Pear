

using DSLNG.PEAR.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DSLNG.PEAR.Data.Entities.Mir
{
    public class MirArtifact
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Artifact Artifact { get; set; }
        public MirArtifactType Type { get; set; }
    }
}
