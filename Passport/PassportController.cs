using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Passport
{
    public static class PassportController
    {
        private static string[] validEyeColors = new[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
        public static int NumberOfValidPassports(string passports)
        {
            var valid = new List<string>();
            foreach (var passport in passports.Split("\r\n\r\n"))
            {
                var passportData = passport.Replace("\r\n", " ");
                if (IsValid(passportData))
                {
                    valid.Add(passportData);
                }
            }
            return valid.Count();
        }

        private static bool IsValid(string passportData)
        {
            if (Regex.Match(passportData, "byr:([0-9]*)").Success &&
                Regex.Match(passportData, "iyr:([0-9]*)").Success &&
                Regex.Match(passportData, "eyr:([0-9]*)").Success &&
                Regex.Match(passportData, "hgt:([0-9]*cm)|hgt:([0-9]*in)").Success &&
                Regex.Match(passportData, "hcl:(#[a-f,0-9]*)").Success &&
                Regex.Match(passportData, "ecl:(amb|blu|brn|gry|grn|hzl|oth)").Success &&
                Regex.Match(passportData, "pid:([0-9]*)").Success)
            {
                var byr = Regex.Match(passportData, "byr:([0-9]*)").Groups.Values.LastOrDefault().Value;
                var iyr = Regex.Match(passportData, "iyr:([0-9]*)").Groups.Values.LastOrDefault().Value;
                var eyr = Regex.Match(passportData, "eyr:([0-9]*)").Groups.Values.LastOrDefault().Value;
                var hgtcm = Regex.Match(passportData, "hgt:([0-9]*)cm").Groups.Values.LastOrDefault().Value;
                var hgtin = Regex.Match(passportData, "hgt:([0-9]*)in").Groups.Values.LastOrDefault().Value;
                var hcl = Regex.Match(passportData, "hcl:(#[A-F,a-f,0-9]*)").Groups.Values.LastOrDefault().Value;
                var pid = Regex.Match(passportData, "pid:([0-9]*)").Groups.Values.LastOrDefault().Value;

                if (byr.Length != 4 || int.Parse(byr) < 1920 || int.Parse(byr) > 2002)
                {
                    return false;
                }
                if (iyr.Length != 4 || int.Parse(iyr) < 2010 || int.Parse(iyr) > 2020)
                {
                    return false;
                }
                if (eyr.Length != 4 || int.Parse(eyr) < 2020 || int.Parse(eyr) > 2030)
                {
                    return false;
                }
                if (hgtcm.Length > 0 && (int.Parse(hgtcm) < 150 || int.Parse(hgtcm) > 193))
                {
                    return false;
                }
                if (hgtin.Length > 0 && (int.Parse(hgtin) < 59 || int.Parse(hgtin) > 76))
                {
                    return false;
                }
                if (string.IsNullOrEmpty(hgtcm) && string.IsNullOrEmpty(hgtin))
                {
                    return false;
                }
                if (!hcl.StartsWith("#") || hcl.Length != 7)
                {
                    return false;
                }
                if (Regex.Matches(passportData, "ecl:").Count > 1)
                {
                    return false;
                }
                if (pid.Length != 9)
                {
                    return false;
                }
                return true;
            }
            return false;
        }
    }
}
