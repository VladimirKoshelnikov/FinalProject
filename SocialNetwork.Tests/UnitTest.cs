using SocialNetwork.Class;

namespace SocialNetwork.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestUserClass()
        {
            Assert.Pass(User.GetCorrectString("Èìÿ", User.CheckStringData));
            Assert.Pass();
        }
    }
}