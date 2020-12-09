using MongoDal.Interface;
using MongoDB.Driver;
using MongoDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDal.Concrete
{
    public class PostDal : IPostDal
    {
        private readonly string _conn;
        private readonly string DataName;

        public PostDal(string conn, string Name)
        {
            this._conn = conn;
            this.DataName = Name;
        }

        public void AddCommentToPost(int id, CommentDTO comment)
        {
            try
            {
                var client = new MongoClient(_conn);

                var posts = DB.GetCollection<PostDTO>("post");

                var UpdateFilter = Builders<PostDTO>.Update.AddToSet("comment", comment);

                var DB = client.GetDatabase(DataName);

                posts.UpdateOne(g => g.PostId == id, UpdateFilter);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public PostDTO CreatePost(PostDTO post)
        {
            try
            {
                var client = new MongoClient(_conn);
                var DB = client.GetDatabase(DataName);
                var posts = DB.GetCollection<PostDTO>("post");
                var count_id = posts.CountDocuments(p => p.PostId > 0);
                post.PostId = (int)count_id + 1;
                posts.InsertOne(post);
                return post;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public void DeleteC(int post_id, int comment_id)
        {
            try
            {
                var DB = client.GetDatabase(DataName);
                var posts = DB.GetCollection<PostDTO>("post");
                var post = this.GetPostById(post_id);
                var comment = post.Comments[comment_id - 1];
                var client = new MongoClient(_conn);
              

                var UpdateFilter = Builders<PostDTO>.Update.Pull("comment", comment);
                posts.UpdateOne(g => g.PostId == post_id, UpdateFilter);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public void DeleteP(int id)
        {
            try
            {
                var client = new MongoClient(_conn);
                var DB = client.GetDatabase(DataName);
                var posts = DB.GetCollection<PostDTO>("post");
                posts.DeleteOne(p => p.PostId == id);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public void Dislike(int post_id, DislikeDTO dislike)
        {
            try
            {
                var client = new MongoClient(_conn);
                var db = client.GetDatabase(DataName);
                var posts = db.GetCollection<PostDTO>("post");
                var UpdateFilter = Builders<PostDTO>.Update.AddToSet("Dislikes", dislike);
                posts.UpdateOne(g => g.PostId == post_id, UpdateFilter);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public List<PostDTO> GetAllPosts()
        {
            try
            {
                var client = new MongoClient(_conn);
                var db = client.GetDatabase(DataName);
                var posts = db.GetCollection<PostDTO>("post");

                var all_posts = posts.Find(p => p.PostId > 0).ToList();
                return all_posts;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public PostDTO GetPostById(int id)
        {
            try
            {
                var client = new MongoClient(_conn);
                var db = client.GetDatabase(DataName);
                var posts = db.GetCollection<PostDTO>("post");
                var founded = posts.Find(p => p.PostId == id).Single();
                return founded;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public void NLike(int post_id, LikeDTO like)
        {
            try
            {
                var client = new MongoClient(_conn);
                var db = client.GetDatabase(DataName);
                var posts = db.GetCollection<PostDTO>("post");
                var UpdateFilter = Builders<PostDTO>.Update.Pull("Likes", like);
                posts.UpdateOne(g => g.PostId == post_id, UpdateFilter);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public void Like(int post_id, LikeDTO like)
        {
            try
            {
                var client = new MongoClient(_conn);
                var db = client.GetDatabase(DataName);
                var posts = db.GetCollection<PostDTO>("post");
                var UpdateFilter = Builders<PostDTO>.Update.AddToSet("Likes", like);
                posts.UpdateOne(g => g.PostId == post_id, UpdateFilter);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public void NDislike(int post_id, DislikeDTO dislike)
        {
            try
            {
                var client = new MongoClient(_conn);
                var DB = client.GetDatabase(DataName);
                var posts = DB.GetCollection<PostDTO>("post");
                var UpdateFilter = Builders<PostDTO>.Update.Pull("Dislikes", dislike);
                posts.UpdateOne(g => g.PostId == post_id, UpdateFilter);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        

    }
}
