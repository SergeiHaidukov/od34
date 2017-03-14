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

        /// <summary>
        /// Возвращает все посты
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Возвращает модель по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static PostModel GetPostById(Guid? id)
        {
            PostModel pm;
            if(DB.Connect())
            {
                pm = new PostModel(DB.conn.Get<Entity.Post>(id));
                return pm;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Сохраняет или обновляет пост
        /// </summary>
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