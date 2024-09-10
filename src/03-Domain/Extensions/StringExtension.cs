using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PosTech.Fase3.AddContact.Domain.Extensions
{
    public static class StringExtension
    {
        public static bool IsValidEmail(this string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Padrão de regex para validar o formato de e-mail
                string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        public static bool IsValidPhone(this string numero)
        {
            if (string.IsNullOrWhiteSpace(numero))
                return false;

            if (numero.Contains("123456789") || numero.Contains("98765432")
                    || numero.Contains("11223344"))
                return false;

            if (numero.All(c => c == numero[0]))
                return false;

            try
            {
                string pattern = @"^\d{9,}$";
                return Regex.IsMatch(numero, pattern);
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}
