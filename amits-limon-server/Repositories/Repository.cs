using amits_limon_server.Data;
using amits_limon_server.Interfaces;
using amits_limon_server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace amits_limon_server.Repositories
{
    public class Repository : IGetSongs, IAddComments, IGetSpotlights, IAdminOperations, IGetNotes, IGetRecommendations
    {
        readonly private DataContext _context;
        public Repository(DataContext context)
        {
            _context = context;
        }

        public async Task Add(object obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Comment>> AddComment(Comment comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
            Song song = _context.Songs.Find(comment.SongId);
            return song.Comments;
        }

        public async Task Delete(object obj)
        {
            _context.Remove(obj);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Comment> GetComments()
        {
            return _context.Comments;
        }

        public IEnumerable<Note> GetNotes()
        {
            return _context.Notes;
        }

        public IEnumerable<Recommendation> GetRecommendations()
        {
            return _context.Recommendations;
        }

        public Song GetSong(int id)
        {
            return _context.Songs.Find(id);
        }

        public IEnumerable<Song> GetSongs()
        {
            return _context.Songs;
        }

        public IEnumerable<Spotlight> GetSpotlights()
        {
            return _context.Spotlights;
        }

        public async Task Update(object obj)
        {
            _context.Update(obj);
            await _context.SaveChangesAsync();
        }
    }
}
