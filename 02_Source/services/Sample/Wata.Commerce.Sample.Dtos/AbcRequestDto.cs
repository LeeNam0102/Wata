using System;
using System.ComponentModel.DataAnnotations;
using Wata.Commerce.Common.Objects;

namespace Wata.Commerce.Sample.Dtos
{
    public partial class AbcRequestDto : RequestDtoBase
    {
		[Required]
		public int AbcID{ get; set; }

		[Required]
		[StringLength(50)]
		public string Name{ get; set; }


		//put your code here

    }
}