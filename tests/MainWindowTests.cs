using NUnit.Framework;
using System.Collections.Generic;

namespace ElfeledettVarosokWPF.Tests
{
	/// <summary>
	/// A MainWindow oszt�ly egys�gtesztjei, amelyek a j�t�k kritikus funkci�it ellen�rzik.
	/// </summary>
	[TestFixture]
	public class MainWindowTests
	{
		private MainWindow mainWindow;

		/// <summary>
		/// Minden teszt el�tt �j j�t�kot inicializ�lunk.
		/// </summary>
		[SetUp]
		public void SetUp()
		{
			mainWindow = new MainWindow();
		}

		/// <summary>
		/// Teszt, amely ellen�rzi, hogy a szobav�lt�s megfelel�en m�k�dik, �s a szobale�r�s friss�l.
		/// </summary>
		[Test]
		public void TestNavigateRooms()
		{
			// Arrange: A j�t�k inicializ�l�sa �s c�lhelysz�n be�ll�t�sa
			mainWindow.InitializeGame();
			string targetRoom = "Fogad�";

			// Act: Szobav�lt�s v�grehajt�sa
			mainWindow.NavigateRooms(targetRoom);

			// Assert: Ellen�rz�s, hogy az aktu�lis szoba a c�lhelysz�n, �s a le�r�s tartalmazza a helyhez ill� sz�veget
			Assert.AreEqual(targetRoom, mainWindow.CurrentRoom, "A szobav�lt�s nem t�rt�nt meg megfelel�en.");
			Assert.IsTrue(mainWindow.RoomDescription.Text.Contains("Egy meleg, bar�ts�gos hely"), "A szobale�r�s nem friss�lt megfelel�en.");
		}

		/// <summary>
		/// Teszt, amely ellen�rzi, hogy az ellens�gek helyesen gener�l�dnak a szob�kban.
		/// </summary>
		[Test]
		public void TestEnemyGeneration()
		{
			// Arrange: A j�t�k inicializ�l�sa
			mainWindow.InitializeGame();

			// Act: Ellens�ggener�l�s v�grehajt�sa
			var enemies = mainWindow.EnemiesInRooms;

			// Assert: K�t ellens�g gener�l�dott, �s nem az aktu�lis szob�ban vannak
			Assert.AreEqual(2, enemies.Count, "Nem pontosan k�t ellens�g gener�l�dott.");
			foreach (var room in enemies.Keys)
			{
				Assert.AreNotEqual(mainWindow.CurrentRoom, room, "Ellens�g nem lehet az aktu�lis szob�ban.");
				Assert.IsTrue(mainWindow.Rooms.ContainsKey(room), "Az ellens�g nem egy l�tez� szob�ban tal�lhat�.");
				Assert.IsTrue(mainWindow.Enemies.ContainsKey(enemies[room]), "Az ellens�g t�pusa nem �rv�nyes.");
			}
		}

		/// <summary>
		/// Teszt, amely ellen�rzi, hogy a harci logika helyesen m�k�dik (gy�zelem/veres�g).
		/// </summary>
		[Test]
		public void TestStartFight()
		{
			// Arrange: Egy teszthelyzet el�k�sz�t�se
			mainWindow.InitializeGame();
			string testRoom = "Fogad�";
			mainWindow.EnemiesInRooms[testRoom] = "S�t�t Lovag";
			mainWindow.CurrentRoom = testRoom;

			// Act: Harc ind�t�sa
			mainWindow.StartFight();
			bool isEnemyDefeated = !mainWindow.EnemiesInRooms.ContainsKey(testRoom);

			// Assert: Ha gy�zelem, az ellens�g elt�nik, ha veres�g, akkor a j�t�kos a F�t�rre ker�l
			Assert.IsTrue(isEnemyDefeated || mainWindow.CurrentRoom == "F�t�r", "A harci logika nem m�k�dik megfelel�en.");
		}
	}
}
