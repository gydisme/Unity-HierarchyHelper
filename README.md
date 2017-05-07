Unity-HierarchyHelper
=
The fastest way to create Unity Hierarchy GUI items ever.
![](http://i.imgur.com/cU2iwYG.png)
![](http://i.imgur.com/nPJcYNG.gif)

- Author: Gyd Tseng
- Email: kingterrygyd@gmail.com
- Twitter: @kingterrygyd
- Facebook: facebook.com/barbariangyd
- Donation: <a href='https://pledgie.com/campaigns/32250'><img alt='Click here to lend your support to: Unity-HierarchyHelper and make a donation at pledgie.com !' src='https://pledgie.com/campaigns/32250.png?skin_name=chrome' border='0' ></a>

Installation
=
Download and copy the folder Assets/HierarchyHelper/ fto your project.

QuickStart
=
Enable Hierarchy Helper System
```
1. press Tools/HierarchyHelper/Open Setting Window to open Setting Window
2. toggle Enable Helper Sytem
```

Create A GUI Function for a Component
```
1. Create a method without args, ex: public void DrawHelper( )
2. add property to the method [HelperInfoAttribute( "categroyName", priority )]
```

Create A GUI Function for a non-Component
```
1. Create a static method with an arg of GameObject, ex: public static void DrawHelper( GameObject obj )
2. add property to the method [HelperInfoAttribute( "categroyName2", priority )]
```

Important
```
Use HierarchyHelperManager.GetControlRect( width ) to get the correct DrawRect for your draw method.
```

Examples
```
Download and copy the folder Assets/Exmaples/ to your project.
```

Support
=
Email Me, Let's talk :)

Contributing
=
Your contribution will be licensed under the MIT license for this project.

