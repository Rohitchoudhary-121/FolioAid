using Application.CommandResults.Models;
using Domain.Common;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Commands.Account
{
    public class RegisterUserCommand : GuidModelBase, IRequest<GenericBaseResult<ApplicationUserResultModel>>
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }

        public string? FullName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool Status { get; set; }
        [MaxLength(255)]
        public string? ActivationString { get; set; }
        [MaxLength(36)]
        public string? PortfolioString { get; set; }

        public string? UserRole { get; set; }
        public string? Password { get; set; }
        public DateTime? ActivationStringExpiryDate { get; set; }
    }
}
