using blog.Models;
using blog.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace blog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        [HttpPost]
        public ActionResult AddNewRecord(AddPostDto addPostDto)
        {
            try
            {
                using (var context = new Models.BlogDbContext())
                {

                    var post = new Post
                    {
                        Category = addPostDto.Category,
                        Description = addPostDto.Description,
                        BloggerId = addPostDto.BloggerId
                    };

                    if (post != null)
                    {
                        context.posts.Add(post);
                        context.SaveChanges();
                        return StatusCode(201, new { message = "Sikeres hozzáadás", result = post });
                    }

                    else
                    {
                        return StatusCode(404, new { message = "Sikertelen hozzáadás.", result = post });
                    }

                }
            }
            catch (Exception ex)
            {
                return StatusCode(400, new { message = ex.Message, result = "" });
            }
        }
    }
}
