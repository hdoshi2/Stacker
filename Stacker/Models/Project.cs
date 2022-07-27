using Postgrest.Attributes;
using Stacker.Supabase;
using Supabase;

namespace Stacker.Models
{
    [Table("designBuild_projects")]
    internal class Project : SupabaseTableBase
    {
        [Column("name")]
        public string Name { get; set; }
    }
}
