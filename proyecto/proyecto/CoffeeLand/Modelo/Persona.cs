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
    
    public partial class Persona
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Persona()
        {
            this.DeudaPersona = new HashSet<DeudaPersona>();
            this.SalarioPersonaPermanente = new HashSet<SalarioPersonaPermanente>();
            this.SalarioPersonaTemporal = new HashSet<SalarioPersonaTemporal>();
        }
    
        public string DocumentoPersona { get; set; }
        public string TipoDocumento { get; set; }
        public string TipoContratoPersona { get; set; }
        public string NombrePersona { get; set; }
        public string Genero { get; set; }
        public string Telefono { get; set; }
        public System.DateTime FechaNacimineto { get; set; }
        public string EstadoPersona { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DeudaPersona> DeudaPersona { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SalarioPersonaPermanente> SalarioPersonaPermanente { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SalarioPersonaTemporal> SalarioPersonaTemporal { get; set; }
    }
}
