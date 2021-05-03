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
