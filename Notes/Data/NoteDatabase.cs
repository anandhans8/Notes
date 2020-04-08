namespace Notes.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using SQLite;
    using Notes.Models;

    public class NoteDatabase
    {
        readonly SQLiteAsyncConnection database;

        public NoteDatabase(string dbPath)
        {
            this.database = new SQLiteAsyncConnection(dbPath);
            this.database.CreateTableAsync<Note>().Wait();
        }

        public async Task<List<Note>> GetNotesAsync()
        {
            return await this.database.Table<Note>().ToListAsync();
        }

        public async Task<Note> GetNoteAsync(int id)
        {
            return await this.database.Table<Note>()
                .Where(a => a.ID == id)
                .FirstOrDefaultAsync();
        }

        public async Task<int> SaveNoteAsync(Note note)
        {
            if (note.ID !=0)
            {
                return await this.database.UpdateAsync(note);
            }

            return await this.database.InsertAsync(note);
        }

        public async Task<int> DeleteNoteAsync(Note note)
        {
            return await this.database.DeleteAsync(note);
        }
    }
}
