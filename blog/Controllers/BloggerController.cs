using blog.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using blog.Models.Dtos;

namespace blog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BloggerController : ControllerBase
    {
        [HttpPost]
        public ActionResult<Blog> AddNewRecord(Blog blogger)
        {
            using (var context = new BlogDbContext())
            {
                var newBlogger = new Blog
                {
                    Name = blogger.Name,
                    Email = blogger.Email,
                    RegTime = blogger.RegTime,
                    ModTime = blogger.ModTime
                };

                if (newBlogger != null)
                {
                    context.blog.Add(newBlogger);
                    context.SaveChanges();
                    return StatusCode(201, newBlogger);
                }

                return BadRequest(new
                {
                    message = "Sikertelen feltöltés."
                });
            }
        }
        [HttpGet]
        public ActionResult<Blog> GetAllRecord()
        {
            using (var context = new BlogDbContext())
            {
                var blogs = context.blog.ToList();

                if (blogs != null)
                {
                    return Ok(blogs);
                }

                return BadRequest(new
                {
                    message = "Sikertelen lekérdezés."
                });
            }

        }
        [HttpPut]
        public ActionResult PutRecord(int id, UpdateBlogDto updateBlogDto)
        {
            using (var context = new BlogDbContext())
            {
                var exitstingBlog = context.blog.FirstOrDefault(blog => blog.Id == id);

                if (exitstingBlog != null)
                {
                    exitstingBlog.Name = updateBlogDto.Name;
                    exitstingBlog.Email = updateBlogDto.Email;
                    exitstingBlog.RegTime = updateBlogDto.RegTime;
                    exitstingBlog.ModTime = updateBlogDto.ModTime;

                    context.blog.Update(exitstingBlog);
                    context.SaveChanges();

                    return Ok(new
                    {
                        message = "Sikeres frisítés."
                    });
                }

                return NotFound(new
                {
                    meassage = "Nincs mit frissíteni!"
                });
            }
        }
    }
}