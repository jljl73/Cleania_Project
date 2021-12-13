# CursorControl

A mouse cursor control module for Unity3D. It allows you to get and set the global mouse cursor position (relative to the OS), set the local mouse cursor position (relative to the unity game window), and simulate left, middle and right clicks.

Currently supporting Windows only.

A compiled version of this package is available on the Unity Asset Store: http://u3d.as/wbv

## Usage Example

The cursor control module has the following static methods:

* Vector2 CursorControl.GetGlobalCursorPos()
* void CursorControl.SetGlobalCursorPos(Vector2 pos)
* void CursorControl.SetLocalCursorPos(Vector2 pos)
* void CursorControl.SimulateLeftClick()
* void CursorControl.SimulateMiddleClick()
* void CursorControl.SimulateRightClick()

Note: Operating Systems such as Windows consider the origin position of the mouse cursor to be the top-left corner of the display, whereas Unity considers it to be the bottom-left corner of the game window.

See the CursorControlExample scene for a simple example.

## Release History

* 1.1 (August 16, 2016)
    * Added functions to simulate left, middle and right clicks
    * Added unit tests to github repo
* 1.0 (June 29, 2016)
    * Initial Release
    
## Known Issues

* If your mouse cursor is outside the Unity game window when calling the SetLocalCursorPos() function, it may set it to an inaccurate position.
* If you run Unity builds in full screen mode at a non-native resolution and call the SetLocalCursorPos() function, it may set it to an inaccurate position.

## Contributing

Feel free to contribute and add a pull request to the github project: https://github.com/znebby/UnityCursorControl In particular, supporting extra platforms would be useful.

## Author

This module was written by znebby.

## License

This project is licensed under the MIT License - see the LICENSE.txt file for details.