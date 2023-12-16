using System.ComponentModel.DataAnnotations;

namespace Star_Communications.Core.Dtos
{
	public class PaginationDto
	{
		[Range(1, 10, ErrorMessage = "page size starts from 1 till 10")]
		public int PageSize { get; set; }


		[Range(1, int.MaxValue, ErrorMessage = "page starts from 1")]
		public int Page { get; set; }
	}
}
