# RTS-Engine
![](https://img.shields.io/github/repo-size/RTSProductions/RTSEngine?style=for-the-badge)

A simple 2D game engine with physics.

# Instructions
Open the RTSEngine folder and doubble click on the RTSEngine.sln to open the project and use it.
# How To Make A Game
<details>
  <summary>Click to expand!</summary>

## Pretutorial Info

before we start you need to understand some things about the code.                                                                      
VECTOR2:
A vector 2 is a float array with two variables `x` and `y` this can be used for the scale and the position of an object.

LOG:
Log is a class the can be used to log things to the consel window like this `Log.Normal("Hello World")` you can also use `Log.Info`, `Log.Warning`, and `Log.Error`.

SPRITE2D:
Sprite2D is a sprite that can be rendered, moved, sacled, destroyed, it can even collide with other sprites and you can also make it a physics object.
To creat a sprite use:
```cs
Sprite2D sprite = new Sprite2D(Vector2 Position, Vector2 Scale, String Directory, String Tag)
```
The position and scale are both vector2's the tag and directory are string's, for the position and scale use something like `new Vector2(10, 10)` for the tag use something that makes sence for the object like `Ground` or `Coin` and the directory is the diectory used to get the actual sprite, to find the directory of the sprite you want go to `Assets\Sprites\PNG` if you don't see the sprite your looking for or the folder the sprite is in the make sure its in the `Assets\Sprites\PNG(directory)` directory to see if it is there.

## Game Tutorial

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
Next in the `Program` class replace the `DemoGame game = new DemoGame();` with `ExampleGame game = new ExampleGame();` like this:
```cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RTSEngine.RTSEngine;

namespace RTSEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            ExampleGame game = new ExampleGame();
        }
    }
}
```
This will allow us to actaully play our game, but don't try it yet because we still have nothing
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

            Sprite2D playerRef = new Sprite2D("Players/Player Green/playerGreen_walk1");
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
            BackroundColor = Color.Black;

            Sprite2D groundRef = new Sprite2D("Tiles/Blue tiles/tileBlue_03");
            Sprite2D jewelRef = new Sprite2D("Items/yellowJewel");
            Sprite2D coinRef = new Sprite2D("Items/yellowCrystal");

            for (int i = 0; i < Map.GetLength(1); i++)
            {
                for (int j = 0; j < Map.GetLength(0); j++)
                {
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
                }
            }
        }

        public override void OnUpdate()
        {
            
        }
    }
}
```
Now we need to add the player
In the map array add a `p` for the player like this:
```cs
        string[,] Map =
        {
            {"g","g","g","g","g","g","g"},
            {"g",".",".",".",".","c","g"},
            {"g","j","p","j","g","c","g"},
            {"g",".","g","g","g","c","g"},
            {"g",".","g","j","g",".","g"},
            {"g",".","g","j",".",".","g"},
            {"g","g","g","g","g","g","g"},
        };
```
Then in the second for loop add another if statment:
```cs
if (Map[j, i] == "p")
{
    player = new Sprite2D(new Vector2(i * 50, j * 50), new Vector2(30, 40), playerRef, "Player");
}
```
Now if we start we should see a little green blob where you put the `p`.
But we can't move him, luckly we are reciveing input from the player so all we need to do is use it.
So in the `OnUpdate` method add:
```cs
            if (player != null)
            {

            }
```
Now we can use the input bools.
First add the y axsis like this:
```cs
                if (up)
                {
                    player.Position.y -= speed;
                }
                if (down)
                {
                    player.Position.y += speed;
                }
```
Now add the x axsis:
```cs
                if (left)
                {
                    player.Position.x -= speed;
                }
                if (right)
                {
                    player.Position.x += speed;
                }
```
Now if we start the game we have a little green player that can move round, fun!
But it wont collide with the walls.
So we need to make it registor the collsion, luckly the `Sprite2D` does all the work for us.
So next lets add this:
```cs
                if (player.IsColliding("Ground") != null)
                {

                }
```
In the collide statment add:
```cs
                    player.Position.x = lastPos.x;
                    player.Position.y = lastPos.y;
```
This checks if were colliding with the walls and if so makes it so we can't move through the walls!
Now add an else statment so we can update the lasp position of the player:
```cs
                else
                {
                    lastPos.x = player.Position.x;
                    lastPos.y = player.Position.y;
                }
```
Now if you play it you will notice that we can't collect the coins and jewels
So first add:
```cs
                Sprite2D jewel = player.IsColliding("Jewel");
                if (jewel != null)
                {
                    jewel.DestroySelf();
                }
```
This will make it so we can collect the jewels.
Now do the same thing with the coins like this:
```cs
                Sprite2D coin = player.IsColliding("Coin");
                if (coin != null)
                {
                    coin.DestroySelf();
                }
