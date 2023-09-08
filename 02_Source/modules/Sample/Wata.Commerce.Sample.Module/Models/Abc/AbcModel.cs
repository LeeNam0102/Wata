using System.ComponentModel.DataAnnotations;
using Wata.Commerce.Common.Objects;

namespace Wata.Commerce.Sample.Module.Models.Abc
{
    public partial class AbcModel : ModelBase
    {
		[Required]
		public int AbcID { get; set; }

		[Required]
		[StringLength(50)]
		public string Name { get; set; }

		public DateTime CreateDate { get; set; }

		public DateTime? UpdateDate { get; set; }

		public string CreateBy { get; set; }

		public string UpdateBy { get; set; }
		//put your code here
    }
}