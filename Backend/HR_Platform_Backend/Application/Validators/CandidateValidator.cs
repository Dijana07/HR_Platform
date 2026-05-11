using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public static class CandidateValidator
    {
        public static ResultDTO ValidateCandidateDto(CandidateDTO candidate)
        {
            List<string> errors = new();
            if (candidate == null)
            {
                return new ResultDTO{ Success = false, Message = "DTO not found." };
            }
            if (string.IsNullOrEmpty(candidate.Name))
            {
                errors.Add("name");
            }
            if (string.IsNullOrEmpty(candidate.Email))
            {
                errors.Add("email");
            }
            if (string.IsNullOrEmpty(candidate.ContactNumber))
            {
                errors.Add("contact number");
            }
            if (candidate.DateOfBirth == default)
            {
                errors.Add("date of birth");
            }

            if (errors.Any())
            {
                string msg = "Missing required field(s): ";
                for (int i = 0; i < errors.Count(); i++)
                {
                    if (i != errors.Count() - 1)
                    {
                        msg += errors[i] + ", ";
                    }
                    else
                    {
                        msg += errors[i];
                    }
                }
                return new ResultDTO { Success = false, Message = msg };
            }

            // it's empty but just in case
            errors.Clear();
            if (candidate.Skills != null && candidate.Skills.Any())
            {
                foreach (var skill in candidate.Skills)
                {
                    if (skill.Id != null && skill.Id <= 0)
                    {
                        errors.Add("invalid skill id");
                    }

                    if (skill.Id == null &&
                        string.IsNullOrWhiteSpace(skill.Name))
                    {
                        errors.Add("skill name");
                    }
                }
            }

            if (errors.Any())
            {
                return new ResultDTO
                {
                    Success = false,
                    Message = $"Invalid skill data: {string.Join(", ", errors.Distinct())}"
                };
            }


            return new ResultDTO { Success = true, Message = "Validation successful." };
        }
    }
}
