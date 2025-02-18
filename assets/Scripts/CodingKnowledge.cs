using UnityEngine;
// using UnityEngine;: This line imports the necessary Unity classes to use its features.
public class CodingKnowledge : MonoBehaviour { }

//class: This keyword is used to declare a new class.
// When a class is declared as public, it means that it can be accessed and interacted with from other scripts or components within the Unity project. This allows for communication between different parts of the project, such as accessing variables, calling methods, or triggering events.//MonoBehaviour is a class provided by Unity that serves as the base class for scripts that are attached to GameObjects in a Unity scene. It provides a framework for implementing behaviors and functionalities that interact with the Unity Engine.

// When a script inherits from MonoBehaviour, it gains access to various Unity-specific functionalities such as lifecycle methods (Start, Update, FixedUpdate, etc.), input handling, collision detection, and more.

// So, while MonoBehaviour is technically inheriting from something (it's part of Unity's scripting framework), from the perspective of your scripts, it's the base class from which they directly inherit. This means your scripts inherit all the capabilities and functionalities provided by MonoBehaviour, allowing them to interact with the Unity Engine and be attached to GameObjects in the scene.//if your write public, it means that it can be accessed and interacted with from other scripts or components within the Unity project. This allows for communication between different parts of the project, such as accessing variables, calling methods, or triggering events.If you write private: This access modifier restricts access to members (fields, methods, properties) only within the same class. Other classes cannot access private members directly.

// Curly Braces { } in C# are used to define the beginning and end of code blocks. These code blocks can contain various elements such as classes, methods, loops, conditional statements, and more. Here's how curly braces are commonly used:

// In Unity, when you use TextMeshPro (often abbreviated as TMPro), you need to include the using TMPro; directive in your C# scripts. This directive allows you to access the TextMeshPro-related classes and components in your scripts.
// In Unity, when a script's name is followed by : MonoBehaviour, it indicates that the script is inheriting from the MonoBehaviour class. This inheritance is essential for Unity scripts because MonoBehaviour is the base class for most Unity scripts that interact with GameObjects in the game world.
// in Unity and many game development frameworks, it's common to create a base script or class that defines common behaviors and properties for a category of objects. In your script, the Poison Zombie boss script PoisonZombie inherits from an Enemy class, which suggests that Enemy is a base class containing common behaviors and properties shared among different types of enemies in your game-
// - When a script inherits from a base class, it means that the child script (PoisonZombie) will inherit all the methods and fields of the parent class (Enemy)and can also override or extend them as needed. This is a fundamental concept in object-oriented programming known as inheritance.

// void: This indicates that the method does not return any value. Just like with Update(), it means the method doesn't produce a result that you can use in your code.
// Start(): This is the name of the method. The Start() method is called automatically by Unity once when the GameObject this script is attached to is created or initialized.

// void: This is the return type of the method. It means that the method does not return any value. In other words, the method doesn't produce a result that you can use in your code.
// Update(): This is the name of the method. In Unity's scripting API, Update() is a special method that gets called every frame (frame-rate dependent). It's often used for performing actions that need to be continuously updated, like user input, character movement, animation, and more.

// Public means can be seen in the Inpector Panel.
// Private means it cannot be seen in the Inspector Panel.

// In Unity C#, data types are used to specify the type of data that a variable can hold. They define what kind of values a variable can store, such as numbers, text, or more complex types like objects.
// Here are some common data types in Unity C#:
// 1. int: Represents whole numbers (e.g., -5, 0, 42).
// 2. float: Represents floating-point numbers (e.g., 3.14, -0.5, 1.0).
// 3. string: Represents text(e.g., "Hello, Unity!").
// 4. bool: Represents boolean values (true or false).

// f after the number is a float number.
// float is a data type that represents floating-point numbers, which are numbers with a decimal point. Floats are used to represent values that can have fractional parts, as opposed to integers (int) which represent whole numbers.

// CamelCase capitalizes the first letter of each word (except the first word) without spaces, making it easier to read and distinguish words, like for example public float zombieFireballSpeedToHit, In the Unity Inspector panel, this would be displayed as: Zombie Fireball Speed To Hit.
// The standard naming convention for scripts in Unity follows the PascalCase convention. PascalCase capitalizes the first letter of each word in the name, including the first word, without using spaces or underscores. This creates a clear and readable name for your script.


// Transform is a fundamental component that represents the position, rotation, and scale of a GameObject in 3D space. It allows you to manipulate the spatial properties of an object.
// transform.Rotate allows you to rotate a GameObjects transform by a specified amount around a specified axis. Use transform.Rotate inside the Update() method in the script to continuosly rotate the GameObject around the axis by 90 degrees per second.
// Vector3 is a struct and is commonly used to reference the X. Y and Z position of an object, it can also be used for detecting direction and also used with rotations as well.
// After Vector3. can be up, down, left, and right
// SpinSpeed is not a built-in type in C# but it can be implemented as a float variable to represent the rate of rotation in degrees and radians per second. 
// *This symbol means to multiply.
// Time.deltaTime in C# is a property of the time in Unity, it represents the time in seconds it took for the last frame to complete. By multiplying the distance an object should move, or rotate at a constant speed regardless of how quickly or slowly the game is running.
// gameObject is a special variable in Unity's scripting API. It refers to the GameObject to which the script is attached. It's automatically available in any MonoBehaviour script you create in Unity.
// Invoke method is used to call a method or delegate asynchronously. It allows you to schedule the execution of a method or delegate at a later time or after a specified delay.
// Input.GetKey(KeyCode.W) is used to check if a specific key on the keyboard is being held down. In this case, it's checking if the "W" key is pressed.

// SceneManager is a class in Unity's C# scripting API that provides methods for managing scenes and scene transitions in your Unity game. It's part of the UnityEngine.SceneManagement namespace.


// Variable names are identifiers that you choose to represent data or values in your code. They help you give meaningful names to different pieces of information, making your code more readable and understandable.
// In C#, it's common to use camel case for variable names. Camel case means starting with a lowercase letter and capitalizing the first letter of each subsequent word. For example, playerScore, enemyCount.
// Consistency: Be consistent with your naming conventions throughout your code.
// Examples of good variable names:
// playerHealth
// enemyPosition
// numEnemies
// spawnDelay
// By using meaningful and descriptive variable names, you make your code easier to understand and maintain, both for yourself and for others who may read your code.

// Debug.Log("Hello, World!");: This line uses Debug.Log() to print the string "Hello, World!" to the Unity console. The Debug class provides methods for logging messages that help with debugging your code.
