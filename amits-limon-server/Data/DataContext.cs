using amits_limon_server.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace amits_limon_server.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Spotlight> Spotlights { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Recommendation> Recommendations { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Song>().HasData(
            new Song
            {
                SongId = 1,
                Name = "test song 1",
                Date = DateTime.Now,
                ImgSource = "https://localhost:5001/api/Images//am.jpg",
                AudioSource = "https://localhost:5001/api/Audio?FileName=shazamat.mp3",
                Description = "this is a test song"
            },
            new Song
            {
                SongId = 2,
                Name = "test song 2",
                Date = DateTime.Now,
                ImgSource = "https://localhost:5001/api/Images//nirvana.jpg",
                AudioSource = "https://localhost:5001/api/Audio?FileName=shabak.mp3",
                Description = "this is a test song"
            },
            new Song
            {
                SongId = 3,
                Name = "test song 3",
                Date = DateTime.Now,
                ImgSource = "https://localhost:5001/api/Images//redhot.jpg",
                AudioSource = "https://localhost:5001/api/Audio?FileName=shabak.mp3",
                Description = "this is a test song"
            },
            new Song
            {
                SongId = 4,
                Name = "test song 4",
                Date = DateTime.Now,
                ImgSource = "https://localhost:5001/api/Images//ar.jpg",
                AudioSource = "https://localhost:5001/api/Audio?FileName=shabak.mp3",
                Description = "this is a test song"
            },
            new Song
            {
                SongId = 5,
                Name = "test song 5",
                Date = DateTime.Now,
                ImgSource = "https://localhost:5001/api/Images//am.jpg",
                AudioSource = "https://localhost:5001/api/Audio?FileName=shabak.mp3",
                Description = "this is a test song"
            });
            modelBuilder.Entity<Spotlight>().HasData(
            new Spotlight
            {
                SpotlightId = 1,
                Name = "spotlight test1",
                Youtube = "dQw4w9WgXcQ",
                Deezer = "https://www.youtube.com/watch?v=dQw4w9WgXcQ&ab_channel=RickAstleyVEVO",
                Amazone = "https://www.youtube.com/watch?v=dQw4w9WgXcQ&ab_channel=RickAstleyVEVO",
                Apple = "https://www.youtube.com/watch?v=dQw4w9WgXcQ&ab_channel=RickAstleyVEVO",
                Itunes = "https://www.youtube.com/watch?v=dQw4w9WgXcQ&ab_channel=RickAstleyVEVO",
                Spotify = "https://www.youtube.com/watch?v=dQw4w9WgXcQ&ab_channel=RickAstleyVEVO",
                Tidal = "https://www.youtube.com/watch?v=dQw4w9WgXcQ&ab_channel=RickAstleyVEVO"
            },
            new Spotlight
            {
                SpotlightId = 2,
                Name = "spotlight test2",
                Youtube = "0G383538qzQ",
                Deezer = "https://www.youtube.com/watch?v=dQw4w9WgXcQ&ab_channel=RickAstleyVEVO",
                Amazone = "https://www.youtube.com/watch?v=dQw4w9WgXcQ&ab_channel=RickAstleyVEVO",
                Apple = "https://www.youtube.com/watch?v=dQw4w9WgXcQ&ab_channel=RickAstleyVEVO",
                Itunes = "https://www.youtube.com/watch?v=dQw4w9WgXcQ&ab_channel=RickAstleyVEVO",
                Spotify = "https://www.youtube.com/watch?v=dQw4w9WgXcQ&ab_channel=RickAstleyVEVO",
                Tidal = "https://www.youtube.com/watch?v=dQw4w9WgXcQ&ab_channel=RickAstleyVEVO"
            },
            new Spotlight
            {
                SpotlightId = 3,
                Name = "spotlight test3",
                Youtube = "hC8CH0Z3L54",
                Deezer = "https://www.youtube.com/watch?v=dQw4w9WgXcQ&ab_channel=RickAstleyVEVO",
                Amazone = "https://www.youtube.com/watch?v=dQw4w9WgXcQ&ab_channel=RickAstleyVEVO",
                Apple = "https://www.youtube.com/watch?v=dQw4w9WgXcQ&ab_channel=RickAstleyVEVO",
                Itunes = "https://www.youtube.com/watch?v=dQw4w9WgXcQ&ab_channel=RickAstleyVEVO",
                Spotify = "https://www.youtube.com/watch?v=dQw4w9WgXcQ&ab_channel=RickAstleyVEVO",
                Tidal = "https://www.youtube.com/watch?v=dQw4w9WgXcQ&ab_channel=RickAstleyVEVO"
            },
            new Spotlight
            {
                SpotlightId = 4,
                Name = "spotlight test4",
                Youtube = "Gs069dndIYk",
                Deezer = "https://www.youtube.com/watch?v=dQw4w9WgXcQ&ab_channel=RickAstleyVEVO",
                Amazone = "https://www.youtube.com/watch?v=dQw4w9WgXcQ&ab_channel=RickAstleyVEVO",
                Apple = "https://www.youtube.com/watch?v=dQw4w9WgXcQ&ab_channel=RickAstleyVEVO",
                Itunes = "https://www.youtube.com/watch?v=dQw4w9WgXcQ&ab_channel=RickAstleyVEVO",
                Spotify = "https://www.youtube.com/watch?v=dQw4w9WgXcQ&ab_channel=RickAstleyVEVO",
                Tidal = "https://www.youtube.com/watch?v=dQw4w9WgXcQ&ab_channel=RickAstleyVEVO"
            },
            new Spotlight
            {
                SpotlightId = 5,
                Name = "spotlight test5",
                Youtube = "dQw4w9WgXcQ",
                Deezer = "https://www.youtube.com/watch?v=dQw4w9WgXcQ&ab_channel=RickAstleyVEVO",
                Amazone = "https://www.youtube.com/watch?v=dQw4w9WgXcQ&ab_channel=RickAstleyVEVO",
                Apple = "https://www.youtube.com/watch?v=dQw4w9WgXcQ&ab_channel=RickAstleyVEVO",
                Itunes = "https://www.youtube.com/watch?v=dQw4w9WgXcQ&ab_channel=RickAstleyVEVO",
                Spotify = "https://www.youtube.com/watch?v=dQw4w9WgXcQ&ab_channel=RickAstleyVEVO",
                Tidal = "https://www.youtube.com/watch?v=dQw4w9WgXcQ&ab_channel=RickAstleyVEVO"
            },
            new Spotlight
            {
                SpotlightId = 6,
                Name = "spotlight test6",
                Youtube = "dQw4w9WgXcQ",
                Deezer = "https://www.youtube.com/watch?v=dQw4w9WgXcQ&ab_channel=RickAstleyVEVO",
                Amazone = "https://www.youtube.com/watch?v=dQw4w9WgXcQ&ab_channel=RickAstleyVEVO",
                Apple = "https://www.youtube.com/watch?v=dQw4w9WgXcQ&ab_channel=RickAstleyVEVO",
                Itunes = "https://www.youtube.com/watch?v=dQw4w9WgXcQ&ab_channel=RickAstleyVEVO",
                Spotify = "https://www.youtube.com/watch?v=dQw4w9WgXcQ&ab_channel=RickAstleyVEVO",
                Tidal = "https://www.youtube.com/watch?v=dQw4w9WgXcQ&ab_channel=RickAstleyVEVO"
            },
            new Spotlight
            {
                SpotlightId = 7,
                Name = "spotlight test7",
                Youtube = "dQw4w9WgXcQ",
                Deezer = "https://www.youtube.com/watch?v=dQw4w9WgXcQ&ab_channel=RickAstleyVEVO",
                Amazone = "https://www.youtube.com/watch?v=dQw4w9WgXcQ&ab_channel=RickAstleyVEVO",
                Apple = "https://www.youtube.com/watch?v=dQw4w9WgXcQ&ab_channel=RickAstleyVEVO",
                Itunes = "https://www.youtube.com/watch?v=dQw4w9WgXcQ&ab_channel=RickAstleyVEVO",
                Spotify = "https://www.youtube.com/watch?v=dQw4w9WgXcQ&ab_channel=RickAstleyVEVO",
                Tidal = "https://www.youtube.com/watch?v=dQw4w9WgXcQ&ab_channel=RickAstleyVEVO"
            },
            new Spotlight
            {
                SpotlightId = 8,
                Name = "spotlight test8",
                Youtube = "dQw4w9WgXcQ",
                Deezer = "https://www.youtube.com/watch?v=dQw4w9WgXcQ&ab_channel=RickAstleyVEVO",
                Amazone = "https://www.youtube.com/watch?v=dQw4w9WgXcQ&ab_channel=RickAstleyVEVO",
                Apple = "https://www.youtube.com/watch?v=dQw4w9WgXcQ&ab_channel=RickAstleyVEVO",
                Itunes = "https://www.youtube.com/watch?v=dQw4w9WgXcQ&ab_channel=RickAstleyVEVO",
                Spotify = "https://www.youtube.com/watch?v=dQw4w9WgXcQ&ab_channel=RickAstleyVEVO",
                Tidal = "https://www.youtube.com/watch?v=dQw4w9WgXcQ&ab_channel=RickAstleyVEVO"
            });
            modelBuilder.Entity<Comment>().HasData(
            new Comment
            {
                CommentId = 1,
                Author = "roy",
                Content = "comment test 1",
                SongId = 1,
                Date = DateTime.Now,
                TimeStamp = 5
            },
            new Comment
            {
                CommentId = 2,
                Author = "amit",
                Content = "comment test 2",
                SongId = 1,
                Date = DateTime.Now,
                TimeStamp = 5
            },
            new Comment
            {
                CommentId = 3,
                Author = "roy",
                Content = "comment test 3",
                SongId = 1,
                Date = DateTime.Now,
                TimeStamp = 5
            },
            new Comment
            {
                CommentId = 4,
                Author = "roy",
                Content = "comment test 4",
                SongId = 1,
                Date = DateTime.Now,
                TimeStamp = 10
            },
            new Comment
            {
                CommentId = 5,
                Author = "roy",
                Content = "comment test 5",
                SongId = 1,
                Date = DateTime.Now,
                TimeStamp = 10
            },
            new Comment
            {
                CommentId = 6,
                Author = "roy",
                Content = "comment test 6",
                SongId = 2,
                Date = DateTime.Now,
                TimeStamp = 5
            },
            new Comment
            {
                CommentId = 7,
                Author = "roy",
                Content = "comment test 7",
                SongId = 2,
                Date = DateTime.Now,
                TimeStamp = 15
            },
            new Comment
            {
                CommentId = 8,
                Author = "roy",
                Content = "comment test 8",
                SongId = 3,
                Date = DateTime.Now,
                TimeStamp = 1
            });
            modelBuilder.Entity<Note>().HasData(
            new Note
            {
                NoteId = 1,
                Date = DateTime.Now,
                Title = "test note 1",
                Content = "this is a test note, this should be long enuogh to cut in the middle and shown completly when opend aaaa     Lorem ipsum dolor sit amet consectetur adipisicing elit. Possimus,reprehenderit consequatur, in quis nemo totamquasi debitis neque id dolor  commodi magnam ducimus incidunt aspernatur laudantium accusamus alias illo provident perspiciatis sint sunt pariatur, qui iste.Deleniti modi, explicabo sint ab unde odio earum atque dolor nostrum minima quaerat voluptatum impedit soluta vel repellendus culpa aliquam quas fugiat repudiandae rem quidem architecto nobis ducimus ? Expedita voluptate sintveritatis ? Doloribus, commodi, magnam voluptatibus maxime voluptates ducimus  quasi, dolorum ab accusantium ad unde mollitia rem praesentium corporis sit vero animi amet ? Ad fugiat omnis veniam provident placeat in. Maxime veniam  exercitationem nam est quia tempora,   reprehenderit consequuntur expedita non  vero quaerat! Tenetur"
            },
            new Note
            {
                NoteId = 2,
                Date = DateTime.Now,
                Title = "test note 2",
                Content = "this is a test note, this should be long enuogh to cut in the middle and shown completly when opend"
            },
            new Note
            {
                NoteId = 3,
                Date = DateTime.Now,
                Title = "test note 3",
                Content = "this is a test note, this should be long enuogh to cut in the middle and shown completly when opend"
            },
            new Note
            {
                NoteId = 4,
                Date = DateTime.Now,
                Title = "test note 4",
                Content = "this is a test note, this should be long enuogh to cut in the middle and shown completly when opend"
            },
            new Note
            {
                NoteId = 5,
                Date = DateTime.Now,
                Title = "test note 5",
                Content = "this is a test note, this should be long enuogh to cut in the middle and shown completly when opend"
            },
            new Note
            {
                NoteId = 6,
                Date = DateTime.Now,
                Title = "test note 6",
                Content = "this is a test note, this should be long enuogh to cut in the middle and shown completly when opend"
            },
            new Note
            {
                NoteId = 7,
                Date = DateTime.Now,
                Title = "test note 7",
                Content = "this is a test note, this should be long enuogh to cut in the middle and shown completly when opend"
            });
            modelBuilder.Entity<Recommendation>().HasData(
            new Recommendation
            {
                RecommendationId = 1,
                Date = DateTime.Now,
                Title = "test playlist 1",
                Link = "https://www.youtube.com/watch?v=dQw4w9WgXcQ&ab_channel=RickAstleyVEVO"
            },
            new Recommendation
            {
                RecommendationId = 2,
                Date = DateTime.Now,
                Title = "test playlist 2",
                Link = "https://www.youtube.com/watch?v=dQw4w9WgXcQ&ab_channel=RickAstleyVEVO"
            },
            new Recommendation
            {
                RecommendationId = 3,
                Date = DateTime.Now,
                Title = "test playlist 3",
                Link = "https://www.youtube.com/watch?v=dQw4w9WgXcQ&ab_channel=RickAstleyVEVO"
            },
            new Recommendation
            {
                RecommendationId = 4,
                Date = DateTime.Now,
                Title = "test playlist 4",
                Link = "https://www.youtube.com/watch?v=dQw4w9WgXcQ&ab_channel=RickAstleyVEVO"
            });
        }
    }
}
