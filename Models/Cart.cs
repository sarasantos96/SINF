//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FirstREST.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Cart
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
    
        public virtual Utilizador Utilizador { get; set; }
    }
}
