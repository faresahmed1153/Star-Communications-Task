using System.ComponentModel.DataAnnotations;

namespace Star_Communications.Core.Dtos
{
	public class LoginDto
	{


		[RegularExpression(@"^[a-zA-Z_][a-zA-Z0-9_]{0,14}$",
			ErrorMessage = "username must Starts with a letter or underscore and Contains only letters, numbers, and underscores Has a maximum length of 15 characters (including the initial letter or underscore)")]
		public string UserName { get; set; }


		[RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$",
			ErrorMessage = "password Has minimum 8 characters in length and include at least 1 lowercase, at least 1 uppercase, at least 1 numeric character and at least one special character")]
		public string Password { get; set; }
	}
}
