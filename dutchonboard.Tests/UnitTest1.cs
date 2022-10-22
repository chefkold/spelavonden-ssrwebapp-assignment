
namespace dutchonboard.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void GetPlayerCountDisplay_With4PlayersAddedOnA12Maximum_ShouldGive4of12AsDisplay()
        {
            var gameNight = new GameNight() { MaxPlayerAmount = 12 };
            var playersList = new[] { new Player(), new Player(), new Player(), new Player(), };
            gameNight.Players = playersList;
            const string expectedFormat = "4 / 12";

            var formatResult = gameNight.GetPlayerCountDisplay();

            Assert.Equal(expectedFormat, formatResult);
        }
    }
}