using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeptunoNet2023.Entidades.Dtos.Producto
{
    public class ProductoListDto : ICloneable
    {
        public int ProductoId { get; set; }
        public string NombreProducto { get; set; }
        public string NombreCategoria { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int UnidadesEnStock { get; set; }
        public bool Suspendido { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
