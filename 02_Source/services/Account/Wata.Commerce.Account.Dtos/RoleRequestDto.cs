using System;
using System.ComponentModel.DataAnnotations;
using Wata.Commerce.Common.Objects;

namespace Wata.Commerce.Account.Dtos
{
    public partial class RoleRequestDto : RequestDtoBase
    {
		[Required]
		[StringLength(450)]
		public string Id{ get; set; }

		[StringLength(4000)]
		public string? ConcurrencyStamp{ get; set; }

		[StringLength(256)]
		public string? Name{ get; set; }

		[StringLength(256)]
		public string? NormalizedName{ get; set; }
		//put your code here

    }
}