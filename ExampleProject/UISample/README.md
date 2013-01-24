# UISample Example

---
The UISample example demonstrates how to use our Unity Prefab GameObjects to calibrate and drive Sphero with touch gestures.

## Joystick Prefab

The code for the joystick is in the `/Plugins/Sphero` directory.  However, I won't explain the code, but how to use the Prefab.  The first Inpsector variable you need to know is the `Velocity Scale`.  It controls the scale factor of all Roll commands sent to Sphero.  When the joystick puck is on the edge, a value of 1.0f will get scaled and clamped and then sent the ball.  So, if the scale is 0.6f, then the max Sphero can go is 0.6f.  Another Inspector variable is the joystick scale.  The joystick scale, is essentially the size of the joystick from 0.0-1.0. With 1.0 being the full size of the screens limiting dimension.  For example, if you are in landscape mode and a joystick scale of 0.5, then the joystick graphic will be half the screen height.  Also, you can control the center position of the joystick using the Joystick Prefab's position transform. Joystick color can also be changed by editing the puck and background GUITexture's color Inpector variable.

## Calibration Button

Through product testing, we have determined that the easiest way for users to calibrate Sphero in a drive app is with a single button press and touch gesture.  You can change the size of the button using the Button Scale Inspector variable.  You can change the position using the position transform on the Prefab GameObject.  You can change the pop-up ring sizes using the Ring Scale Inspector variable.  Finally, you can change the button and ring colors by adjusting the GUITexture color Inspector variable.


## Community and Help

---

* [Developer Forum](http://forum.gosphero.com/) - Share your project, get help and talk to the Sphero developers!