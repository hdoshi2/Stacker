using Postgrest.Attributes;
using Supabase;

namespace Stacker.Supabase
{
    internal class SupabaseTableBase : SupabaseModel
    {
        [PrimaryKey("id", false)] public int Id { get; set; }
    }
}
