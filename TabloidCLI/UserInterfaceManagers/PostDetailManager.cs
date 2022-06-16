using System;
using System.Collections.Generic;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    internal class PostDetailManager : IUserInterfaceManager
    {
        private IUserInterfaceManager _parentUI;
        private AuthorRepository _authorRepository;
        private PostRepository _postRepository;
        private TagRepository _tagRepository;
        private string _connectionString;
        private NoteRepository _noteRepository;
        private int _postId;

        public PostDetailManager(IUserInterfaceManager parentUI, string connectionString, int postId)
        {
            _parentUI = parentUI;
            _authorRepository = new AuthorRepository(connectionString);
            _postRepository = new PostRepository(connectionString);
            _tagRepository = new TagRepository(connectionString);
            _connectionString = connectionString;
            _postId = postId;
            _noteRepository = new NoteRepository(connectionString);

        }

        public IUserInterfaceManager Execute()
        {
            Post post = _postRepository.Get(_postId);
            Console.WriteLine($"Details on the post - {post.Title}:");
            Console.WriteLine();
            Console.WriteLine(" 1) View");
            Console.WriteLine(" 2) Add Tag");
            Console.WriteLine(" 3) Remove Tag");
            Console.WriteLine(" 4) Note Management");
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    View();
                    return this;
                // following cases 3 and 4 are added as a placeholder
                // it will be addressed in a separate ticket that is already 
                // created for it.
                case "2":
                    //AddTag();
                    return this;
                case "3":
                    //RemoveTag();
                    return this;
                case "4":
                    NoteManager();
                    return this;
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }

        private void View()
        {
            Console.WriteLine("Post Details:");
            Post post = _postRepository.Get(_postId);
            Console.WriteLine($"Title: {post.Title}");
            Console.WriteLine($"Url: {post.Url}");
            Console.WriteLine($"Publish Date and Time: {post.PublishDateTime}");
            Console.WriteLine();
        }

        private void NoteManager()
        {
            new NoteManager(this, _connectionString, _postId).Execute();
        }

        private void AddTag()
        {
            Post post = _postRepository.Get(_postId);
            Console.WriteLine($"Which tag would you like to add to {post.Title}?");
            List<Tag> tags = _tagRepository.GetAll();

            for (int i = 0; i < tags.Count; i++)
            {
                Tag tag = tags[i];
                Console.WriteLine($" {i + 1}) {tag.Name}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                Tag tag = tags[choice - 1];

                // avoid adding duplicate entries to the PostTag table
                Post postWithSameTag = _postRepository.GetPostByTagId(tag.Id);
                if (postWithSameTag == null)
                {
                    _postRepository.InsertTag(post, tag);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Selection. Won't add any tags.");
            }
        }
    }
}
