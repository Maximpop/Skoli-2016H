// Exercise 15.3: Ball.cs
// Represents a ball that bounces inside a Panel.
using System;
using System.Drawing;
using System.Threading;

class Ball
{
	private float x; // horizontal position of ball
	private float y; // vertical position of ball
	private float radius; // radius of ball
	private Color color; // color of ball
	private float dx; // horizontal speed of ball
	private float dy; // vertical speed of ball
	private int max_x; // horizontal size of containing Panel
	private int max_y; // vertical size of containing Panel

	public Ball( float posx, float posy, float rad, Color col,
		float speedx, float speedy, int panelx, int panely )
	{
		x = posx;
		y = posy;
		radius = rad;
		color = col;
		dx = speedx;
		dy = speedy;
		max_x = panelx;
		max_y = panely;
	} // end constructor

	// bounces the ball around
	public void Run()
	{
		// loop until the program is terminated
		while ( true )
		{
			Thread.Sleep( 20 ); // sleep for 20 milliseconds

			x += dx; // move ball horizontally

			// check for collision with a horizontal wall
			if ( x < radius )
			{
				dx = -dx;
				x = radius;
			} // end if
			else if ( x > max_x - radius )
			{
				dx = -dx;
				x = max_x - radius;
			} // end else if

			y += dy; // move ball vertically

			// check for collision with a vertical wall
			if ( y < radius )
			{
				dy = -dy;
				y = radius;
			} // end if
			else if ( y > max_y - radius )
			{ 
				dy = -dy;
				y = max_y - radius;
			} // end else if
		} // end while
	} // end method Run

	// property X
	public float X
	{
		get
		{
			return x;
		} // end get
	} // end property X

	// property Y
	public float Y
	{
		get
		{
			return y;
		} // end get
	} // end property Y

	// property Radius
	public float Radius
	{
		get
		{
			return radius;
		} // end get
	} // end property Radius

	// property Color
	public Color Color
	{
		get
		{
			return color;
		} // end get
	} // end property Color
} // end class Ball

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

