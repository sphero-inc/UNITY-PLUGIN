using System.Threading;
using UnityEngine;

/// <summary>
/// The goal of this class is to let you load levels (from the Sphero callbacks) in a way that 
/// doesn't try to make Unity calls on a thread other than the Unity thread. Unity takes issue with
/// this and will throw a crashing-tantrum.
/// </summary>
public class ThreadSafeLoadLevel : MonoBehaviour
{
	#region Static instance management
	/// <summary>
	/// Static instance of this script
	/// </summary>
	private static ThreadSafeLoadLevel s_instance;

	/// <summary>
	/// No creating an instance after OnApplicationQuit is called - can get funky in the editor
	/// </summary>
	private static bool m_exiting;

	/// <summary>
	/// Gets the static instance of his script. This is *NOT* thread safe. Call it from Unity's
	/// thread (creates a Unity object)
	/// </summary>
	public static ThreadSafeLoadLevel Instance
	{
		get
		{
			if (s_instance == null && !m_exiting)
			{
				var go = new GameObject("ThreadSafeLoadLevel");
				s_instance = go.AddComponent<ThreadSafeLoadLevel>();
			}
			return s_instance;
		}
	}
	#endregion

	/// <summary>
	/// An object to lock on
	/// </summary>
	private readonly Object m_lock = new Object();

	/// <summary>
	/// The level we want to load
	/// </summary>
	private string m_levelToLoad;

	#region Unity-style callbacks
	void Update()
	{
		// non-blocking lock acquire
		if (Monitor.TryEnter(m_lock))
		{
			if (!string.IsNullOrEmpty(m_levelToLoad))
			{
				Application.LoadLevel(m_levelToLoad);
			}

			Monitor.Exit(m_lock);
		}
	}

	void OnApplicationQuit()
	{
		m_exiting = true;
	}
	#endregion

	/// <summary>
	/// Loads the given level by name
	/// </summary>
	/// <param name="levelName">a level we wish to load</param>
	public void LoadLevel(string levelName)
	{
		lock (m_lock)
		{
			m_levelToLoad = levelName;
		}
	}
}
