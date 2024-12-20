using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Timers;

namespace ElfeledettVarosokWPF
{
    public partial class MainWindow : Window
    {
        private string playerName;
        private Dictionary<string, string> rooms;
        private string currentRoom;
        private List<string> inventory = new List<string>();
        private Dictionary<string, (int, int)> enemies = new Dictionary<string, (int, int)>
        {
            { "Sötét Lovag", (30, 10) },
            { "Vérszomjas Bestia", (50, 15) }
        };
        private Dictionary<string, string> enemiesInRooms = new Dictionary<string, string>();  // Store enemies in rooms
        private string currentEnemy;
        private Timer fightTimer;
        private bool isFighting = false;

        public MainWindow()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            // Játékos neve
            playerName = Microsoft.VisualBasic.Interaction.InputBox("Add meg a neved:", "Játékos név");
            PlayerNameDisplay.Text = $"Üdvözöllek, {playerName}!";

            // Szobák inicializálása
            rooms = new Dictionary<string, string>
            {
                { "Főtér", "Egy hatalmas tér közepén állsz, körülötted kereskedők és épületek." },
                { "Fogadó", "Egy meleg, barátságos hely, ahol pihenhetsz." },
                { "Fegyvertár", "Fegyverek csillogó sora fogad." },
                { "Taverna", "A taverna zajos és tele van élettel." },
                { "Erőd", "Egy sötét és fenyegető erőd tornyosul előtted." },
                { "Széchenyi István Egyetem", "Egy modern és rejtélyes hely, ahova csak kivételes módon juthatsz el." }
            };

            currentRoom = "Főtér";
            UpdateRoom();
            GenerateEnemies();
        }

        private void GenerateEnemies()
        {
            Random rand = new Random();
            List<string> roomList = rooms.Keys.ToList();
            roomList.Remove(currentRoom);  // Ne helyezze az ellenséget az aktuális szobába

            // Véletlenszerűen választunk két szobát
            for (int i = 0; i < 2; i++)
            {
                string room = roomList[rand.Next(roomList.Count)];
                string enemy = enemies.Keys.ToList()[rand.Next(enemies.Count)];
                enemiesInRooms[room] = enemy;
                roomList.Remove(room); // Ne választható újra ugyanaz a szoba
            }
        }

        private void UpdateRoom()
        {
            if (rooms.ContainsKey(currentRoom))
            {
                RoomDescription.Text = rooms[currentRoom];
                DialogueOptions.Items.Clear();
                DialogueOptions.Items.Add("Helyszínek");

                // Hozzáadjuk az összes elérhető helyszínt
                foreach (var room in rooms.Keys)
                {
                    DialogueOptions.Items.Add(room);
                }

                DialogueOptions.Items.Add("Inventory megtekintése");
                DialogueOptions.Items.Add("Kilépés");

                // Ha ellenség van a szobában, felajánlja a harcot
                if (enemiesInRooms.ContainsKey(currentRoom))
                {
                    DialogueOptions.Items.Add("Harcolj!");
                }
            }
        }

        private void DialogueOptions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DialogueOptions.SelectedItem != null)
            {
                string selectedOption = DialogueOptions.SelectedItem.ToString();
                if (selectedOption == "Helyszínek")
                {
                    NavigateRooms();
                }
                else if (selectedOption == "Inventory megtekintése")
                {
                    OpenInventory();
                }
                else if (selectedOption == "Kilépés")
                {
                    ExitGame();
                }
                else if (selectedOption == "Harcolj!" && !isFighting)
                {
                    StartFight();
                }
                else if (rooms.ContainsKey(selectedOption))
                {
                    currentRoom = selectedOption;
                    UpdateRoom();
                }
            }
        }

        private void NavigateRooms()
        {
            string newRoom = Microsoft.VisualBasic.Interaction.InputBox("Hova szeretnél menni?", "Szobaváltás");
            if (rooms.ContainsKey(newRoom))
            {
                currentRoom = newRoom;
                UpdateRoom();
            }
            else
            {
                MessageBox.Show("Ez a helyszín nem érhető el innen.", "Hiba");
            }
        }

        private void StartFight()
        {
            if (isFighting) return;

            isFighting = true;
            currentEnemy = enemiesInRooms[currentRoom];

            // Véletlenszerű esély, hogy nyerjünk (60%)
            Random rand = new Random();
            int winChance = rand.Next(0, 101); // 0-100 között
            if (winChance <= 60)
            {
                MessageBox.Show($"Győztél a {currentEnemy}-val/vel szemben!", "Győzelem!");
                enemiesInRooms.Remove(currentRoom); // Elveszítette az ellenséget
            }
            else
            {
                MessageBox.Show($"Vesztettél a {currentEnemy}-val/vel szemben! Visszatérsz a Főtérre.", "Vereség!");
                currentRoom = "Főtér"; // Visszatérés a főtérre
            }
            isFighting = false;
            UpdateRoom();
        }

        private void OpenInventory()
        {
            string inventoryItems = inventory.Count > 0 ? string.Join(", ", inventory) : "Az inventory üres.";
            MessageBox.Show($"Inventory: {inventoryItems}", "Inventory");
        }

        private void ExitGame()
        {
            Application.Current.Shutdown();
        }

        private void OpenInventory_Click(object sender, RoutedEventArgs e)
        {
            OpenInventory();
        }

        private void ExitGame_Click(object sender, RoutedEventArgs e)
        {
            ExitGame();
        }
    }
}
