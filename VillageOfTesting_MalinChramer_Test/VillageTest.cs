using Moq;
using VillageOfTesting_MalinChramer;

namespace VillageOfTesting_MalinChramer_Test
{
    public class VillageTest
    {
        private void DayIterator(int iterations, Village village)
        {
            for (int i = 0; i < iterations; i++)
            {
                village.Day();
            }
        }

        [Fact]
        public void AddWorkerTest1_AddWorkers_CountWorkers()
        {
            // Arrange
            Village village= new Village();

            // Act
            village.AddWorker(1);
            village.AddWorker(2);
            village.AddWorker(3);

            // Assert
            Assert.Equal(3, village.Workers_list.Count());
        }

        [Fact]
        public void AddWorkerTest2_AddWorkerWithoutHouse_CountWorkers()
        {
            //Arrange
            Village village= new Village();

            //Act
            village.AddWorker(1);
            village.AddWorker(2);
            village.AddWorker(3);
            village.AddWorker(4);
            village.AddWorker(1);
            village.AddWorker(2);
            village.AddWorker(3);
            village.AddWorker(4);

            // Assert
            Assert.Equal(6, village.Workers_list.Count());
        }

        [Fact]
        public void AddWorkerTest3_CheckOccupationTasks_DayFunction()
        {
            //Arrange
            Village village= new Village();

            //Act
            village.AddWorker(1); //Lumberjack
            village.Day();

            //Assert
            Assert.Equal(1, village.Wood);
        }

        [Fact]
        public void DayTest1_NoWorkers_DaysGone()
        {
            //Arrange
            Village village = new Village();

            //Act
            village.Day();

            //Assert
            Assert.Equal(1, village.DaysGone);
        }

        [Fact]
        public void DayTest2_AddWorkers_EnoughFood()
        {
            //Arrange
            Village village = new Village();

            //Act
            village.AddWorker(1); //Lumberjack
            village.AddWorker(2); //Miner
            village.Day();

            //Assert
            Assert.Equal(8, village.Food);
            Assert.Equal(1, village.Wood);
            Assert.Equal(1, village.Metal);
            Assert.Equal(1, village.DaysGone);
        }

        [Fact]
        public void DayTest3_AddWorkers_NotEnoughFood()
        {
            //Arrange
            Village village = new Village();

            //Act
            village.AddWorker(2); //Miner
            village.AddWorker(2); //Miner
            village.AddWorker(2); //Miner
            village.AddWorker(2); //Miner
            village.AddWorker(2); //Miner
            village.AddWorker(2); //Miner

            DayIterator(50, village);

            //Assert
            Assert.Equal(0, village.Food);
            Assert.Equal(10, village.Metal);
            Assert.Empty(village.Workers_list); // Här kollar vi om alla är döda.
        }

        [Fact]
        public void AddProjectTest1_AddBuilding_CheckResources()
        {
            //Arrange
            Village village = new Village();
            
            //Act
            village.AddWorker(1); //Lumberjack
            village.AddWorker(4); //Builder
            village.AddWorker(3); //Farmer

            DayIterator(5, village);
            village.AddProject(1); //House, kostar 5wood, 3days to build
            DayIterator(3, village);

            //Assert
            Assert.Equal(3, village.Wood);
        }
        
        [Fact]
        public void AddProjectTest2_AddBuildningWithoutValidResourses_ShouldNotWork()
        {
            //Arrange
            Village village = new Village();
            /* När vi skapar en ny village har vi inga 
             * bygg-resurser (wood, metal eller byggare)
             * Alltså ska det inte gå att lägga till några 
             * byggnader i Projects_list.*/

            //Act
            village.AddProject(1);
            village.AddProject(2);
            village.AddProject(3);
            village.AddProject(4);
            village.AddProject(5);

            //Assert
            Assert.Empty(village.Projects_list);
            /*Här kollar vi om Projects_list är tom för att 
             * kolla så ingen byggnad har blivit tillagd */
        }

        [Fact]
        public void AddProjectTest3_AddWoodmill_MoreWood()
        {
            //Arrange
            Village village = new Village();

            //Act
            village.AddWorker(4); //Builder
            village.AddWorker(1); //Lumberjack
            village.AddWorker(2); //Miner
            village.AddWorker(3); //Farmer

            DayIterator(5, village); //We now have 5 wood
            village.AddProject(2); //Adds Woodmill, cost 5 wood -> wood = 0
            DayIterator(4, village); //Woodmill tar 5 dagar att bygga, så dagen innan har vi 4 wood.

            int expectedWoodBeforeWoodmill = 4;
            int actuallWoodBeforeWoodmill = village.Wood;

            village.Day();  //Här blir Woodmill klar och antal wood ökar med 2 per dag
                            //Femte dagen har vi 5 wood, sjätte dagen har vi 2 mer ved, alltså 7st. 

            int expectedWoodAfterWoodmill = 7;
            int actuallWoodAfterWoodmill = village.Wood;

            //Assert
            Assert.Equal(expectedWoodBeforeWoodmill, actuallWoodBeforeWoodmill);
            Assert.Equal(expectedWoodAfterWoodmill, actuallWoodAfterWoodmill);
        }

