
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace WebApplication2.Models
{

using System;
    using System.Collections.Generic;
    
public partial class GioHang
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public GioHang()
    {

        this.ChiTietGioHangs = new HashSet<ChiTietGioHang>();

    }


    public string MaGioHang { get; set; }

    public string TaiKhoanKhachHang { get; set; }



    public virtual AspNetUser AspNetUser { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<ChiTietGioHang> ChiTietGioHangs { get; set; }

}

}
