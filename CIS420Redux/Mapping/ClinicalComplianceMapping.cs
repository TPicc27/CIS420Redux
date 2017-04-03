using System.Data.Entity.ModelConfiguration;
using CIS420Redux.Models;

namespace CIS420Redux.Mapping
{
    public class ClinicalComplianceMapping : EntityTypeConfiguration<ClincalCompliance>
    {
        public ClinicalComplianceMapping()
        {
            
            Property(p => p.DocumentId).IsOptional();
            
            //HasKey(p => p.ID);
            //Property(p => p.Type).IsRequired();
            Property(p => p.ExpirationDate).IsOptional();
            //Property(p => p.IsExpired).IsOptional();
            //Property(p => p.IsCompliant).IsOptional();
            //Property(p => p.StudentId).IsRequired();                        


            //HasRequired(p => p.Student);

        }

    }
}