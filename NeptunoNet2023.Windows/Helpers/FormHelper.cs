using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeptunoNet2023.Windows.Helpers
{
    public class FormHelper
    {

        public static int CalcularPaginas(int registros, int registrosPorPagina)
        {
            if (registros < registrosPorPagina)
            {
                return 1;
            }
            else if (registros % registrosPorPagina == 0)
            {
                return registros / registrosPorPagina;
            }
            else
            {
                return registros / registrosPorPagina + 1;
            }
        }

    }
}
