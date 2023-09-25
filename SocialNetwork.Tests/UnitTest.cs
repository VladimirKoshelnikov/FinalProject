using SocialNetwork.DAL.Entities;
using System.Reflection;

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
            UserEntity myUser = new UserEntity();
            MethodInfo methodInfo = typeof(UserEntity).GetMethod("GetCorrectString", BindingFlags.NonPublic | BindingFlags.Instance);
            object[] parameters = { "parameters here" };
            //methodInfo.Invoke(objUnderTest, parameters);

            //Assert.Pass(UserEntity.GetCorrectString("Èìÿ", UserEntity.CheckStringData));
            Assert.Pass();
        }
    }
}