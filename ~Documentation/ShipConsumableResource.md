<!--ShipConsumableResource.md (c) 2021
Author: Justin Abbott (lastmilegames@gmail.com)
Desc: description
Created:  2021-07-28T21:11:49.935Z
Modified: 2021-07-28T21:29:00.717Z
-->

# ShipConsumableResources.cs

`ShipConsumableResource` is a data class, similar to Unity's Scriptable Objects. It represents a consumable resource type such as fuel, electrical charge, or atmosphere.

To create a new 'ShipConsumableResource':

1. Click the "Create a new resource and edit it button" at the top of the Inspector.
![Showing inspector button](.\images\01-ShipConsumableResource.png)

Alternatively, right-click in the FileSystem dock and click **New Resource**

2. Select the base "Resource" type then click **Create**
![Showing resource selection](.\images\02-ShipConsumableResource.png)

3. Under the **Reference Section**, expand **Script**, click on the dropdown (marked *[empty]* by default) and click **Load**

4. Navigate to the ShipConsumableResrouce.cs file, select it, and click **Open**.

5. Click the **Save** button at the top of the window.
![Showing sav button](.\images\03-ShipConsumableResource.png)

Once you have added the newly created resource into an accepted field, you can edit it from that Node in the inspector!

![Showing resource editing in inspector](.\images\03-ShipConsumableResource.png)
