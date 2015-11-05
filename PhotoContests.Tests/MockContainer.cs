using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using PhotoContests.Data.Repositories;
using PhotoContests.Models;

namespace PhotoContests.Tests
{
    public class MockContainer
    {
        public Mock<IRepository<User>> FakeUserRepository { get; set; }

        public Mock<IRepository<Contest>> FakeContestRepository { get; set; }

        public ICollection<User> FakeUsers { get; set; }

        public ICollection<Contest> FakeContests { get; set; }

        public void PrepareMock()
        {
            this.SetupFakeUsers();
            this.SetupFakeContests();
        }

        private void SetupFakeContests()
        {
            var fakeContets = new List<Contest>()
                {
                    new Contest()
                        {
                            Id = 1,
                            Title = "Contest one",
                            Description = "Contests one description",
                            DateCreated = DateTime.Now.AddDays(-8),
                            IsClosed = false,
                            ContestOwnerId = "fakeuser1id",
                            VotingStrategy = VotingStrategy.Open,
                            ParticipationStrategy = ParticipationStrategy.Open,
                            RewardStrategy = RewardStrategy.SingleUser,
                            DeadlineStrategy = DeadlineStrategy.ByNumberOfParticipants,
                            MaxParticipants = 10
                        },
                    new Contest()
                        {
                            Id = 2,
                            Title = "Contest two",
                            Description = "Contests two description",
                            DateCreated = DateTime.Now.AddDays(-5),
                            IsClosed = false,
                            ContestOwnerId = "fakeuser2id",
                            VotingStrategy = VotingStrategy.Open,
                            ParticipationStrategy = ParticipationStrategy.Open,
                            RewardStrategy = RewardStrategy.SingleUser,
                            DeadlineStrategy = DeadlineStrategy.ByNumberOfParticipants,
                            MaxParticipants = 5
                        }
                };

            this.FakeContestRepository = new Mock<IRepository<Contest>>();

            this.FakeContestRepository
                .Setup(c => c.All())
                .Returns(fakeContets.AsQueryable());

            this.FakeContestRepository
                .Setup(c => c.Find(It.IsAny<int>()))
                .Returns((int id) => fakeContets[id]);
        }

        private void SetupFakeUsers()
        {
            var fakeUsers = new List<User>()
                {
                    new User()
                        {
                            Id = "fakeuser1id",
                            UserName = "FakeUserOne",
                            Email = "fakeuserone@example.com",
                            FirstName = "Fake User One",
                            LastName = "Fake One"
                        },
                    new User()
                        {
                            Id = "fakeuser2id",
                            UserName = "FakeUserTwo",
                            Email = "fakeusertwo@example.com",
                            FirstName = "Fake User Two",
                            LastName = "Fake Two"
                        }
                };

            this.FakeUserRepository = new Mock<IRepository<User>>();

            this.FakeUserRepository
                .Setup(r => r.All())
                .Returns(fakeUsers.AsQueryable());

            this.FakeUserRepository
                .Setup(r => r.Find(It.IsAny<int>()))
                .Returns((int id) => fakeUsers[id]);
        }
    }
}
