//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Modelo
{
    using System;
    using System.Collections.Generic;
    
    public partial class Finca
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Finca()
        {
            this.Lote = new HashSet<Lote>();
        }
    
        public byte idFinca { get; set; }
        public string NombreFinca { get; set; }
        public string Propietario { get; set; }
        public short idMunicipio { get; set; }
        public string Vereda { get; set; }
        public string Telefono { get; set; }
        public string Cuadras { get; set; }
    
        public virtual Municipio Municipio { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Lote> Lote { get; set; }
    }
}
