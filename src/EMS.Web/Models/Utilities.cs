using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.Web.Models
{
    public static class Utilities
    {
        public static Dictionary<int, string> PlayingExperience
        {
            get
            {
                return new Dictionary<int, string>
                {
                    { 1, "No organized basketball experience"},
                    { 2, "YMCA, YWCA or grade school"},
                    { 3, "Other youth program"},
                    { 4, "AAU"},
                    { 5, "Junior high/middle school"},
                    { 6, "High school freshman"},
                    { 7, "High school junior varsity"},
                    { 8, "Varsity high school (<500 students)"},
                    { 9, "Varsity high school (>500 students)"},
                    {10, "Adult league or college intermurals"},
                    {11, "College"},
                    {12, "Professional"}
                };
            }
        }

        public static Dictionary<int, string> PlayingFrequency
        {
            get
            {
                return new Dictionary<int, string>
                {
                    { 1, "None (<5 times)"},
                    { 2, "Some (5-25 times)"},
                    { 3, "Lots (>25 times)"}
                };
            }
        }
    }
}
