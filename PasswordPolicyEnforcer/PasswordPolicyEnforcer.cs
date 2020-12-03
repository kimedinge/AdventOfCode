using System;
using System.Collections.Generic;
using System.Linq;

namespace PasswordPolicyEnforcer
{
    public static class PasswordPolicy
    {
        public static IEnumerable<Password> CheckPasswordsPart1(IEnumerable<Password> passwords)
        {
            var validPasswords = new List<Password>();
            foreach(var password in passwords)
            {
                var characterCount = password.passwordToValidate.Length - password.passwordToValidate.Replace(password.characterToValidate.ToString(), string.Empty).Length;
                if (characterCount >= password.minCharacters && characterCount <= password.maxCharacters)
                {
                    validPasswords.Add(password);
                }
            }
            return validPasswords;
        }
        public static IEnumerable<Password> CheckPasswordsPart2(IEnumerable<Password> passwords)
        {
            var validPasswords = new List<Password>();
            foreach (var password in passwords)
            {
                var firstPosition = false;
                var secondPosition = false;
                if (password.passwordToValidate.Length - 1 >= password.minCharacters-1)
                {
                    firstPosition = password.passwordToValidate[password.minCharacters-1].Equals(password.characterToValidate);
                }

                if (password.passwordToValidate.Length - 1 >= password.maxCharacters-1)
                {
                    secondPosition = password.passwordToValidate[password.maxCharacters-1].Equals(password.characterToValidate);
                }

                if (firstPosition ^ secondPosition)
                {
                    validPasswords.Add(password);
                }
            }
            return validPasswords;
        }
    }
}
