//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FinalP10.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class REPORTE
    {
        public int Id { get; set; }
        public int Servicio { get; set; }
        public int Cliente { get; set; }
        public Nullable<int> IVA { get; set; }
        public Nullable<int> ISR { get; set; }
        public Nullable<int> AHORRO { get; set; }
        public Nullable<int> MESES { get; set; }
        public Nullable<decimal> TOTAL { get; set; }
    
        public virtual CLIENTE CLIENTE1 { get; set; }
        public virtual SERVICIO SERVICIO1 { get; set; }
    }
}