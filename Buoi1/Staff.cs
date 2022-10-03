using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buoi1
{
    public class Staff
    {
        const float BaseSalary = 1490000;
        const float CarWarranty = 60000;
        const float MedicalWarranty = 67050;
        const float PercentIncomeTax = 0.2f;


        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime DateStart { get; set; }
        public int TotalWorkHourDay { get; set; }
        public int TotalAbsentDay { get; set; }
        public string Degree { get; set; }
        public string Poisition { get; set; }
        public string Department { get; set; }

        public Staff() { }

        public Staff(string id, string name, DateTime dateStart, int totalWorkHourDay, int totalAbsentDay, string degree, string poisition, string department)
        {
            Id = id;
            Name = name;
            DateStart = dateStart;
            TotalWorkHourDay = totalWorkHourDay;
            TotalAbsentDay = totalAbsentDay;
            Degree = degree;
            Poisition = poisition;
            Department = department;
        }

        // Check this staff how long does he/she work, then caculate his/her seniority coeficient
        public float GetSeniorityCoeficient()
        {
            DateTime CurrentTime = DateTime.Now;
            float TotalMonth = (CurrentTime.Month - DateStart.Month) / 12;
            float TotalYear = (float)Math.Floor(TotalMonth);

            float coeficient = 0;

            if (TotalYear > 0)
                coeficient = TotalYear * 1;
            else coeficient = 1;

            return coeficient;
        }

        // Check this staff how his/her level degree, then give his/her degree coeficient
        public float GetDegreeCoeficient()
        {
            float coeficient = 0;

            switch (Degree)
            {
                case "TH":
                    coeficient = 1.2f;
                    break;
                case "CD":
                    coeficient = 1.5f;
                    break;
                case "DH":
                    coeficient = 2.0f;
                    break;
                case "THS":
                    coeficient = 3.0f;
                    break;
                case "TS":
                    coeficient = 4.5f;
                    break;
                default:
                    coeficient = 1.0f;
                    break;
            }

            return coeficient;
        }

        // Check this staff how his/her position, then give his/her position coeficient
        public float GetPositionCoeficient()
        {
            float coeficient = 0;

            switch (Degree)
            {
                case "GD":
                    coeficient = 5.0f;
                    break;
                case "PGD":
                    coeficient = 4.0f;
                    break;
                case "TP":
                    coeficient = 3.0f;
                    break;
                case "PP":
                    coeficient = 2.0f;
                    break;
                default:
                    coeficient = 1.0f;
                    break;
            }

            return coeficient;
        }

        // Check allowance if his/her available
        public float CheckDepartmentAllowance()
        {
            if (Department == "NS" || Department == "HC" || Department == "KT")
                return BaseSalary * 0.3f;
            return 0;
        }

        // Calculate staff monthly salary
        public float CalculateMonthSalary()
        {
            return BaseSalary * (GetSeniorityCoeficient() + GetDegreeCoeficient() + GetPositionCoeficient()) + CheckDepartmentAllowance();
        }

        //Calculate staff monthly partime salary
        public float CalculatePartimeSalary()
        {
            return CalculateMonthSalary() * 4.5f;
        }

        // Calculate staff receive salary per hour
        public float CalculateEachHourSalary()
        {
            int TotalWorkingDay = 22;
            int TotalHoursWorkPerDay = 8;

            return CalculateMonthSalary() * (TotalWorkingDay * TotalHoursWorkPerDay) * TotalWorkHourDay;
        }

        public float CalculateOvertimeSalary()
        {
            int TotalHoursWorkPerDay = 8;
            int OverTimeHours = TotalWorkHourDay - TotalHoursWorkPerDay;

            return CalculateEachHourSalary() * OverTimeHours * 1.5f;
        }

        public float CaculateAfterTaxSalary()
        {
            return CalculateMonthSalary() - CarWarranty - MedicalWarranty - CalculateMonthSalary() * PercentIncomeTax;
        }

        public float CalculateBonusSalary()
        {
            DateTime CurrentTime = DateTime.Now;
            float TotalMonth = CurrentTime.Month - DateStart.Month;
            float BonusMoney = 0;

            if (TotalMonth > 25)
                BonusMoney = 1.5f * CalculateMonthSalary();
            else if (TotalMonth >= 18)
                BonusMoney = 1.2f * CalculateMonthSalary();
            else if (TotalMonth > 12)
                BonusMoney = 1.0f * CalculateMonthSalary();
            else if (TotalMonth > 6)
                BonusMoney = 0.7f * CalculateMonthSalary();
            else BonusMoney = 0.5f * CalculateMonthSalary();
        }
    }
}
