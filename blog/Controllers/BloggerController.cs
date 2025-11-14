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
        [HttpGet("byId")]
        public ActionResult<Blog> GetRecordById(int id)
        {
            using (var context = new BlogDbContext())
            {
                var blogId = context.blog.FirstOrDefault(blog => blog.Id == id);

                if (blogId != null)
                {
                    return Ok(new
                    {
                        message = "Sikeres lekérdezés",
                        result = blogId
                    });
                }

                return NotFound(new
                {
                    meassage = "Nincs ilyen id!"
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
        [HttpDelete]
        public ActionResult DeleteRecord(int id)
        {
            using (var context = new BlogDbContext())
            {
                var blogDelete = context.blog.FirstOrDefault(blog => blog.Id == id);

                if (blogDelete != null)
                {
                    context.blog.Remove(blogDelete);
                    context.SaveChanges();
                    return Ok(new
                    {
                        message = "Sikeres törlés."
                    });
                }

                return NotFound(new
                {
                    meassage = "Nincs mit törölni!"
                });
            }
        }
        [HttpGet("count")]
        public ActionResult<Blog> GetBloggersAmount()
        {
            using (var context = new BlogDbContext())
            {
                var blogs = context.blog.ToList();

                if (blogs != null)
                {
                    return Ok(blogs.Count());
                }

                return BadRequest(new
                {
                    message = "Sikertelen lekérdezés."
                });
            }

        }
        [HttpGet("NameEmail")]
        public ActionResult<Blog> GetBloggersNameEmail()
        {
            using (var context = new BlogDbContext())
            {
                var namesEmails = context.blog
                    .Select(b => new { b.Name, b.Email })
                    .ToList();

                if (namesEmails != null && namesEmails.Count > 0)
                {
                    return Ok(namesEmails);
                }

                return BadRequest(new
                {
                    message = "Sikertelen lekérdezés."
                });
            }

        }
        [HttpGet("oldest")]
        public ActionResult<Blog> GetOldestBlogger()
        {
            using (var context = new BlogDbContext())
            {
                var oldest = context.blog
                    .OrderBy(b => b.RegTime)
                    .FirstOrDefault();

                if (oldest != null)
                {
                    return Ok(new
                    {
                        message = "Sikeres lekérdezés",
                        result = oldest
                    });
                }

                return NotFound(new
                {
                    message = "Nincsenek blogger adatok."
                });
            }
        }
    }
}