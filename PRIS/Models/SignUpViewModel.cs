using PRIS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace PRIS.Models
{
    public class SignUpViewModel
    {
        //public virtual ICollection<Language> Languages { get; set; }

        public virtual ICollection<Skill> Skills { get; set; }

        public virtual ICollection<WorkingExperience> WorkingExperiences { get; set; }

        public virtual ICollection<Qualification> Qualifications { get; set; }

        Language[] lArray = new Language[100];

        public List<SelectListItem> States { get; set; }
        public int[] StateId { get; set; }

        public List<Language> Languages { get; set; }
        public int LanguageId { get; set; }
        public string LanguageLearned { get; set; }

        public int WEId { get; set; }
        public string WorkingExp { get; set; }

        public int SkillId { get; set; }
        public string SkillAcquired { get; set; }

        public int ApplicationId { get; set; }
        public System.DateTime ApplicationDate { get; set; }

        [Required]
        public string JobTitle { get; set; }
        public Nullable<double> Salary { get; set; }
        public string JobRequirement { get; set; }
        public string JobDescription { get; set; }
        public string Location { get; set; }
        public string ShortDescription { get; set; }
        public Nullable<System.DateTime> PostingDate { get; set; }
        public Nullable<System.DateTime> ExpiredDate { get; set; }
        public string ClosingDate { get; set; }
        public Nullable<int> Test { get; set; }
        public Nullable<int> test2 { get; set; }

        public Nullable<int> JobId { get; set; }
        public Nullable<int> JobseekerId { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNo { get; set; }
        public string Qualification { get; set; }
        public string FieldOfStudy { get; set; }
        public string PreferredLocation { get; set; }
        public Nullable<double> PreferredSalary { get; set; }
        public string PreferredField { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public byte[] Resume { get; set; }
        public byte[] Picture { get; set; }

        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyWebsite { get; set; }
        public string CompanyInformation { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyEmail { get; set; }
        public string CompanyPhoneNo { get; set; }
        public byte[] CompanyLogo { get; set; }

        public int TestId { get; set; }        
        public Nullable<int> Part { get; set; }
        public Nullable<int> Total { get; set; }
    }
}