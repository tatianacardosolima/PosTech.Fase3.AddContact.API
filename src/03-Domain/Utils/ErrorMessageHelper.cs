using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosTech.Fase3.AddContact.Domain.Utils
{
    public static class ErrorMessageHelper
    {
        public const string CONTACT001 = "CONTACT001";
        public const string CONTACT002 = "CONTACT002";
        public const string CONTACT003 = "CONTACT003";

        public static Dictionary<string, string> ErrorMessage = new Dictionary<string, string>
        {
            { CONTACT001, "O sobrenome deve ser diferente do nome do contato." },
            { CONTACT002, "E-mail inválido." },
            { CONTACT003, "Telefone inválido." },
        };

    }
}
