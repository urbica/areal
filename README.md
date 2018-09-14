# [AReal](http://areal.urbica.co/)

![AReal](https://raw.githubusercontent.com/urbica/areal/master/AReal_github.png)

AReal is an iOS augmented reality app with a map of Saint Petersburg and it's 3D landmark models. AReal is built with [ARKit 1.5](https://developer.apple.com/arkit/), [Xcode 9.4.1](https://developer.apple.com/xcode/), [Unity 2018.1.1f1](https://unity3d.com/unity/whatsnew/unity-2018.1.1), and [Maps SDK 1.4.4 for Unity by Mapbox](https://www.mapbox.com/unity/).

To run AReal, you need an iPhone 6S, SE or above with iOS 11.0 or newer.

## Usage

1. Open the project in [Unity Editor](https://unity3d.com/unity/editor)

If you just want to run AReal on your device without any changes, start with step 3.

2. Editing scripts

Open the script in any C# editor, make the changes, build the script and switch to Unity Editor. Wait till Unity Editor reflects the changes.

3. Exporting the project to Xcode

In Unity Editor, open `File —> Build Settings`. Check the scenes `SupportScene` and `ArealMainScene`, and make sure that iOS platform is selected. Set the path in `Run in Xcode` dropdown, click `Build`, choose a destination path and click `Save`. Wait till `Build successful` message appears.

4. Building the app

Open the project in Xcode and set your team in `Signing` section. ARKit requires hardware to run a build, so a device should be connected and chosen as a target. Run the project.

5. Enjoy!