```
So now we can collect the coins, great!
So now our basic game is done, awesome!
So your script should look like this:
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
            {"g","j","p","j","g","c","g"},
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
            BackroundColor = Color.Black;

            Sprite2D playerRef = new Sprite2D("Players/Player Green/playerGreen_walk1");
            Sprite2D groundRef = new Sprite2D("Tiles/Blue tiles/tileBlue_03");
            Sprite2D jewelRef = new Sprite2D("Items/yellowJewel");
            Sprite2D coinRef = new Sprite2D("Items/yellowCrystal");

            for (int i = 0; i < Map.GetLength(1); i++)
            {
                for (int j = 0; j < Map.GetLength(0); j++)
                {
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
                    if (Map[j, i] == "p")
                    {
                        player = new Sprite2D(new Vector2(i * 50, j * 50), new Vector2(30, 40), playerRef, "Player");
                    }
                }
            }
        }

        public override void OnUpdate()
        {
            if (player != null)
            {
                if (up)
                {
                    player.Position.y -= speed;
                }
                if (down)
                {
                    player.Position.y += speed;
                }
                if (left)
                {
                    player.Position.x -= speed;
                }
                if (right)
                {
                    player.Position.x += speed;
                }

                if (player.IsColliding("Ground") != null)
                {
                    player.Position.x = lastPos.x;
                    player.Position.y = lastPos.y;
                }
                else
                {
                    lastPos.x = player.Position.x;
                    lastPos.y = player.Position.y;
                }

                Sprite2D jewel = player.IsColliding("Jewel");
                if (jewel != null)
                {
                    jewel.DestroySelf();
                }

                Sprite2D coin = player.IsColliding("Coin");
                if (coin != null)
                {
                    coin.DestroySelf();
                }
            }
        }
    }
}
```
Now for a little bonus we'll make a new big map
First change the map array to this:
```cs
        string[,] Map =
        {
            {"g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g"},
            {"g", ".", ".", ".", ".", ".", "g", ".", ".", "c", ".", "j", "g", "j", "g"},
            {"g", ".", "g", ".", ".", ".", "g", ".", ".", "g", ".", "j", "g", "j", "g"},
            {"g", ".", "g", ".", ".", ".", "g", ".", ".", "g", ".", "j", "g", "j", "g"},
            {"g", "c", "g", ".", ".", ".", "g", ".", ".", "g", ".", ".", "g", "j", "g"},
            {"g", "c", "g", ".", ".", ".", "g", ".", "g", "g", "g", ".", "g", "j", "g"},
            {"g", "c", "g", "c", ".", ".", "g", ".", ".", ".", "g", ".", "g", "c", "g"},
            {"g", ".", "g", "g", "g", ".", "g", ".", ".", ".", "g", ".", "g", "c", "g"},
            {"g", ".", "g", "p", "g", ".", "g", "g", "g", ".", "g", ".", "g", "c", "g"},
            {"g", ".", "g", ".", "g", ".", "g", ".", ".", ".", "g", ".", "g", "c", "g"},
            {"g", ".", ".", ".", "g", ".", "j", ".", ".", ".", "g", ".", ".", "c", "g"},
            {"g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g"},
        };
```
And in the `OnLoad` method add:
```cs
            CameraZoom = new Vector2(.8f, .8f);
```
To zoom out the camera.
So now our script should look like this:
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
            {"g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g"},
            {"g", ".", ".", ".", ".", ".", "g", ".", ".", "c", ".", "j", "g", "j", "g"},
            {"g", ".", "g", ".", ".", ".", "g", ".", ".", "g", ".", "j", "g", "j", "g"},
            {"g", ".", "g", ".", ".", ".", "g", ".", ".", "g", ".", "j", "g", "j", "g"},
            {"g", "c", "g", ".", ".", ".", "g", ".", ".", "g", ".", ".", "g", "j", "g"},
            {"g", "c", "g", ".", ".", ".", "g", ".", "g", "g", "g", ".", "g", "j", "g"},
            {"g", "c", "g", "c", ".", ".", "g", ".", ".", ".", "g", ".", "g", "c", "g"},
            {"g", ".", "g", "g", "g", ".", "g", ".", ".", ".", "g", ".", "g", "c", "g"},
            {"g", ".", "g", "p", "g", ".", "g", "g", "g", ".", "g", ".", "g", "c", "g"},
            {"g", ".", "g", ".", "g", ".", "g", ".", ".", ".", "g", ".", "g", "c", "g"},
            {"g", ".", ".", ".", "g", ".", "j", ".", ".", ".", "g", ".", ".", "c", "g"},
            {"g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g"},
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
            BackroundColor = Color.Black;

            Sprite2D playerRef = new Sprite2D("Players/Player Green/playerGreen_walk1");
            Sprite2D groundRef = new Sprite2D("Tiles/Blue tiles/tileBlue_03");
            Sprite2D jewelRef = new Sprite2D("Items/yellowJewel");
            Sprite2D coinRef = new Sprite2D("Items/yellowCrystal");

            CameraZoom = new Vector2(.8f, .8f);

            for (int i = 0; i < Map.GetLength(1); i++)
            {
                for (int j = 0; j < Map.GetLength(0); j++)
                {
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
                    if (Map[j, i] == "p")
                    {
                        player = new Sprite2D(new Vector2(i * 50, j * 50), new Vector2(30, 40), playerRef, "Player");
                    }
                }
            }
        }

        public override void OnUpdate()
        {
            if (player != null)
            {
                if (up)
                {
                    player.Position.y -= speed;
                }
                if (down)
                {
                    player.Position.y += speed;
                }
                if (left)
                {
                    player.Position.x -= speed;
                }
                if (right)
                {
                    player.Position.x += speed;
                }

                if (player.IsColliding("Ground") != null)
                {
                    player.Position.x = lastPos.x;
                    player.Position.y = lastPos.y;
                }
                else
                {
                    lastPos.x = player.Position.x;
                    lastPos.y = player.Position.y;
                }

                Sprite2D jewel = player.IsColliding("Jewel");
                if (jewel != null)
                {
                    jewel.DestroySelf();
                }

                Sprite2D coin = player.IsColliding("Coin");
                if (coin != null)
                {
                    coin.DestroySelf();
                }
            }
        }
    }
}
```
And if you play it we are in a huge map, awesome!
So thats it now you can make your own games!
</details>

# How To Add Physics
<details>
  <summary>Click to expand!</summary>
  
  </details>
