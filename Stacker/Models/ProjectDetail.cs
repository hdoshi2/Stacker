using Newtonsoft.Json.Linq;
using Postgrest.Attributes;
using Stacker.Supabase;

namespace Stacker.Models
{
    [Table("designBuild_projectDetails")]
    internal class ProjectDetail : SupabaseTableBase
    {
        [Column("json")] public JToken Json { get; set; }

        [Column("name")] public string Name { get; set; }

        [Column("project_ID")] public int ProjectId { get; set; }

        [Column("report_NAME")] public string ReportName { get; set; }
    }
}
