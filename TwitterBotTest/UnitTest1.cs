using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StatsTwitterBot.Classes;
using StatsTwitterBot.Objects;

namespace TwitterBotTest
{

    [TestClass]
    public class UnitTest1
    {
        private DataAccessor _dataAccess = new DataAccessor();

        //dbacccessmethods
        #region
        [TestMethod]
        public void TestGetPlayerIDByName()
        {
            int expectedPlayerId = 4;
            string firstName = "Thurman";
            string lastName = "Thomas";

            var result = _dataAccess.GetIdByName(firstName + lastName).FirstOrDefault();

            Assert.AreEqual(expectedPlayerId, result);
        }

        [TestMethod]
        public void TestGetPlayerIdByName_NotFound()
        {
            int expectedPlayerId = 0;
            string firstName = "Bug Rar Who";
            string lastName = "Thomas";

            var result = _dataAccess.GetIdByName(firstName + lastName).FirstOrDefault();

            Assert.AreEqual(expectedPlayerId, result);
        }

        [TestMethod]
        public void TestGetPlayerIdByNumberAndPosition()
        {
            int expectedPlayerId = 8439;
            string number = "12";
            string team = "GB";

            var result = _dataAccess.GetIdByNumberAndTeam(team, number);

            Assert.AreEqual(expectedPlayerId, result);
        }

        [TestMethod]
        public void TestGetPlayerIdByNumberAndPosition_NotFound()
        {
            int expectedPlayerId = 0;
            string number = "99";
            string team = "xxx";

            var result = _dataAccess.GetIdByNumberAndTeam(team, number);

            Assert.AreEqual(expectedPlayerId, result);
        }
        #endregion 

        //Twitter objects
        #region
        //private StatSetFactory _statSetFactory = new StatSetFactory();

        //[TestMethod]
        //public void TestGetRushingStats()
        //{
        //    var rushingStatSet = _statSetFactory.GetStatSet("Rushing");
        //    string value = rushingStatSet.GetStatString()

        //    Assert.
        //}


        #endregion
    }
}
