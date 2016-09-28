// Fig. 23.7: Square.cs
// A Square on the TicTacToe board.
using System.Windows.Forms;

// the representation of a square in a tic-tac-toe grid
public class Square
{
   private Panel panel; // GUI Panel that represents this Square
   private char mark; // player’s mark on this Square (if any)
   private int location; // location on the board of this Square

   // constructor
   public Square( Panel newPanel, char newMark, int newLocation )
   {
      panel = newPanel;
      mark = newMark;
      location = newLocation;
   } // end constructor
   
   // property SquarePanel; the panel which the square represents
   public Panel SquarePanel
   {
      get
      {
         return panel;
      } // end get
   } // end property SquarePanel

   // property Mark; the mark on the square
   public char Mark
   {
      get
      {
         return mark;
      } // end get
      set
      {
         mark = value;
      } // end set
   } // end property Mark

   // property Location; the square's location on the board
   public int Location
   {
      get
      {
         return location;
      } // end get
   } // end property Location
} // end class Square

/**************************************************************************
 * (C) Copyright 1992-2006 by Deitel & Associates, Inc. and               *
 * Pearson Education, Inc. All Rights Reserved.                           *
 *                                                                        *
 * DISCLAIMER: The authors and publisher of this book have used their     *
 * best efforts in preparing the book. These efforts include the          *
 * development, research, and testing of the theories and programs        *
 * to determine their effectiveness. The authors and publisher make       *
 * no warranty of any kind, expressed or implied, with regard to these    *
 * programs or to the documentation contained in these books. The authors *
 * and publisher shall not be liable in any event for incidental or       *
 * consequential damages in connection with, or arising out of, the       *
 * furnishing, performance, or use of these programs.                     *
 *************************************************************************/