        [Fact]
        public void AddProjectTest4_AddQuarry_MoreMetal()
        {
            //Arrange
            Village village = new Village();

            //Act
            village.AddWorker(4); //Builder
            village.AddWorker(1); //Lumberjack
            village.AddWorker(2); //Miner
            village.AddWorker(3); //Farmer

            DayIterator(5, village); //Nu har vi 5 metall
            village.AddProject(3); //Adds Quarry, cost 5 metall -> metall = 0
            DayIterator(6, village); //Quarry tar 7 dagar att bygga, så dagen innan har vi 6 metall.

            int expectedMetalBeforeQuarry = 6;
            int actuallMetalBeforeQuarry = village.Metal;

            village.Day();  //Här blir Quarry klar och antal metall ökar med 2 per dag
                            //Sjunde dagen har vi 7 metall, åttonde dagen har vi 2 mer metall, alltså 9st. 

            int expectedMetalAfterQuarry = 9;
            int actuallMetallAfterQuarry = village.Metal;

            //Assert
            Assert.Equal(expectedMetalBeforeQuarry, actuallMetalBeforeQuarry);
            Assert.Equal(expectedMetalAfterQuarry, actuallMetallAfterQuarry);
        }

        [Fact]        
        public void AddProjectTest5_AddFarm_MoreFood()
        {
            //Arrange
            Village village = new Village(); //Food = 10

            //Act
            village.AddWorker(4); //Builder
            village.AddWorker(1); //Lumberjack
            village.AddWorker(2); //Miner
            village.AddWorker(3); //Farmer

            DayIterator(5, village); //Nu har vi 5 wood & 5 metall, Food = 15
            village.AddProject(4); //Adds Farm, cost 5 wood & 2 metall -> wood = 0, metall = 3
            DayIterator(4, village); //Farm tar 5 dagar att bygga, så dagen innan har vi Food = 19

            int expectedFoodBeforeFarm = 19;
            int actuallFoodBeforeFarm = village.Food;

            village.Day();  //Här blir Farm klar och antal food ökar med 15 per dag.
                            //Femte dagen har vi 19 food, sjätte dagen har vi 15 mer food, alltså 34
                            //Sen behöver vi ta bort 4 då alla ska äta så vi har 30 food. 

            int expectedFoodAfterFarm = 30;
            int actuallFoodAfterFarm = village.Food;

            //Assert
            Assert.Equal(expectedFoodBeforeFarm, actuallFoodBeforeFarm);
            Assert.Equal(expectedFoodAfterFarm, actuallFoodAfterFarm);
        }

        [Fact]
        public void AddProjectTest6_AddHouse_CompleteInRightAmountOfDays()
        {
            //Arrange
            Village village = new Village();

            //Act
            village.AddWorker(3); //Farmer
            village.AddWorker(1); //Lamberjack

            DayIterator(5, village); //Nu har vi 5 wood och kan betala för huset.

            village.AddProject(1);   //Lägger till Hus
            village.AddWorker(4);   //Builder
            village.AddWorker(4);   //Builder

            DayIterator(2, village); // tar 3 dagar att bygga ett hus
                                     // eftersom jag lagt till 2 builders 
                                     // tar det 1,5 dagar.

            //Assert
            Assert.Empty(village.Projects_list); // projektlistan ska vara tom när huset är klart.
        }

        [Fact]
        public void WorkerTest1_HungryWorker_DontWork()
        {
            //Arrange
            Village village = new Village(); //10 food

            //Act
            village.AddWorker(1); //Lamberjack
            DayIterator(11, village); //elfte dagen ska arbetaren vara hungrig
            village.Day(); //här bör alltså inte samlas mer wood. Wood = 10

            //Assert
            Assert.Equal(10, village.Wood);
        }

        [Fact]        
        public void WorkerTest2_WorkerHungryFor40days_IsGone()
        {
            //Arrange
            Village village = new Village();

            //Act
            village.AddWorker(1); //Lumberjack
            DayIterator(50, village); //varit hungrig i 40 dagar

            //Assert
            Assert.Empty(village.Workers_list);
            // när arbetaren varit hungrig i 40 dagar tas den bort
            // där med kan man testa om listan är tom
        }

