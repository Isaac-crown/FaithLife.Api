using FaithLife.Api.Data.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace FaithLife.Api.Model
{
    public class Person
    {
        public Guid Id { get; set; }

        public string MemberId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Title { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public Nullable<int> DayOfBirth { get; set; }

        [Display(Name = "Month Of Birth")]
        public int? MonthOfBirth { get; set; }

        [Display(Name = "Year Of Birth")]
        public Nullable<int> YearOfBirth { get; set; }


        [Display(Name = "Day Of Wedding")]
        public Nullable<int> DayOfWedding { get; set; }

        public MaritalStatus MaritalStatus { get; set; }

        [Required]
        public string Phonenumber { get; set; }
        public Gender Gender { get; set; }

        public WorkForce WorkForce { get; set; }


        [Required]
        public string Email { get; set; }

      
    }
}
