using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wata.Commerce.Sample.Domain.Models
{
	[Table("tbl_abcs")]
    public class Abc
    {
		[Key]
		[Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AbcID{ get; set; }

		[Required]
		[StringLength(50)]
		public string Name{ get; set; }

		[Required]
		public DateTime CreateDate{ get; set; }

		public DateTime? UpdateDate{ get; set; }

		[StringLength(36)]
		public string? CreateBy{ get; set; }

		[StringLength(36)]
		public string? UpdateBy{ get; set; }
    }
}