        [Fact]        
        public void WorkerTest3_FeedWorkerIsAliveFalse_DontWork()
        {
            //Arrange
            Village village = new Village();

            //Act
            village.AddWorker(1); //Lumberjack
            DayIterator(50, village); //varit hungrig i 40 dagar
            village.FeedWorkers(); 

            //Assert
            Assert.Empty(village.Workers_list);
            //Samma som ovan test så tar arbetaren bort från Workers_listan när IsAlive sätts till false.
        }

        [Fact]
        public void BuryDeadTest1_RemovesDeadWorkers()
        {
            //Arrange
            Village village = new Village();

            //Act
            village.AddWorker(1); //Lumberjack
            village.AddWorker(2); //Miner
            village.AddWorker(4); //Builder
                                  
            DayIterator(50, village); //varit hungrig i mer än 40 dagar

            //Assert
            Assert.Empty(village.Workers_list);
            // när arbetaren varit hungrig i 40 dagar tas den bort
            // där med kan man testa om listan är tom
        }

        [Fact]
        public void TheBIGtest_DoAllFunctionsAndBuildCastle_AmountOfDaysToWin()
        {
            /*
            Kör alla funktioner, en kombination av AddWorker(), AddProject() och 
            Day() tills ett slott blir klart. Lista hur många dagar det tog till slut. 
            Testa om det tog det antal dagar som det borde ha.
            */

            //Arrange
            Village w = new Village();
            // -> Food 10, Wood 0, Metal 0

            //Act
            w.AddWorker(1); //Lumberjack
            w.AddWorker(2); //Miner
            w.AddWorker(3); //Farmer
            w.AddWorker(4); //Builder

            DayIterator(5, w);
            // -> Food 14, Wood 5, Metal 5

            w.AddProject(1); //House, 5 wood, 3 days
            // -> Food 14, Wood 0, Metal 5

            DayIterator(3, w);
            // House complete
            // -> Food 17, Wood 3, Metal 7

            w.AddProject(3); //Quarry, 3 wood, 5 metal, 7 days -> 3 METAL/DAY
            // -> Food 17, Wood 0, Metal 2
            
            DayIterator(7, w);
            // Quarry complete
            // -> Food 24, Wood 7, Metal 9
            // Från nu 3 METAL/day
            
            w.AddProject(2); //Woodmill, 5 wood, 1 metal, 5 days -> 3 WOOD/DAY
            // -> Food 24, Wood 2, Metal 8
            
            DayIterator(5, w);
            // Woodmill complete
            // -> Food 27, Wood 7, Metal 23
            // Från nu 3 WOOD/day
            
            w.AddProject(4); //Farm, 5 wood, 2 metal, 5 days -> 15 FOOD/DAY
            // -> Food 27, Wood 2, Metal 21
            
            DayIterator(5, w);
            // Farm complete
            // -> Food 32, Wood 17, Metal 36
            
            DayIterator(11, w);
            // -> Food many, Wood 50, Metal < 50
            
            w.AddProject(5); //Castle, 50 wood, 50 metal, 50 days
            DayIterator(50, w);
            // Castle complete

            //Assert
            //Testa att det tog det antal dagar som det borde ha.
            Assert.Equal(86, w.DaysGone);

        }

        // VG TESTER NEDAN:
        [Fact]
        public void AddRandomWorkerTest_MockWithSameWorkerEveryTest_CheckIfWorkerDidWhatHeShould()
        {
            //Arrange
            Village w = new Village();
            Mock<RandomClass> mock= new Mock<RandomClass>();
            /* 
            Mock<Här sätter du in klassen du vill mocka> 
            Detta skapar en ny instans av RandomClass så vi kan bestämma över den.
            Vi trycker sedan in värdet i vår village klass genom 
            */

            w.RandomCounter = mock.Object;
            /*
            w.randomCounter = mock.Object är den som "bestämmer" att AddRandomWorker 
            funktionen i village klassen ska ta det värde vi sätter nedanför.
            */

            mock
                .Setup(mock => mock.RandomInt())
                .Returns(1); // Lumberjack

            //Act
            w.AddRandomWorker(); 
            w.Day();
      
            //Assert
            Assert.Equal(1, w.Wood);
        }

        [Fact]
        public void TestLoadProgress_MockWoodFunktion_GetExpectedWoodValue()
        {
            //Arrange
            Village v = new Village();
            Mock<DBConnectionClass> mock = new Mock<DBConnectionClass>();

            v.DbConnection = mock.Object;

            mock
                .Setup(mock => mock.GetWood())
                .Returns(999);

            //Act
            v.LoadProgress();
            
            //Assert
            Assert.Equal(999, v.Wood);
        }
    }
}