using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SettingsBehaviour : MonoBehaviour
{
	private const string FILENAME = "Settings.dat";

	private void Awake()
	{
		if (Settings.Current == null)
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
		if (File.Exists(Application.persistentDataPath + "/" + FILENAME))
		{
			FileStream fileStream = File.OpenRead(Application.persistentDataPath + "/" + FILENAME);
			BinaryFormatter formatter = new BinaryFormatter();

			Settings.Current = (Settings)formatter.Deserialize(fileStream);

			fileStream.Close();

		}
		else
		{
			Settings.Current = new Settings();
		}
	}

	private void Save()
	{
		FileStream fileStream = File.Open(Application.persistentDataPath + "/" + FILENAME, FileMode.OpenOrCreate);
		BinaryFormatter formatter = new BinaryFormatter();

		formatter.Serialize(fileStream, Settings.Current);

		fileStream.Close();
	}
}

