using System;
using System.Collections.Generic;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    internal class NoteManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private PostRepository _postRepository;
        private NoteRepository _noteRepository;
        private string _connectionString;
        private int _postId;
     

        public NoteManager(IUserInterfaceManager parentUI, string connectionString, int postId)
        {
            _parentUI = parentUI;
            _postRepository = new PostRepository(connectionString);
            _noteRepository = new NoteRepository(connectionString);
            _connectionString = connectionString;
            _postId = postId;
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
                Console.WriteLine($"{note.Id}, {note.Title}, {note.Content}");
            }
            Console.ReadLine();
           
        }
        private Note Choose(string prompt = null)
        {
            if (prompt == null)
            {
                prompt = "Please choose a Note:";
            }

            Console.WriteLine(prompt);

            List<Note> notes = _noteRepository.GetAll();
            for (int i = 0; i < notes.Count; i++)
            {
                Note note = notes[i];
                Console.WriteLine($"{i + 1}) {note.Title}");
            }
            Console.Write(">");
            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                return notes[choice - 1];
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Selection");
                return null;
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
            note.Post.Id = _postId;
        }

        private void Remove()
        {
            Note noteToDelete = Choose("Which note would you like to remove?");
            if (noteToDelete != null)
            {
                return;
            }
            Console.WriteLine();

                _noteRepository.Delete(noteToDelete.Id);
            }
        }
    }

