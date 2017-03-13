using Dapper;
using System;

namespace Od34.Entity
{
    [Table("Post")]
    public class Post
    {
        [Key]
        public virtual Guid id_post { get; set; }
        public virtual string title { get; set; }
        public virtual string meta_title { get; set; }
        public virtual string header_image { get; set; }
        public virtual string description { get; set; }
        public virtual string meta_description { get; set; }
        public virtual string post_body { get; set; }
        public virtual DateTime dbcreate { get; set; }
        public virtual Int32 status { get; set; }
    }
}