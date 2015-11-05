using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.SignalR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PhotoContests.App.Controllers;
using PhotoContests.App.Models.BindingModels;
using PhotoContests.Data.UnitOfWork;
using PhotoContests.Models;

namespace PhotoContests.Tests.UnitTests
{
    [TestClass]
    public class ContestsControllerTests
    {
        private MockContainer mocks;

        [TestInitialize]
        public void InitTest()
        {
            this.mocks = new MockContainer();
            this.mocks.PrepareMock();
        }

        [TestMethod]
        public void AddContestShouldInsertOneRecordToTheRepository()
        {
            var contests = new List<Contest>();

            var fakeUser = this.mocks.FakeUserRepository.Object.All().FirstOrDefault();
            if (fakeUser == null)
            {
                Assert.Fail();
            }

            this.mocks.FakeContestRepository
                .Setup(r => r.Add(It.IsAny<Contest>()))
                .Callback(
                    (Contest contest) =>
                    {
                        contest.ContestOwner = fakeUser;
                        contests.Add(contest);
                    });

            var mockContext = new Mock<IPhotoContestsData>();
            mockContext.Setup(c => c.Users)
                       .Returns(this.mocks.FakeUserRepository.Object);
            mockContext.Setup(c => c.Contests)
                       .Returns(this.mocks.FakeContestRepository.Object);

            var mockIdProvider = new Mock<IUserIdProvider>();
            mockIdProvider.Setup(ip => ip.GetUserId())
                          .Returns(fakeUser.Id);

            var contestController = new ContestsController(mockContext.Object, mockIdProvider.Object);

            var newContest = new ContestBindingModel()
            {
                Title = "Contest three",
                Description = "Contests three description",
                VotingStrategy = VotingStrategy.Open,
                ParticipationStrategy = ParticipationStrategy.Open,
                RewardStrategy = RewardStrategy.SingleUser,
                DeadlineStrategy = DeadlineStrategy.ByNumberOfParticipants
            };

            var response = contestController.TempCreate(newContest);

            var expected = contests.FirstOrDefault(c => c.Title == newContest.Title);
            if (expected == null)
            {
                Assert.Fail();
            }

            mockContext.Verify(c => c.SaveChanges(), Times.Once);
            Assert.IsInstanceOfType(response, typeof(RedirectToRouteResult));
            Assert.AreEqual(1, contests.Count);
            Assert.AreEqual(newContest.Description, expected.Description);
        }

        [TestMethod]
        public void EditContestShouldUpdateTheRecordCorrectly()
        {
            var fakeContest = this.mocks.FakeContestRepository.Object.All().FirstOrDefault();
            if (fakeContest == null)
            {
                Assert.Fail();
            }

            var fakeUser = this.mocks.FakeUserRepository.Object.All().FirstOrDefault();
            if (fakeUser == null)
            {
                Assert.Fail();
            }

            var mockContext = new Mock<IPhotoContestsData>();
            mockContext.Setup(c => c.Users)
                       .Returns(this.mocks.FakeUserRepository.Object);
            mockContext.Setup(c => c.Contests)
                       .Returns(this.mocks.FakeContestRepository.Object);

            var mockIdProvider = new Mock<IUserIdProvider>();
            mockIdProvider.Setup(ip => ip.GetUserId())
                          .Returns(fakeUser.Id);

            var contestController = new ContestsController(mockContext.Object, mockIdProvider.Object);

            var editContest = new ContestBindingModel()
            {
                Title = "Edited contest title",
                Description = "Edited contest description",
                VotingStrategy = VotingStrategy.Open,
                ParticipationStrategy = ParticipationStrategy.Open,
                RewardStrategy = RewardStrategy.SingleUser,
                DeadlineStrategy = DeadlineStrategy.ByNumberOfParticipants
            };

            var response = contestController.EditContest(editContest, fakeContest.Id);

            var result = this.mocks.FakeContestRepository.Object.All().FirstOrDefault();
            if (result == null)
            {
                Assert.Fail();
            }

            mockContext.Verify(c => c.SaveChanges(), Times.Once);
            Assert.IsInstanceOfType(response, typeof(RedirectToRouteResult));
            Assert.AreEqual(editContest.Title, result.Title);
            Assert.AreEqual(editContest.Description, result.Description);
        }

        [TestMethod]
        public void DissmissContestShouldWorkCorrectly()
        {
            var fakeContest = this.mocks.FakeContestRepository.Object.All().FirstOrDefault();
            if (fakeContest == null)
            {
                Assert.Fail();
            }

            var fakeUser = this.mocks.FakeUserRepository.Object.All().FirstOrDefault();
            if (fakeUser == null)
            {
                Assert.Fail();
            }

            var mockContext = new Mock<IPhotoContestsData>();
            mockContext.Setup(c => c.Users)
                       .Returns(this.mocks.FakeUserRepository.Object);
            mockContext.Setup(c => c.Contests)
                       .Returns(this.mocks.FakeContestRepository.Object);

            var mockIdProvider = new Mock<IUserIdProvider>();
            mockIdProvider.Setup(ip => ip.GetUserId())
                          .Returns(fakeUser.Id);

            var contestController = new ContestsController(mockContext.Object, mockIdProvider.Object);

            var response = contestController.DismissContest(fakeContest.Id);

            mockContext.Verify(c => c.SaveChanges(), Times.Once);
            Assert.IsInstanceOfType(response, typeof(RedirectToRouteResult));
        }
    }
}
