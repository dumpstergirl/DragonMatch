using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DragonMatch
{
    public partial class Form1 : Form
    {
        //creates random objecst for the squares
        Random random = new Random();

        //Each lett is an icon in the webdings font
        List<string> icons = new List<string>()
        {
            "!", "!","N", "N", ",", ",", "k", "k", 
            "b", "b", "v", "v", "w", "w", "z", "z"
        };

        // firstClicked points to the first Label control
        Label firstClicked = null;
        //secondClicked points to the second label clicked
        Label secondClicked = null;


        //assign each icon from the list of icons to a random square
        private void AssignIconsToSquares()
        {
            //an icon is pulled at random from the list and added to each label
            foreach(Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;
                if (iconLabel != null)
                {
                    int randomNumber = random.Next(icons.Count);
                    iconLabel.Text = icons[randomNumber];
                    iconLabel.ForeColor = iconLabel.BackColor;
                    //iconLabel.Visible = false;
                    icons.RemoveAt(randomNumber);
                }
            }
        }
        public Form1()
        {
            InitializeComponent();
            AssignIconsToSquares();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }


        //Every (card) label's Click event is handeled by this event handler
        private void label_Click(object sender, EventArgs e)
        {
            //timer for if two non-matching icons have been shown to player, so ignore any
            // clicks if the timer is running
            if (timer1.Enabled == true)
                return;
            

            Label clickedLabel = sender as Label;
            
            if (clickedLabel != null)
            {
                //if the clicked label is visible, that icon has already been revealed- ignore that click
                if (clickedLabel.ForeColor == Color.Black)
                    return;
              //  clickedLabel.Visible = true;
                //If firstClicked is null, this is the first icon in the pair that was clicked, so set firstClicked 
                // to the label that was clicked, change its color to black, and return
                if (firstClicked == null)
                {
                    firstClicked = clickedLabel;
                    firstClicked.ForeColor = Color.Black;
                    return;
                }

                secondClicked = clickedLabel;
                secondClicked.ForeColor = Color.Black;

                if(CheckForWinner())
                {
                       DialogResult dialogResult = MessageBox.Show("Do you want to reset?", "You Wom!  Have a Cookie!", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.No)
                    {
                        Close();
                    }
                    else if (dialogResult == DialogResult.Yes)
                    {
                        ResetGame();
                        return;
                    }

                }

                if (firstClicked.Text == secondClicked.Text)
                {
                    firstClicked = null;
                    secondClicked = null;
                    return;
                }


                timer1.Start();            
            }

        }

        //This timer is started when the player clicks two icons that don't match, so it counts three 
        // quarters of a second and then turns itself off and both icons
        private void timer1_Tick(object sender, EventArgs e)
        {
            //Stop the timer
            timer1.Stop();
            
            //Hide both icons
            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;

            //reset click refs
            firstClicked = null;
            secondClicked = null;
        }
        
        private bool CheckForWinner()
        {

            //Go through all labels in the TableLayoutPanel, checking each one to see if its icon is matched
            foreach(Control control in tableLayoutPanel1.Controls)
            {
                Label iconlabel = control as Label;
                if (iconlabel != null)
                {
                    if (iconlabel.ForeColor == iconlabel.BackColor)
                        return false;
                }
            }
            return true;
            //MessageBox.Show("You matched all the icons", "Have a cookie!");
         
        }

        private void ResetGame()
        {
            firstClicked = null;
            secondClicked = null;
            

            icons = new List<string>()
        {
            "!", "!","N", "N", ",", ",", "k", "k", 
            "b", "b", "v", "v", "w", "w", "z", "z"
        };
          AssignIconsToSquares();
            
        }
         
    }

   
}
