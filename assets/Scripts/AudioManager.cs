using System; // Provides access to basic C# functionality.
using System.Collections; // Allows handling of basic collections like Arrays.
using System.Collections.Generic; // Enables advanced collections like Dictionaries and Lists.
using UnityEngine; // Grants access to Unity's core features like GameObjects, Audio, and Scene Management.

public class AudioManager : MonoBehaviour // 🎶 Manages background music and sound effects for the game.
{
    public static AudioManager Instance; // 🔄 Creates a Singleton instance to ensure **only one AudioManager exists**.
    public SoundEffect[] soundEffects; // 🔊 Stores **all available sound effects**, like gunshots, explosions, etc.
    public Music[] musics; // 🎵 Stores **all background music** tracks for different scenes.
    private Dictionary<string, SoundEffect> effectdictionary; // 🔍 Creates a **dictionary for sound effects**, allowing quick lookup.
    private Dictionary<string, Music> musicdictionary; // 🔍 Creates a **dictionary for music**, making track access easier.

    private void Awake() // ✅ Runs automatically when the game starts → Initializes the AudioManager.
    {
        if (Instance == null) // ❓ Checks if **AudioManager already exists**, preventing duplicate instances.
        {
            Instance = this; // ✅ Sets this instance as the **global reference**.
            DontDestroyOnLoad(gameObject); // 🔄 Ensures **AudioManager persists across scene transitions**.
        }
        GetMusic(); // 🎵 Loads all available music tracks into the dictionary.
        GetSoundeffects(); // 🔊 Loads all sound effects into the dictionary.
    }

    private void GetSoundeffects() // 🔄 Populates the **effect dictionary** with sound effect objects.
    {
        effectdictionary = new Dictionary<string, SoundEffect>(); // ✅ Initializes the sound effect dictionary.

        foreach (SoundEffect effect in soundEffects) // 🔄 Loops through all sound effects in the array.
        {
            AudioSource audioSource = gameObject.AddComponent<AudioSource>(); // ✅ Adds an AudioSource component to play the sound.
            effect.CreateAudioSource(audioSource); // 🔊 Initializes the sound effect with its assigned AudioSource.
            Debug.Log($"addingsoundeffect {effect.SoundID} - {audioSource.ToString()}"); // 📝 Logs the added sound effect for debugging.
            effectdictionary.Add(effect.SoundID, effect); // ✅ Stores the sound effect in the dictionary for easy lookup.
        }
    }

    private void GetMusic() // 🔄 Populates the **music dictionary** with background tracks.
    {
        musicdictionary = new Dictionary<string, Music>(); // ✅ Initializes the music dictionary.

        foreach (Music music in musics) // 🔄 Loops through all available music tracks.
        {
            AudioSource audioSource = gameObject.AddComponent<AudioSource>(); // ✅ Adds an AudioSource component to play music.
            music.CreateAudioSource(audioSource); // 🎵 Initializes the music track with its assigned AudioSource.
            musicdictionary.Add(music.SoundID, music); // ✅ Stores the music track in the dictionary for easy lookup.
        }
    }

    public void PlayEffect(string ID) // 🎶 Plays a **sound effect** based on its assigned ID.
    {
        Debug.Log($"playingsound {ID}"); // 📝 Logs which sound effect is being played.
        if (effectdictionary.ContainsKey(ID)) // ❓ Checks if the sound effect exists in the dictionary.
        {
            AudioSource source = effectdictionary[ID].AudioSource; // ✅ Retrieves the assigned AudioSource for the effect.
            Debug.Log($"playingsound {ID} - {source}"); // 📝 Logs additional sound effect details.
            source.spatialBlend = 0.0f; // 🎛️ Sets the sound effect to **2D audio** (ignores 3D positioning).
            source.Play(); // 🔊 Plays the sound effect.
        }
        else { Debug.Log("couldnotfindsound"); } // 🚨 Logs an error if the sound effect ID is not found.
    }

    public void PlayMusic(string ID) // 🎵 Plays **background music** based on its assigned ID.
    {
        AudioSource source = musicdictionary[ID].AudioSource; // ✅ Retrieves the assigned AudioSource for the music track.
        if (source && !source.isPlaying) // ❓ Ensures the music **only plays if it's not already playing**.
        {
            source.spatialBlend = 0.0f; // 🎛️ Sets the music to **2D audio** (ignores 3D positioning).
            source.Play(); // 🎶 Plays the music track.
        }
    }

    public void StopMusic(string ID) // ❌ Stops a **specific background music track** based on its ID.
    {
        AudioSource source = musicdictionary[ID].AudioSource; // ✅ Retrieves the assigned AudioSource for the music track.
        if (source && source.isPlaying) // ❓ Ensures the music **only stops if it's currently playing**.
        {
            source.Stop(); // ❌ Stops the music track.
        }
    }

    public void StopAllMusic() // ❌ Stops **all active background music**.
    {
        foreach (var music in musicdictionary) // 🔄 Loops through all stored music tracks.
        {
            StopMusic(music.Key); // ❌ Calls `StopMusic()` on each track to stop playback.
        }
    }
}

// ===================== AUDIO MANAGER SUMMARY =====================
// 🔹 The AudioManager script handles **background music and sound effects** for the game.
//
// ✅ Core Responsibilities:
// ✅ Uses Singleton Pattern (`public static AudioManager Instance`) → Ensures **only ONE AudioManager exists globally**.
// ✅ Stores all sound effects (`public SoundEffect[] soundEffects`) → Centralized management for explosions, gunshots, etc.
// ✅ Stores all music (`public Music[] musics`) → Keeps background tracks structured and accessible.
// ✅ Uses dictionaries for **fast lookup** → Allows **quick retrieval of sounds & music** based on their assigned ID.
// ✅ Plays sound effects (`PlayEffect(ID)`) → Retrieves a sound and plays it using AudioSource.
// ✅ Plays music (`PlayMusic(ID)`) → Ensures background music plays only when needed.
// ✅ Stops individual or all music (`StopMusic(ID)`, `StopAllMusic()`) → Handles audio transitions.
//
// ❌ What It Does NOT Do:
// ❌ Does NOT generate new sound effects → Only plays **preloaded sound assets** stored in the dictionaries.
// ❌ Does NOT manage player-specific sound logic → Separate scripts handle in-game sounds like footsteps.
//
// 🔹 Think of AudioManager as **the game's central audio hub**, ensuring seamless playback and smooth transitions across all levels!



