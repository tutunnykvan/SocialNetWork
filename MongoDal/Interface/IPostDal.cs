using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDTO;

namespace MongoDal.Interface
{
    public interface IPostDal
    {
        PostDTO UpdatePost(PostDTO post);

        void AddCommentToPost(int id, CommentDTO comment);
        
        PostDTO CreatePost(PostDTO post);

        void DeleteC(int post_id, int comment_id);
        void DeleteP(int id);
        void Dislike(int post_id, DislikeDTO dislike);

        List<PostDTO> GetAllPost();

        PostDTO GetPostById(int id);

        void NLike(int post_id, LikeDTO like);
        void Like(int post_id, LikeDTO like);
        void NDislike(int post_id, DislikeDTO dislike);

                                   
    }
}
