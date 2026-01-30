Unity-HierarchyHelper
=
The fastest way to create Unity Hierarchy GUI items ever.
![](http://i.imgur.com/nPJcYNG.gif)
![](https://i.imgur.com/oWhbFgi.png)
![](http://i.imgur.com/4QC5t3b.png)
![](http://i.imgur.com/cU2iwYG.png)
- Author: Gyd Tseng
- Email: kingterrygyd@gmail.com
- Twitter: @kingterrygyd
- Facebook: facebook.com/barbariangyd
- [![Donate via PayPal](https://img.shields.io/badge/Donate-PayPal-blue.svg)](https://www.paypal.com/ncp/payment/TY3G9Z2WJR2JJ)

Installation
=
Download and copy the folder Assets/HierarchyHelper/ to your project.
(Optional) Download and copy the folder Assets/HierarchyHelperImplementations/ to your project.
> **_Add issues if you wanna request new implementations_**

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
Download and copy the folder Assets/HierarchyHelperExmaples/ to your project.
```

FAQ
```
1. Why my helperUIs coverd each other?
A: Use HierarchyHelperManager.GetControlRect( width ) to get the correct DrawRect to draw.

2. How to prevent my helperUIs covered by plugin's hierarchy gui items?
A: Assign HierarchyHelperManager.CalculateOffset and calculate how many offsets you need for them.

3. Could I add more the one Draw method in one class?
A: YES.
```

Support
=
Email Me, Let's talk :)

Contributing
=
Your contribution will be licensed under the MIT license for this project.

