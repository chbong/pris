
//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace PRIS.Models
{

using System;
    using System.Collections.Generic;
    
public partial class Language
{

    public int LanguageId { get; set; }

    public int JobseekerId { get; set; }

    public string LanguageLearned { get; set; }

    public Nullable<bool> DeleteLanguage { get; set; }



    public virtual Jobseeker Jobseeker { get; set; }

}

}
