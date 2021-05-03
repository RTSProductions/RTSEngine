# RTS-Engine
![](https://img.shields.io/github/repo-size/RTSProductions/RTSEngine?style=for-the-badge)

A simple 2D game engine with physics I made.

# Instructions
Open the RTSEngine folder and doubble click on the RTSEngine.sln to open the project and use it.
# How To Make A Game

first creat a class and call it something like `Game` or `Platformer` or `TopDown` or `Dungeon`

then replace all the using tags with these using tags.

```cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RTSEngine.RTSEngine;
using System.Drawing;
using System.Windows.Forms;
using System.Numerics;
using Box2DX.Dynamics;
using Box2DX.Collision;
using Box2DX.Common;
using Color = System.Drawing.Color;
```
then on the class add `: RTSEngine.RTSEngine` like this
```cs
    class ExampleGame : RTSEngine.RTSEngine
    {

    }
```

now your class should look like this:
```cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RTSEngine.RTSEngine;
using System.Drawing;
using System.Windows.Forms;
using System.Numerics;
using Box2DX.Dynamics;
using Box2DX.Collision;
using Box2DX.Common;
using Color = System.Drawing.Color;

namespace RTSEngine
{
    class ExampleGame : RTSEngine.RTSEngine
    {

    }
}

```
Now you should be getting an error on the class. Don't worry were about to fix that.

Add some overide voids
```cs
        public override void GetKeyDown(KeyEventArgs e)
        {
            
        }

        public override void GetKeyUp(KeyEventArgs e)
        {
            
        }

        public override void OnDraw()
        {
            
        }

        public override void OnLoad()
        {
            
        }

        public override void OnUpdate()
        {
            
        }
```
Now you should still be getting one error. That is because you need to add a struct.
So add this in the code
```cs
        public ExampleGame()
        {

        }
```
Now add on : `: base(new Vector2(615, 515), "Example Game")` Like this:
```cs
        public ExampleGame() : base(new Vector2(615, 515), "Example Game")
        {

        }
```
Now add some variables
```cs
        //the player
        Sprite2D player = null;

        bool left;
        bool right;
        bool up;
        bool down;
        Vector2 lastPos = Vector2.Zero();
        public float speed = 6;
```
Now in the `GetKeyDown` method add :
```cs
            if (e.KeyCode == Keys.W || e.KeyCode == Keys.Up) { up = true; }
            if (e.KeyCode == Keys.S || e.KeyCode == Keys.Down) { down = true; }
            if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left) { left = true; }
            if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right) { right = true; }
```
And in the `GetKeyUp` mothod add :
```cs
            if (e.KeyCode == Keys.W || e.KeyCode == Keys.Up) { up = false; }
            if (e.KeyCode == Keys.S || e.KeyCode == Keys.Down) { down = false; }
            if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left) { left = false; }
            if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right) { right = false; }
```
So now we are getting input from the player wich we can use to move.

Now in oder to make the player and the map we make a 2 dimentinal string array or `string[,] Map` then in the array us the `.` character for an empty space and use the `g` for a ground space also add the `j` for a jewel and `c` for a coin.
Like this:
```cs
        string[,] Map =
        {
            {"g","g","g","g","g","g","g"},
            {"g",".",".",".",".","c","g"},
            {"g","j",".","j","g","c","g"},
            {"g",".","g","g","g","c","g"},
            {"g",".","g","j","g",".","g"},
            {"g",".","g","j",".",".","g"},
            {"g","g","g","g","g","g","g"},
        };
```
This will be our map.
Now in the `OnLoad` method add three sprites that we can use for all the other spites to load in oder to make it more preforment and make it load faster. Also add change the backround color (optinal)
```cs
            BackroundColor = Color.Black;

            Sprite2D groundRef = new Sprite2D("Tiles/Blue tiles/tileBlue_03");
            Sprite2D jewelRef = new Sprite2D("Items/yellowJewel");
            Sprite2D coinRef = new Sprite2D("Items/yellowCrystal");
```
Back in the `OnLoad` method creat two for loops like this:
```cs
           for (int i = 0; i < Map.GetLength(1); i++)
           {
                for (int j = 0; j  < Map.GetLength(0); j ++)
                {

                }
            }
```
This way we can loop through each character in the array.
Now we will make it so that the sprites will render.
Add this in the second for loop :
```cs
                   if (Map[j, i] == "g")
                    {
                        new Sprite2D(new Vector2(i * 50, j * 50), new Vector2(50, 50), groundRef, "Ground");
                    }
                    if (Map[j, i] == "j")
                    {
                        new Sprite2D(new Vector2(i * 50 + 15, j * 50 + 15), new Vector2(25, 25), jewelRef, "Jewel");
                    }
                    if (Map[j, i] == "c")
                    {
                        new Sprite2D(new Vector2(i * 50 + 15, j * 50 + 15), new Vector2(25, 25), coinRef, "Coin");
                    }
```
Now if we hit play we should see a nice little map for the player to wonder around in.
So right now our script should look like this:
```cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RTSEngine.RTSEngine;
using System.Drawing;
using System.Windows.Forms;
using System.Numerics;
using Box2DX.Dynamics;
using Box2DX.Collision;
using Box2DX.Common;
using Color = System.Drawing.Color;

namespace RTSEngine
{
    class ExampleGame : RTSEngine.RTSEngine
    {
        //the player
        Sprite2D player = null;

        bool left;
        bool right;
        bool up;
        bool down;
        Vector2 lastPos = Vector2.Zero();
        public float speed = 6;

        string[,] Map =
        {
            {"g","g","g","g","g","g","g"},
            {"g",".",".",".",".","c","g"},
            {"g","j",".","j","g","c","g"},
            {"g",".","g","g","g","c","g"},
            {"g",".","g","j","g",".","g"},
            {"g",".","g","j",".",".","g"},
            {"g","g","g","g","g","g","g"},
        };

        public ExampleGame() : base(new Vector2(615, 515), "RTS Engine Demo")
        {

        }

        public override void GetKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W || e.KeyCode == Keys.Up) { up = true; }
            if (e.KeyCode == Keys.S || e.KeyCode == Keys.Down) { down = true; }
            if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left) { left = true; }
            if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right) { right = true; }
        }

        public override void GetKeyUp(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W || e.KeyCode == Keys.Up) { up = false; }
            if (e.KeyCode == Keys.S || e.KeyCode == Keys.Down) { down = false; }
            if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left) { left = false; }
            if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right) { right = false; }
        }

        public override void OnDraw()
        {
            
        }

        public override void OnLoad()
        {
            for (int i = 0; i < Map.GetLength(1); i++)
            {
                for (int j = 0; j < Map.GetLength(0); j++)
                {

                }
            }
        }

        public override void OnUpdate()
        {
            
        }
    }
}
```
