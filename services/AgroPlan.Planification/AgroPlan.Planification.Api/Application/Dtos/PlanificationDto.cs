using System;

namespace AgroPlan.Planification.Api.Application.Dtos
{
    public class PlanificationDto
    {
        public PlanificationDto()
        {

        }
        public PlanificationDto(string cnp, int year, float surface, string fullName)
        {
            CNP = cnp;
            Year = year;
            Surface = surface;
            FullName = fullName;
        }

        public string CNP { get; set; }
        public int Year { get; set; }
        public float Surface { get; set; }
        public string FullName { get; set; }
    }
}