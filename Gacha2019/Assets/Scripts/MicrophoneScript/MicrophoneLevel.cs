using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MicrophoneLevel : MonoBehaviour
{
	// In commentary, Second Microphone
	public float m_testSound1;
	//public float testSound2;
	public static float s_MicLoudness1;
	//public static float MicLoudness2;
	private string m_Microphone;
	private AudioClip m_clipRecord1 = null;
	//private AudioClip _clipRecord2 = null;
	private int m_sampleWindow = 128;
	private bool m_isInitialized;
	private bool m_requestPending;

	public float m_thresholdWeak = 0.2f;
	public float m_thresholdStrong;

	private static MicrophoneLevel s_Instance;

	public static MicrophoneLevel getInstance()
	{
		if(s_Instance == null)
		{
			s_Instance = GameObject.FindObjectOfType<MicrophoneLevel>();
		}
		return s_Instance;
	}

	void Start()
	{
		foreach (string device in Microphone.devices)
		{
			if (m_Microphone == null /*&& _device2 == null*/)
			{
				m_Microphone = device;
				//_device2 = Microphone.devices[1];
			}
		}
		m_clipRecord1 = Microphone.Start(m_Microphone, true, 999, 44100);
		//_clipRecord2 = Microphone.Start(_device2, true, 999, 44100);
	}

	float LevelMax(string _device, AudioClip _clipRecord)
	{
		float levelMax1 = 0;
		float[] waveData1 = new float[m_sampleWindow];
		int micPosition1 = Microphone.GetPosition(_device) - (m_sampleWindow + 1);
		if (micPosition1 < 0)
		{
			return 0;
		}
		else { 
			_clipRecord.GetData(waveData1, micPosition1);
			for (int i = 0; i < m_sampleWindow; ++i)
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
		s_MicLoudness1 = LevelMax(m_Microphone, m_clipRecord1);

	}

	public float getMicLoudness()
	{
		return s_MicLoudness1;
	}

}