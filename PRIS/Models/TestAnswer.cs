
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
    
public partial class TestAnswer
{

    public int Id { get; set; }

    public int TestQuestionID { get; set; }

    public int TestScore { get; set; }

    public string TestDescription { get; set; }



    public virtual TestQuestion TestQuestion { get; set; }

}

}
