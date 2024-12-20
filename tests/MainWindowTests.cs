using NUnit.Framework;
using System.Collections.Generic;

namespace ElfeledettVarosokWPF.Tests
{
	/// <summary>
	/// A MainWindow osztály egységtesztjei, amelyek a játék kritikus funkcióit ellenõrzik.
	/// </summary>
	[TestFixture]
	public class MainWindowTests
	{
		private MainWindow mainWindow;

		/// <summary>
		/// Minden teszt elõtt új játékot inicializálunk.
		/// </summary>
		[SetUp]
		public void SetUp()
		{
			mainWindow = new MainWindow();
		}

		/// <summary>
		/// Teszt, amely ellenõrzi, hogy a szobaváltás megfelelõen mûködik, és a szobaleírás frissül.
		/// </summary>
		[Test]
		public void TestNavigateRooms()
		{
			// Arrange: A játék inicializálása és célhelyszín beállítása
			mainWindow.InitializeGame();
			string targetRoom = "Fogadó";

			// Act: Szobaváltás végrehajtása
			mainWindow.NavigateRooms(targetRoom);

			// Assert: Ellenõrzés, hogy az aktuális szoba a célhelyszín, és a leírás tartalmazza a helyhez illõ szöveget
			Assert.AreEqual(targetRoom, mainWindow.CurrentRoom, "A szobaváltás nem történt meg megfelelõen.");
			Assert.IsTrue(mainWindow.RoomDescription.Text.Contains("Egy meleg, barátságos hely"), "A szobaleírás nem frissült megfelelõen.");
		}

		/// <summary>
		/// Teszt, amely ellenõrzi, hogy az ellenségek helyesen generálódnak a szobákban.
		/// </summary>
		[Test]
		public void TestEnemyGeneration()
		{
			// Arrange: A játék inicializálása
			mainWindow.InitializeGame();

			// Act: Ellenséggenerálás végrehajtása
			var enemies = mainWindow.EnemiesInRooms;

			// Assert: Két ellenség generálódott, és nem az aktuális szobában vannak
			Assert.AreEqual(2, enemies.Count, "Nem pontosan két ellenség generálódott.");
			foreach (var room in enemies.Keys)
			{
				Assert.AreNotEqual(mainWindow.CurrentRoom, room, "Ellenség nem lehet az aktuális szobában.");
				Assert.IsTrue(mainWindow.Rooms.ContainsKey(room), "Az ellenség nem egy létezõ szobában található.");
				Assert.IsTrue(mainWindow.Enemies.ContainsKey(enemies[room]), "Az ellenség típusa nem érvényes.");
			}
		}

		/// <summary>
		/// Teszt, amely ellenõrzi, hogy a harci logika helyesen mûködik (gyõzelem/vereség).
		/// </summary>
		[Test]
		public void TestStartFight()
		{
			// Arrange: Egy teszthelyzet elõkészítése
			mainWindow.InitializeGame();
			string testRoom = "Fogadó";
			mainWindow.EnemiesInRooms[testRoom] = "Sötét Lovag";
			mainWindow.CurrentRoom = testRoom;

			// Act: Harc indítása
			mainWindow.StartFight();
			bool isEnemyDefeated = !mainWindow.EnemiesInRooms.ContainsKey(testRoom);

			// Assert: Ha gyõzelem, az ellenség eltûnik, ha vereség, akkor a játékos a Fõtérre kerül
			Assert.IsTrue(isEnemyDefeated || mainWindow.CurrentRoom == "Fõtér", "A harci logika nem mûködik megfelelõen.");
		}
	}
}
