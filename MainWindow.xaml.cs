using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ElfeledettVarosokWPF
{
    public partial class MainWindow : Window
    {
        private string playerName;
        private Dictionary<string, string> rooms;
        private string currentRoom;
        private List<string> inventory = new List<string>();
        private Dictionary<string, (int HP, int Damage)> enemies = new Dictionary<string, (int, int)>()
        {
            { "Sötét Lovag", (HP: 30, Damage: 10) },
            { "Vérszomjas Bestia", (HP: 50, Damage: 15) }
        };
        private int playerHP = 100;

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
        }

        private void UpdateRoom()
        {
            if (rooms.ContainsKey(currentRoom))
            {
                RoomDescription.Text = rooms[currentRoom];
                DialogueOptions.Items.Clear();
                DialogueOptions.Items.Add("Helyszínek");
                DialogueOptions.Items.Add("Inventory megtekintése");
                DialogueOptions.Items.Add("Kilépés");
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
            }
        }

        private void NavigateRooms()
        {
            string newRoom = Microsoft.VisualBasic.Interaction.InputBox("Hova szeretnél menni? (pl. Fogadó, Taverna, stb.)", "Szobaváltás");
            if (rooms.ContainsKey(newRoom))
            {
                currentRoom = newRoom;
                UpdateRoom();
            }
            else if (newRoom == "Széchenyi István Egyetem" && currentRoom == "Taverna")
            {
                currentRoom = "Széchenyi István Egyetem";
                UpdateRoom();
            }
            else
            {
                MessageBox.Show("Ez a helyszín nem érhető el innen.", "Hiba");
            }
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
