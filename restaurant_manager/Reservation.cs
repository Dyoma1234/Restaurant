//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace restaurant_manager
{
    using System;
    using System.Collections.Generic;
    
    public partial class Reservation
    {
        public int Id { get; set; }
        public System.DateTime Time { get; set; }
        public int Guests_Id { get; set; }
        public int Tables_Id { get; set; }
    
        public virtual Guests GuestsSet { get; set; }
        public virtual Tables TablesSet { get; set; }
    }
}
