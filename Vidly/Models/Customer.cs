﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
	public class Customer
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Please enter customer's name.")]
		[StringLength(255)]
		public string Name { get; set; }

		public bool IsSubscribedToNewsletter { get; set; }

		public MembershipType MembershipType { get; set; }

		public byte MembershipTypeId { get; set; }

		[Min18YearsIfAMember]
		public DateTime? Birthdate { get; set; }
	}
}