

using DSLNG.PEAR.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DSLNG.PEAR.Data.Entities.Mir
{
    public class MirHighlight
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public SelectOption HighlightType { get; set; }
        public MirHighlightType Type { get; set; }
    }
}
