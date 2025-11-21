using blog.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using blog.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace blog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BloggerController : ControllerBase
    {
        [HttpPost("AddNewBlogger")]
        public ActionResult<Blogger> AddNewRecord(AddBloggerDto blogger)
        {
            using (var context = new BlogDbContext())
            {
                var newBlogger = new Blogger
                {
                    Name = blogger.Name,
                    Email = blogger.Email,
                    Password = blogger.Password
                };

                if (newBlogger != null)
                {
                    context.blog.Add(newBlogger);
                    context.SaveChanges();
                    return StatusCode(201, new {message = "Sikeres felvitel" , result = newBlogger});
                }

                return BadRequest(new
                {
                    message = "Sikertelen feltöltés."
                });
            }
        }

        [HttpGet("GetAllBloggers")]
        public ActionResult<Blogger> GetAllRecord()
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


        [HttpGet("GetBloggersWithPosts")]
        public ActionResult<Blogger> GetBloggersWithPosts()
        {
            using (var context = new BlogDbContext())
            {
                var bloggersWithPosts = context.blog.Include(x => x.Posts).ToList();
                if (bloggersWithPosts != null)
                {
                    return Ok(new
                    {
                        message = "Sikeres lekérdezés",
                        result = bloggersWithPosts
                    });
                }
                return BadRequest(new
                {
                    message = "Sikertelen lekérdezés."
                });
            }
        }


        [HttpGet("GetBloggerByIdWithPosts")]
        public ActionResult<Blogger> GetRecordByIdWithPost(int id)
        {
            try
            {
                using (var context = new BlogDbContext())
                {
                    var bloggersWithPosts = context.blog.Include(x => x.Posts).FirstOrDefault(x => x.Id == id);
                    var blogger = new
                    {
                        Name = bloggersWithPosts.Name,
                        Category = bloggersWithPosts.Posts.Select(x => new { x.Category, x.Description })
                    };
                    return Ok(new
                    {
                        message = "Sikeres lekérdezés",
                        result = blogger
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = "Sikertelen lekérdezés."
                });
            }
        }


        [HttpGet("GetBloggerById")]
        public ActionResult<Blogger> GetRecordById(int id)
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


        [HttpGet("CountBloggers")]
        public ActionResult<Blogger> GetBloggersAmount()
        {
            using (var context = new BlogDbContext())
            {
                var bloggers = context.blog.ToList();

                if (bloggers != null)
                {
                    return Ok(bloggers.Count());
                }

                return BadRequest(new
                {
                    message = "Sikertelen lekérdezés."
                });
            }

        }


        [HttpGet("NameEmail")]
        public ActionResult<Blogger> GetBloggersNameEmail()
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


        [HttpGet("OldestBlogger")]
        public ActionResult<Blogger> GetOldestBlogger()
        {
            using (var context = new BlogDbContext())
            {
                var oldest = context.blog.OrderBy(b => b.RegTime).FirstOrDefault();

                if (oldest != null)
                {
                    return Ok(new { message = "Sikeres lekérdezés", result = oldest });
                }
                return NotFound(new { message = "Nincsenek blogger adatok." });
            }
        }


        [HttpGet("CountBloggersPosts")]
        public ActionResult<Blogger> GetBloggersPostsAmount()
        {
            try
            {
                using (var context = new BlogDbContext())
                {
                    var bloggersPostsCount = context.blog.Select(b => new { BloggerId = b.Id, BloggerName = b.Name, PostsCount = b.Posts.Count}).ToList();

                    if (bloggersPostsCount != null && bloggersPostsCount.Count > 0){
                        return Ok(new { message = "Sikeres lekérdezés", result = bloggersPostsCount });
                    }
                    return NotFound(new { message = "Nincsenek blogger adatok." });
                }
            }
            catch (Exception ex){
                return BadRequest(new { message = "Hiba a lekérdezés során." });
            }
        }
    }
}