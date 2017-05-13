using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SettingsBehaviour : MonoBehaviour
{
	private const string FILENAME = "Settings.dat";

	private void Awake()
	{
		if(Settings.Current == null)
		{
			Load();
		}
	}

	private void OnDestroy()
	{
		Save();
	}


	private void Load()
	{
		if(File.Exists(Application.persistentDataPath + "/" + FILENAME))
		{
			try
			{
				FileStream fileStream = File.OpenRead(Application.persistentDataPath + "/" + FILENAME);
				BinaryFormatter formatter = new BinaryFormatter();

				Settings.Current = (Settings)formatter.Deserialize(fileStream);

				fileStream.Close();
			}
			catch(Exception ex)
			{
				Settings.Current = new Settings();
				Debug.Log("Unity will not save settings: " + ex.Message);
			}
		}
		else
		{
			Settings.Current = new Settings();
		}
		Settings.Current.Load();
	}

	private void Save()
	{
		try
		{
			FileStream fileStream = File.Open(Application.persistentDataPath + "/" + FILENAME, FileMode.OpenOrCreate);
			BinaryFormatter formatter = new BinaryFormatter();

			formatter.Serialize(fileStream, Settings.Current);

			fileStream.Close();
		}
		catch(Exception ex)
		{
			Debug.Log("Unity has not saved settings: " + ex.Message);
		}
	}
}

