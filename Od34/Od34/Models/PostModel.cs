using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Od34.Models
{
    public class PostModel
    {
        static DB _conn = new DB();
        public Entity.Post _post_entity;

        public PostModel()
        {
            _post_entity = new Entity.Post();
            _post_entity.id_post = new Guid();
        }

        public PostModel(Entity.Post post_entity)
        {
            _post_entity = post_entity;
        }

        public static List<PostModel> GetAllPosts()
        {
            List<PostModel> posts = new List<PostModel>();
            if (DB.Connect())
            {
                List<Entity.Post> ent_posts = DB.conn.GetList<Entity.Post>("ORDER BY dbcreate DESC").ToList();
                if(ent_posts != null)
                {
                    foreach(Entity.Post p in ent_posts)
                    {
                        posts.Add(new PostModel(p));
                    }
                }
                return posts;
            }
            else
                return null;            
        }

        public void ModifyPost()
        {
            if(_post_entity.id_post == new Guid())
            {
                if (DB.Connect())
                {
                    _post_entity.id_post = DB.conn.Insert<Guid>(_post_entity);
                }
            }
            else
            {
                if (DB.Connect())
                {
                    DB.conn.Update(_post_entity);
                }
            }
        }
    }
}