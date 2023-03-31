using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _1_utkast_SKALA_GENERATOR
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool[] positions = new bool[12];

        bool hasLoaded = false;

        Tuning[] tunings = new Tuning[]
        {
            
            new Tuning()
            {
                Name = "E Standard",
                Notes = new MusicNotes[] { MusicNotes.E, MusicNotes.A, MusicNotes.D, MusicNotes.G, MusicNotes.B, MusicNotes.E }
            },
            new Tuning()
            {
                Name = "Drop D",
                Notes = new MusicNotes[] { MusicNotes.D, MusicNotes.A, MusicNotes.D, MusicNotes.G, MusicNotes.B, MusicNotes.E, }
            },
            new Tuning()
            {
                Name = "D Standard",
                Notes = new MusicNotes[] { MusicNotes.D, MusicNotes.G,MusicNotes.C,MusicNotes.F,MusicNotes.A,MusicNotes.D, }
            },

        };

        

        Dictionary<int, Dictionary<int, MusicNotes?>> strings = new Dictionary<int, Dictionary<int, MusicNotes?>>()
        {
                {0, new Dictionary<int,MusicNotes?>
                    {
                        {0, null },{1, null}, {2, null},
                        {3, null}, {4, null}, {5, null},
                        {6, null}, {7, null}, {8, null},
                        {9, null}, {10, null}, {11, null},
                        {12, null}, {13, null}, {14, null},
                        {15, null}, {16, null}, {17, null},
                        {18, null}, {19, null}, {20, null},
                        {21, null}, {22, null},
                    } 
                },
                {1, new Dictionary<int,MusicNotes?>
                    {
                        {0, null },{1, null}, {2, null},
                        {3, null}, {4, null}, {5, null},
                        {6, null}, {7, null}, {8, null},
                        {9, null}, {10, null}, {11, null},
                        {12, null}, {13, null}, {14, null},
                        {15, null}, {16, null}, {17, null},
                        {18, null}, {19, null}, {20, null},
                        {21, null}, {22, null},
                    }
                },
                {2, new Dictionary<int,MusicNotes?>
                    {
                        {0, null },{1, null}, {2, null},
                        {3, null}, {4, null}, {5, null},
                        {6, null}, {7, null}, {8, null},
                        {9, null}, {10, null}, {11, null},
                        {12, null}, {13, null}, {14, null},
                        {15, null}, {16, null}, {17, null},
                        {18, null}, {19, null}, {20, null},
                        {21, null}, {22, null},
                    }
                },
                {3, new Dictionary<int,MusicNotes?>
                    {
                        {0, null },{1, null}, {2, null},
                        {3, null}, {4, null}, {5, null},
                        {6, null}, {7, null}, {8, null},
                        {9, null}, {10, null}, {11, null},
                        {12, null}, {13, null}, {14, null},
                        {15, null}, {16, null}, {17, null},
                        {18, null}, {19, null}, {20, null},
                        {21, null}, {22, null},
                    }
                },
                {4, new Dictionary<int,MusicNotes?>
                    {
                        {0, null },{1, null}, {2, null},
                        {3, null}, {4, null}, {5, null},
                        {6, null}, {7, null}, {8, null},
                        {9, null}, {10, null}, {11, null},
                        {12, null}, {13, null}, {14, null},
                        {15, null}, {16, null}, {17, null},
                        {18, null}, {19, null}, {20, null},
                        {21, null}, {22, null},
                    }
                },
                {5, new Dictionary<int,MusicNotes?>
                    {
                        {0, null },{1, null}, {2, null},
                        {3, null}, {4, null}, {5, null},
                        {6, null}, {7, null}, {8, null},
                        {9, null}, {10, null}, {11, null},
                        {12, null}, {13, null}, {14, null},
                        {15, null}, {16, null}, {17, null},
                        {18, null}, {19, null}, {20, null},
                        {21, null}, {22, null},
                    }
                },
        };

        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            foreach( var child in toggleButtonStackPanel.Children)
            {
                ((ToggleButton)child).IsChecked = false;
                
            }
            Array.Fill(positions, false);
            generateScalePattern();
        }

        private void RandomizePositions_Click(object sender, RoutedEventArgs e)
        {
            Random random = new Random();
            for(int i = 0; i < positions.Length; i++)
            {
                positions[i] = random.Next(2) == 1 ? true : false;
            }

            for(int i = 0; i < positions.Length; i++)
            {
                ((ToggleButton)toggleButtonStackPanel.Children[i]).IsChecked = positions[i];
            }

            generateScalePattern();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Add Tunings To ComboBox
            foreach( var item in tunings)
            {
                tuningsComboBox.Items.Add(item);
            }
            //Add Notes To ComboBox
            foreach(var note in Enum.GetValues(typeof(MusicNotes)))
            {
                keyNoteComboBox.Items.Add(note);
            }
            hasLoaded = true;

            //GetControl();

        }

        private void positionToggleButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (ToggleButton)sender;
            var position = toggleButtonStackPanel.Children.IndexOf(button);
            positions[position] = !positions[position];

            generateScalePattern();
        }

        private void tuningDropdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            setNotesOnStrings();
            setNotesOnButtons();

            if(hasLoaded)
            {
                generateScalePattern();
            }

        }
        private void setNotesOnStrings()
        {
            var tuningString = 0;
            foreach (var child in strings)
            {

                var gString = child.Value;


            var tuning = ((Tuning)tuningsComboBox.SelectedItem).Notes;

            var notes = (MusicNotes[])Enum.GetValues(typeof(MusicNotes));

            var notesPosition = 0;

            for (int i = 0; i < gString.Count; i++)
            {
                if (i == 0)
                {
                    gString[0] = tuning[tuningString];
                    notesPosition = Array.IndexOf(notes, tuning[tuningString]);
                }
                else
                {
                    gString[i] = notes[notesPosition];
                }
                notesPosition++;
                if (notesPosition >= 12)
                {
                    notesPosition = 0;
                }
            }

                tuningString++;
            }


        }

        private void setNotesOnButtons()
        {
            var canvasStrings = stringCanvas.Children; //WHERE IM INPUTTING THE CONTENT/GONNA MARK THE SCALE NOTES
            
            var canvasNumber = 5; //INDEXING STRINGS

            foreach(var canvas in canvasStrings)
            {
                var currentStringNotes = strings[canvasNumber];
                //
                for (int i = 0; i < ((Canvas)canvas).Children.Count; i++)
                {
                    var button = (Button)((Canvas)canvas).Children[i];

                    var content = Enum.GetName(typeof(MusicNotes), currentStringNotes[i].Value);
                    
                    if(content.Length > 1)
                    {
                        content = content[0] + "#";
                    }

                    button.Content = content;
                    
                }
                canvasNumber--;
            }
        }

        private void setScaleOntoNotes_Click(object sender, RoutedEventArgs e)
        {
            generateScalePattern();
        }

        private void generateScalePattern()
        {
            var key = (MusicNotes)keyNoteComboBox.SelectedItem;
            var notes = (MusicNotes[])Enum.GetValues(typeof(MusicNotes));

            var noteIndex = Array.IndexOf(notes, key);

            var notesOnScale = new MusicNotes?[12];

            Array.Fill(notesOnScale, null); //GET NOTES FOR SCALE

            for (int i = 0; i < positions.Length; i++)
            {
                if (noteIndex >= 12)
                {
                    noteIndex = 0;
                }

                if (positions[i])
                {
                    notesOnScale[i] = notes[noteIndex];
                }
                noteIndex++;

            }

            var canvasStrings = stringCanvas.Children;
            var canvasIndex = 5;

            foreach (var canvas in canvasStrings)
            {
                var currentStringNotes = strings[canvasIndex];
                
                for (int i = 0; i < ((Canvas)canvas).Children.Count; i++)
                {
                    var button = (Button)((Canvas)canvas).Children[i];

                    if (notesOnScale.Any(x => x == currentStringNotes[i].Value))
                    {
                        button.Background = Brushes.Red;
                        
                    }
                    else
                    {
                        button.Background = Brushes.DarkGray;
                        button.Foreground = Brushes.Black;
                    }

                }
                canvasIndex--;
            }
        }

        private void keyNoteComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            generateScalePattern();
        }
    }
}
