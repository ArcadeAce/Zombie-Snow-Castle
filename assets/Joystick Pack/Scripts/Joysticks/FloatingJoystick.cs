using System.Collections; // Provides access to C# collections like Lists and Arrays.
using System.Collections.Generic; // Gives advanced collection capabilities, such as Lists<>.
using UnityEngine; // Grants access to Unity's core functionality (GameObjects, Physics, UI).
using UnityEngine.EventSystems; // Allows interaction with UI elements like buttons and touch controls.

public class FloatingJoystick : Joystick // 🕹️ Defines a class **FloatingJoystick** that **inherits from Joystick**, enabling mobile-friendly input.
{
    protected override void Start() // Called automatically when the scene loads → Initializes the joystick.
    {
        base.Start(); // 🔄 Calls the parent **Joystick** class's Start() function → Ensures default joystick setup runs first.
        background.gameObject.SetActive(false); // ❌ Hides the joystick background **until the player touches it**.
    }

    public override void OnPointerDown(PointerEventData eventData) // Runs when the player **presses** the joystick on the screen.
    {
        background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position); // 🔄 Moves the joystick background to the **exact touch position**.
        background.gameObject.SetActive(true); // **Shows the joystick background**, making it visible to the player.
        base.OnPointerDown(eventData); // 🔄 Calls the parent **Joystick** class’s pointer-down logic to process the touch input.
    }

    public override void OnPointerUp(PointerEventData eventData) // Runs when the player **lifts their finger off** the joystick.
    {
        background.gameObject.SetActive(false); // ❌ Hides the joystick background again → Prevents clutter when idle.
        base.OnPointerUp(eventData); // 🔄 Calls the parent **Joystick** class’s pointer-up logic to reset input.
    }
}






// Floating Joystick script things you can adjust in the Inpector Panel:

// The handleRange in that context controls how far the joystick’s visual handle can move from the center of the joystick's background. 
// It’s entirely a UI/UX setting—basically how much "give" the thumbstick shows when you drag.
// So, yeah—it’s just visual and affects the look and feel of the joystick (like that Look Button Sprite), not the actual gameplay input strength unless it’s directly tied to the input vector scaling in code (which in many joystick scripts, it isn’t).

// Dead zone in the inspector  is like a “no-response zone” around the joystick center where tiny, accidental movements are ignored. 🎯
// But here’s the twist: it's not about swipe size—it’s more about drag sensitivity. Think of it like this:
// 🌀 Without a dead zone → even the slightest bump or finger jiggle can cause your camera or player to move
// 🧊 With a dead zone (say 0.15f) → the stick has to move at least 15% away from center before Unity says, “Okay, this is real input”
// So it’s more like a stabilizer than a swipe-detector—it won’t make swipes “bigger,” but it will help ignore tiny unintended ones.

// Axis Options in the Inspector control which directions the joystick outputs movement or look data in:
// Both → The joystick responds to both horizontal (X) and vertical(Y) drag movement.This is ideal for FPS camera look or full 2D movement.
// Only Horizontal → The joystick only outputs input along the X-axis.Dragging up/down does nothing.
// Only Vertical → The joystick only outputs input along the Y-axis.Dragging left/right is ignored.

/////////////


// ===================== FLOATING JOYSTICK SUMMARY =====================

// 🔹 The FloatingJoystick script enhances **touch-based movement** for mobile FPS games.
// 🔹 It allows players to **look around** by dragging their finger across the joystick area.
//
// ✅ Core Responsibilities:
// ✅ Inherits from Unity’s Joystick → Expands built-in touch input logic.
// ✅ Hides the joystick when idle → Prevents unnecessary UI clutter.
// ✅ Moves joystick dynamically based on **finger touch location**.
// ✅ Tracks input (`OnPointerDown()` & `OnPointerUp()`) → Enables/disables joystick as needed.
//
// ✅ Where It’s Used:
// ✅ Attached to **UI_Canvas_StarterAssetsInputs_Joysticks** → Part of the **Unity Starter Assets FPS Controller** system.
// ✅ Controls **camera movement** when playing on **smartphones**.
//
// ❌ What It Does NOT Do:
// ❌ It does **not handle movement** → Only controls **player aiming via touch**.
// ❌ It does **not affect weapon switching or firing** → Separate UI buttons handle those.
//
// 🔹 Think of FloatingJoystick as the **touch-based aiming system**, ensuring mobile FPS players can seamlessly adjust their view!

// This script is on the **UI_Canvas_StarterAssetsInputs_Joysticks** that is a child of the Unity Starter Assets FPS Controller, this is for the player to look around when the player puts his finger on the mobile phone screen.



/////////////////////

//How my game works:

// My game is a smartphone game for PlayStore I am using the Samsung Galaxy A15 to test my game.
// I am using the Unity Standard Assets FPS Controller for the player.
// With my left finger is the move button on the screen on my phone UIVirtualJoystick move has the UIVirtualJoystick script, this is for the moving forwards and backwards.
// For looking around there is the Floating Joystick with the floaring joystick script on it.
// The UI_Virtual_Joystick_Look is turned off because it just does not work the way I want.


