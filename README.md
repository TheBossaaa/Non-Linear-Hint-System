# Non-Linear-Hint-System
This is a small system that I created for a horror game. Flawed, will be improved in the long-term.

Made for Unity Game Development.

This system is meant to create hints without destorying the ones that existing. Hints can be displayed repeatedly. 
Current version is still flawed and require improvements. However it does the job for my current project. 
In time I'll adjust this system.

How-To:
1) Create UI elements for hints to display.
2) Create an empty GameObject and place HintSystem.cs inside that object.
3) Place the relevant UI elements inside the script component.
4) Go to Project view and right-click to create the scriptable object. The default is: Create>BossaCustom>Hints>Hints Asset.
5) Rename and fill the data inside the scriptable object.
6) Place the created scriptable object inside the "Hint Assets" inside the Hint System
7) Create an empty GameObject in the hierarchy and rename it according to the task.
8) Place HintTrigger.cs inside the object.
9) Place the scriptable object you created inside the Hint Trigger component. Select the relevant key you want to display. 
10) Create your trigger or interaction and use UnityEvent. Place the object that has HintTrigger inside the event and select TriggerHint().
11) Now your hint will be visible on the screen.

NOTE: Please note that the scriptable object is using UHFPS.Runtime and GString instead of String.
This is because I'm using the package for game development and using their localisation elements.
If you don't own the package then simply remove "using UHFPS.Runtime" and rename "GString" to "String".

