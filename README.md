# RTS-Engine
![](https://img.shields.io/github/repo-size/RTSProductions/RTSEngine?style=for-the-badge)

A simple 2D game engine with physics I made.

# Intructions
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
