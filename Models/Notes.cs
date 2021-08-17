using System;





namespace Noteapp.Models
{
    public partial class Notes
    {
       
        public  Guid Noteid { get; set; }
        public string Notedetails { get; set; }
        public DateTime? Notedate { get; set; }
        
        
    }
}