// My problem below in an explanation for you Copilot for the spinning problem with the Floating Joystick:

// So my problem in my game is when I play my game on my smartphone when I look left or right with my finger and hold my finger on the screen either left or right the screen, I think the camera keeps spinning in circles.
// What I want is for when I move my finger and kind of swipe right for the camera to turn like half of 180 degrees, so like the camera would turn like 90 degrees right, or 90 degrees if I turn left. Does this make sense?


////////////////////////////////////


//NOTE: /* and */ at the end comments out the entire script!


// Let’s connect the dots between that `Joystick` script and your `FloatingJoystick` script—they’re working together like this:



//  What’s Happening Behind the Scenes:

//**The `Joystick` script(from Unity Standard Assets) * * is the * *base logic** that handles:
//-Registering virtual input axes(`Horizontal`, `Vertical`)
//-Detecting drag position
//- Updating virtual input values based on how far the joystick moves

//**The `FloatingJoystick` script** **inherits from `Joystick`**, meaning:
//-It adds functionality **on top of the joystick logic**, like hiding the joystick UI when not in use
//- It customizes when and how the joystick appears (on touch), using `OnPointerDown()` and `OnPointerUp()`

///////////////////////////////////////////////////

//How They Work Together:

//When you touch the screen, `FloatingJoystick.OnPointerDown()` is triggered → it makes the joystick appear where your finger is.
//Then, `Joystick.OnDrag()` (from the parent class) kicks in, updating the horizontal and vertical axis values based on your drag movement.
//Those values can be used in your **player controller script** to move or aim the player.

//So together, they handle **UI interaction** *and* **in-game input mapping**, like "player wants to turn right."


/////////////////////////////////////////////////////////////////////


// Why You Might Not See the `Joystick` Script in Your Scene:

//- It’s likely attached to a **base prefab or package**, and the actual object in your scene is using **`FloatingJoystick`**, which silently inherits and executes everything from `Joystick` behind the scenes.
//- If you open `FloatingJoystick` in the Unity Inspector, Unity won’t show `Joystick` components separately—but its logic is *there and active*.



////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////


// My other problem with the moving and shooting that may be related to the floating joystick and the fire button on the Unity Starter Assets FPS Controller.


/* Explanantion of my problem with the shoot button and when moving: 

so when I play my Zombie Snow Castle game on the Laptop I can move and shoot.
I can jump and move and shoot all at the same time and this is great.
But my game is for mobile smartphones.
But when I play my game on the smartphone I cannot move and shoot its like I have to be standing still in order to shoot my Twin Turbos guns.
Do you know what the problem might be?
I think the sprite fire button/sprite maybe might need a seperate input system, and as well for the floating joystick.
The Twin Turbos gun firing button/sprite that also is the fire button for all my other weapons like the shotgun.
The floating Joystick is child of the UI_Canvas_StarterAssetsInputs_Joysticks.
The firing button is child of the Buttons, and the Buttons is a child of the UI_Canvas_StarterAssetsInputs_Joysticks.
The floating Joystick is ahead of the buttons that has the fire button as a child of it in the heirarchy order going downwards.*/




/*Maybe this is the answer (below) Possible fix.

🎮 Your UI hierarchy setup explains why movement and shooting aren’t working together on mobile.

🔎 Why Your Mobile Controls Might Not Be Working Together:**  
The Floating Joystick and Fire Button are part of the same UI canvas (`UI_Canvas_StarterAssetsInputs_Joysticks`)** → This means **touch input might be conflicting** between them.  
Touch input for movement (`Floating Joystick`) and shooting (`Fire Button`) might be handled by the same event system** → Unity could be prioritizing **one touch gesture at a time**, preventing **multi-touch (moving and shooting simultaneously)**.  
UI hierarchy affects event order** → Since the Floating Joystick is **above** the Buttons, it could be intercepting touches **before the Fire button gets input**, blocking shooting while moving.  

Fixes to Try When You Can Test on Mobile:*
🔹 Separate Input Systems for Joystick & Fire Button →  
Go to `UI_EventSystem` and make sure the joystick **does NOT override the fire button**. **Both must have distinct touch input listeners!**  

🔹 Check "Raycast Target" on Fire Button** →  
In the Fire Button’s UI settings, **disable "Raycast Target", so it **doesn’t block movement input while touching the screen**.  

🔹 Enable MultiTouch Input in `Input System UI Module` →  
Open `UI_EventSystem`, go to **"Input System UI Module", and **set "MultiTouch Enabled" to true**—this allows multiple fingers to register touches separately!  

🔹 Prioritize Both Inputs in Code (`EventSystem.current.IsPointerOverGameObject()`) →  
Modify the Fire Button script so it **does not stop movement when pressed**, ensuring **both actions happen at the same time!**  

When you get a new phone, test these fixes and let me know if movement and shooting still conflict!** 

*/
