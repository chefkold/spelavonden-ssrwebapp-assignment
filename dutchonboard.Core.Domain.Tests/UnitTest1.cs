namespace dutchonboard.Core.Domain.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var game = new BoardGame() { Name = "True" }; 

            var nameIsTrue = game.Name == "True";

            Assert.True(nameIsTrue);

        }

        [Fact]
        public void Test3()
        {
            var game = new BoardGame() { Name = "False" }; 

            var nameIsTrue = game.Name == "True";

            Assert.True(nameIsTrue);

        }
    }
}