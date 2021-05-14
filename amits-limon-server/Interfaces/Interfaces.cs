using amits_limon_server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace amits_limon_server.Interfaces
{
    public interface IGetSongs
    {
        IEnumerable<Song> GetSongs();
        Song GetSong(int id);
    }
    public interface IGetSpotlights
    {
        IEnumerable<Spotlight> GetSpotlights();

    }
    public interface IAddComments
    {
        Task<IEnumerable<Comment>> AddComment(Comment comment);
        IEnumerable<Comment> GetComments();
    }
    public interface IAdminOperations
    {
        Task Add(object obj);
        Task Delete(object obj);
        Task Update(object obj);
    }
    public interface IGetNotes
    {
        IEnumerable<Note> GetNotes();
    }
    public interface IGetRecommendations
    {
        IEnumerable<Recommendation> GetRecommendations();
    }

}
