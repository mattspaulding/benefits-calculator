using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Http.Description;
using System.Web.Mvc;

namespace AngularMaterialDotNet.API.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "The name is too long.")]
        public string Name { get; set; }
        [Required]
        [Range(0, 5, ErrorMessage = "Choose a number between 0 and 5.")]
        public int NumberOfDependents { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }
        [JsonIgnore]
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
    public class EmployeeRequest
    {
        [Required]
        [StringLength(30, ErrorMessage = "The name is too long.")]
        public string Name { get; set; }
        [Required]
        [Range(0, 5, ErrorMessage = "Choose a number between 0 and 5.")]
        public int NumberOfDependents { get; set; }
    }
    public class EmployeeResponse
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public double NumberOfDependents { get; set; }
        public double DiscountPercentage
        {
            get
            {
                if (this.Name.StartsWith("A"))
                    return 10;
                else
                    return 0;
            }
        }
        public double Discount
        {
            get
            {
                if (this.Name.StartsWith("A"))
                    return .9;
                else
                    return 1;
            }
        }
        public double AnnualAmount
        {
            get
            {
                return 2000 * 26;
            }
        }
        public double PaycheckAmount
        {
            get
            {
                return 2000;
            }
        }
        public double AnnualCostOfBenefits
        {
            get
            {
                return 1000 * this.Discount;
            }
        }
        public double PaycheckCostOfBenefits
        {
            get
            {
                return 1000 / 26 * this.Discount;
            }
        }
        public double AnnualCostOfDependents
        {
            get
            {
                return this.NumberOfDependents * 500 * this.Discount;
            }
        }
        public double PaycheckCostOfDependents
        {
            get
            {
                return this.NumberOfDependents * 500 / 26 * this.Discount;
            }
        }
        public double AnnualCostTotal
        {
            get
            {
                return AnnualCostOfBenefits + AnnualCostOfDependents;
            }
        }
        public double PaycheckCostTotal
        {
            get
            {
                return PaycheckCostOfBenefits + PaycheckCostOfDependents;
            }
        }
        public double AnnualTotal
        {
            get
            {
                return AnnualAmount - AnnualCostTotal;
            }
        }
        public double PaycheckTotal
        {
            get
            {
                return PaycheckAmount - PaycheckCostTotal;
            }
        }
    }

}
