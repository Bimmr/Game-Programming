/*
	Form.cs
	Assignment 1
	Revision History:
		Randy Bimm - 9/14/16 - Created
		Randy Bimm - 9/16/16 - Revisions
*/

using System;
using System.Linq;

namespace RBimmAssignment1
{
    public partial class Form : System.Windows.Forms.Form
    {
        private string[] _bookedArray = new string[15];
        private string[] _waitingArray = new string[10];

        private int _debugMode;


        public Form()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Add someone to the booked array
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBook_Click(object sender, EventArgs e)
        {
            if (tbxName.Text.Trim().Length > 0)
            {
                int selectedSeat = getSeatSelected();

                if (selectedSeat != -1 && _bookedArray[selectedSeat] == null)
                {
                    _bookedArray[selectedSeat] = tbxName.Text;
                    updateBookedList();
                    tbxStatus.Text = "You have been added to the booked list";
                }
                else if (getSeatSelected() == -1)
                    tbxStatus.Text = "No valid seat was selected";
                else{
                    tbxStatus.Text = "The selected seat is already taken.";

                    int emptyWaiting = -1;
                    for (int i = 0; i < _waitingArray.Length; i++)
                        if (_waitingArray[i] == null)
                        {
                            emptyWaiting = i;
                            break;
                        }
                    if (emptyWaiting != -1)
                    {
                        _waitingArray[emptyWaiting] = tbxName.Text;
                        tbxStatus.Text = "You have been added to the waiting list.";
                        updateWaitingList();
                    }
                    else
                        tbxStatus.Text += "\nThere are no empty spots in the waiting list";
                }
            }
            else
                tbxStatus.Text = "No name was given.";
        }


        /// <summary>
        /// Remove someone from the booked array
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            int selectedSeat = getSeatSelected();
            if (selectedSeat != -1 && _bookedArray[selectedSeat] != null)
            {
                _bookedArray[selectedSeat] = null;
                tbxStatus.Text = "Your seat has been canceled";
                moveWaitingIntoBooked();
                updateForm();
            }
            else if (selectedSeat == -1)
                tbxStatus.Text = "No valid seat was selected.";
            else
                tbxStatus.Text = "No one is in that seat.";
        }


        /// <summary>
        /// Add a person to the waiting list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddToWait_Click(object sender, EventArgs e)
        {
            bool foundEmpty = _bookedArray.Any(bookedName => bookedName == null);

            if (!foundEmpty)
            {
                int emptyWaiting = -1;
                for (int i = 0; i < _waitingArray.Length; i++)
                    if (_waitingArray[i] == null)
                    {
                        emptyWaiting = i;
                        break;
                    }
                if (emptyWaiting != -1)
                {
                    _waitingArray[emptyWaiting] = tbxName.Text;
                    updateWaitingList();
                    tbxStatus.Text = "You have been added to the waiting list.";
                }
                else
                    tbxStatus.Text = "There are no empty spots in the waiting list";
            }
            else
                tbxStatus.Text = "There is still an empty seat.";
        }


        /// <summary>
        /// Show the status of the selected seat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCheckStatus_Click(object sender, EventArgs e)
        {
            int selectedSeat = getSeatSelected();

            if (selectedSeat != -1)
                tbxStatus.Text = _bookedArray[selectedSeat] == null ? "The seat selected is available." 
                    : "The seat selected is not available.";
            else
                tbxStatus.Text = "No valid seat selected";
        }


        /// <summary>
        /// Update the book list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShowBooked_Click(object sender, EventArgs e)
        {
            updateBookedList();
        }


        /// <summary>
        /// Update the waiting list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShowWaiting_Click(object sender, EventArgs e)
        {
            updateWaitingList();
        }


        /// <summary>
        /// Fills the arrays with fillers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDebug_Click(object sender, EventArgs e)
        {
            switch (_debugMode)
            {
                case 0:
                    for (int seat = 0; seat < _bookedArray.Length; seat++)
                        if (_bookedArray[seat] == null)
                            _bookedArray[seat] = "BPerson " + seat;
                    _debugMode++;
                    break;
                case 1:
                    for (int i = 0; i < _waitingArray.Length; i++)
                        if (_waitingArray[i] == null)
                            _waitingArray[i] = "WPerson " + i;
                    _debugMode++;
                    break;
                default:
                    _bookedArray = new string[15];
                    _waitingArray = new string[10];
                    _debugMode = 0;
                    break;
            }
            updateForm();
        }


        /// <summary>
        /// On Form load, update the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void onLoad(object sender, EventArgs e)
        {
            updateForm();
        }


        /// <summary>
        /// Update the Form
        /// </summary>
        private void updateForm()
        {
            updateBookedList();
            updateWaitingList();
        }


        /// <summary>
        /// Looks at the row and column selected and gets the seat's array value
        /// </summary>
        /// <returns></returns>
        private int getSeatSelected()
        {
            int row = lstRow.SelectedIndex;
            int col = lstCol.SelectedIndex;

            if (row == -1 || col == -1)
                return -1;

            return row * 3 + col;
        }


        /// <summary>
        /// Update the list of booked seats
        /// </summary>
        private void updateBookedList()
        {
            rtbBooked.Text = "";
            int seat = 0;
            for (char c = 'A'; c < 'F'; c++)
                for (int i = 0; i <= 2; i++)
                {
                    string seatName = "[" + c + ", " + i + "]";
                    string bookedBy = _bookedArray[seat] != null ? _bookedArray[seat] : "";
                    rtbBooked.Text += seatName + " -- " + bookedBy + "\n";
                    seat++;
                }
        }


        /// <summary>
        /// Update the list of waiting people
        /// </summary>
        private void updateWaitingList()
        {
            rtbWaiting.Text = "";
            for (int seat = 0; seat < 10; seat++)
            {
                string waitingPos = "[" + (seat + 1) + "]";
                string waitingName = _waitingArray[seat] != null ? _waitingArray[seat] : "";
                rtbWaiting.Text += waitingPos + " -- " + "" + waitingName + "\n";
            }
        }


        /// <summary>
        /// Try and move people from the waiting list into the booked list
        /// </summary>
        private void moveWaitingIntoBooked()
        {
            //Grab the first empty booked seat
            int emptyBookedSeat = -1;
            for (int seat = 0; seat < _bookedArray.Length; seat++)
            {
                if (_bookedArray[seat] == null)
                {
                    emptyBookedSeat = seat;
                    break;
                }
            }
            if (emptyBookedSeat == -1) return;

            //Grab the first person in the waiting list, and bump everyone else up a place
            string waitingName = _waitingArray[0];
            _bookedArray[emptyBookedSeat] = waitingName;
            tbxStatus.Text += "\n And Someone else has taken your seat";
            _waitingArray[0] = null;

            for (int i = 1; i < _waitingArray.Length; i++)
            {
                if (_waitingArray[i - 1] == null)
                {
                    _waitingArray[i - 1] = _waitingArray[i];
                    _waitingArray[i] = null;
                }
            }
        }

    }
}