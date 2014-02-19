﻿using System;
using System.Collections.Generic;
using System.Linq;
using GenericProject.Core.Data;
using GenericProject.Core.Model;
using Newtonsoft.Json;

namespace GenericProject.Database
{
    public class DataGenerator
    {
        private readonly IRepositoryFactory _repositoryFactory;

        private IEnumerable<User> _users;

        private IEnumerable<Role> _roles;

        private IEnumerable<Relation> _relations;

        private IEnumerable<Tag> _tags;

        private IEnumerable<Title> _titles;

        private IEnumerable<Peep> _peeps;

        public DataGenerator(IRepositoryFactory repositoryFactory) { _repositoryFactory = repositoryFactory; }

        public bool IsSeeded { get { return _repositoryFactory.GetRepository<User>().Count() > 0; } }

        public IEnumerable<User> LoadUsers()
        {
            {
                return _users ?? (_users = new[] {
                                                     new User {
                                                                  Name = "Pseudonym Incognito",
                                                                  Title = "Guy of Stuff",
                                                                  Roles = _roles.Where(x => x.Name.Equals("Administrator")).ToList(),
                                                                  EmailAddress = "admin@domain.com",
                                                                  UserName = "aadmin"
                                                              },
                                                     new User {
                                                                  Name = "Alfred User",
                                                                  Title = "Standard User",
                                                                  Roles = _roles.Where(x => x.Name.Equals("User")).ToList(),
                                                                  EmailAddress = "user@domain.com",
                                                                  UserName = "auser"
                                                              },
                                                     new User {
                                                                  Name = "Manager Guy",
                                                                  Title = "El Manager",
                                                                  Roles = _roles.Where(x => x.Name.Equals("Manager")).ToList(),
                                                                  EmailAddress = "manager@domain.com",
                                                                  UserName = "amanager"
                                                              },
                                                     new User {
                                                                  Name = "Tommy Lead",
                                                                  Title = "Co-Junior Assistant Team Lead",
                                                                  Roles = _roles.Where(x => x.Name.Equals("Team Lead")).ToList(),
                                                                  EmailAddress = "team-lead@domain.com",
                                                                  UserName = "ateamlead"
                                                              }
                                                 });
            }
        }

        public void GenerateUsers()
        {
            LoadRoles();
            var repo = _repositoryFactory.GetRepository<User>();
            LoadUsers().ForEach(repo.Add);
        }


        private IEnumerable<Role> LoadRoles()
        {
            return _roles ?? (_roles = new[] {
                                                 new Role { Name = "Admin" },
                                                 new Role { Name = "Manager" },
                                                 new Role { Name = "Team Lead" },
                                                 new Role { Name = "User" },
                                             });
        }

        public void GenerateRoles()
        {
            var repo = _repositoryFactory.GetRepository<Role>();
            LoadRoles().ForEach(repo.Add);
        }


        private IEnumerable<Relation> LoadRelations()
        {
            return _relations ?? (_relations = new[] {
                                                 new Relation { Name = "Friend" },
                                                 new Relation { Name = "Co-Worker" },
                                                 new Relation { Name = "Acquaintance" },
                                                 new Relation { Name = "Enemy" },
                                                 new Relation { Name = "Family" },
                                                 new Relation { Name = "Stranger" },
                                             });
        }

        public void GenerateTags()
        {
            var repo = _repositoryFactory.GetRepository<Tag>();
            LoadTags().ForEach(repo.Add);
        }

        private IEnumerable<Tag> LoadTags()
        {
            return _tags ?? (_tags = new[] {
                                                 new Tag { Name = "Lazy" },
                                                 new Tag { Name = "Sneezes Funny" },
                                                 new Tag { Name = "Eats Crayons" },
                                                 new Tag { Name = "Dead Eyes" },
                                                 new Tag { Name = "Sarcastic" },
                                                 new Tag { Name = "Way Too Happy" },
                                                 new Tag { Name = "Bi-Polar" },
                                                 new Tag { Name = "Diabetic" },
                                             });
        }


        public void GenerateTitles()
        {
            var repo = _repositoryFactory.GetRepository<Title>();
            LoadTitles().ForEach(repo.Add);
        }

        private IEnumerable<Title> LoadTitles()
        {
            return _titles ?? (_titles = new[] {
                                                 new Title { Name = "Mr." },
                                                 new Title { Name = "Mrs." },
                                                 new Title { Name = "MIss" },
                                                 new Title { Name = "Lord" },
                                                 new Title { Name = "Lady" },
                                                 new Title { Name = "Duke" },
                                                 new Title { Name = "Duchess" },
                                                 new Title { Name = "Spymaster" },
                                                 new Title { Name = "Supereme Overlord" },
                                             });
        }


        public void GenerateRelations()
        {
            var repo = _repositoryFactory.GetRepository<Relation>();
            LoadRelations().ForEach(repo.Add);
        }

        // TODO: get a better source than this, or at least relative pathing
        // 
        // the entire dataset is not in the json in intentionally, as this is
        // just working with different sources for different parts of the data
        public IEnumerable<Peep> LoadPeeps()
        {
            LoadRelations();
            LoadTags();

            if (_peeps != null) return _peeps;

            var json = System.IO.File.ReadAllText(@"C:\SRC\GenericProject\GenericProject.Database\fakePeeps.json");
            _peeps = JsonConvert.DeserializeObject<List<Peep>>(json);

            // just for fun, some randomness
            var rand = new Random();
            var bDayStart = new DateTime(1950, 1, 1);
            var bDayRange = (new DateTime(2000, 1, 1) - bDayStart).Days;
            _peeps.ForEach(x => { 
                x.HatsOwned = rand.Next(0, 117); 
                if (x.HatsOwned % 3 != 0) x.Birthday = bDayStart.AddDays(rand.Next(bDayRange)); 

                var randCount = rand.Next(0, _relations.Count() - 1);
                if (randCount > 0) x.Relations = _relations.SelectRandom(randCount).ToList();

                randCount = rand.Next(0, _tags.Count() - 1);
                if (randCount > 0) x.Tags = _tags.SelectRandom(randCount).ToList();

                randCount = rand.Next(0, _titles.Count() - 1);
                if (randCount > 0) x.Title = _titles.SelectRandom();
            });

            return _peeps;
        }

        public void GeneratePeeps()
        {
            var repo = _repositoryFactory.GetRepository<Peep>();
            LoadPeeps().ForEach(repo.Add);
        }
    }
}