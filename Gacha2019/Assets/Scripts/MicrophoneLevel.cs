using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicrophoneLevel : MonoBehaviour
{
	public float testSound1;
	public float testSound2;
	public static float MicLoudness1;
	public static float MicLoudness2;
	private string _device1;
	private string _device2;
	private AudioClip _clipRecord1 = null;
	private AudioClip _clipRecord2 = null;
	private int _sampleWindow = 128;
	private bool _isInitialized;
	private bool requestPending;
	public float seuil;

	void InitMic()
	{
		Debug.Log("Nombre de micro : " + Microphone.devices.Length);
		foreach(var device in Microphone.devices)
		{
			Debug.Log(device);
		}
		if (_device1 == null && _device2 == null)
		{
			_device1 = Microphone.devices[0];
			_device2 = Microphone.devices[1];
			_clipRecord1 = Microphone.Start(_device1, true, 999, 44100);
			_clipRecord2 = Microphone.Start(_device2, true, 999, 44100);
			Debug.Log("Microphone Start : " + _clipRecord1);
			Debug.Log("Microphone Start : " + _clipRecord2);
		}
	}

	void StopMicrophone()
	{
		Microphone.End(_device1);
		Microphone.End(_device2);
	}

	float LevelMax(string _device, AudioClip _clipRecord)
	{
		float levelMax1 = 0;
		float[] waveData1 = new float[_sampleWindow];
		int micPosition1 = Microphone.GetPosition(_device) - (_sampleWindow + 1);
		if (micPosition1 < 0)
		{
			return 0;
		}
		else { 
			_clipRecord.GetData(waveData1, micPosition1);
			for (int i = 0; i < _sampleWindow; ++i)
			{
				float wavePeak1 = waveData1[i] * waveData1[i];
				if (levelMax1 < wavePeak1)
				{
					levelMax1 = wavePeak1;
				}

			}
		}
		return levelMax1;
	}

	void Update()
	{
		MicLoudness1 = LevelMax(_device1, _clipRecord1);
		MicLoudness2 = LevelMax(_device2, _clipRecord2);
		testSound1 = MicLoudness1;
		testSound2 = MicLoudness2;
		if (MicLoudness1 > seuil) { 
			Debug.Log("Débit du microphone " + Microphone.devices[0] + " : " + MicLoudness1);
		}
		if (MicLoudness2 > seuil)
		{
			Debug.Log("Débit du microphone " + Microphone.devices[1] + " : " + MicLoudness2);
		}

	}

	void OnEnable()
	{
		InitMic();
		_isInitialized = true;
	}

	void OnDisable()
	{
		StopMicrophone();
	}

	void OnDestory()
	{
		StopMicrophone();
	}

	void OnApplicationFocus(bool focus)
	{
		if (focus)
		{
			if (!_isInitialized)
			{
				InitMic();
				_isInitialized = true;
			}
		}

		if (!focus)
		{
			StopMicrophone();
			_isInitialized = false;
		}
	}

}