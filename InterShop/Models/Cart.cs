//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InterShop.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Cart
    {
        public int ID { get; set; }
        public string ClientEmail { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int Qvantity { get; set; }
        public Nullable<System.DateTime> DatePurchased { get; set; }
        public decimal Price { get; set; }
    
        public virtual Products Products { get; set; }
    }
}
