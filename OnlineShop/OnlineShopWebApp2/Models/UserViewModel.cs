using OnlineShopWebApp.Models.Abstract;
using System;


namespace OnlineShopWebApp.Models
{
    public class UserViewModel:LoadFile
    {

		public string? Email { get; set; }

		public string UserName { get; set; }

		public int Age { get; set; }

		public string PhoneNumber { get; set; }

		public UserViewModel( string email, string name, int age, string phone)
		{
			Email = email;
			UserName = name;
			Age = age;
			PhoneNumber = phone;
		}

        public UserViewModel() { }
	}
}
