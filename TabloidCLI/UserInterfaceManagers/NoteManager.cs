using System;
using System.Collections.Generic;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    public class NoteManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private NoteRepository _noteRepository;
        private string _connectionString;

        public NoteManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _noteRepository = new NoteRepository(connectionString);
            _connectionString = connectionString;
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Note Management Menu");
            Console.WriteLine(" 1) List Notes");
            Console.WriteLine(" 2) Add Note");
            Console.WriteLine(" 3) Remove Note");
            Console.WriteLine(" 0) Return");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.WriteLine("Notes List");
                    Console.WriteLine("-----------");
                    List();
                    Console.WriteLine();
                    return this;
                case "2":
                    Add();
                    return this;
                case "3":
                    Remove();
                    return this;
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }

        private void List()
        {
            List<Note> notes = _noteRepository.GetAll();
            Console.WriteLine("List of Note Titles and Text Content:");
            foreach (Note note in notes)
            {
                Console.WriteLine($"{note.Id}, { note.Title}, { note.Content}");
            }
            Note selectedNote = Select();
            if (selectedNote != null)

            {
                new PostDetailManager(this, _connectionString, selectedNote.Id).Execute();
            }
        }

        
        private void Add()
        {
            Console.WriteLine("Add Note");
            Note note = new Note();

            Console.Write("Title: ");
            note.Title = Console.ReadLine();

            Console.Write("Text content: ");
            note.Content = Console.ReadLine();

            Console.Write("Created: ");
            note.CreateDateTime = DateTime.Now;

            

            _noteRepository.Insert(note);

            Console.WriteLine($"A related Post: {note.Title} is added to the notes list.");
            note.PostId = Console.ReadLine();
        }

        private void Remove()
        {
            Note noteToDelete = Choose("Which note would you like to remove?");
            if (noteToDelete != null)
            {
                _noteRepository.Delete(noteToDelete.Id);
            }
        }
    }
}
